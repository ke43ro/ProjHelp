Imports System.Deployment.Application
Imports LibVLCSharp.Shared

Partial Class F_Main
    Friend isDebug As Boolean
    Friend isAutoShort As Boolean
    Friend ProjHelpData As New PHServer

    Private ReadOnly PlayList As New PlayList
    Private ReadOnly myMsgBox As New DlgMsgBox
    Private isShort As Boolean = False
    Private myStatus As String = "Loading"
    Private T_filesTable As FilesTable
    Private PrefDisplay As Screen

    Public Sub New()
        InitializeComponent()
        ' Other initialization
    End Sub

    ' See the following secrtions for fastwer access to the code
    ' *** Section Form Load, Updates, set up ***
    ' *** Section Controls and supports ***
    ' *** Section Control Interactions ***
    ' *** Section Play methods ***
    ' *** Section test routines ***
    ' subs and functions are in alphabetical order within their section with
    ' supporting subs and functions immediately following the first call 

    ' *** Section Form Load, Updates, set up ***
    Private Sub F_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If ProjHelpData.ConnectDatabase() Is Nothing Then
            myMsgBox.Show("Unable to connect to the database.",
                            "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Close()
            Exit Sub
        End If

        Dim szVersion As String = GetPublishVersion()
        'Environment.GetEnvironmentVariable("ClickOnce_CurrentVersion")
        If szVersion = "Proto" Then
            LblVersion.Text = "Version 2.0.0.6 Proto"
            szVersion = "2.0.0.6"
        Else
            LblVersion.Text = "Version " & szVersion
        End If
        'Z.Y.X.W - Z.Y is major/minor version; X is build number, always 0; W is VS publish number
        'These are set in Publish Profile, W is automatically incremented
        'See the end of the file for history

        ' Check for any necessary upgrades
        If Not CheckUpdates(szVersion, ProjHelpData) Then
            Close()
            Exit Sub
        End If

        isShort = My.Settings.SelectedOnly
        'If isShort IsNot (Nothing) Then
        ChkShortList.Checked = isShort
        'End If
        SetDisplay()

        isAutoShort = My.Settings.AutoShortList

        T_filesTable = New FilesTable(isShort, ProjHelpData.GetConnection)
        T_filesDataGridView.DataSource = T_filesTable ' T_filesTable.GetDataByPhrase(TxtSearch.Text)
        T_filesTable.SetDGProperties(T_filesDataGridView)
        TxtSearch.Focus()
        myStatus = "Loaded"
    End Sub

    Private Sub F_Main_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        TxtSearch.Focus()
    End Sub

    Private Function GetPublishVersion() As String
        If ApplicationDeployment.IsNetworkDeployed Then
            Dim ver = ApplicationDeployment.CurrentDeployment.CurrentVersion
            Return $"{ver.Major}.{ver.Minor}.{ver.Build}.{ver.Revision}"
        Else
            ' Not ClickOnce deployed, fallback to assembly version or custom string
            Return "Proto"
        End If
    End Function

    Private Sub SetDisplay()
        Dim myScreen As Screen = Nothing

        'myMsgBox.Show("Checking screens [" & Screen.AllScreens.Length & "]")
        If Screen.AllScreens.Length = 1 Then
            myScreen = Screen.AllScreens(0)
        Else
            For Each thisScreen In Screen.AllScreens
                If isDebug Then
                    Dim szTemp = "Top: " + thisScreen.Bounds.Top.ToString + "; Left: " + thisScreen.Bounds.Left.ToString +
                     "; Height: " + thisScreen.Bounds.Height.ToString + "; Width: " + thisScreen.Bounds.Width.ToString +
                     "; Location: " + thisScreen.Bounds.Location.ToString & "; Prim: " & thisScreen.Primary
                    myMsgBox.Show(szTemp)
                End If

                If thisScreen.Primary = False Then
                    myScreen = thisScreen
                End If
            Next
        End If

        If myScreen Is Nothing Then
            myMsgBox.Show("Can't find a display to use", "Startup")
        End If

        PrefDisplay = myScreen
    End Sub

    Private Function CheckUpdates(szVersion As String, db As PHServer) As Boolean
        Dim szPriorVersion As String = My.Settings.Version
        Dim szResult As String = "", szUpdated As String, szTemp As String

        Dim szMsg As String = ""

        If szPriorVersion <> szVersion Then
            szUpdated = DoUpdates(CanonizeVersion(szPriorVersion), db)
            szTemp = szUpdated.Substring(0, 6)
            Dim szUpdateversion As String = szUpdated.Substring(szUpdated.IndexOf("|"c) + 1)

            Select Case szTemp
                Case "No Act"
                    My.Settings.Version = szVersion
                    My.Settings.Save()

                Case "Succes"
                    My.Settings.Version = szUpdateversion
                    My.Settings.Save()
                    myMsgBox.Show(szUpdated & vbCrLf & "An upgrade was done.  Projection Helper must be restarted",
                                "Automatic Upgrader", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False

                Case "Failed"
                    myMsgBox.Show(szUpdated & vbCrLf & "An upgrade failed.  Please contact Projection Helper vendor",
                                "Automatic Upgrader", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False

            End Select

        End If
        Return True

    End Function

    Private Function DoUpdates(priorVersion As String, db As PHServer) As String
        Try
            'If szDebug = "Yes" Then MyMsgBox.Show("priorVersion is " & priorVersion)
            If priorVersion = "" Then
                ' Initial installation. Latest version of database will have been installed through Set Up
                DoUpdates = "No Action: Initial Installation"
            Else
                Select Case priorVersion
                    Case "0"
                        ' use db to avoid "unused" warning
                        DoUpdateDummy("dummy", db)
                        DoUpdates = "No Action: Dummy|2.0.0.1"

                    Case ""
                        ' Initial run
                        DoUpdates = "No Action: First run|2.0.0.1"

                        ' Sample of case code saved from PPLink
                        'Case < "001.004.000.036"
                        '    Dim cmd1 As New SqlCommand("use ProHelp", connection)
                        '    Dim cmd2 As New SqlCommand("update t_files set last_dt = create_dt where last_dt is null", connection)
                        '    cmd1.ExecuteNonQuery()
                        '    cmd2.ExecuteNonQuery()
                        '    DoUpdates = "Success: Upgraded t_files.last_dt - never null|1.4.0.36"

                        'Case = "001.005.000.053"
                        '    ' NOP - affects test bed only
                        '    DoUpdates = "No Action: No upgrade necessary|1.5.0.53"

                        'Case < "001.006.000.055"
                        '    If DoUpdate1_6_0_55(connection) = True Then
                        '        DoUpdates = "Success: Upgraded t_files, ignore punctuation in search|1.6.0.55"
                        '    Else
                        '        DoUpdates = "Failed: Upgrade 1.6.0.55|1.6.0.55"
                        '    End If

                    Case Else
                        DoUpdates = "No Action: No upgrade necessary"

                End Select
            End If
        Catch ex As Exception
            DoUpdates = "Failed:" & ex.Message
        End Try
    End Function

    Private Sub DoUpdateDummy(flag As String, db As PHServer)
        Exit Sub
    End Sub

    ' Sample of update routine saved from PPLink
    'Private Function DoUpdate1_6_0_55(connection As SqlConnection) As Boolean
    '    DoUpdate1_6_0_55 = True
    '    Try
    '        RunSQL("use ProHelp", connection)
    '        RunSQL("if not exists (select * from sys.all_columns where object_id = object_id('prohelp.dbo.t_files') " &
    '                        " and name = 's_search' and max_length = 400)" &
    '                        vbCrLf & "alter table t_files add s_search varchar(400)", connection)
    '        RunSQL("if exists (select * from sys.objects where name = 'StripPunc') drop function StripPunc", connection)
    '        RunSQL("create Function dbo.StripPunc(@s_input varchar(max)) returns varchar(400)" &
    '                        vbCrLf & " begin Declare @s_temp varchar(400)" &
    '                        vbCrLf & "  Set @s_temp = replace(@s_input, '''', '')" &
    '                        vbCrLf & "  Set @s_temp = replace(@s_temp, '""', '')" &
    '                        vbCrLf & "  Set @s_temp = replace(@s_temp, ',', '')" &
    '                        vbCrLf & "  Set @s_temp = replace(@s_temp, '’', '')" &
    '                        vbCrLf & "  Return @s_temp" &
    '                        vbCrLf & " end", connection)
    '        RunSQL("update t_files set s_search = dbo.strippunc(f_name) + '; ' + dbo.strippunc(f_altname)", connection)
    '        RunSQL("if exists (select * from sys.objects where name = 't_files_insert_set_search') " &
    '                        vbCrLf & "DROP TRIGGER dbo.t_files_insert_set_search", connection)
    '        RunSQL("CREATE TRIGGER dbo.t_files_insert_set_search On t_files AFTER INSERT " &
    '                        vbCrLf & "AS update t_files set s_search = dbo.StripPunc(f_name) + '; ' + dbo.StripPunc(f_altname) " &
    '                        vbCrLf & " where file_no in (select file_no from inserted)", connection)
    '        RunSQL("if exists (select * from sys.objects where name = 't_files_update_set_search') " &
    '                        vbCrLf & "DROP TRIGGER dbo.t_files_update_set_search", connection)
    '        RunSQL("CREATE TRIGGER dbo.t_files_update_set_search On t_files AFTER Update " &
    '                        vbCrLf & "AS update t_files set s_search = dbo.StripPunc(f_name) + '; ' + dbo.StripPunc(f_altname) " &
    '                        vbCrLf & " where file_no in (select a.file_no from deleted a join inserted b on a.file_no = b.file_no)", connection)

    '    Catch ex As Exception
    '        MyMsgBox.Show("SQL Command failed during update." & vbCrLf &
    '                            "Please contact the programmer with this message:" & vbCrLf & ex.Message,
    '                        "Udpdate Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        DoUpdate1_6_0_55 = False

    '    End Try

    'End Function

    'Private Sub RunSQL(SQLCmdText As String, connection As SqlConnection)
    '' Will need update to use SQLiteCommand
    '    If szDebug = "Yes" Then
    '        If DialogResult.No = MyMsgBox.Show("Next upgrade command is" & SQLCmdText & vbCrLf & vbCrLf & "Run it?", "Upgrading",
    '                                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) Then Exit Sub
    '    End If

    '    Dim SQLCmd As New SqlCommand(SQLCmdText, connection)
    '    SQLCmd.ExecuteNonQuery()
    'End Sub

    Private Function CanonizeVersion(szVersion As String) As String
        Dim szTemp As String = ""
        Dim iLoop As Integer = 0

        'CanonizeVersion = szVersion

        Dim iIndex As Integer = szVersion.IndexOf(" Proto")

        If iIndex > 0 Then
            szVersion = szVersion.Substring(0, iIndex)
        End If

        Dim szParts() As String = szVersion.Split("."c)
        Do
            If IsNothing(szParts(iLoop)) Then Exit Do
            szTemp &= szParts(iLoop).PadLeft(3, "0")
            iLoop += 1
            If iLoop = szParts.Length Then Exit Do
            szTemp &= "."
        Loop

        CanonizeVersion = szTemp

    End Function


    ' *** Section Controls and supports ***
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        PlayList_AddRecord()
        TxtSearch.Text = ""
        TxtSearch.Focus()
    End Sub

    Private Sub PlayList_AddRecord()
        Dim iSongNo As Integer
        Dim szFileName As String, szFullPath As String

        '-- get file_no
        Dim bRowSel As Boolean = True

        If T_filesDataGridView.SelectedRows.Count = 0 Then
            bRowSel = False
        ElseIf T_filesDataGridView.SelectedRows(0).IsNewRow Then
            bRowSel = False
        End If

        If bRowSel = False Then
            myMsgBox.Show("Please select a File from the lower table to add to this list",
                            "Adding a file to the PlayList",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        iSongNo = T_filesDataGridView.SelectedRows(0).Cells(0).Value
        szFileName = T_filesDataGridView.SelectedRows(0).Cells(1).Value
        szFullPath = T_filesDataGridView.SelectedRows(0).Cells(2).Value
        If isAutoShort Then AddToShortList(iSongNo)

        '-- Add the new row
        LBPlayList.Items.Add(iSongNo & vbTab & szFileName & vbTab & " ")
    End Sub

    Private Sub AddToShortList(songNo As Integer)
        Dim iRow As Integer
        Dim filesView As DataView = T_filesTable.DefaultView
        filesView.Sort = "file_no"

        iRow = filesView.Find(songNo)
        If iRow >= 0 Then
            filesView.Item(iRow).BeginEdit()
            filesView.Item(iRow)("isShortList") = "Y"
            filesView.Item(iRow).EndEdit()
            T_filesTable.AcceptChanges()
        End If
    End Sub

    Private Sub BtnAdvanced_Click(sender As Object, e As EventArgs) Handles BtnAdvanced.Click
        F_Advanced.ShowDialog()
        T_filesTable.LoadActive(isShort)
        T_filesDataGridView.Update()
        isAutoShort = My.Settings.AutoShortList
        TxtSearch.Focus()
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        LBPlayList.Items.Clear()
        TxtSearch.Text = ""
    End Sub

    Private Sub BtnHelp_Click(sender As Object, e As EventArgs) Handles BtnHelp.Click
        F_Help.ShowDialog()
        TxtSearch.Focus()
    End Sub

    Private Sub BtnLoadList_Click(sender As Object, e As EventArgs) Handles BtnLoadList.Click
        DlgLoadList.Fillqueue(LBPlayList)
        DlgLoadList.ShowDialog()
        ' returns No if a Save was done, Yes if a Load was done or Cancel
        If DlgLoadList.DialogResult = DialogResult.Yes Then
            DlgLoadList.GetList(LBPlayList)
        End If
        TxtSearch.Focus()
    End Sub

    Private Sub BtnPlay_Click(sender As Object, e As EventArgs) Handles BtnPlay.Click
        If LBPlayList.Items.Count = 0 Then
            myMsgBox.Show("Please add some items to the list",
                            "Playing the list", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtSearch.Focus()
            Exit Sub
        End If

        MyBase.WindowState = FormWindowState.Minimized
        PlayList.Run(LBPlayList.Items)
        MyBase.WindowState = FormWindowState.Normal
        TxtSearch.Text = ""
        TxtSearch.Focus()
    End Sub

    Private Sub BtnSetup_Click(sender As Object, e As EventArgs) Handles BtnSetup.Click
        Dim Result = F_SetUp.ShowDialog()
        TxtSearch.Focus()

        'If Result = DialogResult.Yes Then
        T_filesTable.LoadActive(isShort)
        T_filesDataGridView.Update()
        'End If
        TxtSearch.Focus()
    End Sub


    ' *** Section Control Interactions ***
    Private Sub ChkShortList_CheckedChanged(sender As Object, e As EventArgs) Handles ChkShortList.CheckedChanged
        isShort = ChkShortList.Checked

        'If szDebug = "Yes" Then MyMsgBox.Show("ChkChgd - Check if Files Initialized. Conn String=" &
        '   T_filesTable.Connection.ConnectionString)

        If myStatus = "Loaded" Then
            'If szDebug = "Yes" Then MyMsgBox.Show("ChkChgd - Files Initialized: Fill")
            T_filesTable.LoadByPhrase(isShort, TxtSearch.Text)
            'If szDebug = "Yes" Then MyMsgBox.Show("ChkChgd - Files Initialized: Save settings")
            My.Settings.SelectedOnly = isShort
            My.Settings.Save()
        End If

        'If szDebug = "Yes" Then MyMsgBox.Show("ChkShortList_CheckedChanged Done")
    End Sub

    Private Sub Files_DoubleClick(sender As Object, e As EventArgs) Handles T_filesDataGridView.DoubleClick
        Dim szFName As String, iFile_no As Integer, szFullPath As String

        szFName = T_filesDataGridView.SelectedRows(0).Cells(1).Value
        iFile_no = T_filesDataGridView.SelectedRows(0).Cells(0).Value
        szFullPath = System.IO.Path.Combine(T_filesDataGridView.SelectedRows(0).Cells(2).Value, szFName)
        If isAutoShort Then AddToShortList(CInt(iFile_no))
        LBInstant.Items.Clear()
        LBInstant.Items.Add(iFile_no.ToString & vbTab & szFName & vbTab & szFullPath)
        MyBase.WindowState = FormWindowState.Minimized
        PlayList.Run(LBInstant.Items)
        MyBase.WindowState = FormWindowState.Normal
        TxtSearch.Text = ""
        TxtSearch.Focus()
    End Sub

    Private Sub LBPlayList_DoubleClick(sender As Object, e As EventArgs) Handles LBPlayList.DoubleClick
        Dim szListItem As String, iFileNo As Integer

        If IsNothing(LBPlayList.SelectedItem) Then Exit Sub

        szListItem = LBPlayList.SelectedItems(0)
        iFileNo = CInt(szListItem.Substring(0, szListItem.IndexOf(vbTab)))
        If isAutoShort Then AddToShortList(iFileNo)
        LBInstant.Items.Clear()
        LBInstant.Items.Add(szListItem)
        MyBase.WindowState = FormWindowState.Minimized
        'MyMsgBox.Show("Playing " & szListItem, "Playing a file",
        '        MessageBoxButtons.OK, MessageBoxIcon.Information)
        PlayList.Run(LBInstant.Items)
        MyBase.WindowState = FormWindowState.Normal
        TxtSearch.Text = ""
        TxtSearch.Focus()
    End Sub

    Private Sub LBPlayList_DragDrop(sender As Object, e As DragEventArgs) Handles LBPlayList.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        Dim File As String

        For Each path In files
            File = ChkPath(path)
            If File <> "" Then
                LBPlayList.Items.Add(File)
            End If
        Next

    End Sub

    Private Function ChkPath(Path) As String
        Dim myMedia As Media
        Dim myPath = Path.ToString
        Dim Extn = GetExtn(Path)

        Select Case Extn
            Case ".ppt", ".pptx", ".pptm", ".doc", ".docx", ".pdf"
                ' All good

            Case Else
                myMedia = GetMedia(Path)
                If myMedia Is Nothing Then
                    myMsgBox.Show("Sorry, I don't know how to process [" & Path & "]")
                    myPath = ""
                End If

        End Select

        Dim nameIndex As Integer = myPath.LastIndexOf("\")
        myPath = "-1" & vbTab & myPath.Substring(nameIndex + 1) & vbTab & myPath

        Return myPath

    End Function

    Private Function GetExtn(Path) As String
        Dim Extn As String
        Dim idx = Path.LastIndexOf(".")

        If idx < 1 Then
            Extn = ""
        Else
            Extn = Path.Substring(idx)
        End If

        Return Extn
    End Function

    Private Sub LBPlayList_DragEnter(sender As Object, e As DragEventArgs) Handles LBPlayList.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub LBPlayList_KeyDown(sender As Object, e As KeyEventArgs) Handles LBPlayList.KeyDown
        Dim iSelected As Integer = LBPlayList.SelectedIndex
        Dim iIndex As Integer = 0
        Dim szQueue As New Queue(Of String)

        Select Case e.KeyCode
            ' Handle via KeyPress catches both num pad and normal plus key
            'Case Keys.Add

            ' Handle via KeyPress catches both num pad and normal minus key
            'Case Keys.Subtract

            Case Keys.Delete
                While iIndex < LBPlayList.Items.Count
                    If iIndex <> iSelected Then szQueue.Enqueue(LBPlayList.Items(iIndex))
                    iIndex += 1
                End While
                LBPlayList.Items.Clear()
                'iIndex = 0
                While szQueue.Count > 0
                    LBPlayList.Items.Add(szQueue.Dequeue)
                End While

            Case Else
                Exit Sub
        End Select

        e.SuppressKeyPress = True

    End Sub

    Private Sub LBPlayList_KeyPress(sender As Object, e As KeyPressEventArgs) Handles LBPlayList.KeyPress
        Dim iSelected As Integer = LBPlayList.SelectedIndex
        Dim szQueue As New Queue(Of String)

        Select Case e.KeyChar
            ' Delete key doesn't generate a KeyPress
            'Case Microsoft.VisualBasic.ChrW(Keys.Delete)

            Case "+"
                If iSelected < LBPlayList.Items.Count - 1 Then
                    szQueue.Enqueue(LBPlayList.Items(iSelected))
                    szQueue.Enqueue(LBPlayList.Items(iSelected + 1))
                    LBPlayList.Items(iSelected + 1) = szQueue.Dequeue
                    LBPlayList.Items(iSelected) = szQueue.Dequeue
                    LBPlayList.SelectedIndex += 1
                End If

            Case "-"
                If iSelected > 0 Then
                    szQueue.Enqueue(LBPlayList.Items(iSelected))
                    szQueue.Enqueue(LBPlayList.Items(iSelected - 1))
                    LBPlayList.Items(iSelected - 1) = szQueue.Dequeue
                    LBPlayList.Items(iSelected) = szQueue.Dequeue
                    LBPlayList.SelectedIndex -= 1
                End If

            Case Else
                Exit Sub
        End Select

        e.Handled = True
    End Sub

    Sub TfilesChangeSelection(ByVal iChange As Integer)
        Dim iRow As Integer = 0
        'Dim iSelected As DataGridViewRow = T_filesDataGridView.SelectedRows(0)

        While iRow < T_filesDataGridView.RowCount
            If T_filesDataGridView.SelectedRows.Contains(T_filesDataGridView.Rows(iRow)) Then Exit While
            iRow += 1
        End While

        If iRow = T_filesDataGridView.RowCount Then
            ' no selected row
            Exit Sub
        ElseIf iChange = 1 And iRow = T_filesDataGridView.RowCount - 1 Then
            ' at end of list
            Exit Sub
        ElseIf iChange = -1 And iRow = 0 Then
            ' at beginning of list
            Exit Sub
        End If

        T_filesDataGridView.Rows(iRow + iChange).Selected = True
    End Sub

    Private Sub TxtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtSearch.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                PlayList_AddRecord()
                TxtSearch.Text = ""
            Case Keys.Up
                TfilesChangeSelection(-1)
            Case Keys.Down
                TfilesChangeSelection(1)
            Case Else
                TxtSearch.Focus()
                Exit Sub
        End Select

        e.SuppressKeyPress = True
        TxtSearch.Focus()
    End Sub

    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs) Handles TxtSearch.TextChanged
        Dim szText As String = T_filesTable.StripPunc(TxtSearch.Text)
        T_filesTable.LoadByPhrase(isShort, szText)
        T_filesDataGridView.Update()
    End Sub


    ' *** Section Play methods ***
    Private Sub PlayFile(myPath As String)
        Dim PlayPowerPoint As VBPlayPowerPoint, PlayPDF As VBPlayPDF, PlayWord As VBPlayWord
        Dim myForm As F_Video
        Dim myMedia As Media
        Dim myExtn = GetExtn(myPath)

        Select Case myExtn
            Case ".doc", ".docx"
                PlayWord = New VBPlayWord
                PlayWord.Run(myPath, PrefDisplay)
                PlayWord = Nothing

            Case ".ppt", ".pptx", "pptm"
                PlayPowerPoint = New VBPlayPowerPoint
                PlayPowerPoint.Run(myPath)
                PlayPowerPoint = Nothing

            Case ".pdf"
                PlayPDF = New VBPlayPDF
                PlayPDF.Run(myPath)
                PlayPDF = Nothing

            Case Else
                myMedia = GetMedia(myPath)
                If myMedia Is Nothing Then
                    myMsgBox.Show("Program Error - can't process " & myPath, "Displaying Media")
                Else
                    myForm = New F_Video()
                    'szTemp = "PlayFile() Top: " + PrefDisplay.Bounds.Top.ToString + "; Left: " + PrefDisplay.Bounds.Left.ToString +
                    '    "; X: " & PrefDisplay.Bounds.X & "; Y: " & PrefDisplay.Bounds.Y &
                    '    "; Height: " & PrefDisplay.Bounds.Height.ToString + "; Width: " + PrefDisplay.Bounds.Width.ToString +
                    '    "; Location: " + PrefDisplay.Bounds.Location.ToString
                    'myMsgBox.Show(szTemp)
                    With myForm
                        .LoadMedia(myMedia)
                        .LoadBounds(PrefDisplay.Bounds)
                        .LoadPause(ChkPause.Checked)
                        .ShowDialog()
                    End With
                    myForm = Nothing
                End If

        End Select
    End Sub

    Private Function GetMedia(myPath As String) As Media
        Dim libVLC As New LibVLC()
        Dim myMediaOptions() As String = {":mouse-events", ":keyboard-events"}
        Dim myUri As New Uri(myPath)
        Dim myMedia = New LibVLCSharp.Shared.Media(libVLC, myUri, myMediaOptions)
        myMedia.Parse(MediaParseOptions.ParseLocal)
        System.Threading.Thread.Sleep(1000)
        If myMedia.Tracks.Length = 0 Then
            Return Nothing
        End If
        Return myMedia
    End Function

    ' *** Section test routines ***
    ' Used to analyse the results of KeyDown and KeyPress events
    'Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) 'Handles TextBox1.KeyDown
    '    Dim messageBoxVB As New System.Text.StringBuilder()
    '    messageBoxVB.AppendFormat("{0} = {1}", "Alt", e.Alt)
    '    messageBoxVB.AppendLine()
    '    messageBoxVB.AppendFormat("{0} = {1}", "Control", e.Control)
    '    messageBoxVB.AppendLine()
    '    messageBoxVB.AppendFormat("{0} = {1}", "Handled", e.Handled)
    '    messageBoxVB.AppendLine()
    '    messageBoxVB.AppendFormat("{0} = {1}", "KeyCode", e.KeyCode)
    '    messageBoxVB.AppendLine()
    '    messageBoxVB.AppendFormat("{0} = {1}", "KeyValue", e.KeyValue)
    '    messageBoxVB.AppendLine()
    '    messageBoxVB.AppendFormat("{0} = {1}", "KeyData", e.KeyData)
    '    messageBoxVB.AppendLine()
    '    messageBoxVB.AppendFormat("{0} = {1}", "Modifiers", e.Modifiers)
    '    messageBoxVB.AppendLine()
    '    messageBoxVB.AppendFormat("{0} = {1}", "Shift", e.Shift)
    '    messageBoxVB.AppendLine()
    '    messageBoxVB.AppendFormat("{0} = {1}", "SuppressKeyPress", e.SuppressKeyPress)
    '    messageBoxVB.AppendLine()
    '    MyMsgBox.Show(messageBoxVB.ToString(), "KeyDown Event")

    'End Sub

    'Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) 'Handles TextBox1.KeyPress
    '    Dim messageBoxVB As New System.Text.StringBuilder()
    '    messageBoxVB.AppendFormat("{0} = {1}", "Char", e.KeyChar)
    '    messageBoxVB.AppendLine()
    '    'messageBoxVB.AppendFormat("{0} = {1}", "Control", e.Control)
    '    'messageBoxVB.AppendLine()
    '    'messageBoxVB.AppendFormat("{0} = {1}", "Handled", e.Handled)
    '    'messageBoxVB.AppendLine()
    '    'messageBoxVB.AppendFormat("{0} = {1}", "KeyCode", e.KeyCode)
    '    'messageBoxVB.AppendLine()
    '    'messageBoxVB.AppendFormat("{0} = {1}", "KeyValue", e.KeyValue)
    '    'messageBoxVB.AppendLine()
    '    'messageBoxVB.AppendFormat("{0} = {1}", "KeyData", e.KeyData)
    '    'messageBoxVB.AppendLine()
    '    'messageBoxVB.AppendFormat("{0} = {1}", "Modifiers", e.Modifiers)
    '    'messageBoxVB.AppendLine()
    '    'messageBoxVB.AppendFormat("{0} = {1}", "Shift", e.Shift)
    '    'messageBoxVB.AppendLine()
    '    'messageBoxVB.AppendFormat("{0} = {1}", "SuppressKeyPress", e.SuppressKeyPress)
    '    'messageBoxVB.AppendLine()
    '    MyMsgBox.Show(messageBoxVB.ToString(), "KeyDown Event")

    'End Sub
End Class

'Version number
'Z.Y.X.W - Z.Y.X is major version.minor version.build; W is VS publish number.  Missing publish numbers were used in testing
'2.0.0.6    Amended documentation to match the behaviour of ProjHelp
'2.0.0.5    Tidy up, add splash screen, icon
'2.0.0.3    Fixed runtime issue: not able to find SQLite.Interop.dll
'2.0.0.1    Fixed installation issue with manifest file
'2.0.0.0    First Version of full merged the ad hoc file showing feature with the lyrics database management
'           Using SQLite instead of SQL Server