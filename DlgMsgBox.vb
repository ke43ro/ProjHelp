Imports System.Windows.Forms

Partial Class DlgMsgBox

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Public Overloads Function Show(Message As String, Title As String, Optional Buttons As MessageBoxButtons = MessageBoxButtons.OK, Optional Icon As MessageBoxIcon = MessageBoxIcon.Information) As DialogResult
        Me.Text = "Projection Helper: " & Title
        Me.TextBox1.Text = Message
        Me.IconPictureBox.Image = SystemIcons.Information.ToBitmap ' Default icon
        Select Case Icon
            Case MessageBoxIcon.Error
                Me.IconPictureBox.Image = SystemIcons.Error.ToBitmap
            Case MessageBoxIcon.Warning
                Me.IconPictureBox.Image = SystemIcons.Warning.ToBitmap
            Case MessageBoxIcon.Information
                Me.IconPictureBox.Image = SystemIcons.Information.ToBitmap
            Case MessageBoxIcon.Question
                Me.IconPictureBox.Image = SystemIcons.Question.ToBitmap
        End Select
        If Buttons = MessageBoxButtons.OK Then
            OK_Button.Visible = True
            Cancel_Button.Visible = False
        ElseIf Buttons = MessageBoxButtons.OKCancel Then
            OK_Button.Visible = True
            Cancel_Button.Visible = True
        End If
        Return Me.ShowDialog()
    End Function

    Public Overloads Function Show(myPrompt As String, myTitle As String) As DialogResult
        Dim Response As DialogResult
        Dim myDialog = New DlgMsgBox
        myDialog.TextBox1.Text = myPrompt
        myDialog.Text = "Projection Helper: " + myTitle
        Response = myDialog.ShowDialog()
        Return Response
    End Function

    Public Overloads Function Show(myPrompt As String) As DialogResult
        Dim Response As DialogResult
        Dim myDialog = New DlgMsgBox
        myDialog.TextBox1.Text = myPrompt
        myDialog.Text = "Projection Helper: Debug"
        OK_Button.Visible = True
        Cancel_Button.Visible = False
        Response = myDialog.ShowDialog()
        Return Response
    End Function
End Class
