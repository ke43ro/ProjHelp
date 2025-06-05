Imports System.Data.SQLite
Imports System.Runtime.CompilerServices

Public Class DlgLoadList
    Private isLoaded As Boolean = False
    Private T_filesTable As FilesTable
    Private T_playlistsTable As PlaylistsTable
    Private Tx_playlist2FileTable As Tx_playlistTable
    Private isShort As Boolean = False
    Private ReadOnly myMsgBox As New DlgMsgBox

    Friend Sub GetList(ByRef myList As ListBox)
        For Each listItem In LstQueue.Items
            myList.Items.Add(listItem)
        Next
    End Sub

    Friend Sub Fillqueue(ByRef ListBox As ListBox)
        Dim iLoop As Integer = 0

        LstQueue.Items.Clear()

        While iLoop < ListBox.Items.Count
            LstQueue.Items.Add(ListBox.Items(iLoop))
            iLoop += 1
        End While
    End Sub

    Private Sub DlgLoadList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim nSongs As Integer = LstQueue.Items.Count
        Dim songtext As String
        isShort = My.Settings.SelectedOnly
        Dim connection As SQLiteConnection = F_Main.ProjHelpData.GetConnection()
        If connection.State <> ConnectionState.Open Then
            connection.Open()
        End If

        Select Case nSongs
            Case 0
                songtext = "are no songs"
            Case 1
                songtext = "is 1 song"
            Case Else
                songtext = "are " & nSongs & " songs"
        End Select
        TxtStatus.Text = "There " & songtext & " in the current unnamed Play List"
        ChkFuture.Checked = True

        Tx_playlist2FileTable = New Tx_playlistTable(connection)
        T_filesTable = New FilesTable(False, connection)
        T_playlistsTable = New PlaylistsTable(connection, ChkFuture.Checked)
        T_playlistsBindingSource = New BindingSource With {
            .DataSource = T_playlistsTable.DefaultView
        }
        T_playlistsDataGridView.DataSource = T_playlistsBindingSource
        T_playlistsTable.SetDGProperties(T_playlistsDataGridView, NoEdit:=False)

        isLoaded = True
    End Sub

    Private Sub Load_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Load_Button.Click
        Dim searchView As DataView = Tx_playlist2FileTable.DefaultView
        Dim ListRows() As DataRowView
        Dim iListNo As Integer, bRowSel As Boolean = True

        T_playlistsTable.AcceptChanges()
        If T_playlistsDataGridView.SelectedRows.Count = 0 Then
            bRowSel = False
        ElseIf T_playlistsDataGridView.SelectedRows(0).IsNewRow Then
            bRowSel = False
        End If

        If bRowSel = False Then
            myMsgBox.Show("Please select a Play List from the table to load",
                            "Loading a PlayList",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        iListNo = T_playlistsDataGridView.SelectedRows(0).Cells(0).Value

        searchView.Sort = "list_no"
        ListRows = searchView.FindRows(iListNo)
        If ListRows.Length = 0 Then
            myMsgBox.Show("There are no entries yet in this Play List", "Loading a Play List",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else
            myMsgBox.Show(ListRows.Length & " entries from this saved Play List will be added to the current", "Loading a Play List",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        LstQueue.Items.Clear()
        Dim filesView As DataView = T_filesTable.DefaultView
        filesView.Sort = "file_no"
        Dim filesRow() As DataRowView
        For Each Row In ListRows
            filesRow = filesView.FindRows(Row("FILE_NO"))
            LstQueue.Items.Add(Row("FILE_NO") & vbTab & filesRow(0)("F_NAME"))
        Next

        DialogResult = DialogResult.Yes ' indicates a "Load"
        Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub Save_Button_Click(sender As Object, e As EventArgs) Handles Save_Button.Click
        Dim szParms() As String
        Dim iListNo As Integer
        Dim iLoop As Integer = 0
        Dim result As DialogResult
        Dim bRowSel As Boolean = True

        ' Save the current Play List to the database
        If T_playlistsDataGridView.SelectedRows.Count = 0 Then
            bRowSel = False
        ElseIf T_playlistsDataGridView.SelectedRows(0).IsNewRow Then
            bRowSel = False
        End If

        If bRowSel = False Then
            myMsgBox.Show("Please select a Play List from the table to save the list",
                            "Saving a PlayList",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' ensure that the selected row is updated (insert if necessary)
        T_playlistsDataGridView.Update()
        iListNo = T_playlistsTable.UpdateInsert(T_playlistsDataGridView.SelectedRows(0))

        ' check whether this playlist has any records already
        If Tx_playlist2FileTable.Count(iListNo) > 0 Then
            result = myMsgBox.Show("There are already records in this Play List." & vbCrLf &
                            "Do you want to replace them with the current list?", "Saving a Play List",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Error)
            If result = DialogResult.No Then Exit Sub
        End If

        'Dim searchView As DataView = Tx_playlist2FileTable.DefaultView
        'searchView.Sort = "list_no"
        'If searchView.FindRows(iListNo).Count > 0 Then

        '    For Each deleteRow In searchView.FindRows(iListNo)
        '        Tx_playlist2FileTable.Delete(deleteRow("REC_NO"), deleteRow("LIST_NO"), deleteRow("SEQ_NO"), deleteRow("FILE_NO"))
        '    Next

        '    Tx_playlist2FileTable.AcceptChanges()
        'End If

        'add records to tx_playlist_song(iListNo)
        While iLoop < LstQueue.Items.Count
            szParms = LstQueue.Items(iLoop).Split(vbTab)
            Tx_playlist2FileTable.Insert(iListNo, iLoop, szParms(0), szParms(2))
            iLoop += 1
        End While
        Tx_playlist2FileTable.AcceptChanges()

        DialogResult = DialogResult.No ' indicates a "Save"
        Close()
    End Sub

    Private Sub ChkFuture_CheckedChanged(sender As Object, e As EventArgs) Handles ChkFuture.CheckedChanged
        ' this event can occur during form loading so we check that the control
        ' has been created i.e. form has finished loading
        If isLoaded Then
            If ChkFuture.Checked Then
                T_playlistsTable.LoadFuture(Today)
            Else
                T_playlistsTable.LoadAll()
            End If
        End If
    End Sub

    Private Sub TplaylistsDataGridView_RowStateChanged(sender As Object, e As DataGridViewRowStateChangedEventArgs) Handles T_playlistsDataGridView.RowStateChanged
        If e.StateChanged = DataGridViewElementStates.Selected Then
            Dim iCount As Integer = Tx_playlist2FileTable.Count(e.Row.Cells(0).Value)
            TxtStatus.Text = "There are " & iCount & " items in this Play List"
        End If
    End Sub
End Class
