Imports System.IO

Public Class GetFiles
    ' This class contains a method to retrieve files from a specified directory.
    ' It uses the FileIO namespace to access file system operations and returns
    ' then in a DataTable
    Private ReadOnly fileTable As DataTable
    Private ReadOnly myMsgBox As New DlgMsgBox

    Public Sub New()
        ' Initialize the DataTable with columns for file information
        fileTable = New DataTable("Files")
        fileTable.Columns.Add("FileName", GetType(String))
        fileTable.Columns.Add("FilePath", GetType(String))
        fileTable.Columns.Add("Status", GetType(Integer))
    End Sub

    Public Function GetPPs(ByVal directoryPath As String) As DataTable
        ' If the directory path ends in a folder named "MASTER" (case sensitive)
        ' it is assumed that the folder structure is in Parklea format i.e. MASTER
        ' contains single character folders named a, b. c, ...z and each of these
        ' folders contains PowerPoint files.  Only those are collected.
        ' Otherwise all PPTs in all subfiolders are collected
        ' Create a new DataTable to hold the list of files
        ' Check if the directory path ends with "MASTER"
        If directoryPath.EndsWith("MASTER", StringComparison.CurrentCulture) Then
            Dim allDirs As List(Of String) = GetFoldersPklea(directoryPath)
            GetFilesPklea(allDirs)       ' modifies base table
        Else
            ' Get all PowerPoint files in all subdirectories
            GetFilesHier(directoryPath)
        End If

        Return fileTable
    End Function

    Private Sub GetFilesHier(myFolder As String)
        Dim myFile As String
        For Each szPath In FileIO.FileSystem.GetFiles(myFolder, FileIO.SearchOption.SearchAllSubDirectories, "*.*")
            If Directory.Exists(szPath) Then
                GetFilesHier(szPath)
            ElseIf File.Exists(szPath) Then
                myFile = FileIO.FileSystem.GetName(szPath)
                Select Case myFile.Substring(myFile.Length - 4, 4).ToLower()
                    Case ".ppt", "pptx", "pptm"
                        AddRow(fileTable, myFile, szPath)
                End Select
            End If
        Next szPath
    End Sub

    Private Function GetFoldersPklea(szFolder As String) As List(Of String)
        ' This function is used to list the Parklea format folders
        Dim myList As New List(Of String), i As Integer = 0

        For Each szPath In FileIO.FileSystem.GetDirectories(szFolder)
            Select Case szPath
                Case ".", ".."
                Case Else
                    If szPath.Length = 1 Then
                        myList.Add(szFolder & "\" & szPath)
                        i += 1
                    End If
            End Select
        Next

        If i = 0 Then
            myMsgBox.Show("There are no alpha folders here:" & vbCrLf &
                szFolder & vbCrLf & "Please locate a Parklea-type songs MASTERS folder",
                "Finding the songs", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
        Return myList
    End Function

    Private Sub GetFilesPklea(myFolders As List(Of String))
        ' This subroutine is used to get the files from Parklea format folders
        Dim szPath As String, myFile As String

        For Each szPath In myFolders
            For Each myFile In FileIO.FileSystem.GetFiles(szPath, FileIO.SearchOption.SearchAllSubDirectories, "*.*")
                Select Case myFile.Substring(myFile.Length - 4, 4).ToLower()
                    Case ".ppt", "pptx"
                        AddRow(fileTable, myFile, szPath)
                End Select
            Next myFile
        Next szPath

    End Sub

    Private Sub AddRow(myTable As DataTable, s1 As String, s2 As String)
        Dim workRow As DataRow = myTable.NewRow()
        workRow("FileName") = s1
        workRow("FilePath") = s2
        workRow("Status") = 0
        myTable.Rows.Add(workRow)
        myTable.AcceptChanges()
    End Sub

End Class
