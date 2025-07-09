Imports System.Windows.Forms

Partial Class DlgMsgBox
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        DialogResult = System.Windows.Forms.DialogResult.OK
        Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        DialogResult = System.Windows.Forms.DialogResult.Cancel
        Close()
    End Sub

    Public Overloads Function Show(Message As String, Title As String, Optional Buttons As MessageBoxButtons = MessageBoxButtons.OK, Optional Icon As MessageBoxIcon = MessageBoxIcon.Information) As DialogResult
        Text = "Projection Helper: " & Title
        TextBox1.Text = Message
        IconPictureBox.Image = SystemIcons.Information.ToBitmap ' Default icon
        Select Case Icon
            Case MessageBoxIcon.Error
                IconPictureBox.Image = SystemIcons.Error.ToBitmap
            Case MessageBoxIcon.Warning
                IconPictureBox.Image = SystemIcons.Warning.ToBitmap
            Case MessageBoxIcon.Information
                IconPictureBox.Image = SystemIcons.Information.ToBitmap
            Case MessageBoxIcon.Question
                IconPictureBox.Image = SystemIcons.Question.ToBitmap
        End Select
        If Buttons = MessageBoxButtons.OK Then
            OK_Button.Visible = True
            Cancel_Button.Visible = False
        ElseIf Buttons = MessageBoxButtons.OKCancel Then
            OK_Button.Visible = True
            Cancel_Button.Visible = True
        End If
        Return ShowDialog()
    End Function

    Public Overloads Function Show(myPrompt As String, myTitle As String) As DialogResult
        Dim Response As DialogResult
        TextBox1.Text = myPrompt
        Text = "Projection Helper: " + myTitle
        Response = ShowDialog()
        Return Response
    End Function

    Public Overloads Function Show(myPrompt As String) As DialogResult
        Dim Response As DialogResult

        Me.SuspendLayout()
        IconPictureBox.Image = SystemIcons.Information.ToBitmap
        TextBox1.Text = myPrompt
        Text = "Projection Helper: Debug"
        OK_Button.Visible = True
        Cancel_Button.Visible = False
        Me.ResumeLayout(False)
        Response = ShowDialog()
        Return Response
    End Function
End Class
