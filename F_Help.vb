Public Class F_Help
    Private ReadOnly myMsgBox As New DlgMsgBox

    Private Sub BtnAdmin_Click(sender As Object, e As EventArgs) Handles BtnAdmin.Click
        Try
            Process.Start(Application.StartupPath & "\ProjHelp Administrator Manual.pdf")
        Catch ex As Exception
            myMsgBox.Show("Can't open Admninistrator Manual" & vbCrLf & ex.Message, "Help",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub BtnUser_Click(sender As Object, e As EventArgs) Handles BtnUser.Click
        MyMsgBox.Show(Application.StartupPath)

        Try
            Process.Start(Application.StartupPath & "\ProjHelp User Manual.pdf")
        Catch ex As Exception
            myMsgBox.Show("Can't open User Manual" & vbCrLf & ex.Message, "Help",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
End Class