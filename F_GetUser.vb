Public Class F_GetUser
    Private Sub BtnChoose_Click(sender As Object, e As EventArgs) Handles BtnChoose.Click
        ' save the current record if changed, select it and close the form
        DialogResult = DialogResult.Yes
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        ' Save all changed records, do not select any, just close the form
        DialogResult = DialogResult.OK
    End Sub
End Class