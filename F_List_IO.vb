﻿Imports System.Data.SQLite

' Used to import and export lists of files in Projection Helper
Public Class F_List_IO
    Private filesView As DataView
    Private isT_FilesUpdated As Boolean = False
    Private T_filesTable As FilesTable
    Private ReadOnly myMsgBox As New DlgMsgBox

    Private Sub F_List_IO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connection As SQLiteConnection = F_Main.ProjHelpData.GetConnection()
        If connection.State <> ConnectionState.Open Then
            connection.Open()
        End If

        T_filesTable = New FilesTable(connection, InclInActive:=True)
        'T_filesTable.LoadAll(InclInActive:=True)
    End Sub

    Private Sub BtnExport_Click(sender As Object, e As EventArgs) Handles BtnExport.Click
        Dim fViewRow As DataRowView
        Dim filePath As String = ""
        Dim fileName, szF_name, szF_path, szF_altname, szSelected, szInactive As String
        Dim iRandom As New Random

        filesView = T_filesTable.DefaultView

        If filesView.Count() = 0 Then
            MyMsgBox.Show("There are no files in the table", "Projection Helper Export",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else
            filesView.Sort = "f_name"
            fileName = "PPLink" & iRandom.Next & ".txt"

            Try
                filePath = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, fileName)
                My.Computer.FileSystem.WriteAllText(filePath,
                    "F_name" & vbTab & "F_Path" & vbTab & "F_altname" & vbTab & "Selected" & vbTab & "Inactive" & vbCrLf,
                     False)
            Catch fileException As Exception
                MyMsgBox.Show("Failed to create file " & filePath, "Projection Helper Export",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            MyMsgBox.Show("This will write " & filesView.Count & " lines to a file " & filePath & " in Documents",
                            "Projection Helper Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
            For Each fViewRow In filesView
                szF_name = fViewRow("F_NAME")
                szF_path = fViewRow("F_PATH")
                szF_altname = IIf(IsDBNull(fViewRow("F_ALTNAME")), "", fViewRow("F_ALTNAME"))
                szSelected = IIf(IsDBNull(fViewRow("ISSHORTLIST")), "", fViewRow("ISSHORTLIST"))
                szInactive = IIf(IsDBNull(fViewRow("ISACTIVE")), "", fViewRow("ISACTIVE"))
                My.Computer.FileSystem.WriteAllText(filePath,
                    szF_name & vbTab & szF_path & vbTab & szF_altname & vbTab & szSelected & vbTab & szInactive & vbCrLf,
                    True)
            Next
            MyMsgBox.Show("File creation complete", "Projection Helper Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Sub BtnImport_Click(sender As Object, e As EventArgs) Handles BtnImport.Click
        Dim szParts(5) As String
        Dim szImportFile, szLine As String
        Dim szFileComment, szOldSel, szNewSel, szOldInact, szNewInact, szSelComment, szInactComment As String
        Dim szOldAlt, szNewAlt, szAltComment As String
        Dim MatchRows() As DataRowView
        Dim bSelIn, bXSelIn, bInactIn, bXInactIn, bRepAlt, bAddAlt As Boolean
        Dim nTotal, nNotLocal, nNotRemote, nSelSet, nSelRem, nINactSet, nINactRem, nAlt As Integer

        bXSelIn = ChkRemoveSelect.Checked
        bSelIn = ChkAddSelect.Checked
        bXInactIn = ChkRemInactive.Checked
        bInactIn = ChkAddInactive.Checked
        bRepAlt = ChkReplaceAlt.Checked
        bAddAlt = ChkAddAlt.Checked

        Dim T_Results = CreateTable()

        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            szImportFile = OpenFileDialog1.FileName
        Else
            myMsgBox.Show("This feature can only be run if a file is specified",
                            "Import a list", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        filesView = T_filesTable.DefaultView

        If filesView.Count() = 0 Then
            MyMsgBox.Show("There are no files in the table", "Projection Helper Export",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else
            'MyMsgBox.Show(filesView.Count, "")
            filesView.Sort = "f_name"
        End If

        Dim fileReader = My.Computer.FileSystem.OpenTextFileReader(szImportFile)
        Do
            szLine = fileReader.ReadLine
            If szLine Is Nothing Then Exit Do

            szParts = szLine.Split(vbTab)
            If szParts(0) = "F_name" Then Continue Do

            nTotal += 1
            MatchRows = filesView.FindRows(szParts(0))

            szFileComment = ""
            szAltComment = ""
            szSelComment = ""
            szInactComment = ""

            If MatchRows.Length = 0 Then
                'this file is not included in local database
                nNotLocal += 1
                szFileComment = "Not found in local database"
                szOldAlt = ""
                szNewAlt = ""
                szOldSel = ""
                szNewSel = ""
                szOldInact = ""
                szNewInact = ""
                ResultAdd(T_Results, 0, szParts(0), szParts(1), szFileComment, szOldAlt, szNewAlt, szAltComment, szOldSel, szNewSel,
                              szSelComment, szOldInact, szNewInact, szInactComment)
            Else
                For Each MatchRow In MatchRows
                    MatchRow.BeginEdit()

                    ' Alt name
                    szOldAlt = IIf(IsDBNull(MatchRow(3)), "", MatchRow(3))
                    szNewAlt = szParts(2)
                    If szOldAlt = szNewAlt Then
                        szAltComment = "No difference"
                        szNewAlt = ""
                    ElseIf szParts(2).Length = 0 Then
                        szAltComment = "No Alternative text in import"
                    Else
                        If bRepAlt Then
                            szAltComment = "Alternative text replaced"
                            MatchRow(3) = szNewAlt
                            nAlt += 1
                        ElseIf bAddAlt Then
                            If szOldAlt = "" Then
                                MatchRow(3) = szNewAlt
                                szSelComment = "New alternative inserted"
                                nAlt += 1
                            ElseIf szNewAlt.IndexOf(MatchRow(3)) > 0 Then
                                MatchRow(3) = szNewAlt
                                szSelComment = "Existing text included in import - replaced"
                                nAlt += 1
                            ElseIf MatchRow(3).IndexOf(szNewAlt) > 0 Then
                                szSelComment = "Imported text already included - no change"
                            Else
                                MatchRow(3) = MatchRow(3) & "; " & szNewAlt
                                szSelComment = "Alternative text extended"
                                nAlt += 1
                            End If
                        End If
                    End If

                    ' Selected
                    szOldSel = IIf(IsDBNull(MatchRow(4)), "", MatchRow(4))
                    szNewSel = szParts(3)
                    If szOldSel = szNewSel Then
                        szSelComment = "No change"
                        szNewSel = ""
                    Else
                        If szOldSel = "Y" And bXSelIn = True Then
                            szSelComment = "Selection removed"
                            MatchRow(4) = ""
                            nSelRem += 1
                        ElseIf szParts(3) = "Y" And bSelIn = True Then
                            szSelComment = "Selection added"
                            MatchRow(4) = "Y"
                            nSelSet += 1
                        End If
                    End If

                    'Inactive
                    szOldInact = IIf(IsDBNull(MatchRow(8)), "", MatchRow(8))
                    szNewInact = szParts(4)
                    If szOldInact = szNewInact Then
                        szInactComment = "No change"
                        szNewInact = ""
                    Else
                        If szOldInact = "Y" And bXInactIn = True Then
                            szInactComment = "Inactive removed"
                            MatchRow(8) = ""
                            nINactRem += 1
                        ElseIf szParts(4) = "Y" And bInactIn = True Then
                            szInactComment = "Inactive added"
                            MatchRow(8) = "Y"
                            nINactSet += 1
                        End If
                    End If

                    MatchRow.EndEdit()
                    ResultAdd(T_Results, MatchRow(0), MatchRow(1), MatchRow(2), szFileComment, szOldAlt, szNewAlt, szAltComment,
                              szOldSel, szNewSel, szSelComment, szOldInact, szNewInact, szInactComment)
                    T_filesTable.Update(MatchRow)
                Next
            End If
        Loop
        T_filesTable.AcceptChanges()
        isT_FilesUpdated = True

        'scan for files not in the import
        Dim ResultsView As New DataView(T_Results, "", "file_no", DataViewRowState.CurrentRows) With {
            .Sort = "file_no"
        }
        'MyMsgBox.Show("Results: " & ResultsView.Count, "")
        For Each MatchRow In filesView
            If ResultsView.Find(MatchRow(0)) < 0 Then
                'file missing from remote
                nNotRemote += 1
                ResultAdd(T_Results, MatchRow(0), MatchRow(1), MatchRow(2), "Not found in import file",
                          "", "", "", "", "", "", "", "", "")
            End If
        Next

        Dim result As DialogResult
        Dim szMessage As String = "Import Complete" & vbCrLf & "Total files in import: " & nTotal &
                   vbCrLf & "Not found in local table: " & nNotLocal &
                   vbCrLf & "In local table, but not in import: " & nNotRemote &
                   vbCrLf & "Alternative text altered: " & nAlt &
                   vbCrLf & "Selection set on: " & nSelSet &
                   vbCrLf & "Selection turned off: " & nSelRem &
                   vbCrLf & "Marked Inactive: " & nINactSet &
                   vbCrLf & "Inactive status cleared: " & nINactRem &
                   vbCrLf & vbCrLf & "Would you like to save these results?"
        result = myMsgBox.Show(szMessage, "Import", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If result = DialogResult.Yes Then
            SaveImportResults(T_Results)
        End If
        ResultsView.Dispose()
    End Sub

    Private Function CreateTable() As DataTable
        Dim myTable As New DataTable("T_Results")
        myTable.Columns.Add("file_no", Type.GetType("System.Int32"))
        myTable.Columns.Add("f_name", Type.GetType("System.String"))
        myTable.Columns.Add("f_path", Type.GetType("System.String"))
        myTable.Columns.Add("file_comment", Type.GetType("System.String"))
        myTable.Columns.Add("f_oldalt", Type.GetType("System.String"))
        myTable.Columns.Add("f_newalt", Type.GetType("System.String"))
        myTable.Columns.Add("alt_comment", Type.GetType("System.String"))
        myTable.Columns.Add("old_sel", Type.GetType("System.String"))
        myTable.Columns.Add("new_sel", Type.GetType("System.String"))
        myTable.Columns.Add("sel_comment", Type.GetType("System.String"))
        myTable.Columns.Add("old_inact", Type.GetType("System.String"))
        myTable.Columns.Add("new_inact", Type.GetType("System.String"))
        myTable.Columns.Add("inact_comment", Type.GetType("System.String"))
        'myTable.Columns.Add("new_sel", Type.GetType("System.String"))

        CreateTable = myTable
    End Function

    Private Sub ChkAddAlt_CheckedChanged(sender As Object, e As EventArgs) Handles ChkAddAlt.CheckedChanged
        If ChkAddAlt.Checked Then
            If ChkReplaceAlt.Checked Then ChkReplaceAlt.Checked = False
        End If
    End Sub

    Private Sub ChkReplaceAlt_CheckedChanged(sender As Object, e As EventArgs) Handles ChkReplaceAlt.CheckedChanged
        If ChkReplaceAlt.Checked Then
            If ChkAddAlt.Checked Then ChkAddAlt.Checked = False
        End If
    End Sub

    Private Sub ResultAdd(myTable As DataTable, i1 As Integer, s1 As String, s2 As String, s3 As String, s4 As String, s5 As String,
                          s6 As String, s7 As String, s8 As String, s9 As String, s10 As String, s11 As String, s12 As String)
        Dim workRow As DataRow = myTable.NewRow()
        workRow("file_no") = i1
        workRow("f_name") = s1
        workRow("f_path") = s2
        workRow("file_comment") = s3
        workRow("f_oldalt") = s4
        workRow("f_newalt") = s5
        workRow("alt_comment") = s6
        workRow("old_sel") = s7
        workRow("new_sel") = s8
        workRow("sel_comment") = s9
        workRow("old_inact") = s10
        workRow("new_inact") = s11
        workRow("inact_comment") = s12
        myTable.Rows.Add(workRow)
        myTable.AcceptChanges()
    End Sub

    Private Sub F_List_IO_Closed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If isT_FilesUpdated Then
            Me.DialogResult = DialogResult.Yes
        Else
            Me.DialogResult = DialogResult.No
        End If
    End Sub

    Private Sub SaveImportResults(ByRef myTable As DataTable)
        Dim iRandom As New Random
        Dim filePath As String = ""
        Dim fileName As String = "PPLinkImport" & iRandom.Next & ".txt"

        Try
            filePath = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, fileName)
            My.Computer.FileSystem.WriteAllText(filePath,
                "File_no" & vbTab & "F_name" & vbTab & "F_path" & vbTab & "File_comment" & vbTab &
                "F_oldalt" & vbTab & "F_newalt" & vbTab & "Alt_comment" & vbTab &
                "Old_sel" & vbTab & "New_sel" & vbTab & "Sel_comment" & vbTab &
                "Old_inact" & vbTab & "New_inact" & vbTab & "Inact_comment" & vbTab & vbCrLf,
                False)
        Catch fileException As Exception
            MyMsgBox.Show("Failed to create file " & filePath, "Projection Helper Import Results Save",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim myRows() As DataRow = myTable.Select
        MyMsgBox.Show("This will write " & myRows.Length & " lines to a file " & filePath & " in Documents",
                        "Projection Helper Import Results Save", MessageBoxButtons.OK, MessageBoxIcon.Information)
        For Each myRow In myRows
            My.Computer.FileSystem.WriteAllText(filePath,
                myRow("file_no") & vbTab & myRow("f_name") & vbTab & myRow("f_path") & vbTab & myRow("file_comment") & vbTab &
                myRow("f_oldalt") & vbTab & myRow("f_newalt") & vbTab & myRow("alt_comment") & vbTab &
                myRow("old_sel") & vbTab & myRow("new_sel") & vbTab & myRow("sel_comment") & vbTab &
                myRow("old_inact") & vbTab & myRow("new_inact") & vbTab & myRow("inact_comment") & vbTab & vbCrLf,
                True)
        Next
        MyMsgBox.Show("File creation complete", "Projection Helper Import Results Save", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class