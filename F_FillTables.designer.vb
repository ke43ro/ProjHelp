<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_FillTables
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
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.txtFolder = New System.Windows.Forms.TextBox()
        Me.txtResults = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.BtnLoadTable = New System.Windows.Forms.Button()
        Me.BtnEmpty = New System.Windows.Forms.Button()
        Me.T_filesDataGridView = New System.Windows.Forms.DataGridView()
        CType(Me.T_filesDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnClose
        '
        Me.BtnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.Location = New System.Drawing.Point(434, 345)
        Me.BtnClose.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(189, 55)
        Me.BtnClose.TabIndex = 0
        Me.BtnClose.Text = "Close"
        Me.ToolTip1.SetToolTip(Me.BtnClose, "Close the Dialog")
        '
        'txtFolder
        '
        Me.txtFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFolder.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFolder.Location = New System.Drawing.Point(18, 17)
        Me.txtFolder.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtFolder.Name = "txtFolder"
        Me.txtFolder.ReadOnly = True
        Me.txtFolder.Size = New System.Drawing.Size(611, 24)
        Me.txtFolder.TabIndex = 1
        Me.txtFolder.TabStop = False
        '
        'txtResults
        '
        Me.txtResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtResults.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResults.Location = New System.Drawing.Point(18, 53)
        Me.txtResults.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtResults.Multiline = True
        Me.txtResults.Name = "txtResults"
        Me.txtResults.ReadOnly = True
        Me.txtResults.Size = New System.Drawing.Size(611, 283)
        Me.txtResults.TabIndex = 2
        Me.txtResults.TabStop = False
        Me.txtResults.Text = "<Results>"
        Me.ToolTip1.SetToolTip(Me.txtResults, "Shows what is happening in the program")
        '
        'BtnLoadTable
        '
        Me.BtnLoadTable.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnLoadTable.Location = New System.Drawing.Point(230, 345)
        Me.BtnLoadTable.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtnLoadTable.Name = "BtnLoadTable"
        Me.BtnLoadTable.Size = New System.Drawing.Size(189, 55)
        Me.BtnLoadTable.TabIndex = 4
        Me.BtnLoadTable.Text = "Load table"
        Me.ToolTip1.SetToolTip(Me.BtnLoadTable, "Scan the specified MASTERS folder and record the names of all PPT & PPTX files fo" &
        "und in the Files table.")
        Me.BtnLoadTable.UseVisualStyleBackColor = True
        '
        'BtnEmpty
        '
        Me.BtnEmpty.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnEmpty.Location = New System.Drawing.Point(26, 346)
        Me.BtnEmpty.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtnEmpty.Name = "BtnEmpty"
        Me.BtnEmpty.Size = New System.Drawing.Size(189, 55)
        Me.BtnEmpty.TabIndex = 7
        Me.BtnEmpty.Text = "Empty Table"
        Me.ToolTip1.SetToolTip(Me.BtnEmpty, "Delete all entries from the Files table to start again.")
        Me.BtnEmpty.UseVisualStyleBackColor = True
        '
        'T_filesDataGridView
        '
        Me.T_filesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.T_filesDataGridView.Location = New System.Drawing.Point(98, 410)
        Me.T_filesDataGridView.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.T_filesDataGridView.Name = "T_filesDataGridView"
        Me.T_filesDataGridView.Size = New System.Drawing.Size(450, 202)
        Me.T_filesDataGridView.TabIndex = 6
        '
        'F_FillTables
        '
        Me.AcceptButton = Me.BtnClose
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(660, 636)
        Me.Controls.Add(Me.BtnEmpty)
        Me.Controls.Add(Me.T_filesDataGridView)
        Me.Controls.Add(Me.BtnLoadTable)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.txtResults)
        Me.Controls.Add(Me.txtFolder)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "F_FillTables"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Recreate Song List"
        CType(Me.T_filesDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents txtFolder As TextBox
    Friend WithEvents txtResults As TextBox
    Friend WithEvents BtnLoadTable As Button
    'Friend WithEvents T_filesTable As ProHelpDataSetTables.T_filesTable
    'Friend WithEvents TableManager As ProHelpDataSetTables.TableManager
    Friend WithEvents T_filesDataGridView As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As DataGridViewTextBoxColumn
    Friend WithEvents BtnEmpty As Button
End Class
