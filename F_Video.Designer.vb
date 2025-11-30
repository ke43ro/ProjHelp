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
        Me.components = New System.ComponentModel.Container()
        Me.VideoView1 = New LibVLCSharp.WinForms.VideoView()
        Me.BarSeek = New System.Windows.Forms.TrackBar()
        Me.BarVol = New System.Windows.Forms.TrackBar()
        Me.BtnPause = New System.Windows.Forms.Button()
        Me.BtnStop = New System.Windows.Forms.Button()
        Me.TxtCur = New System.Windows.Forms.TextBox()
        Me.TxtEnd = New System.Windows.Forms.TextBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        CType(Me.VideoView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarSeek, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarVol, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'VideoView1
        '
        Me.VideoView1.BackColor = System.Drawing.Color.Black
        Me.VideoView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VideoView1.Location = New System.Drawing.Point(0, 0)
        Me.VideoView1.Margin = New System.Windows.Forms.Padding(4)
        Me.VideoView1.MediaPlayer = Nothing
        Me.VideoView1.Name = "VideoView1"
        Me.VideoView1.Size = New System.Drawing.Size(1029, 582)
        Me.VideoView1.TabIndex = 0
        Me.VideoView1.Text = "VideoView1"
        '
        'BarSeek
        '
        Me.BarSeek.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BarSeek.Location = New System.Drawing.Point(495, 4)
        Me.BarSeek.Maximum = 1000
        Me.BarSeek.Name = "BarSeek"
        Me.BarSeek.Size = New System.Drawing.Size(460, 45)
        Me.BarSeek.TabIndex = 2
        '
        'BarVol
        '
        Me.BarVol.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BarVol.Location = New System.Drawing.Point(316, 587)
        Me.BarVol.Maximum = 100
        Me.BarVol.Name = "BarVol"
        Me.BarVol.Size = New System.Drawing.Size(140, 45)
        Me.BarVol.TabIndex = 3
        Me.BarVol.UseWaitCursor = True
        Me.BarVol.Value = 75
        '
        'BtnPause
        '
        Me.BtnPause.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnPause.Location = New System.Drawing.Point(3, 5)
        Me.BtnPause.Name = "BtnPause"
        Me.BtnPause.Size = New System.Drawing.Size(136, 45)
        Me.BtnPause.TabIndex = 4
        Me.BtnPause.Text = "Play/Pause"
        Me.BtnPause.UseVisualStyleBackColor = True
        '
        'BtnStop
        '
        Me.BtnStop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnStop.Location = New System.Drawing.Point(149, 588)
        Me.BtnStop.Name = "BtnStop"
        Me.BtnStop.Size = New System.Drawing.Size(136, 45)
        Me.BtnStop.TabIndex = 5
        Me.BtnStop.Text = "Stop and Close"
        Me.BtnStop.UseVisualStyleBackColor = True
        '
        'TxtCur
        '
        Me.TxtCur.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtCur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtCur.Enabled = False
        Me.TxtCur.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCur.Location = New System.Drawing.Point(961, 587)
        Me.TxtCur.Name = "TxtCur"
        Me.TxtCur.Size = New System.Drawing.Size(68, 22)
        Me.TxtCur.TabIndex = 6
        '
        'TxtEnd
        '
        Me.TxtEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtEnd.Enabled = False
        Me.TxtEnd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEnd.Location = New System.Drawing.Point(961, 610)
        Me.TxtEnd.Name = "TxtEnd"
        Me.TxtEnd.Size = New System.Drawing.Size(68, 22)
        Me.TxtEnd.TabIndex = 7
        '
        'Timer1
        '
        Me.Timer1.Interval = 500
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.BarSeek)
        Me.Panel1.Controls.Add(Me.BtnPause)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 582)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1029, 53)
        Me.Panel1.TabIndex = 8
        '
        'F_Video
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1029, 635)
        Me.ControlBox = False
        Me.Controls.Add(Me.TxtEnd)
        Me.Controls.Add(Me.TxtCur)
        Me.Controls.Add(Me.BtnStop)
        Me.Controls.Add(Me.BarVol)
        Me.Controls.Add(Me.VideoView1)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "F_Video"
        Me.Text = "Keyboard: P to pause or play, R to restart, S to stop and go to next. Clicker: Bl" &
    "ack to pause or play, Previous to restart, Next to stop and go to next."
        CType(Me.VideoView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarSeek, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarVol, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents VideoView1 As LibVLCSharp.WinForms.VideoView
    Friend WithEvents BarSeek As TrackBar
    Friend WithEvents BarVol As TrackBar
    Friend WithEvents BtnPause As Button
    Friend WithEvents BtnStop As Button
    Friend WithEvents TxtCur As TextBox
    Friend WithEvents TxtEnd As TextBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Panel1 As Panel
End Class
