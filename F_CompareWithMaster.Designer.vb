<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F_CompareWithMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_CompareWithMaster))
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.BtnBrowse = New System.Windows.Forms.Button()
        Me.TxtFolder = New System.Windows.Forms.TextBox()
        Me.BtnCompare = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.BtnCompareFile = New System.Windows.Forms.Button()
        Me.TxtCompareFile = New System.Windows.Forms.TextBox()
        Me.LstResults = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Enabled = False
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(12, 3)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(776, 98)
        Me.TextBox1.TabIndex = 1
        Me.TextBox1.Text = resources.GetString("TextBox1.Text")
        '
        'BtnBrowse
        '
        Me.BtnBrowse.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnBrowse.Location = New System.Drawing.Point(609, 106)
        Me.BtnBrowse.Name = "BtnBrowse"
        Me.BtnBrowse.Size = New System.Drawing.Size(179, 25)
        Me.BtnBrowse.TabIndex = 32
        Me.BtnBrowse.Text = "Browse for MASTERS folder"
        Me.BtnBrowse.UseVisualStyleBackColor = True
        '
        'TxtFolder
        '
        Me.TxtFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtFolder.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFolder.Location = New System.Drawing.Point(13, 107)
        Me.TxtFolder.Name = "TxtFolder"
        Me.TxtFolder.Size = New System.Drawing.Size(580, 24)
        Me.TxtFolder.TabIndex = 31
        Me.TxtFolder.Text = "Master Folder"
        '
        'BtnCompare
        '
        Me.BtnCompare.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCompare.Location = New System.Drawing.Point(319, 172)
        Me.BtnCompare.Name = "BtnCompare"
        Me.BtnCompare.Size = New System.Drawing.Size(161, 33)
        Me.BtnCompare.TabIndex = 33
        Me.BtnCompare.Text = "Compare"
        Me.BtnCompare.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        Me.OpenFileDialog1.InitialDirectory = "Documents"
        '
        'BtnCompareFile
        '
        Me.BtnCompareFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCompareFile.Location = New System.Drawing.Point(609, 136)
        Me.BtnCompareFile.Name = "BtnCompareFile"
        Me.BtnCompareFile.Size = New System.Drawing.Size(179, 25)
        Me.BtnCompareFile.TabIndex = 35
        Me.BtnCompareFile.Text = "Browse for Compare File"
        Me.BtnCompareFile.UseVisualStyleBackColor = True
        '
        'TxtCompareFile
        '
        Me.TxtCompareFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtCompareFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCompareFile.Location = New System.Drawing.Point(13, 137)
        Me.TxtCompareFile.Name = "TxtCompareFile"
        Me.TxtCompareFile.Size = New System.Drawing.Size(580, 24)
        Me.TxtCompareFile.TabIndex = 34
        Me.TxtCompareFile.Text = "Compare File ""LyricsMatch - LyricsList.tsv"""
        '
        'LstResults
        '
        Me.LstResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LstResults.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstResults.FormattingEnabled = True
        Me.LstResults.ItemHeight = 18
        Me.LstResults.Location = New System.Drawing.Point(13, 213)
        Me.LstResults.Name = "LstResults"
        Me.LstResults.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.LstResults.Size = New System.Drawing.Size(775, 200)
        Me.LstResults.TabIndex = 36
        '
        'F_CompareWithMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(808, 426)
        Me.Controls.Add(Me.LstResults)
        Me.Controls.Add(Me.BtnCompareFile)
        Me.Controls.Add(Me.TxtCompareFile)
        Me.Controls.Add(Me.BtnCompare)
        Me.Controls.Add(Me.BtnBrowse)
        Me.Controls.Add(Me.TxtFolder)
        Me.Controls.Add(Me.TextBox1)
        Me.Name = "F_CompareWithMaster"
        Me.Text = "CompareWithMaster"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents BtnBrowse As Button
    Friend WithEvents TxtFolder As TextBox
    Friend WithEvents BtnCompare As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents BtnCompareFile As Button
    Friend WithEvents TxtCompareFile As TextBox
    Friend WithEvents LstResults As ListBox
End Class
