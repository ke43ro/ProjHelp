Imports System.Data.SQLite

Public Class F_UpdateFileList
    'Private connection As SqlConnection
    Private iNoFile As Integer, isVerbose As Boolean = False
    Private isDebug As Boolean = F_Main.IsDebug
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
        If Dir(TxtFolder.Text, vbDirectory) = "" Then
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

        Dim szFolder As String = TxtFolder.Text
        If Dir(szFolder, vbDirectory) = "" Then
            myMsgBox.Show("I can't find folder " & szFolder & ".  Please choose another.", "Update Files List",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Cursor = Cursors.Default
            Exit Sub
        End If

        'Dim T_DiskFiles As DataTable = CreateTableDF()
        'Dim DiskReturn As Integer = GetFiles(T_DiskFiles)
        Dim myPPTs As GetFiles, T_DiskFiles As DataTable
        myPPTs = New GetFiles()
        T_DiskFiles = myPPTs.GetPPs(szFolder)
        Dim DiskReturn As Integer = T_DiskFiles.Rows.Count

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
        T_filesTable.LoadAll()
        Dim filesView As New DataView(T_filesTable) With {.Sort = "file_no"}
        Dim MatchRows As DataView
        Dim nChanges, nTotal, iFileNo, iDupFileNo As Integer
        Dim matchN, matchP As String
        'Dim szSearch, szF_name, szF_path, szFAltName, szSelected, szLast_Action, szIsActive As String
        Dim dtLast_Dt, dtCreate_Dt As DateTime
        'Dim bChanged As Boolean

        If isDebug Then
            myMsgBox.Show("Removing duplicates from the Files table", "Update File List", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        nTotal = filesView.Count
        If nTotal = 0 Then
            RemoveDupes = "0" & vbTab & "There were no records in the Files table"
            myMsgBox.Show("The Files table is empty", "Update File List", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Function
        Else
            nChanges = 0
            For Each fViewRow In filesView
                If fViewRow.Row.RowState = DataRowState.Detached Then
                    ' duplicate already deleted
                    Continue For
                End If
                'bChanged = False
                iFileNo = fViewRow.Row.Item("file_no")
                matchN = fViewRow.Row.Item("f_name")
                matchN = matchN.Replace("'", "''")
                matchP = fViewRow.Row.Item("f_path")
                'If isDebug Then myMsgBox.Show("About to open MatchRows")
                MatchRows = New DataView(T_filesTable, "f_name='" & matchN & "' AND f_path='" & matchP & "'",
                                             "f_name, f_path", DataViewRowState.CurrentRows)
                'If isDebug Then myMsgBox.Show("MatchRows opened")
                For Each myRow In MatchRows
                    iDupFileNo = myRow.row.item("file_no")
                    If iDupFileNo <> iFileNo Then
                        Dim vPlaylists As New DataView(Tx_playlist2FileTable, "file_no=" & iDupFileNo, "file_no", DataViewRowState.CurrentRows)
                        If vPlaylists.Count > 0 Then
                            Tx_playlist2FileTable.UpdateFileNo(iDupFileNo, iFileNo)
                        End If
                        vPlaylists.Dispose()

                        dtLast_Dt = myRow.row.item("last_dt")
                        dtCreate_Dt = myRow.row.item("create_dt")
                        Try
                            'T_filesTable.Delete(iDupFileNo, szF_name, szF_path, szFAltName, szSelected,
                            '   dtCreate_Dt, dtLast_Dt, szLast_Action, szIsActive, szSearch)
                            T_filesTable.Delete(iDupFileNo)
                            'bChanged = True
                            nChanges += 1
                            filesView.Delete(filesView.Find(iDupFileNo))
                        Catch ex As Exception
                            myMsgBox.Show("Error deleting duplicate file record " & iDupFileNo & " (duplicate of " & iFileNo & ")" & vbCrLf &
                                "LD [" & dtLast_Dt & "]; " & "CD [" & dtCreate_Dt & "]" & vbCrLf &
                                            ex.Message, "Update File List", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Finally
                            T_filesTable.AcceptChanges()

                        End Try
                    End If
                Next
                'If bChanged Then
                '    T_filesTable.AcceptChanges()
                'End If
                MatchRows.Dispose()
            Next
        End If

        filesView.Dispose()
        RemoveDupes = "1" & vbTab & "There were " & nTotal & " records in the Files table.  " & nChanges & " duplicates were deleted"
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

        DiskFiles.Sort = "FilePath,FileName"

        For Each DBFile As DataRowView In DBFiles
            szPath = DBFile.Row.Item("f_path")
            szFName = DBFile.Row.Item("f_name")
            szFullPath = szPath & "\" & szFName
            iFileNo = DBFile.Row.Item("file_no")
            iRow = DiskFiles.Find({szPath, szFName})
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
                    If isVerbose Then ListBox1.Items.Add(szFullPath & ": On disk, marked as active, no change necessary")
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

    Private Function CheckNewFiles(T_diskfiles As DataTable) As String
        Dim nFiles As Integer = 0
        Dim DiskFiles As DataView = T_diskfiles.AsDataView
        DiskFiles.Sort = "status"

        CheckNewFiles = "0" & vbTab & "No new files found"
        For Each myRow In DiskFiles.FindRows(0)
            nFiles += 1
            T_filesTable.Insert(myRow.Item("Filename"), myRow.Item("FilePath"), bNewSelect, "New file added")
            ListBox1.Items.Add("New file found and added to the table: " & myRow.Item("FilePath") & "\" & myRow.Item("FileName"))
        Next
        T_filesTable.AcceptChanges()
        If nFiles > 0 Then CheckNewFiles = nFiles & " " & vbTab & "New files found and added"
        DiskFiles.Dispose()
    End Function

    Private Sub ToolTip1_Draw(sender As Object, e As DrawToolTipEventArgs) Handles ToolTip1.Draw
        e.Graphics.FillRectangle(Brushes.LightYellow, e.Bounds)
        e.Graphics.DrawRectangle(Pens.Black, New Rectangle(0, 0, e.Bounds.Width - 1, e.Bounds.Height - 1))
        Using f As New Font("Segoe UI", 10, FontStyle.Regular)
            e.Graphics.DrawString(e.ToolTipText, f, Brushes.Black, New PointF(2, 2))
        End Using
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        isVerbose = CheckBox1.Checked
    End Sub

    Private Sub ToolTip1_Popup(sender As Object, e As PopupEventArgs) Handles ToolTip1.Popup
        Using f As New Font("Segoe UI", 10, FontStyle.Regular)
            Dim textSize = TextRenderer.MeasureText(ToolTip1.GetToolTip(e.AssociatedControl), f, New Size(600, 0), TextFormatFlags.WordBreak)
            ' Add a little padding
            e.ToolTipSize = New Size(Math.Min(textSize.Width, 600), textSize.Height + 4)
        End Using
    End Sub

End Class