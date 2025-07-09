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
        Me.components = New System.ComponentModel.Container()
        Me.BtnUser = New System.Windows.Forms.Button()
        Me.BtnAdmin = New System.Windows.Forms.Button()
        Me.BtnQuick = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'BtnUser
        '
        Me.BtnUser.Location = New System.Drawing.Point(76, 73)
        Me.BtnUser.Name = "BtnUser"
        Me.BtnUser.Size = New System.Drawing.Size(138, 37)
        Me.BtnUser.TabIndex = 0
        Me.BtnUser.Text = "User Manual"
        Me.ToolTip1.SetToolTip(Me.BtnUser, "This document provides detailed instructions on how to use Projection Helper for " &
        "its primary" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "purpose of displaying documents through a data projector or on a se" &
        "cond [large] monitor.")
        Me.BtnUser.UseVisualStyleBackColor = True
        '
        'BtnAdmin
        '
        Me.BtnAdmin.Location = New System.Drawing.Point(76, 143)
        Me.BtnAdmin.Name = "BtnAdmin"
        Me.BtnAdmin.Size = New System.Drawing.Size(138, 37)
        Me.BtnAdmin.TabIndex = 1
        Me.BtnAdmin.Text = "Administrator Manual"
        Me.ToolTip1.SetToolTip(Me.BtnAdmin, "This document provides advice on setting up Projection Helper and" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "maintaining th" &
        "e lyrics database")
        Me.BtnAdmin.UseVisualStyleBackColor = True
        '
        'BtnQuick
        '
        Me.BtnQuick.Location = New System.Drawing.Point(76, 12)
        Me.BtnQuick.Name = "BtnQuick"
        Me.BtnQuick.Size = New System.Drawing.Size(138, 37)
        Me.BtnQuick.TabIndex = 2
        Me.BtnQuick.Text = "Quick Guide"
        Me.ToolTip1.SetToolTip(Me.BtnQuick, "This document is a brief description of the steps required to use Projection Help" &
        "er." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "It is intended to be simple enough to keep open on screen to assist a new o" &
        "perator.")
        Me.BtnQuick.UseVisualStyleBackColor = True
        '
        'ToolTip1
        '
        Me.ToolTip1.OwnerDraw = True
        '
        'F_Help
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(291, 192)
        Me.Controls.Add(Me.BtnQuick)
        Me.Controls.Add(Me.BtnAdmin)
        Me.Controls.Add(Me.BtnUser)
        Me.Name = "F_Help"
        Me.Text = "Help Documents"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnUser As Button
    Friend WithEvents BtnAdmin As Button
    Friend WithEvents BtnQuick As Button
    Friend WithEvents ToolTip1 As ToolTip
End Class
