Partial Class F_SetUp
    Private ReadOnly isDebug As Boolean = My.Settings.Debug
    Private ReadOnly myMsgBox As New DlgMsgBox
    Private ReadOnly szPklea As String = "Projection Helper will collect PowerPoint " &
        "files from all single letter subfolders of the MASTERS folder."
    Private ReadOnly szHier As String = "Projection Helper will collect PowerPoint " &
        "files from the master folder and all subfolders recursively."

    Private Sub F_SetUp_OnLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim szFolder As String = My.Settings.MasterFolder
        If szFolder <> "" Then
            TxtFolder.Text = szFolder.Replace("%DOCUMENTS%", My.Computer.FileSystem.SpecialDirectories.MyDocuments)
        End If

        If Dir(TxtFolder.Text, vbDirectory) <> "" Then
            ' folder exists
            BtnTest.Enabled = True
        Else
            BtnTest.Enabled = False
        End If

        CheckBox.Enabled = False
        CheckBox.Checked = False
        BtnBrowse.Enabled = True
        TxtAdvice.Visible = False
    End Sub


    Private Sub BtnBrowse_Click(sender As Object, e As EventArgs) Handles BtnBrowse.Click
        FolderBrowserDialog1.SelectedPath = TxtFolder.Text
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            TxtFolder.Text = FolderBrowserDialog1.SelectedPath
        End If
        Dim szFname As String = Dir(TxtFolder.Text, vbDirectory)
        If szFname <> "" Then
            BtnTest.Enabled = True
            TxtAdvice.Visible = True
            If szFname = "MASTERS" Then
                ' folder is in the PKLEA format
                TxtAdvice.Text = szPklea
            Else
                TxtAdvice.Text = szHier
            End If

            ' done after Test
            'My.Settings.MasterFolder = TxtFolder.Text
            'My.Settings.Save()
        Else
            myMsgBox.Show("This feature can only be run if a folder is specified",
                            "Testing the setup", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

    End Sub


    Private Sub BtnTest_Click(sender As Object, e As EventArgs) Handles BtnTest.Click
        ' fill the database
        Cursor = Cursors.WaitCursor
        Dim FillForm As New F_FillTables
        Dim result As DialogResult
        Dim dirFormat As Boolean = False ' true if root folder is "MASTERS"

        If Dir(TxtFolder.Text, vbDirectory) = "" Then
            Cursor = Cursors.Default
            myMsgBox.Show("Please choose a valid folder name")
            Exit Sub
        End If

        ' check if the folder is in the PKLEA format
        If InStr(TxtFolder.Text, "MASTERS") > 0 Then
            dirFormat = True
        End If
        ' open FillForm and set the folder name
        FillForm.LoadFolder(TxtFolder.Text, dirFormat)
        Try
            result = FillForm.ShowDialog()

        Catch ex As Exception
            myMsgBox.Show("There was a failure while trying to open the lyrics table.  Please contact the application vendor" &
                "And provide the following message:" & vbCrLf & ex.Message,
                "Install failure", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        If result <> DialogResult.OK Then
            Cursor = Cursors.Default
            myMsgBox.Show("Projection Helper will not be fully operational without the tables filled")
            Exit Sub
        End If

        ' if successful
        My.Settings.MasterFolder = TxtFolder.Text
        My.Settings.Save()
        CheckBox.Checked = True
        Cursor = Cursors.Default
    End Sub


    Private Sub F_SetUp_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim result As DialogResult

        If isDebug Then myMsgBox.Show("testing Checkbox")
        If CheckBox.Checked = True Then
            If isDebug Then myMsgBox.Show("closing")
        Else
            result = myMsgBox.Show("The lyrics table has not been filled - Projection Helper will not function fully." &
                    vbCrLf & "Are you sure you want to close the set up?",
                    "Incomplete Set Up", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = DialogResult.No Then
                e.Cancel = True
            End If
        End If
    End Sub

End Class