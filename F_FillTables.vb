Imports System.Data.SQLite

Partial Class F_FillTables
    Private nMatched As Integer, nMissing As Integer
    Private szFolder As String = ""
    Private iFilesStart, iFilesEnd As Integer
    Private isShort As Boolean = False
    Private isDebug As Boolean = My.Settings.Debug
    Private T_filesTable As FilesTable
    Private ReadOnly myMsgBox As New DlgMsgBox

    Friend Sub LoadFolder(szFolderIn As String)
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
        'T_filesTable.LoadAll()
        T_filesDataGridView.DataSource = T_filesTable
        T_filesTable.SetDGProperties(T_filesDataGridView)
        iFilesStart = T_filesTable.Count()
        If iFilesStart > 0 Then txtResults.Text = "There are already " & iFilesStart & " records in the table"

    End Sub

    Private Sub F_FillTables_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If iFilesEnd - iFilesStart > 0 Or nMatched > 0 Or iFilesStart > 0 Then
            Me.DialogResult = DialogResult.OK
        Else
            Me.DialogResult = DialogResult.Retry
        End If
    End Sub

    Private Sub BtnLoadTable_Click(sender As Object, e As EventArgs) Handles BtnLoadTable.Click
        Dim Result As DialogResult

        If Dir(szFolder, vbDirectory) = "" Then
            myMsgBox.Show("This feature can only be run if a valid folder is specified",
                        "Finding the songs", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Exit Sub
        End If

        If T_filesDataGridView.Rows.Count > 0 Then
            Result = myMsgBox.Show("There are already records in the Files Table." & vbCrLf &
                "This feature is only intended for use on first setting up Projection Helper." & vbCrLf &
                "There is an update feature under the Advanced Options." & vbCrLf & vbCrLf &
                "Do you really wish to continue?",
                "Finding the songs", MessageBoxButtons.YesNo, MessageBoxIcon.Stop)
            If Result = DialogResult.No Then Exit Sub
        End If

        Cursor = Cursors.WaitCursor
        BtnClose.Enabled = False

        txtResults.Text = txtResults.Text & vbCrLf & "Loading file records into the Table"
        CheckFiles()

        Dim AllDirs(30) As String, NextDir As String, i As Integer
        i = 0
        NextDir = Dir(szFolder & "\*", 16)

        While NextDir <> ""
            Select Case NextDir
                Case ".", ".."
                Case Else
                    If Len(NextDir) = 1 Then
                        AllDirs(i) = szFolder & "\" & NextDir
                        i += 1
                    End If
            End Select
            NextDir = Dir()
        End While

        If i = 0 Then
            myMsgBox.Show("There are no alpha folders here:" & vbCrLf &
                szFolder & vbCrLf & "Please locate a Parklea-type songs MASTERS folder",
                "Finding the songs", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        Else
            GetFiles(AllDirs)       ' modifies base table
            'T_filesTable.AcceptChanges()
        End If

        'T_filesDataGridView.DataSource = T_filesTable
        T_filesDataGridView.Update()

        If isDebug Then MyMsgBox.Show("After getting files, records: adapter=" & T_filesTable.Count &
                                   "; view=" & T_filesDataGridView.Rows.Count)
        txtResults.Text = txtResults.Text & vbCrLf & (iFilesEnd - iFilesStart) &
            " file records loaded"
        Cursor = Cursors.Default
        BtnClose.Enabled = True
        'iFilesStart = ProHelpDataSet.t_files.Count
    End Sub

    Private Sub BtnEmpty_Click(sender As Object, e As EventArgs) Handles BtnEmpty.Click
        Dim myRow As DataRow, myCount As Integer = T_filesTable.Count
        Dim msgResult As DialogResult _
            = myMsgBox.Show("This will attempt to delete all records from the T_FILES table." & vbCrLf &
                              "The process will fail and do nothing if any play lists have been saved." & vbCrLf & vbCrLf &
                              "Are you sure you want to do this?",
                        "Set Up", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If msgResult = DialogResult.No Then Exit Sub

        Cursor = Cursors.WaitCursor

        txtResults.Text = txtResults.Text & vbCrLf & "Emptying Lyrics Table..."
        If myCount <> 0 Then
            txtResults.Text = txtResults.Text & vbCrLf & "Files record has " & myCount & " rows"
            For Each myRow In T_filesTable.Rows
                T_filesTable.Delete(myRow("file_no"))
            Next

        Else
            txtResults.Text = txtResults.Text & vbCrLf & "Files record is empty.  Skip this action"
        End If

        T_filesTable.LoadAll(InclInActive:=True)
        T_filesDataGridView.Update()

        If isDebug Then MyMsgBox.Show("After emptying, records: adapter=" & T_filesTable.Count &
                                   "; view=" & T_filesDataGridView.Rows.Count)
        txtResults.Text = txtResults.Text & vbCrLf & "Completed emptying the table"
        Cursor = Cursors.Default
        iFilesStart = 0
        iFilesEnd = 0
        nMatched = 0
        nMissing = 0
    End Sub

    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        If T_filesTable.Count <> 0 Then Me.DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub GetFiles(arPaths As Object)
        Dim szPath As String, NextFile As String
        Dim searchView As New DataView(T_filesTable)
        Dim nNew As Integer = 0, LookUp As Integer, nAlready As Integer = 0

        txtResults.Text = txtResults.Text & vbCrLf & "Collecting New files..."
        searchView.Sort = "f_name"

        For Each szPath In arPaths
            NextFile = Dir(szPath & "\*.*", 0)
            While NextFile <> "" ' j <4
                Select Case NextFile.Substring(NextFile.Length - 4, 4)
                    Case ".ppt", "pptx", ".PPT", "PPTX"
                        LookUp = searchView.Find(NextFile)
                        If LookUp < 0 Then
                            nNew += 1
                            T_filesTable.Insert(NextFile, szPath, True)
                        Else
                            nAlready += 1
                        End If
                End Select
                NextFile = Dir()
            End While
        Next szPath

        iFilesEnd = nNew + nAlready
        T_filesTable.LoadAll(InclInActive:=True)
        T_filesDataGridView.Update()
        txtResults.Text = txtResults.Text & vbCrLf & "Found " & nNew & " new files; " & nAlready & " already listed"
    End Sub

    Private Sub CheckFiles()
        Dim FullName As String
        Dim fViewRow As DataRowView

        'T_filesTable = New FilesTable(False, F_Main.ProjHelpData.GetConnection)
        'T_filesDataGridView.DataSource = T_filesTable
        'T_filesTable.LoadAll(False)  ' False == ignore selection flag
        Dim filesView As New DataView(T_filesTable)

        txtResults.Text = txtResults.Text & vbCrLf & "Checking File List..."
        If filesView.Count() = 0 Then
            nMatched = 0
            nMissing = 0
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
        End If

        T_filesTable.LoadAll(InclInActive:=True)
        If isDebug Then MyMsgBox.Show("After checking files, records: adapter=" & T_filesTable.Count &
                                   "; view=" & T_filesDataGridView.RowCount)

        txtResults.Text = txtResults.Text & vbCrLf & "Checked Files: " & nMissing & " Missing; " & nMatched & " Matched"
    End Sub
End Class
