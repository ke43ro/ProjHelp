<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F_SetUp
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_SetUp))
        Me.BtnTest3 = New System.Windows.Forms.Button()
        Me.TxtAdvice3 = New System.Windows.Forms.TextBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.BtnBrowse = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.TxtFolder = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'BtnTest3
        '
        Me.BtnTest3.Enabled = False
        Me.BtnTest3.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.BtnTest3.Location = New System.Drawing.Point(295, 192)
        Me.BtnTest3.Name = "BtnTest3"
        Me.BtnTest3.Size = New System.Drawing.Size(135, 29)
        Me.BtnTest3.TabIndex = 24
        Me.BtnTest3.Text = "Test"
        Me.BtnTest3.UseVisualStyleBackColor = True
        '
        'TxtAdvice3
        '
        Me.TxtAdvice3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtAdvice3.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.TxtAdvice3.Location = New System.Drawing.Point(12, 12)
        Me.TxtAdvice3.Multiline = True
        Me.TxtAdvice3.Name = "TxtAdvice3"
        Me.TxtAdvice3.ReadOnly = True
        Me.TxtAdvice3.Size = New System.Drawing.Size(419, 170)
        Me.TxtAdvice3.TabIndex = 22
        Me.TxtAdvice3.TabStop = False
        Me.TxtAdvice3.Text = resources.GetString("TxtAdvice3.Text")
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(213, 200)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(41, 17)
        Me.CheckBox1.TabIndex = 28
        Me.CheckBox1.Text = "OK"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'BtnBrowse
        '
        Me.BtnBrowse.Enabled = False
        Me.BtnBrowse.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.BtnBrowse.Location = New System.Drawing.Point(12, 192)
        Me.BtnBrowse.Name = "BtnBrowse"
        Me.BtnBrowse.Size = New System.Drawing.Size(179, 29)
        Me.BtnBrowse.TabIndex = 27
        Me.BtnBrowse.Text = "Browse for MASTERS folder"
        Me.BtnBrowse.UseVisualStyleBackColor = True
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.Description = "Please find the MASTERS folder as described above the Browse button"
        Me.FolderBrowserDialog1.ShowNewFolderButton = False
        '
        'TxtFolder
        '
        Me.TxtFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtFolder.Enabled = False
        Me.TxtFolder.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.TxtFolder.Location = New System.Drawing.Point(13, 236)
        Me.TxtFolder.Name = "TxtFolder"
        Me.TxtFolder.ReadOnly = True
        Me.TxtFolder.Size = New System.Drawing.Size(417, 27)
        Me.TxtFolder.TabIndex = 26
        Me.TxtFolder.TabStop = False
        Me.TxtFolder.Text = "Master Folder"
        '
        'F_SetUp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(445, 273)
        Me.Controls.Add(Me.TxtFolder)
        Me.Controls.Add(Me.BtnTest3)
        Me.Controls.Add(Me.TxtAdvice3)
        Me.Controls.Add(Me.BtnBrowse)
        Me.Controls.Add(Me.CheckBox1)
        Me.HelpButton = True
        Me.Name = "F_SetUp"
        Me.Text = "Set Up Wizard"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnTest3 As Button
    Friend WithEvents TxtAdvice3 As TextBox
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents TxtFolder As TextBox
    Friend WithEvents BtnBrowse As Button
    Friend WithEvents CheckBox1 As CheckBox
End Class
