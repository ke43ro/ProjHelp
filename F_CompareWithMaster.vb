Imports System.Data.SqlClient
Imports System.Data.SQLite

Public Class F_CompareWithMaster
    Private T_filesTable As FilesTable
    Private ReadOnly myMsgBox As New DlgMsgBox

    Private Sub F_CompareWithMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connection As SQLiteConnection = F_Main.ProjHelpData.GetConnection()
        If connection.State <> ConnectionState.Open Then
            connection.Open()
        End If
        T_filesTable = New FilesTable(connection)

        TxtFolder.Text = My.Settings.MasterFolder
    End Sub


    Private Sub BtnBrowse_Click(sender As Object, e As EventArgs) Handles BtnBrowse.Click
        FolderBrowserDialog1.SelectedPath = TxtFolder.Text
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            TxtFolder.Text = FolderBrowserDialog1.SelectedPath
        End If
        If Dir(TxtFolder.Text, vbDirectory) = "" Then
            myMsgBox.Show("I can't find folder " & TxtFolder.Text & ".  Please try again", "Update Files List",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            My.Settings.MasterFolder = TxtFolder.Text
            My.Settings.Save()
        End If

    End Sub


    Private Sub BtnCompareFile_Click(sender As Object, e As EventArgs) Handles BtnCompareFile.Click
        OpenFileDialog1.CheckFileExists = True
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            TxtCompareFile.Text = OpenFileDialog1.FileName
        End If
    End Sub


    Private Sub BtnCompare_Click(sender As Object, e As EventArgs) Handles BtnCompare.Click
        Dim importView, localView As DataView
        Dim szImportFile, szMasterFolder, szPath, szFName As String
        Dim nLocal, nImport As Integer

        If Dir(TxtFolder.Text, vbDirectory) = "" Then
            myMsgBox.Show("I can't find master folder " & TxtFolder.Text & ".  Please try again",
                            "Compare With Master",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else
            If TxtCompareFile.Text.IndexOf(vbDouble) = True Then
                myMsgBox.Show(TxtCompareFile.Text & " is not a valid file path. Please try again",
                            "Compare With Master",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            If Dir(TxtCompareFile.Text, vbNormal) = "" Then
                myMsgBox.Show("I can't find that compare file " & TxtCompareFile.Text & ".  Please try again", "Compare With Master",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If

        szMasterFolder = TxtFolder.Text
        szImportFile = TxtCompareFile.Text

        Dim T_Results = CreateTableRes()
        Dim T_Import = FillTableImp(szImportFile, szMasterFolder)
        Dim T_Local = FillTableLocal()
        Dim iLocalRow, iImportRow, iItems As Integer
        Dim localKey(1) As String

        nImport = T_Import.Rows.Count
        nLocal = T_Local.Rows.Count
        MyMsgBox.Show("Imported:" & nImport & "; Local=" & nLocal)

        importView = T_Import.AsDataView
        localView = T_Local.AsDataView
        localView.Sort = "f_path,f_name"

        For Each myImportRow In importView
            szPath = myImportRow("f_path")
            szFName = myImportRow("f_name")
            localKey(0) = szPath
            localKey(1) = szFName
            iLocalRow = localView.Find(localKey)
            If iLocalRow >= 0 Then
                localView.Item(iLocalRow)("status") = 1
                myImportRow("status") = 1
            End If
        Next
        T_Import.AcceptChanges()
        T_Local.AcceptChanges()

        localView.RowFilter = "status = 0"
        'MyMsgBox.Show("Unmatched local files: " & localView.Count)
        importView.Sort = "f_path,f_name"

        For Each myLocalRow In localView
            szPath = myLocalRow("f_path")
            szFName = myLocalRow("f_name")
            localKey(0) = szPath
            localKey(1) = szFName
            iImportRow = importView.Find(localKey)
            If iImportRow >= 0 Then
                importView.Item(iImportRow)("status") = 1
                myLocalRow("status") = 1
            End If
        Next

        ' write up the results
        LstResults.Items.Clear()
        iItems = 0
        localView.RowFilter = "status = 0"
        importView.RowFilter = "status = 0"
        LstResults.Items.Add("The following entries in your database are not in the Master:")
        For Each myLocalRow In localView
            szPath = myLocalRow("f_path")
            szFName = myLocalRow("f_name")
            LstResults.Items.Add("    " & szPath & "\" & szFName)
            iItems += 1
        Next
        If iItems = 0 Then
            LstResults.Items.Add("None")
        Else
            LstResults.Items.Add("Please upload these to the appropriate folders to share them with the comnmunity")
        End If

        iItems = 0
        localView.RowFilter = "status = 0"
        importView.RowFilter = "status = 0"
        LstResults.Items.Add("The following entries in the Master store are not in your database:")
        For Each myImportRow In importView
            szPath = myImportRow("f_path")
            szFName = myImportRow("f_name")
            LstResults.Items.Add("    " & szPath & "\" & szFName)
            iItems += 1
        Next
        If iItems = 0 Then
            LstResults.Items.Add("None")
        Else
            LstResults.Items.Add("Please download these to the appropriate folders to update your installation")
        End If
    End Sub

    Private Function CreateTableRes() As DataTable
        Dim myTable As New DataTable("T_Results")

        myTable.Columns.Add("f_path", Type.GetType("System.String"))
        myTable.Columns.Add("f_name", Type.GetType("System.String"))
        myTable.Columns.Add("comment", Type.GetType("System.String"))

        CreateTableRes = myTable
    End Function


    Private Function FillTableImp(szFileImport As String, szFolder As String) As DataTable
        Dim myTable As New DataTable("T_Import")
        Dim keys(2) As DataColumn

        myTable.Columns.Add("f_path", Type.GetType("System.String"))
        myTable.Columns.Add("f_name", Type.GetType("System.String"))
        myTable.Columns.Add("status", Type.GetType("System.Int32"))
        ' status: 0=file found; 1=file matched db
        keys(0) = myTable.Columns(0)
        keys(1) = myTable.Columns(1)
        myTable.PrimaryKey = keys

        FillTableImp = myTable

        ' Read the Master file
        Dim fileReader = My.Computer.FileSystem.OpenTextFileReader(szFileImport)
        Dim szLine As String
        Dim szParts(2) As String
        Dim myRow As DataRow

        Do
            szLine = fileReader.ReadLine
            If szLine Is Nothing Then Exit Do

            szParts = szLine.Split(vbTab)
            'myKeyParser.GetKeyValues(szLine, vbTab, szParts)
            If szParts(0) = "F_name" Or szParts(0) = "" Then Continue Do
            myRow = myTable.NewRow()
            myRow("f_path") = szFolder + "\" + szParts(0)
            myRow("f_name") = szParts(1)
            myRow("status") = 0
            myTable.Rows.Add(myRow)
            myTable.AcceptChanges()
        Loop
    End Function


    Private Function FillTableLocal() As DataTable
        Dim myTable As New DataTable("T_Local")
        Dim keys(2) As DataColumn

        myTable.Columns.Add("f_path", Type.GetType("System.String"))
        myTable.Columns.Add("f_name", Type.GetType("System.String"))
        myTable.Columns.Add("status", Type.GetType("System.Int32"))
        ' status: 0=file found; 1=file matched db
        keys(0) = myTable.Columns(0)
        keys(1) = myTable.Columns(1)
        myTable.PrimaryKey = keys

        FillTableLocal = myTable

        ' Read the Master file
        Dim myRow As DataRow, filesView As DataView
        filesView = T_filesTable.DefaultView

        If filesView.Count() = 0 Then
            myMsgBox.Show("There are no files in the table", "Compare With Master",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            myTable.Dispose()
            FillTableLocal = Nothing
            Exit Function
        Else
            'MyMsgBox.Show(filesView.Count)
            filesView.Sort = "f_path,f_name"
        End If

        For Each rowView In filesView
            myRow = myTable.NewRow
            myRow("f_path") = rowView("f_path")
            myRow("f_name") = rowView("f_name")
            myRow("status") = 0
            myTable.Rows.Add(myRow)
            myTable.AcceptChanges()
        Next
    End Function

End Class