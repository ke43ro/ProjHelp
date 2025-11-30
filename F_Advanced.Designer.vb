<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F_Advanced
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
        Me.BtnListIO = New System.Windows.Forms.Button()
        Me.BtnEdit = New System.Windows.Forms.Button()
        Me.BtnUpdate = New System.Windows.Forms.Button()
        Me.BtnCompare = New System.Windows.Forms.Button()
        Me.ChkAutoSelectList = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ChkDebug = New System.Windows.Forms.CheckBox()
        Me.BtnPlayExp = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtnListIO
        '
        Me.BtnListIO.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnListIO.Location = New System.Drawing.Point(99, 19)
        Me.BtnListIO.Name = "BtnListIO"
        Me.BtnListIO.Size = New System.Drawing.Size(191, 33)
        Me.BtnListIO.TabIndex = 0
        Me.BtnListIO.Text = "File List Import/Export"
        Me.ToolTip1.SetToolTip(Me.BtnListIO, "Exports a file that can be read on another installation to transfer your short li" &
        "st" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "and alternative text or vice versa.")
        Me.BtnListIO.UseVisualStyleBackColor = True
        '
        'BtnEdit
        '
        Me.BtnEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnEdit.Location = New System.Drawing.Point(99, 70)
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(191, 33)
        Me.BtnEdit.TabIndex = 2
        Me.BtnEdit.Text = "Edit File List"
        Me.ToolTip1.SetToolTip(Me.BtnEdit, "Provides a way to make manual changes to your file list database.")
        Me.BtnEdit.UseVisualStyleBackColor = True
        '
        'BtnUpdate
        '
        Me.BtnUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUpdate.Location = New System.Drawing.Point(99, 121)
        Me.BtnUpdate.Name = "BtnUpdate"
        Me.BtnUpdate.Size = New System.Drawing.Size(191, 33)
        Me.BtnUpdate.TabIndex = 3
        Me.BtnUpdate.Text = "Update File List"
        Me.ToolTip1.SetToolTip(Me.BtnUpdate, "Updates your database to match the files you have on your disk.")
        Me.BtnUpdate.UseVisualStyleBackColor = True
        '
        'BtnCompare
        '
        Me.BtnCompare.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCompare.Location = New System.Drawing.Point(99, 172)
        Me.BtnCompare.Name = "BtnCompare"
        Me.BtnCompare.Size = New System.Drawing.Size(191, 33)
        Me.BtnCompare.TabIndex = 4
        Me.BtnCompare.Text = "Compare With Master"
        Me.ToolTip1.SetToolTip(Me.BtnCompare, "Compares your database with a file created from the Master Cloud Storage collecti" &
        "on." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "For best results, use Update File List first so that your database is accur" &
        "ate.")
        Me.BtnCompare.UseVisualStyleBackColor = True
        '
        'ChkAutoSelectList
        '
        Me.ChkAutoSelectList.AutoSize = True
        Me.ChkAutoSelectList.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAutoSelectList.Location = New System.Drawing.Point(13, 266)
        Me.ChkAutoSelectList.Name = "ChkAutoSelectList"
        Me.ChkAutoSelectList.Size = New System.Drawing.Size(333, 22)
        Me.ChkAutoSelectList.TabIndex = 5
        Me.ChkAutoSelectList.Text = "Automatically add all files I use to the Short List"
        Me.ToolTip1.SetToolTip(Me.ChkAutoSelectList, "Files that you add to a Play List or show using Instant Play will be permanently" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "added to the Short List")
        Me.ChkAutoSelectList.UseVisualStyleBackColor = True
        '
        'ToolTip1
        '
        Me.ToolTip1.OwnerDraw = True
        '
        'ChkDebug
        '
        Me.ChkDebug.AutoSize = True
        Me.ChkDebug.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkDebug.Location = New System.Drawing.Point(13, 290)
        Me.ChkDebug.Name = "ChkDebug"
        Me.ChkDebug.Size = New System.Drawing.Size(169, 22)
        Me.ChkDebug.TabIndex = 6
        Me.ChkDebug.Text = "Enable debug popups"
        Me.ToolTip1.SetToolTip(Me.ChkDebug, "Provides popup dialogs to show information at critical steps (very annoying in op" &
        "eration)")
        Me.ChkDebug.UseVisualStyleBackColor = True
        '
        'BtnPlayExp
        '
        Me.BtnPlayExp.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPlayExp.Location = New System.Drawing.Point(99, 223)
        Me.BtnPlayExp.Name = "BtnPlayExp"
        Me.BtnPlayExp.Size = New System.Drawing.Size(191, 33)
        Me.BtnPlayExp.TabIndex = 7
        Me.BtnPlayExp.Text = "Play Lists Export"
        Me.ToolTip1.SetToolTip(Me.BtnPlayExp, "Exports a file that can be read on another installation to transfer your short li" &
        "st" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "and alternative text or vice versa.")
        Me.BtnPlayExp.UseVisualStyleBackColor = True
        '
        'F_Advanced
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(388, 319)
        Me.Controls.Add(Me.BtnPlayExp)
        Me.Controls.Add(Me.ChkDebug)
        Me.Controls.Add(Me.ChkAutoSelectList)
        Me.Controls.Add(Me.BtnCompare)
        Me.Controls.Add(Me.BtnUpdate)
        Me.Controls.Add(Me.BtnEdit)
        Me.Controls.Add(Me.BtnListIO)
        Me.Name = "F_Advanced"
        Me.Text = "Advanced Settings"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnListIO As Button
    Friend WithEvents BtnEdit As Button
    Friend WithEvents BtnUpdate As Button
    Friend WithEvents BtnCompare As Button
    Friend WithEvents ChkAutoSelectList As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents ChkDebug As CheckBox
    Friend WithEvents BtnPlayExp As Button
End Class
