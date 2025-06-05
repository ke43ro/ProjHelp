<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_Video
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
        Me.VideoView1 = New LibVLCSharp.WinForms.VideoView()
        CType(Me.VideoView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'VideoView1
        '
        Me.VideoView1.BackColor = System.Drawing.Color.Black
        Me.VideoView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VideoView1.Location = New System.Drawing.Point(0, 0)
        Me.VideoView1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.VideoView1.MediaPlayer = Nothing
        Me.VideoView1.Name = "VideoView1"
        Me.VideoView1.Size = New System.Drawing.Size(1029, 540)
        Me.VideoView1.TabIndex = 0
        Me.VideoView1.Text = "VideoView1"
        '
        'F_Video
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1029, 540)
        Me.ControlBox = False
        Me.Controls.Add(Me.VideoView1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "F_Video"
        Me.Text = "Keyboard: S to stop and go to next, P to pause or play, R to restart. Clicker: Ne" &
    "xt to stop and go to next, Black to pause or play, Previous to restart"
        CType(Me.VideoView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents VideoView1 As LibVLCSharp.WinForms.VideoView
End Class
