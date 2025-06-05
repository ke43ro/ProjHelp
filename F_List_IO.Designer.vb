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
        Me.BtnExport = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.ChkAddAlt = New System.Windows.Forms.CheckBox()
        Me.ChkReplaceAlt = New System.Windows.Forms.CheckBox()
        Me.BtnImport = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ChkAddInactive = New System.Windows.Forms.CheckBox()
        Me.ChkRemInactive = New System.Windows.Forms.CheckBox()
        Me.ChkAddSelect = New System.Windows.Forms.CheckBox()
        Me.ChkRemoveSelect = New System.Windows.Forms.CheckBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        'Me.ProHelpDataSet = New ProHelpDataSet()
        Me.T_filesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        'Me.T_filesTable = New ProHelpDataSetTables.T_filesTable()
        'Me.TableManager = New ProHelpDataSetTables.TableManager()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1.SuspendLayout()
        'CType(Me.ProHelpDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.T_filesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnExport
        '
        Me.BtnExport.Location = New System.Drawing.Point(114, 19)
        Me.BtnExport.Name = "BtnExport"
        Me.BtnExport.Size = New System.Drawing.Size(161, 33)
        Me.BtnExport.TabIndex = 0
        Me.BtnExport.Text = "Export File List"
        Me.ToolTip1.SetToolTip(Me.BtnExport, "Create a file that lists all the records in your database including their Active/" &
        "Inactive and Selected (Short List) status.  Use  Update File List first for best" &
        " results.")
        Me.BtnExport.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBox2)
        Me.GroupBox1.Controls.Add(Me.ChkAddAlt)
        Me.GroupBox1.Controls.Add(Me.ChkReplaceAlt)
        Me.GroupBox1.Controls.Add(Me.BtnImport)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.ChkAddInactive)
        Me.GroupBox1.Controls.Add(Me.ChkRemInactive)
        Me.GroupBox1.Controls.Add(Me.ChkAddSelect)
        Me.GroupBox1.Controls.Add(Me.ChkRemoveSelect)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 64)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(359, 261)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Import File List"
        '
        'TextBox2
        '
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextBox2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBox2.Location = New System.Drawing.Point(8, 188)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(342, 21)
        Me.TextBox2.TabIndex = 9
        Me.TextBox2.TabStop = False
        Me.TextBox2.Text = "* If neither is selected, alternative text in the import will be ignored"
        '
        'ChkAddAlt
        '
        Me.ChkAddAlt.AutoSize = True
        Me.ChkAddAlt.Location = New System.Drawing.Point(12, 168)
        Me.ChkAddAlt.Name = "ChkAddAlt"
        Me.ChkAddAlt.Size = New System.Drawing.Size(242, 17)
        Me.ChkAddAlt.TabIndex = 8
        Me.ChkAddAlt.Text = "Extend Alternative text with that in the import *"
        Me.ChkAddAlt.UseVisualStyleBackColor = True
        '
        'ChkReplaceAlt
        '
        Me.ChkReplaceAlt.AutoSize = True
        Me.ChkReplaceAlt.Location = New System.Drawing.Point(12, 144)
        Me.ChkReplaceAlt.Name = "ChkReplaceAlt"
        Me.ChkReplaceAlt.Size = New System.Drawing.Size(249, 17)
        Me.ChkReplaceAlt.TabIndex = 7
        Me.ChkReplaceAlt.Text = "Replace Alternative text with that in the import *"
        Me.ChkReplaceAlt.UseVisualStyleBackColor = True
        '
        'BtnImport
        '
        Me.BtnImport.Location = New System.Drawing.Point(101, 214)
        Me.BtnImport.Name = "BtnImport"
        Me.BtnImport.Size = New System.Drawing.Size(161, 33)
        Me.BtnImport.TabIndex = 6
        Me.BtnImport.Text = "Import"
        Me.ToolTip1.SetToolTip(Me.BtnImport, "Read a file that has been exported from another installation.  Make changes to yo" &
        "ur database to synchronise (according to your chosen options).")
        Me.BtnImport.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextBox1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBox1.Location = New System.Drawing.Point(12, 112)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(342, 35)
        Me.TextBox1.TabIndex = 5
        Me.TextBox1.TabStop = False
        Me.TextBox1.Text = "* Import does not check whether or not the associated files exist on disk." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  Use" &
    " Advanced > Update File List to verify file existence."
        '
        'ChkAddInactive
        '
        Me.ChkAddInactive.AutoSize = True
        Me.ChkAddInactive.Location = New System.Drawing.Point(12, 92)
        Me.ChkAddInactive.Name = "ChkAddInactive"
        Me.ChkAddInactive.Size = New System.Drawing.Size(240, 17)
        Me.ChkAddInactive.TabIndex = 3
        Me.ChkAddInactive.Text = "Set my Inactive flags if the Import is Inactive *"
        Me.ChkAddInactive.UseVisualStyleBackColor = True
        '
        'ChkRemInactive
        '
        Me.ChkRemInactive.AutoSize = True
        Me.ChkRemInactive.Location = New System.Drawing.Point(12, 68)
        Me.ChkRemInactive.Name = "ChkRemInactive"
        Me.ChkRemInactive.Size = New System.Drawing.Size(282, 17)
        Me.ChkRemInactive.TabIndex = 2
        Me.ChkRemInactive.Text = "Remove my Inactive flags if the Import is not Inactive *"
        Me.ChkRemInactive.UseVisualStyleBackColor = True
        '
        'ChkAddSelect
        '
        Me.ChkAddSelect.AutoSize = True
        Me.ChkAddSelect.Location = New System.Drawing.Point(12, 44)
        Me.ChkAddSelect.Name = "ChkAddSelect"
        Me.ChkAddSelect.Size = New System.Drawing.Size(223, 17)
        Me.ChkAddSelect.TabIndex = 1
        Me.ChkAddSelect.Text = "Set my Selected flags if Import is Selected"
        Me.ChkAddSelect.UseVisualStyleBackColor = True
        '
        'ChkRemoveSelect
        '
        Me.ChkRemoveSelect.AutoSize = True
        Me.ChkRemoveSelect.Location = New System.Drawing.Point(12, 20)
        Me.ChkRemoveSelect.Name = "ChkRemoveSelect"
        Me.ChkRemoveSelect.Size = New System.Drawing.Size(283, 17)
        Me.ChkRemoveSelect.TabIndex = 0
        Me.ChkRemoveSelect.Text = "Remove my Selected flags if the Import is not Selected"
        Me.ChkRemoveSelect.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'ProHelpDataSet
        '
        'Me.ProHelpDataSet.DataSetName = "ProHelpDataSet"
        'Me.ProHelpDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'T_filesBindingSource
        '
        Me.T_filesBindingSource.DataMember = "t_files"
        'Me.T_filesBindingSource.DataSource = Me.ProHelpDataSet
        '
        'T_filesTable
        '
        'Me.T_filesTable.ClearBeforeFill = True
        '
        'TableManager
        '
        'Me.TableManager.BackupDataSetBeforeUpdate = False
        'Me.TableManager.T_filesTable = Me.T_filesTable
        'Me.TableManager.t_playlistsTable = Nothing
        'Me.TableManager.tx_playlist_songTable = Nothing
        'Me.TableManager.UpdateOrder = ProHelpDataSetTables.TableManager.UpdateOrderOption.InsertUpdateDelete
        '
        'F_List_IO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(388, 336)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BtnExport)
        Me.Name = "F_List_IO"
        Me.Text = "Export or Import a List of Files"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        'CType(Me.ProHelpDataSet, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents ChkAddInactive As CheckBox
    Friend WithEvents ChkRemInactive As CheckBox
    Friend WithEvents ChkAddSelect As CheckBox
    Friend WithEvents ChkRemoveSelect As CheckBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents ChkAddAlt As CheckBox
    Friend WithEvents ChkReplaceAlt As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
End Class
