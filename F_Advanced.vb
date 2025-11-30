Imports System.Data.SQLite

Public Class F_Advanced
    Private isLoading As Boolean
    Private ReadOnly myMsgBox As New DlgMsgBox

    Private Sub F_Advanced_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        isLoading = True
        ChkAutoSelectList.Checked = My.Settings.AutoShortList
        ChkDebug.Checked = My.Settings.Debug
        isLoading = False
    End Sub

    Private Sub BtnListIO_Click(sender As Object, e As EventArgs) Handles BtnListIO.Click
        F_List_IO.ShowDialog()
    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        F_EditFileList.ShowDialog()
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        F_UpdateFileList.ShowDialog()
    End Sub

    Private Sub BtnCompare_Click(sender As Object, e As EventArgs) Handles BtnCompare.Click
        F_CompareWithMaster.ShowDialog()
    End Sub

    Private Sub ChkAutoSelectList_CheckedChanged(sender As Object, e As EventArgs) Handles ChkAutoSelectList.CheckedChanged
        If isLoading Then Exit Sub
        F_Main.isAutoShort = ChkAutoSelectList.Checked
        My.Settings.AutoShortList = F_Main.isAutoShort
        My.Settings.Save()
    End Sub

    Private Sub ChkDebug_CheckedChanged(sender As Object, e As EventArgs) Handles ChkDebug.CheckedChanged
        If isLoading Then Exit Sub
        F_Main.IsDebug = ChkDebug.Checked
        My.Settings.Debug = F_Main.IsDebug
        My.Settings.Save()

    End Sub

    Private Sub ToolTip1_Draw(sender As Object, e As DrawToolTipEventArgs) Handles ToolTip1.Draw
        e.Graphics.FillRectangle(Brushes.LightYellow, e.Bounds)
        e.Graphics.DrawRectangle(Pens.Black, New Rectangle(0, 0, e.Bounds.Width - 1, e.Bounds.Height - 1))
        Using f As New Font("Segoe UI", 10, FontStyle.Regular)
            e.Graphics.DrawString(e.ToolTipText, f, Brushes.Black, New PointF(2, 2))
        End Using
    End Sub

    Private Sub ToolTip1_Popup(sender As Object, e As PopupEventArgs) Handles ToolTip1.Popup
        Using f As New Font("Segoe UI", 10, FontStyle.Regular)
            Dim textSize = TextRenderer.MeasureText(ToolTip1.GetToolTip(e.AssociatedControl), f, New Size(600, 0), TextFormatFlags.WordBreak)
            ' Add a little padding
            e.ToolTipSize = New Size(Math.Min(textSize.Width, 650), textSize.Height + 4)
        End Using
    End Sub

    Private Sub BtnPlayExp_Click(sender As Object, e As EventArgs) Handles BtnPlayExp.Click
        Dim filePath As String = ""
        Dim fileName As String
        Dim iRandom As New Random
        Dim connection As SQLiteConnection = F_Main.ProjHelpData.GetConnection()
        If connection.State <> ConnectionState.Open Then
            connection.Open()
        End If

        F_Main.ProjHelpData.EnsurePlaylistFilesView()

        fileName = "ProjHelpPlay" & iRandom.Next & ".txt"

        Try
            filePath = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, fileName)
            My.Computer.FileSystem.WriteAllText(filePath,
                    "PlayDate" & vbTab & "File" & vbTab & "F_altname" & vbTab & "ShortList" & vbTab & "Active" & vbCrLf,
                     False)
        Catch fileException As Exception
            myMsgBox.Show("Failed to create file " & filePath, "Projection Helper Playlists Export",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim sql As String = "SELECT * FROM v_playlist_files ORDER BY play_dt, seq_no"
        Using cmd As New SQLiteCommand(sql, connection)
            'cmd.Parameters.AddWithValue("@list_no", listNo)
            Using rdr = cmd.ExecuteReader()
                While rdr.Read()
                    ' read columns: rdr("seq_no"), rdr("f_name"), rdr("resolved_full_path"), ...
                    My.Computer.FileSystem.WriteAllText(filePath,
                        rdr("play_dt") & vbTab & rdr("resolved_full_path") & vbCrLf,
                        True)
                End While
            End Using
        End Using

        myMsgBox.Show("File creation complete: " & filePath, "Projection Helper Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class