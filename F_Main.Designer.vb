
'<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F_Main
    Inherits System.Windows.Forms.Form

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer


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
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_Main))
        Me.LBPlayList = New System.Windows.Forms.ListBox()
        Me.TxtSearch = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.BtnPlay = New System.Windows.Forms.Button()
        Me.BtnClear = New System.Windows.Forms.Button()
        Me.BtnAdd = New System.Windows.Forms.Button()
        Me.BtnAdvanced = New System.Windows.Forms.Button()
        Me.BtnHelp = New System.Windows.Forms.Button()
        Me.BtnSetup = New System.Windows.Forms.Button()
        Me.BtnLoadList = New System.Windows.Forms.Button()
        Me.ChkShortList = New System.Windows.Forms.CheckBox()
        Me.T_filesDataGridView = New System.Windows.Forms.DataGridView()
        Me.ChkPause = New System.Windows.Forms.CheckBox()
        Me.LBInstant = New System.Windows.Forms.ListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtListNo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.LblVersion = New System.Windows.Forms.Label()
        CType(Me.T_filesDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LBPlayList
        '
        Me.LBPlayList.AllowDrop = True
        Me.LBPlayList.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBPlayList.FormattingEnabled = True
        Me.LBPlayList.ItemHeight = 17
        Me.LBPlayList.Location = New System.Drawing.Point(12, 25)
        Me.LBPlayList.Name = "LBPlayList"
        Me.LBPlayList.Size = New System.Drawing.Size(864, 106)
        Me.LBPlayList.TabIndex = 0
        Me.LBPlayList.TabStop = False
        Me.ToolTip1.SetToolTip(Me.LBPlayList, "This is the list of songs and files that will be played ")
        '
        'TxtSearch
        '
        Me.TxtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSearch.Location = New System.Drawing.Point(208, 68)
        Me.TxtSearch.Name = "TxtSearch"
        Me.TxtSearch.Size = New System.Drawing.Size(117, 24)
        Me.TxtSearch.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.TxtSearch, "Type letters of any word or partial word in the song name (or its alternate name)" &
        "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "to find it in the database")
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(2, 2)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(560, 68)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Search pane:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "While searching:  use UP and DOWN arrow keys to move the selection;" &
    " use ENTER (or the Add button) to add the current selection to the list."
        '
        'ToolTip1
        '
        Me.ToolTip1.OwnerDraw = True
        '
        'BtnPlay
        '
        Me.BtnPlay.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPlay.Location = New System.Drawing.Point(775, 139)
        Me.BtnPlay.Name = "BtnPlay"
        Me.BtnPlay.Size = New System.Drawing.Size(100, 37)
        Me.BtnPlay.TabIndex = 5
        Me.BtnPlay.Text = "&Play all"
        Me.ToolTip1.SetToolTip(Me.BtnPlay, "Start PowerPoint and show the list of songs")
        Me.BtnPlay.UseVisualStyleBackColor = True
        '
        'BtnClear
        '
        Me.BtnClear.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClear.Location = New System.Drawing.Point(617, 139)
        Me.BtnClear.Name = "BtnClear"
        Me.BtnClear.Size = New System.Drawing.Size(113, 37)
        Me.BtnClear.TabIndex = 4
        Me.BtnClear.Text = "Clear Play List"
        Me.ToolTip1.SetToolTip(Me.BtnClear, "Delete all the songs from the Play List")
        Me.BtnClear.UseVisualStyleBackColor = True
        '
        'BtnAdd
        '
        Me.BtnAdd.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAdd.Location = New System.Drawing.Point(444, 48)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(100, 49)
        Me.BtnAdd.TabIndex = 3
        Me.BtnAdd.Text = "Add selected song"
        Me.ToolTip1.SetToolTip(Me.BtnAdd, "Add the song currently selected in the table below to the list above")
        Me.BtnAdd.UseVisualStyleBackColor = True
        '
        'BtnAdvanced
        '
        Me.BtnAdvanced.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAdvanced.Location = New System.Drawing.Point(12, 601)
        Me.BtnAdvanced.Name = "BtnAdvanced"
        Me.BtnAdvanced.Size = New System.Drawing.Size(100, 30)
        Me.BtnAdvanced.TabIndex = 12
        Me.BtnAdvanced.Text = "Advanced"
        Me.ToolTip1.SetToolTip(Me.BtnAdvanced, "Open the advanced features screen")
        Me.BtnAdvanced.UseVisualStyleBackColor = True
        '
        'BtnHelp
        '
        Me.BtnHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnHelp.Location = New System.Drawing.Point(394, 601)
        Me.BtnHelp.Name = "BtnHelp"
        Me.BtnHelp.Size = New System.Drawing.Size(100, 30)
        Me.BtnHelp.TabIndex = 14
        Me.BtnHelp.Text = "Help"
        Me.ToolTip1.SetToolTip(Me.BtnHelp, "Show a Screen with instructions on how to use ProjHelp")
        Me.BtnHelp.UseVisualStyleBackColor = True
        '
        'BtnSetup
        '
        Me.BtnSetup.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSetup.Location = New System.Drawing.Point(776, 601)
        Me.BtnSetup.Name = "BtnSetup"
        Me.BtnSetup.Size = New System.Drawing.Size(100, 30)
        Me.BtnSetup.TabIndex = 9
        Me.BtnSetup.Text = "Set Up"
        Me.ToolTip1.SetToolTip(Me.BtnSetup, "Set up Projection Helper after installation")
        Me.BtnSetup.UseVisualStyleBackColor = True
        '
        'BtnLoadList
        '
        Me.BtnLoadList.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnLoadList.Location = New System.Drawing.Point(743, 212)
        Me.BtnLoadList.Name = "BtnLoadList"
        Me.BtnLoadList.Size = New System.Drawing.Size(130, 37)
        Me.BtnLoadList.TabIndex = 16
        Me.BtnLoadList.Text = "Manage Playlists"
        Me.ToolTip1.SetToolTip(Me.BtnLoadList, "Save the current Play List to a named record or Load a Play List that was previou" &
        "sly saved.")
        Me.BtnLoadList.UseVisualStyleBackColor = True
        '
        'ChkShortList
        '
        Me.ChkShortList.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkShortList.Location = New System.Drawing.Point(595, 20)
        Me.ChkShortList.Name = "ChkShortList"
        Me.ChkShortList.Size = New System.Drawing.Size(272, 24)
        Me.ChkShortList.TabIndex = 21
        Me.ChkShortList.Text = "Show only songs from your Short List"
        Me.ChkShortList.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.ChkShortList, "When this is ticked, only songs denoted as ""Selected"" will appear in the selectio" &
        "n table below.")
        Me.ChkShortList.UseVisualStyleBackColor = True
        '
        'T_filesDataGridView
        '
        Me.T_filesDataGridView.AllowUserToAddRows = False
        Me.T_filesDataGridView.AllowUserToDeleteRows = False
        Me.T_filesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.T_filesDataGridView.Location = New System.Drawing.Point(14, 406)
        Me.T_filesDataGridView.MultiSelect = False
        Me.T_filesDataGridView.Name = "T_filesDataGridView"
        Me.T_filesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.T_filesDataGridView.Size = New System.Drawing.Size(862, 182)
        Me.T_filesDataGridView.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.T_filesDataGridView, "The PPT Name is the actual file name of the song on the hard disk." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "The Other Nam" &
        "e is an alternative title of the song or other search data.")
        '
        'ChkPause
        '
        Me.ChkPause.AutoSize = True
        Me.ChkPause.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkPause.Location = New System.Drawing.Point(674, 182)
        Me.ChkPause.Name = "ChkPause"
        Me.ChkPause.Size = New System.Drawing.Size(201, 22)
        Me.ChkPause.TabIndex = 27
        Me.ChkPause.Text = "Pause at the end of videos"
        Me.ToolTip1.SetToolTip(Me.ChkPause, resources.GetString("ChkPause.ToolTip"))
        Me.ChkPause.UseVisualStyleBackColor = True
        '
        'LBInstant
        '
        Me.LBInstant.FormattingEnabled = True
        Me.LBInstant.Location = New System.Drawing.Point(12, 5)
        Me.LBInstant.Name = "LBInstant"
        Me.LBInstant.Size = New System.Drawing.Size(120, 17)
        Me.LBInstant.TabIndex = 10
        Me.LBInstant.TabStop = False
        Me.LBInstant.Visible = False
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(15, 132)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(539, 149)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = resources.GetString("Label3.Text")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label4.Location = New System.Drawing.Point(129, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(581, 20)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Press a CTRL key to immediately display the currently selected song in the table " &
    "below."
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(297, 2)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(253, 20)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "Hover over any control for a Tool Tip"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtListNo
        '
        Me.TxtListNo.BackColor = System.Drawing.SystemColors.Menu
        Me.TxtListNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtListNo.Location = New System.Drawing.Point(160, 10)
        Me.TxtListNo.Name = "TxtListNo"
        Me.TxtListNo.Size = New System.Drawing.Size(54, 13)
        Me.TxtListNo.TabIndex = 23
        Me.TxtListNo.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(10, 70)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(195, 18)
        Me.Label6.TabIndex = 24
        Me.Label6.Text = "Enter search terms here:"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Controls.Add(Me.TxtSearch)
        Me.Panel1.Controls.Add(Me.BtnAdd)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.ChkShortList)
        Me.Panel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(6, 284)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(879, 311)
        Me.Panel1.TabIndex = 26
        '
        'TextBox1
        '
        Me.TextBox1.Enabled = False
        Me.TextBox1.Location = New System.Drawing.Point(635, 50)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 24)
        Me.TextBox1.TabIndex = 25
        Me.TextBox1.Visible = False
        '
        'LblVersion
        '
        Me.LblVersion.BackColor = System.Drawing.SystemColors.Control
        Me.LblVersion.Location = New System.Drawing.Point(741, 5)
        Me.LblVersion.Name = "LblVersion"
        Me.LblVersion.Size = New System.Drawing.Size(135, 13)
        Me.LblVersion.TabIndex = 11
        Me.LblVersion.Text = "Version"
        Me.LblVersion.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'F_Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(892, 640)
        Me.Controls.Add(Me.ChkPause)
        Me.Controls.Add(Me.TxtListNo)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.BtnLoadList)
        Me.Controls.Add(Me.BtnHelp)
        Me.Controls.Add(Me.BtnAdvanced)
        Me.Controls.Add(Me.LblVersion)
        Me.Controls.Add(Me.LBInstant)
        Me.Controls.Add(Me.BtnSetup)
        Me.Controls.Add(Me.BtnClear)
        Me.Controls.Add(Me.BtnPlay)
        Me.Controls.Add(Me.T_filesDataGridView)
        Me.Controls.Add(Me.LBPlayList)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label3)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_Main"
        Me.Text = "Projection Helper"
        CType(Me.T_filesDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LBPlayList As ListBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents TxtSearch As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents BtnPlay As Button
    Friend WithEvents BtnClear As Button
    Friend WithEvents BtnAdd As Button
    Friend WithEvents BtnSetup As Button
    Friend WithEvents LBInstant As ListBox
    Friend WithEvents BtnAdvanced As Button
    Friend WithEvents BtnHelp As Button
    Friend WithEvents BtnLoadList As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents ChkShortList As System.Windows.Forms.CheckBox
    Friend WithEvents FilenoDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents FnameDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents FpathDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents FaltnameDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents InactiveDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents SsearchDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents Label5 As Label
    Friend WithEvents TxtListNo As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents T_filesDataGridView As DataGridView
    Friend WithEvents LblVersion As Label
    Friend WithEvents ChkPause As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox1 As TextBox
End Class
