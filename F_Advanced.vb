Imports System.Data.SQLite

Public Class F_Advanced
    'Private isT_FilesUpdated As Boolean = False
    'Private T_filesTable As FilesTable
    'Private isShort As Boolean

    Private Sub F_Advanced_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ChkAutoSelectList.Checked = My.Settings.AutoShortList
        'isShort = My.Settings.SelectedOnly

        'Dim connection As SQLiteConnection = F_Main.ProjHelpData.GetConnection()
        'If connection.State <> ConnectionState.Open Then
        '    connection.Open()
        'End If

        'T_filesTable = New FilesTable(isShort, connection)
        'T_filesTable.LoadActive(isShort:=False)

    End Sub

    'Private Sub F_Advanced_Closed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
    '    If isT_FilesUpdated Then
    '        Me.DialogResult = DialogResult.Yes
    '    Else
    '        Me.DialogResult = DialogResult.No
    '    End If
    'End Sub

    Private Sub BtnListIO_Click(sender As Object, e As EventArgs) Handles BtnListIO.Click
        F_List_IO.ShowDialog()
    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        F_EditFileList.ShowDialog()
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        F_UpdateFileList.ShowDialog()
    End Sub

    Private Sub BtnCompare_Click(sender As Object, e As EventArgs) Handles BtnCompare.Click
        F_CompareWithMaster.ShowDialog()
    End Sub

    Private Sub ChkAutoSelectList_CheckedChanged(sender As Object, e As EventArgs) Handles ChkAutoSelectList.CheckedChanged
        My.Settings.AutoShortList = ChkAutoSelectList.Checked
        My.Settings.Save()
    End Sub

    Private Sub BtnDebug_CheckedChanged(sender As Object, e As EventArgs) Handles BtnDebug.CheckedChanged
        F_Main.isDebug = BtnDebug.Checked
        My.Settings.Debug = F_Main.isDebug
        My.Settings.Save()

    End Sub
End Class