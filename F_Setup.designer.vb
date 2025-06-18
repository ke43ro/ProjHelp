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
        Me.BtnTest = New System.Windows.Forms.Button()
        Me.TxtAdvice = New System.Windows.Forms.TextBox()
        Me.CheckBox = New System.Windows.Forms.CheckBox()
        Me.BtnBrowse = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.TxtFolder = New System.Windows.Forms.TextBox()
        Me.TxtInstr = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'BtnTest
        '
        Me.BtnTest.Enabled = False
        Me.BtnTest.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.BtnTest.Location = New System.Drawing.Point(143, 147)
        Me.BtnTest.Name = "BtnTest"
        Me.BtnTest.Size = New System.Drawing.Size(135, 29)
        Me.BtnTest.TabIndex = 24
        Me.BtnTest.Text = "Test"
        Me.BtnTest.UseVisualStyleBackColor = True
        '
        'TxtAdvice
        '
        Me.TxtAdvice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtAdvice.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.TxtAdvice.Location = New System.Drawing.Point(12, 205)
        Me.TxtAdvice.Multiline = True
        Me.TxtAdvice.Name = "TxtAdvice"
        Me.TxtAdvice.ReadOnly = True
        Me.TxtAdvice.Size = New System.Drawing.Size(419, 67)
        Me.TxtAdvice.TabIndex = 22
        Me.TxtAdvice.TabStop = False
        Me.TxtAdvice.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.TxtAdvice.Visible = False
        '
        'CheckBox
        '
        Me.CheckBox.AutoSize = True
        Me.CheckBox.Location = New System.Drawing.Point(205, 182)
        Me.CheckBox.Name = "CheckBox"
        Me.CheckBox.Size = New System.Drawing.Size(41, 17)
        Me.CheckBox.TabIndex = 28
        Me.CheckBox.Text = "OK"
        Me.CheckBox.UseVisualStyleBackColor = True
        '
        'BtnBrowse
        '
        Me.BtnBrowse.Enabled = False
        Me.BtnBrowse.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.BtnBrowse.Location = New System.Drawing.Point(123, 79)
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
        Me.TxtFolder.Location = New System.Drawing.Point(14, 114)
        Me.TxtFolder.Name = "TxtFolder"
        Me.TxtFolder.ReadOnly = True
        Me.TxtFolder.Size = New System.Drawing.Size(417, 27)
        Me.TxtFolder.TabIndex = 26
        Me.TxtFolder.TabStop = False
        Me.TxtFolder.Text = "Master Folder"
        '
        'TxtInstr
        '
        Me.TxtInstr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtInstr.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.TxtInstr.Location = New System.Drawing.Point(14, 12)
        Me.TxtInstr.Multiline = True
        Me.TxtInstr.Name = "TxtInstr"
        Me.TxtInstr.ReadOnly = True
        Me.TxtInstr.Size = New System.Drawing.Size(419, 52)
        Me.TxtInstr.TabIndex = 29
        Me.TxtInstr.TabStop = False
        Me.TxtInstr.Text = "Please use the Browse button to select the top-level folder of your PowerPoint fi" &
    "les collection, then press Test."
        '
        'F_SetUp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(445, 285)
        Me.Controls.Add(Me.TxtInstr)
        Me.Controls.Add(Me.TxtFolder)
        Me.Controls.Add(Me.BtnTest)
        Me.Controls.Add(Me.TxtAdvice)
        Me.Controls.Add(Me.BtnBrowse)
        Me.Controls.Add(Me.CheckBox)
        Me.HelpButton = True
        Me.Name = "F_SetUp"
        Me.Text = "Set Up Wizard"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnTest As Button
    Friend WithEvents TxtAdvice As TextBox
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents TxtFolder As TextBox
    Friend WithEvents BtnBrowse As Button
    Friend WithEvents CheckBox As CheckBox
    Friend WithEvents TxtInstr As TextBox
End Class
