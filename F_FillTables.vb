Imports System.Data.SQLite
Imports System.Runtime.InteropServices

Partial Class F_FillTables
    Private nMatched As Integer, nMissing As Integer
    Private szFolder As String
    Private pkleaFormat As Boolean
    Private iFilesStart, iFilesEnd As Integer
    Private isShort As Boolean = False
    Private ReadOnly isDebug As Boolean = My.Settings.Debug
    Private T_filesTable As FilesTable
    Private ReadOnly myMsgBox As New DlgMsgBox
    Private WithEvents TfilesDataGridView As MyDataGridView
    'Friend WithEvents FilenoDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    'Friend WithEvents FnameDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    'Friend WithEvents FpathDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    'Friend WithEvents FaltnameDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    'Friend WithEvents InactiveDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    'Friend WithEvents SsearchDataGridViewTextBoxColumn As DataGridViewTextBoxColumn


    Friend Sub LoadFolder(szFolderIn As String, pkleaForm As Boolean)
        pkleaFormat = pkleaForm
        szFolder = szFolderIn
        txtFolder.Text = szFolder
    End Sub

    Private Sub FillTables_On_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        isShort = My.Settings.SelectedOnly
        Dim connection As SQLiteConnection = F_Main.ProjHelpData.GetConnection()
        If connection.State <> ConnectionState.Open Then
            connection.Open()
        End If

        T_filesTable = New FilesTable(connection, InclInActive:=True)
        Create_TfilesDataGridView()
        TfilesDataGridView.DataSource = T_filesTable
        T_filesTable.SetDGProperties(TfilesDataGridView)
        iFilesStart = T_filesTable.Count()
        If iFilesStart > 0 Then txtResults.Text = "There are " & iFilesStart & " records in the table"

    End Sub

    Private Sub F_FillTables_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If iFilesEnd - iFilesStart > 0 Or nMatched > 0 Or iFilesStart > 0 Then
            Me.DialogResult = DialogResult.OK
        Else
            Me.DialogResult = DialogResult.Retry
        End If
    End Sub

    Private Sub Create_TfilesDataGridView()
        SuspendLayout()
        TfilesDataGridView = New MyDataGridView With {
            .ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize,
            .Location = New System.Drawing.Point(18, 408),
            .Margin = New System.Windows.Forms.Padding(4),
            .Name = "TfilesDataGridView",
            .Size = New System.Drawing.Size(611, 208),
            .ScrollBars = System.Windows.Forms.ScrollBars.Both,
            .TabIndex = 6
        }
        ToolTip1.SetToolTip(TfilesDataGridView, "The PPT Name is the actual file name of the song on the hard disk." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "The Other Nam" &
        "e is an alternative title of the song or other search data.")
        Controls.Add(TfilesDataGridView)
        CType(Me.TfilesDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Private Sub BtnLoadTable_Click(sender As Object, e As EventArgs) Handles BtnLoadTable.Click
        Dim Result As DialogResult, myFiles As DataTable, myPPTs As GetFiles

        If Dir(szFolder, vbDirectory) = "" Then
            myMsgBox.Show("This feature can only be run if a valid folder is specified",
                        "Finding the songs", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Exit Sub
        End If

        If TfilesDataGridView Is Nothing OrElse TfilesDataGridView.IsDisposed Then
            Create_TfilesDataGridView()
        End If
        T_filesTable.LoadAll(InclInActive:=True)
        TfilesDataGridView.DataSource = T_filesTable
        TfilesDataGridView.Visible = False

        If T_filesTable.Count > 0 Then
            Result = myMsgBox.Show("There are already records in the Files Table." & vbCrLf &
                "This feature is only intended for use on first setting up Projection Helper." & vbCrLf &
                "There is an update feature under the Advanced Options." & vbCrLf & vbCrLf &
                "Do you really wish to continue?",
                "Finding the songs", MessageBoxButtons.YesNo, MessageBoxIcon.Stop)
            If Result = DialogResult.No Then Exit Sub
        End If

        Cursor = Cursors.WaitCursor
        Application.DoEvents()
        BtnClose.Enabled = False
        BtnEmpty.Enabled = False
        BtnLoadTable.Enabled = False

        CheckFiles()

        If pkleaFormat Then
            txtResults.Text = txtResults.Text & vbCrLf & "Loading from Parklea-type MASTERS folder"
            txtResults.Text = txtResults.Text & vbCrLf & "Searching for alpha folders in: " & szFolder
        Else
            txtResults.Text = txtResults.Text & vbCrLf & "Loading from Hierarchical-type folder"
            txtResults.Text = txtResults.Text & vbCrLf & "Searching for folders in: " & szFolder
        End If

        ' get all PowerPoint files in the specified folder
        myPPTs = New GetFiles()
        myFiles = myPPTs.GetPPs(szFolder)

        'compare the files found with those in the table
        Dim searchView As New DataView(T_filesTable) With {.Sort = "f_name"}
        Dim filesView As New DataView(myFiles) With {.Sort = "FileName"}
        Dim iNew As Integer = 0, iAlready As Integer = 0, Lookup As Integer
        txtResults.Text = txtResults.Text & vbCrLf & "Collecting New files into my List..."

        For Each Row In filesView
            Lookup = searchView.Find(Row("FileName"))
            If Lookup < 0 Then
                iNew += 1
                T_filesTable.Insert(Row("FileName"), Row("FilePath"), False)
            Else
                iAlready += 1
            End If
        Next Row

        iFilesEnd = iNew + iAlready
        T_filesTable.LoadAll(InclInActive:=True)
        TfilesDataGridView.Update()
        txtResults.Text = txtResults.Text & vbCrLf & "Found " & iNew & " new files; " & iAlready & " already listed"

        If isDebug Then myMsgBox.Show("After getting files, records: adapter=" & T_filesTable.Count &
                                   "; view=" & TfilesDataGridView.Rows.Count)
        txtResults.Text = txtResults.Text & vbCrLf & (iFilesEnd - iFilesStart) &
            " file records loaded"
        TfilesDataGridView.Visible = True

        Cursor = Cursors.Default
        BtnClose.Enabled = True
        BtnEmpty.Enabled = True
        BtnLoadTable.Enabled = True
    End Sub

    Private Sub BtnEmpty_Click(sender As Object, e As EventArgs) Handles BtnEmpty.Click
        Dim myRow As DataRow, myCount As Integer
        T_filesTable.LoadAll(InclInActive:=True)
        myCount = T_filesTable.Count

        If TfilesDataGridView Is Nothing OrElse TfilesDataGridView.IsDisposed Then
            Create_TfilesDataGridView()
        End If
        TfilesDataGridView.DataSource = T_filesTable
        TfilesDataGridView.Visible = False
        'TfilesDataGridView.Update()
        T_filesTable.SetDGProperties(TfilesDataGridView)

        Dim msgResult As DialogResult _
            = myMsgBox.Show("This will attempt to delete all records from the T_FILES table." & vbCrLf &
                              "The process will fail and do nothing if any play lists have been saved." & vbCrLf & vbCrLf &
                              "Are you sure you want to do this?",
                        "Set Up", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If msgResult = DialogResult.No Then Exit Sub

        Cursor = Cursors.WaitCursor
        Application.DoEvents()
        BtnClose.Enabled = False
        BtnEmpty.Enabled = False
        BtnLoadTable.Enabled = False

        txtResults.Text = txtResults.Text & vbCrLf & "Emptying Lyrics Table..."
        If myCount <> 0 Then
            txtResults.Text = txtResults.Text & vbCrLf & "Files record has " & myCount & " rows"
            For Each myRow In T_filesTable.Rows
                T_filesTable.Delete(myRow("file_no"))
            Next

        Else
            txtResults.Text = txtResults.Text & vbCrLf & "Files record is empty.  Skip this action"
        End If

        ' Reload the data
        T_filesTable.LoadEvery()

        '' Remove the old DataGridView from the form
        'Controls.Remove(TfilesDataGridView)
        'TfilesDataGridView.Dispose()

        '' Create a new DataGridView instance
        'TFilesDataGridView = New MyDataGridView()

        '' Add the new DataGridView to the form
        'Me.Controls.Add(TFilesDataGridView)
        '' Rebind the data source

        TfilesDataGridView.DataSource = T_filesTable

        ' Set properties and re-apply your column settings
        T_filesTable.SetDGProperties(TFilesDataGridView)
        TfilesDataGridView.Update()
        TfilesDataGridView.Visible = True

        If isDebug Then
            Dim nrowsView As Integer = TfilesDataGridView.RowCount
            Dim nrowsTable As Integer = T_filesTable.Count

            myMsgBox.Show("After checking files, records: adapter=" & nrowsTable & "; view=" & nrowsView)
        End If

        txtResults.Text = txtResults.Text & vbCrLf & "Completed emptying the table"

        iFilesStart = 0
        iFilesEnd = 0
        nMatched = 0
        nMissing = 0

        Cursor = Cursors.Default
        BtnClose.Enabled = True
        BtnEmpty.Enabled = True
        BtnLoadTable.Enabled = True
    End Sub

    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        If T_filesTable.Count <> 0 Then Me.DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Function GetFoldersPklea(szFolder As String) As List(Of String)
        ' This function is used to list the Parklea format folders
        Dim myList As New List(Of String)
        Dim NextDir As String
        Dim i As Integer = 0

        NextDir = Dir(szFolder & "\*", 16) ' 16 = directories only
        While NextDir <> ""
            Select Case NextDir
                Case ".", ".."
                Case Else
                    If Len(NextDir) = 1 Then
                        myList.Add(szFolder & "\" & NextDir)
                        i += 1
                    End If
            End Select
            NextDir = Dir()
        End While

        If i = 0 Then
            myMsgBox.Show("There are no alpha folders here:" & vbCrLf &
                szFolder & vbCrLf & "Please locate a Parklea-type songs MASTERS folder",
                "Finding the songs", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
        Return myList
    End Function

    Private Sub GetFilesPklea(arPaths As List(Of String))
        ' This subroutine is used to get the files from Parklea format folders
        Dim szPath As String, NextFile As String
        Dim searchView As New DataView(T_filesTable)
        Dim nNew As Integer = 0, LookUp As Integer, nAlready As Integer = 0

        txtResults.Text = txtResults.Text & vbCrLf & "Collecting New files into my List..."
        searchView.Sort = "f_name"

        For Each szPath In arPaths
            NextFile = Dir(szPath & "\*.*", 0)
            While NextFile <> "" ' j <4
                Select Case NextFile.Substring(NextFile.Length - 4, 4).ToLower()
                    Case ".ppt", "pptx", "pptm"
                        LookUp = searchView.Find(NextFile)
                        If LookUp < 0 Then
                            nNew += 1
                            T_filesTable.Insert(NextFile, szPath, False)
                        Else
                            nAlready += 1
                        End If
                End Select
                NextFile = Dir()
            End While
        Next szPath

        iFilesEnd = nNew + nAlready
        T_filesTable.LoadAll(InclInActive:=True)
        TfilesDataGridView.Update()
        txtResults.Text = txtResults.Text & vbCrLf & "Found " & nNew & " new files; " & nAlready & " already listed"
    End Sub

    Private Sub CheckFiles()
        Dim FullName As String
        Dim fViewRow As DataRowView
        Dim filesView As New DataView(T_filesTable)
        TfilesDataGridView.Visible = False

        txtResults.Text = txtResults.Text & vbCrLf & "Checking my File List against the disk contents..."
        If filesView.Count() = 0 Then
            nMatched = 0
            nMissing = 0
            txtResults.Text = txtResults.Text & vbCrLf & "No files in my list"
        Else
            txtResults.Text = txtResults.Text & vbCrLf & "Files record has " & filesView.Count() & " rows"
            filesView.Sort = "f_name, f_path"
            For Each fViewRow In filesView
                FullName = fViewRow("F_PATH") & "\" & fViewRow("F_NAME")
                If Dir(FullName) <> "" Then
                    nMatched += 1
                    'fViewRow.Edit
                    If IsDBNull(fViewRow("LAST_ACTION")) Then
                        fViewRow("LAST_ACTION") = "MATCHED"
                        fViewRow("LAST_DT") = Now()
                    ElseIf fViewRow("LAST_ACTION") <> "MATCHED" Then
                        fViewRow("LAST_ACTION") = "MATCHED"
                        fViewRow("LAST_DT") = Now()
                    End If
                    fViewRow("ISACTIVE") = "Y"
                    fViewRow.EndEdit()
                Else
                    nMissing += 1
                    If IsDBNull(fViewRow("LAST_ACTION")) Then
                        fViewRow("LAST_ACTION") = "NOT FOUND"
                        fViewRow("LAST_DT") = Now()
                    ElseIf fViewRow("LAST_ACTION") <> "NOT FOUND" Then
                        fViewRow("LAST_ACTION") = "NOT FOUND"
                        fViewRow("LAST_DT") = Now()
                    End If
                    fViewRow("ISACTIVE") = "N"
                    fViewRow.EndEdit()
                End If
            Next
            txtResults.Text = txtResults.Text & vbCrLf & "Checked Files: " & nMissing & " Missing; " & nMatched & " Matched"
        End If

        T_filesTable.LoadAll(InclInActive:=True)
        If isDebug Then
            Dim nrowsView As Integer = TfilesDataGridView.RowCount
            Dim nrowsTable As Integer = T_filesTable.Count
            myMsgBox.Show("After checking files, records: adapter=" & nrowsTable &
                                       "; view=" & nrowsView)
        End If
        TfilesDataGridView.DataSource = T_filesTable
        TfilesDataGridView.Visible = True
        Application.DoEvents()

    End Sub

    Private Sub ToolTip1_Draw(sender As Object, e As DrawToolTipEventArgs) Handles ToolTip1.Draw
        e.Graphics.FillRectangle(Brushes.LightYellow, e.Bounds)
        e.Graphics.DrawRectangle(Pens.Black, New Rectangle(0, 0, e.Bounds.Width - 1, e.Bounds.Height - 1))
        Using f As New Font("Segoe UI", 10, FontStyle.Regular)
            e.Graphics.DrawString(e.ToolTipText, f, Brushes.Black, New PointF(2, 2))
        End Using
    End Sub

    Private Sub ToolTip1_Popup(sender As Object, e As PopupEventArgs) Handles ToolTip1.Popup
        Using f As New Font("Segoe UI", 10, FontStyle.Regular)
            Dim textSize = TextRenderer.MeasureText(ToolTip1.GetToolTip(e.AssociatedControl), f, New Size(600, 0), TextFormatFlags.WordBreak)
            ' Add a little padding
            e.ToolTipSize = New Size(Math.Min(textSize.Width, 600), textSize.Height + 4)
        End Using
    End Sub

End Class

<ComVisible(True)>
<Guid("D1B6A1A2-1234-4B56-ABCD-1234567890AB")>
Public Class MyDataGridView
    Inherits DataGridView

    Public Sub New()
        Me.AccessibleName = ""
        Me.AccessibleRole = AccessibleRole.None
        Me.AccessibleDescription = ""
    End Sub

    Protected Overrides Function CreateAccessibilityInstance() As AccessibleObject
        Return MyBase.CreateAccessibilityInstance()
    End Function
End Class
