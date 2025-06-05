<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_Help
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.BtnUser = New System.Windows.Forms.Button()
        Me.BtnAdmin = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtnUser
        '
        Me.BtnUser.Location = New System.Drawing.Point(76, 31)
        Me.BtnUser.Name = "BtnUser"
        Me.BtnUser.Size = New System.Drawing.Size(138, 37)
        Me.BtnUser.TabIndex = 0
        Me.BtnUser.Text = "User Manual"
        Me.BtnUser.UseVisualStyleBackColor = True
        '
        'BtnAdmin
        '
        Me.BtnAdmin.Location = New System.Drawing.Point(76, 83)
        Me.BtnAdmin.Name = "BtnAdmin"
        Me.BtnAdmin.Size = New System.Drawing.Size(138, 37)
        Me.BtnAdmin.TabIndex = 1
        Me.BtnAdmin.Text = "Administrator Manual"
        Me.BtnAdmin.UseVisualStyleBackColor = True
        '
        'F_Help
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(291, 151)
        Me.Controls.Add(Me.BtnAdmin)
        Me.Controls.Add(Me.BtnUser)
        Me.Name = "F_Help"
        Me.Text = "Help Documents"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnUser As Button
    Friend WithEvents BtnAdmin As Button
End Class
