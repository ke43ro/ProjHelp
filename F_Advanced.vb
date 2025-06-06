Imports System.Data.SQLite

Public Class F_Advanced
    Private Sub F_Advanced_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ChkAutoSelectList.Checked = My.Settings.AutoShortList
        ChkDebug.Checked = My.Settings.Debug
    End Sub

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
        F_Main.isAutoShort = ChkAutoSelectList.Checked
        My.Settings.AutoShortList = F_Main.isAutoShort
        My.Settings.Save()
    End Sub

    Private Sub ChkDebug_CheckedChanged(sender As Object, e As EventArgs) Handles ChkDebug.CheckedChanged
        F_Main.isDebug = ChkDebug.Checked
        My.Settings.Debug = F_Main.isDebug
        My.Settings.Save()

    End Sub
End Class