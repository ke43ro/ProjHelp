Imports System.Data.SQLite

Public Class F_EditFileList
    Private bFormOpen As Boolean = False
    Private T_filesTable As FilesTable
    Private ReadOnly T_filesBindingSource As New BindingSource
    Private T_playlistsTable As PlaylistsTable
    Private Tx_playlist2FileTable As Tx_playlistTable
    Private isShort As Boolean = False
    Private ReadOnly myMsgBox As New DlgMsgBox


    Private Sub F_EditFileList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        isShort = My.Settings.SelectedOnly
        Dim connection As SQLiteConnection = F_Main.ProjHelpData.GetConnection()
        If connection.State <> ConnectionState.Open Then
            connection.Open()
        End If
        T_filesTable = New FilesTable(connection, InclInActive:=True)
        'T_filesTable.LoadAll(InclInActive:=True)

        T_filesBindingSource.DataSource = T_filesTable.DefaultView
        TfilesDataGridView.DataSource = T_filesBindingSource
        'TfilesDataGridView.Update()
        TfilesDataGridView.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        T_filesTable.SetDGProperties(TfilesDataGridView, NoEdit:=False)

        Tx_playlist2FileTable = New Tx_playlistTable(connection)
        Tx_playlist2FileTable.LoadAll()

        bFormOpen = True
    End Sub

    'Private Sub F_EditFileList_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
    '    Try
    '        T_filesTable.AcceptChanges()

    '    Catch ex As Exception
    '        MyMsgBox.Show("Can't save changes to the table.  Send this message to the programmer." & vbCrLf & ex.Message,
    '                    "Closing File Edit",
    '                    MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub TfilesDataGridView_KeyDown(sender As Object, e As KeyEventArgs) Handles TfilesDataGridView.KeyDown
        Dim iFileNo, iRow, iLRow, iListNo, iRecNo, iSeqNo As Integer
        Dim result As DialogResult

        Select Case e.KeyCode
            Case Keys.Delete
                iFileNo = T_filesBindingSource.Current.Row.Item("file_no")
                Dim PlayView As DataView = Tx_playlist2FileTable.DefaultView
                PlayView.Sort = "file_no"
                Dim HasRows As DataRowView() = PlayView.FindRows(iFileNo)
                If HasRows.Count > 0 Then
                    result = myMsgBox.Show("This song has been used in playlists that have been saved." & vbCrLf &
                                "Please check the options carefully:" & vbCrLf &
                                "Press 'Yes' to delete the song record and all play lists that contain this song" & vbCrLf &
                                "    [Will remove some history from this installation]" & vbCrLf &
                                "Press 'No' to delete the song record and also delete just this song from all play lists" & vbCrLf &
                                "    [Will remove this song from the history]" & vbCrLf &
                                "Press 'Cancel' to avoid making any changes to the database",
                                "Deleting record #" & iFileNo,
                                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
                    Select Case result
                        Case DialogResult.Yes
                            Dim ListView As DataView = Tx_playlist2FileTable.DefaultView
                            ListView.Sort = "list_no"
                            ' find each playlist number then delete all the rows with that pl number
                            For Each myRow In HasRows
                                iListNo = myRow.Item("list_no")
                                Do
                                    iLRow = ListView.Find(iListNo)
                                    If iLRow < 0 Then Exit Do
                                    iRecNo = ListView.Item(iLRow)("rec_no")
                                    iSeqNo = ListView.Item(iLRow)("seq_no")
                                    iFileNo = ListView.Item(iLRow)("file_no")
                                    ListView.Delete(iLRow)
                                    Tx_playlist2FileTable.Delete(iRecNo, iListNo, iSeqNo, iFileNo)
                                Loop
                            Next
                            'ListView.Dispose()
                            Tx_playlist2FileTable.AcceptChanges()

                        Case DialogResult.No
                            Do
                                iRow = PlayView.Find(iFileNo)
                                If iRow < 0 Then Exit Do
                                iRecNo = PlayView.Item(iRow)("rec_no")
                                iListNo = PlayView.Item(iRow)("list_no")
                                iSeqNo = PlayView.Item(iRow)("seq_no")
                                PlayView.Delete(iRow)
                                Tx_playlist2FileTable.Delete(iRecNo, iListNo, iSeqNo, iFileNo)
                            Loop
                            Tx_playlist2FileTable.AcceptChanges()

                        Case DialogResult.Cancel
                            e.SuppressKeyPress = True
                            Exit Sub
                    End Select
                End If

                ' delete the file record
                T_filesTable.Delete(iFileNo)
                T_filesBindingSource.RemoveCurrent()
                TfilesDataGridView.Update()
            Case Else
                'Exit Sub
        End Select
    End Sub

    'Private Sub TfilesDataGridView_RowValidated(sender As Object, e As DataGridViewCellEventArgs) Handles TfilesDataGridView.RowValidated
    '    T_filesTable.AcceptChanges()
    'End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        T_filesBindingSource.EndEdit()
        T_filesTable.Update(T_filesBindingSource.DataSource)
        Close()
    End Sub
End Class