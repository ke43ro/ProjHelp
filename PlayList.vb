Imports System.Data.SQLite
Imports Microsoft.Office.Interop.PowerPoint
Imports Microsoft.Office.Interop.PowerPoint.PpViewType

Public Class PlayList
    Private TFadap As FilesTable
    Private Declare Function SetForegroundWindow Lib "user32" (ByVal hWnd As IntPtr) As Integer
    Private ReadOnly myMsgBox As New DlgMsgBox

    Public Sub Run(ByRef arPlayList As ListBox.ObjectCollection)
        Dim PPPres As Application  ' Microsoft.Office.Interop.PowerPoint.Application
        Dim SSWin As SlideShowWindow
        Dim szFileName, szFPath As String, i, iFileNo, iIndex As Integer
        Dim myParts() As String = {"", ""}

        Dim connection As SQLiteConnection = F_Main.ProjHelpData.GetConnection()
        If connection.State <> ConnectionState.Open Then
            connection.Open()
        End If

        TFadap = New FilesTable(connection)
        TFadap.LoadActive(isShort:=False)

        Dim filesView As DataView = TFadap.DefaultView
        filesView.Sort = "file_no"

        PPPres = New Microsoft.Office.Interop.PowerPoint.Application
        Try
            PPPres.Visible = True

        Catch ex As Exception
            myMsgBox.Show("Error making the PowerPoint window visible. Do you have a licenced copy of MS Office?" &
                    vbCrLf & "Please contact the programmer with this message:" & vbCrLf & ex.Message & vbCrLf &
                    ex.StackTrace,
                "Presentation Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub

        End Try

        i = -1
        Do
            i += 1
            If i >= arPlayList.Count Then Exit Do

            szFileName = arPlayList.Item(i)

            ' get file_no
            myParts = szFileName.Split(vbTab)
            iFileNo = myParts(0)
            szFileName = myParts(1)

            ' build path into szFN
            iIndex = filesView.Find(iFileNo)
            If iIndex < 0 Then
                myMsgBox.Show("Can't find a record in the table for " & szFileName & ".  Skipping",
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Continue Do
            Else
                szFPath = filesView(iIndex)("f_path")
            End If

            szFileName = szFPath & "\" & szFileName
            If Dir(szFileName) = "" Then
                myMsgBox.Show("Can't find show file " & szFileName & " on the disk", "Running a show",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
                Continue Do
            End If

            PPPres.Presentations.Open(szFileName)
            Dim iWin = PPPres.Windows.Count
            PPPres.Windows(iWin).ViewType = ppViewNormal
            With PPPres.Presentations(szFileName)
                SSWin = .SlideShowSettings.Run()
                'System.Threading.Thread.Sleep(500)
                SSWin.Activate()
                SSWin.View.First()
                SetForegroundWindow(SSWin.HWND)
                Do
                    If SSWin Is Nothing Then Exit Do
                    Try
                        If SSWin.Active Then System.Threading.Thread.Sleep(1000)
                    Catch ex As Exception
                        Exit Do
                    End Try
                Loop

                ' in case the user closes Powerpoint before closing the show
                Try
                    .Close()
                Catch ex As Exception

                End Try
            End With
        Loop

        Try
            PPPres.WindowState = PpWindowState.ppWindowMinimized
            PPPres.Quit()

        Catch ex As Exception
            myMsgBox.Show("Error minimising the PowerPoint window. Do you have a licenced copy of MS Office?" &
                    vbCrLf & "Please contact the programmer with this message:" & vbCrLf & ex.Message & vbCrLf &
                    ex.StackTrace,
                "Presentation Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End Try

    End Sub
End Class
