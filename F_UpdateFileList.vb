Imports System.Data.SQLite

Public Class F_UpdateFileList
    'Private connection As SqlConnection
    Private iNoFile As Integer
    Private bFoundActive, bNewSelect As Boolean
    Const iInactive As Integer = 1
    Const iDeleteAll As Integer = 2
    Const iDeleteThis As Integer = 3
    Private T_filesTable As FilesTable
    'Private T_playlistsTable As PlaylistsTable
    Private Tx_playlist2FileTable As Tx_playlistTable
    'Private isShort As Boolean = False
    Private ReadOnly myMsgBox As New DlgMsgBox


    Private Sub F_UpdateFileList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connection As SQLiteConnection = F_Main.ProjHelpData.GetConnection()
        If connection.State <> ConnectionState.Open Then
            connection.Open()
        End If

        T_filesTable = New FilesTable(connection)
        Tx_playlist2FileTable = New Tx_playlistTable(connection)

        TxtFolder.Text = My.Settings.MasterFolder
        RBtnMakeActive.Checked = True
        RBtnMakeInactive.Checked = True
        RBtnMakeSelect.Checked = True
    End Sub


    Private Sub BtnBrowse_Click(sender As Object, e As EventArgs) Handles BtnBrowse.Click
        FolderBrowserDialog1.SelectedPath = TxtFolder.Text
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            TxtFolder.Text = FolderBrowserDialog1.SelectedPath
        End If
        If Dir(TxtFolder.Text) = "" Then
            myMsgBox.Show("I can't find folder " & TxtFolder.Text & ".  Please try again", "Update Files List",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            My.Settings.MasterFolder = TxtFolder.Text
            My.Settings.Save()
        End If

    End Sub


    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click

        If Not CheckButtons() Then Exit Sub

        Cursor = Cursors.WaitCursor
        ListBox1.Items.Clear()

        Dim DupesReturn As String = RemoveDupes()
        Dim szParts = DupesReturn.Split(vbTab)
        If szParts(0) = "0" Then
            Cursor = Cursors.Default
            Exit Sub
        End If

        ' message for record is in szParts(1)
        DupesReturn = szParts(1)
        ListBox1.Items.Add("During deduplication, " & DupesReturn)

        If Dir(TxtFolder.Text, vbDirectory) = "" Then
            myMsgBox.Show("I can't find folder " & TxtFolder.Text & ".  Please choose another.", "Update Files List",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Cursor = Cursors.Default
            Exit Sub
        End If

        Dim T_DiskFiles = CreateTableDF()
        Dim DiskReturn As Integer = GetFiles(T_DiskFiles)
        If DiskReturn = 0 Then
            myMsgBox.Show("No files were found in the specified folder structure", "Update Files List",
                            MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Cursor = Cursors.Default
            Exit Sub
        End If
        ListBox1.Items.Add(DiskReturn & " files were found on the disk")

        Dim CheckReturn As String = CheckExistingFiles(T_DiskFiles)
        szParts = CheckReturn.Split(vbTab)
        If szParts(0) = "0" Then
            myMsgBox.Show("No file records were processed during check against the disk", "Update Files List",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Cursor = Cursors.Default
            Exit Sub
        End If
        CheckReturn = szParts(1)
        ListBox1.Items.Add(szParts(0) & " file records were processed during check of database against the disk")
        ListBox1.Items.Add(CheckReturn)

        ' check for new files
        Dim NewReturn As String = CheckNewFiles(T_DiskFiles)
        szParts = NewReturn.Split(vbTab)
        If szParts(0) = "0" Then
            myMsgBox.Show("No new files were found on the disk", "Update Files List",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Cursor = Cursors.Default
            Exit Sub
        End If
        NewReturn = szParts(1)
        ListBox1.Items.Add(szParts(0) & " new file records were added to the database")
        ListBox1.Items.Add(NewReturn)

        Cursor = Cursors.Default
    End Sub

    Private Function CheckButtons() As Boolean
        CheckButtons = True

        If RBtnMakeInactive.Checked Then
            iNoFile = iInactive
        ElseIf RBtnDeleteAll.Checked Then
            iNoFile = iDeleteAll
        ElseIf RBtnDeleteThis.Checked Then
            iNoFile = iDeleteThis
        Else
            myMsgBox.Show("Programming error: impossible state of Missing file radio buttons",
                    "Update the File List",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop)
            CheckButtons = False
        End If

        If RBtnMakeActive.Checked Then
            bFoundActive = True
        ElseIf RBtnNoActive.Checked Then
            bFoundActive = False
        Else
            myMsgBox.Show("Programming error: impossible state of Inactive file radio buttons",
                    "Update the File List",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop)
            CheckButtons = False
        End If

        If RBtnMakeSelect.Checked Then
            bNewSelect = True
        ElseIf RBtnNoSelect.Checked Then
            bNewSelect = False
        Else
            myMsgBox.Show("Programming error: impossible state of new file radio buttons",
                    "Update the File List",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop)
            CheckButtons = False
        End If
    End Function

    Private Function RemoveDupes() As String
        Dim fViewRow As DataRowView
        Dim filesView As New DataView(T_filesTable)
        Dim MatchRows As DataView
        Dim nChanges, nTotal, iFileNo, iDupFileNo As Integer
        Dim matchN, matchP As String
        Dim szF_name, szF_path, szSearch As String  ', szFAltName, szSelected, szLast_Action, szIsActive
        Dim dtLast_Dt, dtCreate_Dt As DateTime
        Dim bChanged As Boolean

        'RemoveDupes = ""
        'txtResults.Text = txtResults.Text & vbCrLf & "Emptying Songs Table..."
        nTotal = filesView.Count
        If nTotal = 0 Then
            RemoveDupes = "0" & vbTab & "There were no records in the Files table"
            myMsgBox.Show("The Files table is empty", "Update File List", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Function
        Else
            nChanges = 0
            For Each fViewRow In filesView
                bChanged = False
                iFileNo = fViewRow.Row.Item("file_no")
                matchN = fViewRow.Row.Item("f_name")
                matchN = matchN.Replace("'", "''")
                matchP = fViewRow.Row.Item("f_path")
                MatchRows = New DataView(T_filesTable, "f_name='" & matchN & "' AND f_path='" & matchP & "'",
                                         "f_name, f_path", DataViewRowState.CurrentRows)
                For Each myRow In MatchRows
                    iDupFileNo = myRow.row.item("file_no")
                    If iDupFileNo <> iFileNo Then
                        Dim vPlaylists As New DataView(Tx_playlist2FileTable, "file_no=" & iDupFileNo, "file_no", DataViewRowState.CurrentRows)
                        If vPlaylists.Count > 0 Then
                            Tx_playlist2FileTable.UpdateFileNo(iDupFileNo, iFileNo)
                        End If
                        vPlaylists.Dispose()

                        szF_name = myRow.row.item("f_name")
                        szF_path = myRow.row.item("f_path")
                        'szFAltName = myRow.row.item("f_altname")
                        'szSelected = myRow.row.item("selected")
                        'szLast_Action = myRow.row.item("last_action")
                        'szIsActive = myRow.row.item("isActive")
                        'dtLast_Dt = myRow.row.item("last_dt")
                        'dtCreate_Dt = myRow.row.item("create_dt")
                        szSearch = myRow.row.Item("s_search")
                        Try
                            'T_filesTable.Delete(iDupFileNo, szF_name, szF_path, szFAltName, szSelected,
                            '   dtCreate_Dt, dtLast_Dt, szLast_Action, szIsActive, szSearch)
                            T_filesTable.Delete(iDupFileNo)
                            bChanged = True
                            nChanges += 1
                        Catch ex As Exception
                            myMsgBox.Show("Error deleting duplicate file record " & iDupFileNo & " (duplicate of " & iFileNo & ")" & vbCrLf &
                                "LD [" & dtLast_Dt & "]; " & "CD [" & dtCreate_Dt & "]" & vbCrLf &
                                            ex.Message, "Update File List", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Finally
                            T_filesTable.AcceptChanges()

                        End Try
                    End If
                Next
                If bChanged Then
                    T_filesTable.AcceptChanges()
                End If
                MatchRows.Dispose()
            Next
        End If

        filesView.Dispose()
        RemoveDupes = "1" & vbTab & "There were " & nTotal & " records in the Files table.  " & nChanges & " duplicates were deleted"
    End Function


    Private Function CreateTableDF() As DataTable
        Dim myTable As New DataTable("T_DiskFiles")
        Dim keys(2) As DataColumn

        myTable.Columns.Add("f_path", Type.GetType("System.String"))
        myTable.Columns.Add("f_name", Type.GetType("System.String"))
        myTable.Columns.Add("fullpath", Type.GetType("System.String"))
        myTable.Columns.Add("status", Type.GetType("System.Int32"))
        ' status: 0=file found; 1=file matched db
        keys(0) = myTable.Columns(0)
        keys(1) = myTable.Columns(1)
        myTable.PrimaryKey = keys

        CreateTableDF = myTable
    End Function


    Private Sub ResultAddDF(myTable As DataTable, s1 As String, s2 As String, i1 As Integer)
        Dim workRow As DataRow = myTable.NewRow()
        workRow("f_path") = s1
        workRow("f_name") = s2
        workRow("fullpath") = s1 & "\" & s2
        workRow("status") = i1
        myTable.Rows.Add(workRow)
        myTable.AcceptChanges()
    End Sub


    Private Function GetFiles(T_diskfiles As DataTable) As Integer
        Dim szPath As String, NextFile As String
        Dim AllDirs(30) As String, NextDir As String, i As Integer

        'txtResults.Text = txtResults.Text & vbCrLf & "Collecting New files..."
        Dim szFolder As String = TxtFolder.Text

        GetFiles = 0
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
                szFolder & vbCrLf & "Please locate a Parklea songs MASTERS folder",
                "Update File List", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Exit Function
        End If

        For Each szPath In AllDirs
            NextFile = Dir(szPath & "\*.*", 0)
            While NextFile <> "" ' j <4
                Select Case NextFile.Substring(NextFile.Length - 4, 4)
                    Case ".ppt", "pptx", ".PPT", "PPTX"
                        ResultAddDF(T_diskfiles, szPath, NextFile, 0)
                End Select
                NextFile = Dir()
            End While
        Next szPath

        GetFiles = T_diskfiles.Rows.Count
    End Function

    Private Function CheckExistingFiles(T_diskfiles As DataTable) As String
        Dim DBFiles As New DataView(T_filesTable, "", "f_path, f_name", DataViewRowState.CurrentRows)
        Dim DiskFiles As DataView = T_diskfiles.AsDataView
        Dim szPath, szFName As String
        Dim iRow, iLRow, iFileNo, iOFileNo, iListNo, iRecNo, iSeqNo As Integer
        Dim szFullPath As String
        Dim nInactive, nDeleteAll, nDeleteThis, nDeleteRecord, nMakeActive, nIgnoreInactive, nNoChange As Integer

        nInactive = 0
        nDeleteAll = 0
        nDeleteThis = 0
        nMakeActive = 0
        nIgnoreInactive = 0
        nNoChange = 0
        'CheckExistingFiles = "0" & vbTab & "Nothing done yet"
        DiskFiles.Sort = "fullpath"

        For Each DBFile As DataRowView In DBFiles
            szPath = DBFile.Row.Item("f_path")
            szFName = DBFile.Row.Item("f_name")
            szFullPath = szPath & "\" & szFName
            iFileNo = DBFile.Row.Item("file_no")
            iRow = DiskFiles.Find(szFullPath)
            If iRow < 0 Then
                'file is no longer on the disk - is it included in a playlist?
                Dim vPlaylists As New DataView(Tx_playlist2FileTable, "file_no=" & iFileNo, "file_no", DataViewRowState.CurrentRows)
                If vPlaylists.Count > 0 Then
                    ' file is included in play lists so we need to delete the entries
                    Select Case iNoFile
                        Case iInactive
                            DBFile.Row.Item("isActive") = "N"
                            ListBox1.Items.Add(szFullPath & ": Not on disk - marked Inactive")
                            nInactive += 1

                        Case iDeleteAll
                            Dim ListView As DataView = Tx_playlist2FileTable.DefaultView
                            ListView.Sort = "list_no"
                            ' find each playlist number then delete all the rows with that pl number
                            For Each myRow In vPlaylists
                                iListNo = myRow.Item("list_no")
                                Do
                                    iLRow = ListView.Find(iListNo)
                                    If iLRow < 0 Then Exit Do
                                    iRecNo = ListView.Item(iLRow)("rec_no")
                                    iSeqNo = ListView.Item(iLRow)("seq_no")
                                    iOFileNo = ListView.Item(iLRow)("file_no")
                                    ListView.Delete(iLRow)
                                    Tx_playlist2FileTable.Delete(iRecNo, iListNo, iSeqNo, iOFileNo)
                                Loop
                            Next
                            Tx_playlist2FileTable.AcceptChanges()
                            Try
                                T_filesTable.Delete(iFileNo)
                                'T_filesTable.Delete(iFileNo, szFName, szPath, DBFile.Row.Item("f_altname"), DBFile.Row.Item("selected"),
                                '    DBFile.Row.Item("create_dt"), DBFile.Row.Item("last_dt"), DBFile.Row.Item("last_action"), DBFile.Row.Item("isActive"),
                                '        DBFile.Row.Item("s_search"))
                            Catch ex As Exception
                                myMsgBox.Show("Error deleting file record " & iFileNo & vbCrLf &
                                        "LD [" & DBFile.Row.Item("last_dt") & "]; " & "CD [" & DBFile.Row.Item("create_dt") & "]" & vbCrLf &
                                        ex.Message, "Update File List", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End Try

                            ListBox1.Items.Add(szFullPath & ": Not on disk - all lists deleted")
                            nDeleteAll += 1

                        Case iDeleteThis
                            For Each myRow In vPlaylists
                                iListNo = myRow.Item("list_no")
                                iRecNo = myRow.Item("rec_no")
                                iSeqNo = myRow.Item("seq_no")
                                myRow.Delete
                                Tx_playlist2FileTable.Delete(iRecNo, iListNo, iSeqNo, iFileNo)
                            Next
                            Tx_playlist2FileTable.AcceptChanges()
                            Try
                                T_filesTable.Delete(iFileNo)
                                'T_filesTable.Delete(iFileNo, szFName, szPath, DBFile.Row.Item("f_altname"), DBFile.Row.Item("selected"),
                                '    DBFile.Row.Item("create_dt"), DBFile.Row.Item("last_dt"), DBFile.Row.Item("last_action"), DBFile.Row.Item("isActive"),
                                '        DBFile.Row.Item("s_search"))

                            Catch ex As Exception
                                myMsgBox.Show("Error deleting file record " & iFileNo & vbCrLf &
                                    "LD [" & DBFile.Row.Item("last_dt") & "]; " & "CD [" & DBFile.Row.Item("create_dt") & "]" & vbCrLf &
                                    ex.Message, "Update File List", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End Try
                            ListBox1.Items.Add(szFullPath & ": Not on disk - deleted from all lists")
                            nDeleteThis += 1

                        Case Else
                            myMsgBox.Show("Programming error: Impossible state in unmatched file test",
                                    "Update File List", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            ListBox1.Items.Add(szFullPath & ": Illegal status for not found file - " & iNoFile)

                    End Select
                    vPlaylists.Dispose()
                Else
                    Try
                        T_filesTable.Delete(iFileNo)
                        'T_filesTable.Delete(iFileNo, szFName, szPath, DBFile.Row.Item("f_altname"), DBFile.Row.Item("selected"),
                        '        DBFile.Row.Item("create_dt"), DBFile.Row.Item("last_dt"),
                        '        DBFile.Row.Item("last_action"), DBFile.Row.Item("isActive"), DBFile.Row.Item("s_search"))
                    Catch ex As Exception
                        myMsgBox.Show("Error deleting file record " & iFileNo & vbCrLf &
                                        "LD [" & DBFile.Row.Item("last_dt") & "]; " & "CD [" & DBFile.Row.Item("create_dt") & "]" & vbCrLf &
                                        ex.Message, "Update File List", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    ListBox1.Items.Add(szFullPath & ": Not on disk or in any playlists - deleted record")
                    nDeleteRecord += 1
                End If

            Else
                'file is matched by one on the disk - mark this as matched
                If DiskFiles.Item(iRow)("status") <> 0 Then
                    ' this has already been marked
                    myMsgBox.Show("File Check error: " & szPath & "\" & szFName & " is listed twice in the database" & vbCrLf &
                                    "Second listing is at record #" & iFileNo,
                                    "Update File List", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    DiskFiles.Item(iRow)("status") = 1
                End If

                If DBFile.Row.Item("isActive") = "N" Then
                    'file was marked inactive but is actually on disk
                    If bFoundActive Then
                        DBFile.Row.Item("isActive") = "Y"
                        nMakeActive += 1
                        ListBox1.Items.Add(szFullPath & ": On disk - marked Active")
                    Else
                        ListBox1.Items.Add(szFullPath & ": On disk - but left as Inactive")
                        nIgnoreInactive += 1
                    End If
                Else
                    ListBox1.Items.Add(szFullPath & ": On disk, marked as active, no change necessary")
                    nNoChange += 1
                End If
            End If
            Tx_playlist2FileTable.AcceptChanges()
        Next
        Dim iTot As Integer = nInactive + nDeleteAll + nDeleteThis + nDeleteRecord + nMakeActive + nIgnoreInactive + nNoChange
        CheckExistingFiles = iTot & vbTab & nInactive & "->INACTIVE; " & nDeleteAll & "->DELETEALL; " & nDeleteThis & "->DELETETHIS;" &
            nDeleteRecord & "->DELETEREC; " & nMakeActive & "->ACTIVE; " & nIgnoreInactive & "->INACTIVE IGNORED" & nNoChange & "->NOCHANGE"
        DBFiles.Dispose()
    End Function

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Function CheckNewFiles(T_diskfiles As DataTable) As String
        Dim nFiles As Integer = 0
        Dim DiskFiles As DataView = T_diskfiles.AsDataView
        DiskFiles.Sort = "status"

        CheckNewFiles = "0" & vbTab & "No new files found"
        For Each myRow In DiskFiles.FindRows(0)
            nFiles += 1
            '     Public Sub Insert(fileName As String, filePath As String, isShort As Boolean)
            T_filesTable.Insert(myRow.Item("f_name"), myRow.Item("f_path"), bNewSelect, "New file added")
            ListBox1.Items.Add("New file found and added to the table: " & myRow.Item("f_path") & "\" & myRow.Item("f_name"))
        Next
        T_filesTable.AcceptChanges()
        If nFiles > 0 Then CheckNewFiles = nFiles & " " & vbTab & "New files found and added"
        DiskFiles.Dispose()
    End Function
End Class