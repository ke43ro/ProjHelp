<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F_List_IO
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_List_IO))
        Me.BtnExport = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.ChkAddAlt = New System.Windows.Forms.CheckBox()
        Me.ChkReplaceAlt = New System.Windows.Forms.CheckBox()
        Me.BtnImport = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ChkAddActive = New System.Windows.Forms.CheckBox()
        Me.ChkRemActive = New System.Windows.Forms.CheckBox()
        Me.ChkAddSelect = New System.Windows.Forms.CheckBox()
        Me.ChkRemoveSelect = New System.Windows.Forms.CheckBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.T_filesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1.SuspendLayout()
        CType(Me.T_filesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnExport
        '
        Me.BtnExport.Location = New System.Drawing.Point(171, 26)
        Me.BtnExport.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtnExport.Name = "BtnExport"
        Me.BtnExport.Size = New System.Drawing.Size(242, 46)
        Me.BtnExport.TabIndex = 0
        Me.BtnExport.Text = "Export File List"
        Me.ToolTip1.SetToolTip(Me.BtnExport, resources.GetString("BtnExport.ToolTip"))
        Me.BtnExport.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBox2)
        Me.GroupBox1.Controls.Add(Me.ChkAddAlt)
        Me.GroupBox1.Controls.Add(Me.ChkReplaceAlt)
        Me.GroupBox1.Controls.Add(Me.BtnImport)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.ChkAddActive)
        Me.GroupBox1.Controls.Add(Me.ChkRemActive)
        Me.GroupBox1.Controls.Add(Me.ChkAddSelect)
        Me.GroupBox1.Controls.Add(Me.ChkRemoveSelect)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 89)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(538, 361)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Import File List"
        '
        'TextBox2
        '
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextBox2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBox2.Location = New System.Drawing.Point(12, 260)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(513, 29)
        Me.TextBox2.TabIndex = 9
        Me.TextBox2.TabStop = False
        Me.TextBox2.Text = "* If neither is selected, alternative text in the import will be ignored"
        '
        'ChkAddAlt
        '
        Me.ChkAddAlt.AutoSize = True
        Me.ChkAddAlt.Location = New System.Drawing.Point(18, 233)
        Me.ChkAddAlt.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ChkAddAlt.Name = "ChkAddAlt"
        Me.ChkAddAlt.Size = New System.Drawing.Size(323, 22)
        Me.ChkAddAlt.TabIndex = 8
        Me.ChkAddAlt.Text = "Extend Alternative text with that in the import *"
        Me.ChkAddAlt.UseVisualStyleBackColor = True
        '
        'ChkReplaceAlt
        '
        Me.ChkReplaceAlt.AutoSize = True
        Me.ChkReplaceAlt.Location = New System.Drawing.Point(18, 199)
        Me.ChkReplaceAlt.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ChkReplaceAlt.Name = "ChkReplaceAlt"
        Me.ChkReplaceAlt.Size = New System.Drawing.Size(332, 22)
        Me.ChkReplaceAlt.TabIndex = 7
        Me.ChkReplaceAlt.Text = "Replace Alternative text with that in the import *"
        Me.ChkReplaceAlt.UseVisualStyleBackColor = True
        '
        'BtnImport
        '
        Me.BtnImport.Location = New System.Drawing.Point(152, 296)
        Me.BtnImport.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtnImport.Name = "BtnImport"
        Me.BtnImport.Size = New System.Drawing.Size(242, 46)
        Me.BtnImport.TabIndex = 6
        Me.BtnImport.Text = "Import"
        Me.ToolTip1.SetToolTip(Me.BtnImport, "Read a file that has been exported from another installation.  Make changes to yo" &
        "ur database to synchronise" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(according to your chosen options).")
        Me.BtnImport.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextBox1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBox1.Location = New System.Drawing.Point(18, 155)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(513, 48)
        Me.TextBox1.TabIndex = 5
        Me.TextBox1.TabStop = False
        Me.TextBox1.Text = "* Import does not check whether or not the associated files exist on disk." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  Use" &
    " Advanced > Update File List to verify file existence."
        '
        'ChkAddActive
        '
        Me.ChkAddActive.AutoSize = True
        Me.ChkAddActive.Location = New System.Drawing.Point(18, 127)
        Me.ChkAddActive.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ChkAddActive.Name = "ChkAddActive"
        Me.ChkAddActive.Size = New System.Drawing.Size(300, 22)
        Me.ChkAddActive.TabIndex = 3
        Me.ChkAddActive.Text = "Set my Active flags if the Import is Active *"
        Me.ChkAddActive.UseVisualStyleBackColor = True
        '
        'ChkRemActive
        '
        Me.ChkRemActive.AutoSize = True
        Me.ChkRemActive.Location = New System.Drawing.Point(18, 94)
        Me.ChkRemActive.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ChkRemActive.Name = "ChkRemActive"
        Me.ChkRemActive.Size = New System.Drawing.Size(359, 22)
        Me.ChkRemActive.TabIndex = 2
        Me.ChkRemActive.Text = "Remove my Active flags if the Import is not Active *"
        Me.ChkRemActive.UseVisualStyleBackColor = True
        '
        'ChkAddSelect
        '
        Me.ChkAddSelect.AutoSize = True
        Me.ChkAddSelect.Location = New System.Drawing.Point(18, 61)
        Me.ChkAddSelect.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ChkAddSelect.Name = "ChkAddSelect"
        Me.ChkAddSelect.Size = New System.Drawing.Size(335, 22)
        Me.ChkAddSelect.TabIndex = 1
        Me.ChkAddSelect.Text = "Set my Short List flags if Import is on Short List"
        Me.ChkAddSelect.UseVisualStyleBackColor = True
        '
        'ChkRemoveSelect
        '
        Me.ChkRemoveSelect.AutoSize = True
        Me.ChkRemoveSelect.Location = New System.Drawing.Point(18, 28)
        Me.ChkRemoveSelect.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ChkRemoveSelect.Name = "ChkRemoveSelect"
        Me.ChkRemoveSelect.Size = New System.Drawing.Size(413, 22)
        Me.ChkRemoveSelect.TabIndex = 0
        Me.ChkRemoveSelect.Text = "Remove my Short List flags if the Import is not Short Listed"
        Me.ChkRemoveSelect.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'T_filesBindingSource
        '
        Me.T_filesBindingSource.DataMember = "t_files"
        '
        'ToolTip1
        '
        Me.ToolTip1.OwnerDraw = True
        '
        'F_List_IO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(582, 465)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BtnExport)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "F_List_IO"
        Me.Text = "Export or Import a List of Files"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.T_filesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnExport As Button
    'Friend WithEvents ProHelpDataSet As ProHelpDataSet
    Friend WithEvents T_filesBindingSource As BindingSource
    'Friend WithEvents T_filesTable As ProHelpDataSetTables.T_filesTable
    'Friend WithEvents TableManager As ProHelpDataSetTables.TableManager
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents BtnImport As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents ChkAddActive As CheckBox
    Friend WithEvents ChkRemActive As CheckBox
    Friend WithEvents ChkAddSelect As CheckBox
    Friend WithEvents ChkRemoveSelect As CheckBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents ChkAddAlt As CheckBox
    Friend WithEvents ChkReplaceAlt As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
End Class
