
Imports System.Data.SQLite
Imports System.Text.RegularExpressions

Public Class PHServer
    ReadOnly SqlCreate As String =
"PRAGMA foreign_keys=ON;
BEGIN TRANSACTION;
CREATE TABLE [t_playlists] (
  [list_no] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [play_dt] datetime NULL
, [l_name] nvarchar(30) NULL COLLATE NOCASE
);
CREATE TABLE [t_files] (
  [file_no] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [f_name] nvarchar(80) NOT NULL COLLATE NOCASE
, [f_path] nvarchar(250) NOT NULL COLLATE NOCASE
, [f_altname] nvarchar(250) NULL COLLATE NOCASE
, [isShortList] nchar(1) NULL COLLATE NOCASE
, [create_dt] datetime NOT NULL
, [last_dt] datetime NULL
, [last_action] nvarchar(30) NULL COLLATE NOCASE
, [isActive] nchar(1) NOT NULL COLLATE NOCASE
, [s_search] nvarchar(400) NULL COLLATE NOCASE
);
insert into t_files
  (file_no, f_name, f_path, create_dt, last_action, isActive)
  values (-1, 'Ad hoc file', 'Ad hoc file', '1970-01-01', 'ADHOC', 'Y')
;
CREATE TABLE [tx_playlist_song] (
  [rec_no] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [list_no] int NOT NULL
, [seq_no] int NOT NULL
, [file_no] int NOT NULL
, [full_path] nvarchar(400) NULL COLLATE NOCASE
, CONSTRAINT [t_file_tx_playlist_song] FOREIGN KEY ([file_no]) REFERENCES [t_files] ([file_no]) ON DELETE NO ACTION ON UPDATE NO ACTION
, CONSTRAINT [t_playlist_tx_playlist_song] FOREIGN KEY ([list_no]) REFERENCES [t_playlists] ([list_no]) ON DELETE NO ACTION ON UPDATE NO ACTION
);
CREATE TRIGGER [fki_tx_playlist_song_file_no_t_files_file_no] BEFORE Insert ON [tx_playlist_song]
 FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table tx_playlist_song violates foreign key constraint t_file_tx_playlist_song')
 WHERE (SELECT file_no FROM t_files WHERE  file_no = NEW.file_no) IS NULL; END;
CREATE TRIGGER [fku_tx_playlist_song_file_no_t_files_file_no] BEFORE Update ON [tx_playlist_song]
 FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table tx_playlist_song violates foreign key constraint t_file_tx_playlist_song')
 WHERE (SELECT file_no FROM t_files WHERE  file_no = NEW.file_no) IS NULL; END;
CREATE TRIGGER [fki_tx_playlist_song_list_no_t_playlists_list_no] BEFORE Insert ON [tx_playlist_song]
 FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table tx_playlist_song violates foreign key constraint t_playlist_tx_playlist_song')
 WHERE (SELECT list_no FROM t_playlists WHERE  list_no = NEW.list_no) IS NULL; END;
CREATE TRIGGER [fku_tx_playlist_song_list_no_t_playlists_list_no] BEFORE Update ON [tx_playlist_song]
 FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table tx_playlist_song violates foreign key constraint t_playlist_tx_playlist_song')
 WHERE (SELECT list_no FROM t_playlists WHERE  list_no = NEW.list_no) IS NULL; END;
COMMIT;
"

    ReadOnly location As String = Application.StartupPath
    ReadOnly fileName As String = "ProjHelpData.db"
    ReadOnly fullPath As String = System.IO.Path.Combine(location, fileName)
    Public connectionString As String = String.Format("Data Source = {0}", fullPath)
    Private sqlConn As New SQLiteConnection

    Public Function ConnectDatabase() As SQLiteConnection
        sqlConn.ConnectionString = connectionString

        If Not System.IO.File.Exists(fullPath) Then
            ' Create the database file if it does not exist
            Dim command As New SQLiteCommand(SqlCreate, sqlConn)
            sqlConn.Open()
            command.ExecuteNonQuery()
        Else
            sqlConn.Open()
        End If

        Return sqlConn

    End Function

    Public Function GetConnection() As SQLiteConnection
        If sqlConn Is Nothing Then
            sqlConn = New SQLiteConnection(connectionString)
        End If
        If sqlConn.State <> ConnectionState.Open Then
            sqlConn.Open()
        End If
        Return sqlConn
    End Function


End Class

Public Class FilesTable
    Inherits DataTable

    Private ReadOnly myConn As SQLiteConnection
    Private ReadOnly myMsgBox As New DlgMsgBox

    Public Sub New(isShort As String, conn As SQLiteConnection)
        myConn = conn
        LoadActive(isShort)
    End Sub

    Public Sub New(conn As SQLiteConnection, Optional InclInActive As Boolean = True)
        myConn = conn
        LoadAll(InclInActive)
    End Sub

    Public Sub LoadByPhrase(isShort As Boolean, phrase As String)
        Dim szSearch As String = IIf(isShort,
                String.Format("SELECT * FROM t_files WHERE f_name like '%{0}%' AND isShortList = '{1}' AND File_no > 0 AND isActive = 'Y'", phrase, isShort),
                String.Format("SELECT * FROM t_files WHERE f_name like '%{0}%' AND File_no > 0 AND isActive = 'Y'", phrase)
            )

        LoadTable(szSearch)
    End Sub


    Public Sub LoadActive(isShort As Boolean)
        Dim szSearch As String = IIf(isShort,
                String.Format("SELECT * FROM t_files WHERE isShortList = '{0}' AND File_no > 0 AND isActive = 'Y'", isShort),
                String.Format("SELECT * FROM t_files WHERE File_no > 0 AND isActive = 'Y'")
            )
        LoadTable(szSearch)
    End Sub


    Public Sub LoadAll(Optional InclInActive As Boolean = False)
        Dim szSearch As String = IIf(InclInActive,
                String.Format("SELECT * FROM t_files WHERE File_no > 0"),
                String.Format("SELECT * FROM t_files WHERE File_no > 0 AND isActive = 'Y'")
            )
        LoadTable(szSearch)
    End Sub


    Private Sub LoadTable(szSrch As String)
        Dim command As New SQLiteCommand With {
            .Connection = myConn,
            .CommandText = szSrch
        }
        Dim rdr As SQLiteDataReader = command.ExecuteReader()
        MyBase.Clear()
        MyBase.Load(rdr)
    End Sub

    Public Function Count() As Integer
        Return MyBase.Rows.Count
    End Function

    Public Sub Insert(fileName As String, filePath As String, isShort As Boolean, Optional tag As String = "INSERT")
        If myConn.State <> ConnectionState.Open Then
            myConn.Open()
        End If

        Dim insertQuery As String = "INSERT into [t_files] " &
            "([f_name], [f_path], [isShortList], [create_dt], [last_dt], [last_action], [isActive], [s_search]) " &
            "VALUES (@fname, @fpath, @isshort, @create_dt, @create_dt, @tag, 'Y', @search)"
        Dim szSearch As String = fileName.ToLower.Replace(".pptx", "")
        szSearch = szSearch.Replace(".ppt", "")

        Dim cmd As New SQLiteCommand(insertQuery, myConn)
        cmd.Parameters.AddWithValue("@fname", fileName)
        cmd.Parameters.AddWithValue("@fpath", filePath)
        cmd.Parameters.AddWithValue("@isshort", IIf(isShort, "Y", "N"))
        cmd.Parameters.AddWithValue("@tag", tag)
        cmd.Parameters.AddWithValue("@create_dt", Now)
        cmd.Parameters.AddWithValue("@search", szSearch)
        Dim showit As String = cmd.CommandText
        cmd.ExecuteNonQuery()

    End Sub

    Public Sub Delete(fileNo As Integer)
        If myConn.State <> ConnectionState.Open Then
            myConn.Open()
        End If

        Dim deleteQuery As String = "DELETE FROM t_files WHERE file_no = @file_no"
        Dim cmd As New SQLiteCommand(deleteQuery, myConn)
        cmd.Parameters.AddWithValue("@file_no", fileNo)
        Dim result As Integer = cmd.ExecuteNonQuery()
        If result = 0 Then
            Throw New Exception(String.Format("Record not deleted. Check the file_no parameter: {0}", fileNo))
        End If
    End Sub


    Public Sub Update(source As DataView)
        For Each row As DataRowView In source
            Select Case row.Row.RowState
                Case DataRowState.Unchanged
                    ' No action needed, row is unchanged
                Case DataRowState.Added
                    Throw New Exception("This is a new row, use Insert method instead.")
                Case DataRowState.Deleted
                    ' delete the row
                    Delete(row("file_no"))
                Case DataRowState.Modified
                    Dim oldRow As DataRow = MyBase.Rows.Find(row("file_no"))
                    If oldRow Is Nothing Then
                        Throw New Exception("Record " & row("file_no") & " Not found For update.")
                    End If
                    UpdateRow(oldRow, row)
                Case Else
                    Throw New Exception("Unknown row state: " & row.Row.RowState.ToString())
            End Select
        Next
    End Sub

    Public Sub Update(newRow As DataRowView)
        Dim oldRow As DataRow = MyBase.Rows.Find(newRow("file_no"))
        If oldRow Is Nothing Then
            Throw New Exception("Record " & newRow("file_no") & " Not found For update.")
        End If

        UpdateRow(oldRow, newRow)
    End Sub

    Private Sub UpdateRow(oldRow As DataRow, newRow As DataRowView)
        If myConn.State <> ConnectionState.Open Then
            myConn.Open()
        End If

        ' check that the record matches (file name and path cannot be altered)
        If oldRow("f_name") <> newRow("f_name") Or oldRow("f_path") <> newRow("f_path") Then
            myMsgBox.Show("Illegal change to record #" & oldRow("file_no") & vbCrLf &
                   "This feature is not designed to change the file name or path. " &
                   "Please use the Update File List feature.",
                   "Saving Table Changes",
                 MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' Update the old row with the new values
        Dim szSearch As String
        If IsDBNull(newRow("s_search")) Then
            szSearch = newRow("f_name").ToString().ToLower().Replace(".pptx", "").Replace(".ppt", "") &
                        "; " & newRow("f_altname").ToString().ToLower()
            szSearch = StripPunc(szSearch)
        Else
            szSearch = newRow("s_search")
        End If

        Dim updateQuery As String = "UPDATE t_files Set " &
            "f_altname = @f_altname, isShortList = @isshort, isActive = @isActive, " &
            "last_dt = @last_dt, last_action = 'EDIT', s_search = @s_search " &
            "WHERE file_no = @file_no"
        Dim cmd As New SQLiteCommand(updateQuery, myConn)
        cmd.Parameters.AddWithValue("@f_altname", newRow("f_altname"))
        cmd.Parameters.AddWithValue("@isshort", newRow("isShortList"))
        cmd.Parameters.AddWithValue("@isActive", newRow("isActive"))
        cmd.Parameters.AddWithValue("@last_dt", Now())
        cmd.Parameters.AddWithValue("@s_search", szSearch)
        cmd.Parameters.AddWithValue("@file_no", newRow("file_no"))
        Dim result As Integer = cmd.ExecuteNonQuery()

        If result = 0 Then
            Throw New Exception(String.Format("Record Not updated. Check the file_no parameter: {0}", oldRow("file_no")))
        End If
    End Sub

    Public Function StripPunc(szString As String) As String
        Dim szTemp As String = ""
        Dim szChar As String
        Dim i As Integer = 0
        Dim regEx As New Regex("[\w\s]", RegexOptions.IgnoreCase)

        While i < szString.Length
            szChar = szString.Substring(i, 1)
            If regEx.IsMatch(szChar) Then
                szTemp += szChar
            End If
            i += 1
        End While

        StripPunc = szTemp
    End Function


    Public Sub SetDGProperties(dg As DataGridView, Optional NoEdit As Boolean = True)
        dg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dg.AutoGenerateColumns = True
        dg.AllowUserToAddRows = False
        dg.AllowUserToDeleteRows = False
        dg.AllowUserToOrderColumns = True
        dg.ReadOnly = NoEdit
        dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dg.MultiSelect = False
        dg.RowHeadersVisible = False
        dg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        'If NoEdit Then
        dg.Columns("file_no").Visible = Not NoEdit
        'End If
        dg.Columns("file_no").Width = 50
        dg.Columns("file_no").HeaderText = "#"
        dg.Columns("f_name").Width = 200
        dg.Columns("f_name").HeaderText = "PPT Name"
        dg.Columns("f_path").Width = 200
        dg.Columns("f_path").HeaderText = "Folder"
        dg.Columns("f_altname").Width = 200
        dg.Columns("f_altname").HeaderText = "Other Name"
        dg.Columns("isShortList").Width = 35
        dg.Columns("isShortList").HeaderText = "ShortL"
        dg.Columns("create_dt").Visible = False
        dg.Columns("last_dt").Visible = False
        dg.Columns("last_action").Visible = False
        dg.Columns("isActive").Width = 35
        dg.Columns("isActive").HeaderText = "Active"
        dg.Columns("s_search").Width = 200
        dg.Columns("s_search").HeaderText = "Search String"
        dg.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10.5, FontStyle.Regular)
    End Sub

End Class

Public Class PlaylistsTable
    Inherits DataTable
    Private ReadOnly myConn As SQLiteConnection
    Private ReadOnly myMsgBox As New DlgMsgBox

    Public Sub New(conn As SQLiteConnection, Optional bFuture As Boolean = False)
        myConn = conn
        If bFuture Then
            LoadFuture(DateTime.Now)
        Else
            LoadAll()
        End If
    End Sub

    Public Sub LoadAll()
        Dim command As New SQLiteCommand("SELECT * FROM t_playlists", myConn)
        Dim rdr As SQLiteDataReader = command.ExecuteReader()
        MyBase.Clear()
        MyBase.Load(rdr)
    End Sub

    Public Function Count() As Integer
        Return MyBase.Rows.Count
    End Function

    Public Sub LoadFuture(dt As DateTime)
        Dim command As New SQLiteCommand("SELECT * FROM t_playlists WHERE play_dt >= @play_dt", myConn)
        command.Parameters.AddWithValue("@play_dt", dt)
        Dim rdr As SQLiteDataReader = command.ExecuteReader()
        MyBase.Clear()
        MyBase.Load(rdr)
    End Sub

    Public Sub Update(source As DataView)
        For Each row As DataRowView In source
            UpdateRow(row)
        Next
    End Sub


    Private Sub UpdateRow(newRow As DataRowView)
        Select Case newRow.Row.RowState
            Case DataRowState.Unchanged
                    ' No action needed, row is unchanged
            Case DataRowState.Added
                InsertRow(newRow)
            Case DataRowState.Deleted
                    ' delete row is not available in this DGV
            Case DataRowState.Modified
                Dim oldRow As DataRow = MyBase.Rows.Find(newRow("file_no"))
                If oldRow Is Nothing Then
                    Throw New Exception("Record " & newRow("file_no") & " Not found For update.")
                End If
                UpdateRow(oldRow, newRow)
            Case Else
                Throw New Exception("Unknown row state: " & newRow.Row.RowState.ToString())
        End Select
    End Sub


    Private Sub UpdateRow(oldRow As DataRow, newRow As DataRowView)
        If myConn.State <> ConnectionState.Open Then
            myConn.Open()
        End If

        ' Update the table with the new values
        Dim ListNo As Integer = oldRow("list_no")
        UpdateRow(ListNo, newRow("play_dt"), newRow("l_name"))
    End Sub


    Private Sub UpdateRow(list_no As Integer, dtPlay As DateTime, szFname As String)
        If list_no = 0 Then
            InsertRow(dtPlay, szFname)
        Else
            Dim updateQuery As String = "UPDATE t_playlists Set " &
            "play_dt = @playdt, l_name = @lname " &
            "WHERE list_no = @list_no"
            Dim cmd As New SQLiteCommand(updateQuery, myConn)
            cmd.Parameters.AddWithValue("@playdt", dtPlay)
            cmd.Parameters.AddWithValue("@lname", szFname)
            cmd.Parameters.AddWithValue("@list_no", list_no)
            Dim result As Integer = cmd.ExecuteNonQuery()

            If result = 0 Then
                Throw New Exception(String.Format("Record Not updated. Check the list_no parameter: {0}", list_no))
            End If

        End If
    End Sub

    ' Insert new values into the table
    Private Sub InsertRow(newRow As DataRowView)
        If myConn.State <> ConnectionState.Open Then
            myConn.Open()
        End If

        Dim ListNo As Integer = newRow("list_no")
        If ListNo <> 0 Then
            myMsgBox.Show("ListNo should be 0 for new entries." & vbCrLf &
                    "Please report this message to the programmer: ListNo = " & ListNo,
                    "Saving Table Changes",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        Dim updateQuery As String = "INSERT t_playlists " &
            "(play_dt, l_name) VAULES (@playdt, @lname)"
        Dim cmd As New SQLiteCommand(updateQuery, myConn)
        cmd.Parameters.AddWithValue("@playdt", newRow("play_dt"))
        cmd.Parameters.AddWithValue("@lname", newRow("l_name"))
        Dim result As Integer = cmd.ExecuteNonQuery()

        If result = 0 Then
            Throw New Exception(String.Format("Record Not updated. Check the list_no parameter: {0}", ListNo))
        End If
    End Sub

    Private Function InsertRow(playDt As DateTime, lName As String) As Integer
        Dim insertQuery As String = "INSERT INTO t_playlists (play_dt, l_name) " &
            "VALUES (@play_dt, @l_name)"
        Dim cmd As New SQLiteCommand(insertQuery, myConn)
        cmd.Parameters.AddWithValue("@play_dt", playDt)
        cmd.Parameters.AddWithValue("@l_name", lName)
        cmd.ExecuteNonQuery()
        cmd.Dispose()
        cmd = New SQLiteCommand("SELECT last_insert_rowid()", myConn)
        Dim lastId As Long = Convert.ToInt64(cmd.ExecuteScalar())
        cmd.Dispose()
        If lastId <= 0 Then
            Throw New Exception("Error inserting new playlist. Last inserted ID is not valid.")
        Else
            cmd = New SQLiteCommand("SELECT list_no FROM t_playlists WHERE list_no = @last_id", myConn)
            cmd.Parameters.AddWithValue("@last_id", lastId)
            Dim listNo As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            Return listNo
        End If
        Return -1
    End Function


    Public Sub SetDGProperties(dg As DataGridView, Optional NoEdit As Boolean = False)
        dg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dg.AutoGenerateColumns = True
        dg.AllowUserToAddRows = True
        dg.AllowUserToDeleteRows = True
        dg.AllowUserToOrderColumns = True
        dg.ReadOnly = NoEdit
        dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dg.MultiSelect = False
        dg.RowHeadersVisible = False
        dg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dg.Columns("list_no").Width = 30
        dg.Columns("list_no").HeaderText = "List No"
        dg.Columns("play_dt").Width = 150
        dg.Columns("play_dt").HeaderText = "Show Time"
        dg.Columns("l_name").Width = 250
        dg.Columns("l_name").HeaderText = "Playlist Name"
        dg.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10.5, FontStyle.Regular)
    End Sub

    Friend Function UpdateInsert(Row As DataGridViewRow) As Integer
        Dim cmd As SQLiteCommand
        Dim isInsert As Boolean = False
        Dim ListNo = Row.Cells(0).Value
        Dim result As Integer
        If ListNo = 0 Then
            isInsert = True
        Else
            cmd = New SQLiteCommand("SELECT list_no FROM t_playlists WHERE list_no = @list_no", myConn)
            cmd.Parameters.AddWithValue("@list_no", ListNo)
            result = Convert.ToInt32(cmd.ExecuteScalar())
            If result <> ListNo Then isInsert = True
        End If

        If isInsert Then
            ListNo = InsertRow(Row.Cells(1).Value, Row.Cells(2).Value)
        Else
            UpdateRow(ListNo, Row.Cells(1).Value, Row.Cells(2).Value)
        End If

        Return ListNo
    End Function
End Class

Public Class Tx_playlistTable
    Inherits DataTable
    Private ReadOnly myConn As SQLiteConnection
    Public Sub New(conn As SQLiteConnection)
        myConn = conn
        LoadAll()
    End Sub
    Public Sub LoadAll()
        Dim command As New SQLiteCommand("SELECT * FROM tx_playlist_song", myConn)
        Dim rdr As SQLiteDataReader = command.ExecuteReader()
        MyBase.Clear()
        MyBase.Load(rdr)
    End Sub
    Public Function Count(Optional ListNo As Integer = -1) As Integer
        If ListNo <> -1 Then
            Dim command As New SQLiteCommand("SELECT COUNT(*) FROM tx_playlist_song WHERE list_no = @list_no", myConn)
            command.Parameters.AddWithValue("@list_no", ListNo)
            Return Convert.ToInt32(command.ExecuteScalar())
        End If

        Return MyBase.Rows.Count
    End Function

    Public Sub Insert(listNo As Integer, seqNo As Integer, fileNo As Integer, fPath As String)
        Dim insertQuery As String = "INSERT INTO tx_playlist_song (list_no, seq_no, file_no, full_path) " &
            "VALUES (@list_no, @seq_no, @file_no, @fullpath)"

        Dim cmd As New SQLiteCommand(insertQuery, myConn)
        cmd.Parameters.AddWithValue("@list_no", listNo)
        cmd.Parameters.AddWithValue("@seq_no", seqNo)
        cmd.Parameters.AddWithValue("@file_no", fileNo)
        cmd.Parameters.AddWithValue("@fullpath", fPath)
        cmd.ExecuteNonQuery()
    End Sub

    Public Sub Delete(recNo As Integer, listNo As Integer, seqNo As Integer, fileNo As Integer)
        Dim deleteQuery As String = "DELETE FROM tx_playlist_song where " &
        "rec_no = @rec_no AND list_no = @list_no AND seq_no = @seq_no AND file_no = @file_no"

        Dim cmd As New SQLiteCommand(deleteQuery, myConn)
        cmd.Parameters.AddWithValue("@rec_no", recNo)
        cmd.Parameters.AddWithValue("@list_no", listNo)
        cmd.Parameters.AddWithValue("@seq_no", seqNo)
        cmd.Parameters.AddWithValue("@file_no", fileNo)
        Dim result As Integer = cmd.ExecuteNonQuery()

        If result = 0 Then
            Throw New Exception(String.Format("Record not deleted. Check the parameters. " &
                       "recNo = {0}; listNo = {1}; seq_no = {2}; fileNo = {3}",
                        recNo, listNo, seqNo, fileNo)
                    )
        End If

    End Sub

    Public Sub UpdateFileNo(DupFileNo As Integer, FileNo As Integer)
        Dim updateQuery As String = "UPDATE tx_playlist_song SET file_no = @file_no WHERE file_no = @dup_file_no"
        Dim cmd As New SQLiteCommand(updateQuery, myConn)
        cmd.Parameters.AddWithValue("@file_no", FileNo)
        cmd.Parameters.AddWithValue("@dup_file_no", DupFileNo)
        Dim result As Integer = cmd.ExecuteNonQuery()
        If result = 0 Then
            Throw New Exception(String.Format("Record not updated. Check the parameters. " &
                       "DupFileNo = {0}; FileNo = {1}", DupFileNo, FileNo))
        End If

    End Sub
End Class