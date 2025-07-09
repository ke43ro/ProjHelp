Public Class F_Help
    Private ReadOnly myMsgBox As New DlgMsgBox

    Private Sub BtnAdmin_Click(sender As Object, e As EventArgs) Handles BtnAdmin.Click
        Try
            Process.Start(Application.StartupPath & "\ProjHelp Administrator Manual.pdf")
        Catch ex As Exception
            myMsgBox.Show("Can't open Administrator Manual" & vbCrLf & ex.Message, "Help",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub BtnUser_Click(sender As Object, e As EventArgs) Handles BtnUser.Click
        Try
            Process.Start(Application.StartupPath & "\ProjHelp User Manual.pdf")
        Catch ex As Exception
            myMsgBox.Show("Can't open User Manual" & vbCrLf & ex.Message, "Help",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub BtnQuick_Click(sender As Object, e As EventArgs) Handles BtnQuick.Click
        Try
            Process.Start(Application.StartupPath & "\ProjHelp Quick Guide.pdf")
        Catch ex As Exception
            myMsgBox.Show("Can't open Quick Guide" & vbCrLf & ex.Message, "Help",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
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