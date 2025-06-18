<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DlgLoadList
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Save_Button = New System.Windows.Forms.Button()
        Me.Load_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.LstQueue = New System.Windows.Forms.ListBox()
        Me.T_playlistsDataGridView = New System.Windows.Forms.DataGridView()
        Me.TxtStatus = New System.Windows.Forms.TextBox()
        Me.ChkFuture = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.T_playlistsDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Save_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Load_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 2, 0)
        Me.TableLayoutPanel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(418, 320)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(224, 40)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Save_Button
        '
        Me.Save_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Save_Button.Location = New System.Drawing.Point(80, 4)
        Me.Save_Button.Name = "Save_Button"
        Me.Save_Button.Size = New System.Drawing.Size(71, 32)
        Me.Save_Button.TabIndex = 2
        Me.Save_Button.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.Save_Button, "Save the unnamed list from the Main screen into the named Play List currently sel" &
        "ected above." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Any previous entries will be removed first.")
        '
        'Load_Button
        '
        Me.Load_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Load_Button.Location = New System.Drawing.Point(3, 4)
        Me.Load_Button.Name = "Load_Button"
        Me.Load_Button.Size = New System.Drawing.Size(71, 32)
        Me.Load_Button.TabIndex = 0
        Me.Load_Button.Text = "Load"
        Me.ToolTip1.SetToolTip(Me.Load_Button, "Copy the titles listed in the currently selected named Play List into the unnamed" &
        " list on the main screen." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "These are added to those already included.")
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(157, 4)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(64, 32)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        Me.ToolTip1.SetToolTip(Me.Cancel_Button, "Close this dialog without making any changes")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(132, 18)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Available Play Lists"
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Enabled = False
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(12, 290)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(627, 24)
        Me.TextBox1.TabIndex = 4
        Me.TextBox1.TabStop = False
        Me.TextBox1.Text = "Select a row then press Save or Load.  Pressing Cancel will leave the main screen" &
    " unchanged"
        '
        'LstQueue
        '
        Me.LstQueue.FormattingEnabled = True
        Me.LstQueue.Location = New System.Drawing.Point(12, 323)
        Me.LstQueue.Name = "LstQueue"
        Me.LstQueue.Size = New System.Drawing.Size(120, 30)
        Me.LstQueue.TabIndex = 7
        Me.LstQueue.TabStop = False
        Me.LstQueue.Visible = False
        '
        'T_playlistsDataGridView
        '
        Me.T_playlistsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.T_playlistsDataGridView.Location = New System.Drawing.Point(13, 60)
        Me.T_playlistsDataGridView.MultiSelect = False
        Me.T_playlistsDataGridView.Name = "T_playlistsDataGridView"
        Me.T_playlistsDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.T_playlistsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.T_playlistsDataGridView.Size = New System.Drawing.Size(626, 226)
        Me.T_playlistsDataGridView.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.T_playlistsDataGridView, "This table lists all the currently defined named Play Lists.  These might be empt" &
        "y or partially built.")
        '
        'TxtStatus
        '
        Me.TxtStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtStatus.Location = New System.Drawing.Point(12, 30)
        Me.TxtStatus.Name = "TxtStatus"
        Me.TxtStatus.Size = New System.Drawing.Size(627, 24)
        Me.TxtStatus.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.TxtStatus, "This is a reminder of how many titles you have iin the unnamed list back on the m" &
        "ain screen" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "and how many are in the selected named play list")
        '
        'ChkFuture
        '
        Me.ChkFuture.AutoSize = True
        Me.ChkFuture.Checked = True
        Me.ChkFuture.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkFuture.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkFuture.Location = New System.Drawing.Point(154, 331)
        Me.ChkFuture.Name = "ChkFuture"
        Me.ChkFuture.Size = New System.Drawing.Size(180, 22)
        Me.ChkFuture.TabIndex = 9
        Me.ChkFuture.Text = "See only Future Shows"
        Me.ToolTip1.SetToolTip(Me.ChkFuture, "When this is ticked, only Play Lists with a date of today and later will be shown" &
        " in the list above." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Clear it to see prior dated Play Lists.")
        Me.ChkFuture.UseVisualStyleBackColor = True
        '
        'ToolTip1
        '
        Me.ToolTip1.OwnerDraw = True
        '
        'DlgLoadList
        '
        Me.AcceptButton = Me.Load_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(654, 386)
        Me.Controls.Add(Me.ChkFuture)
        Me.Controls.Add(Me.TxtStatus)
        Me.Controls.Add(Me.LstQueue)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.T_playlistsDataGridView)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DlgLoadList"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Manage Play Lists"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.T_playlistsDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Load_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents T_playlistsBindingSource As BindingSource
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents LstQueue As ListBox
    Friend WithEvents T_playlistsDataGridView As DataGridView
    Friend WithEvents ListnoDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PlaydtDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents LnameDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents Save_Button As Button
    Friend WithEvents TxtStatus As TextBox
    Friend WithEvents ChkFuture As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
End Class
