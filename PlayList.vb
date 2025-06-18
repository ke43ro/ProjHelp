Imports System.Data.SQLite
Imports LibVLCSharp.Shared

Public Class PlayList
    Private TFadap As FilesTable
    Private Declare Function SetForegroundWindow Lib "user32" (ByVal hWnd As IntPtr) As Integer
    Private ReadOnly myMsgBox As New DlgMsgBox
    Private PrefDisplay As Screen, vidPause As Boolean

    ' get the file extension
    Private Function GetExtn(Path) As String
        Dim Extn As String
        Dim idx = Path.LastIndexOf(".")

        If idx < 1 Then
            Extn = ""
        Else
            Extn = Path.Substring(idx)
        End If

        Return Extn
    End Function

    ' create a media object for the file
    Private Function GetMedia(myPath As String) As Media
        Dim libVLC As New LibVLC()
        Dim myMediaOptions() As String = {":mouse-events", ":keyboard-events"}
        Dim myUri As New Uri(myPath)
        Dim myMedia = New LibVLCSharp.Shared.Media(libVLC, myUri, myMediaOptions)
        myMedia.Parse(MediaParseOptions.ParseLocal)
        System.Threading.Thread.Sleep(1000)
        If myMedia.Tracks.Length = 0 Then
            Return Nothing
        End If
        Return myMedia
    End Function

    ' Play the file
    Private Sub PlayFile(myPath As String)
        Dim PlayPowerPoint As VBPlayPowerPoint, PlayPDF As VBPlayPDF, PlayWord As VBPlayWord
        Dim myForm As F_Video
        Dim myMedia As Media
        Dim myExtn = GetExtn(myPath)

        Select Case myExtn
            Case ".doc", ".docx"
                PlayWord = New VBPlayWord
                PlayWord.Run(myPath, PrefDisplay)
                PlayWord = Nothing

            Case ".ppt", ".pptx", "pptm"
                PlayPowerPoint = New VBPlayPowerPoint
                PlayPowerPoint.Run(myPath)
                PlayPowerPoint = Nothing

            Case ".pdf"
                PlayPDF = New VBPlayPDF
                PlayPDF.Run(myPath)
                PlayPDF = Nothing

            Case Else
                myMedia = GetMedia(myPath)
                If myMedia Is Nothing Then
                    myMsgBox.Show("Program Error - can't process " & myPath, "Displaying Media")
                Else
                    myForm = New F_Video()
                    With myForm
                        .LoadMedia(myMedia)
                        .LoadBounds(PrefDisplay.Bounds)
                        '.LoadPause(vidPause)
                        .ShowDialog()
                    End With
                    myForm = Nothing
                    If vidPause Then
                        myMsgBox.Show("Press OK to continue with the next file in the playlist.",
                                "Pause After Video", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If

        End Select
    End Sub

    Public Sub Run(display As Screen, pause As Boolean, ByRef arPlayList As ListBox.ObjectCollection)
        'Dim PPPres As Application  ' Microsoft.Office.Interop.PowerPoint.Application
        'Dim SSWin As SlideShowWindow
        Dim szFileName, szFPath As String, i, iFileNo, iIndex As Integer
        Dim myParts As String()
        PrefDisplay = display
        vidPause = pause

        Dim connection As SQLiteConnection = F_Main.ProjHelpData.GetConnection()
        If connection.State <> ConnectionState.Open Then
            connection.Open()
        End If

        TFadap = New FilesTable(connection)
        TFadap.LoadActive(isShort:=False)

        Dim filesView As DataView = TFadap.DefaultView
        filesView.Sort = "file_no"

        i = -1
        Do
            i += 1
            If i >= arPlayList.Count Then Exit Do

            szFileName = arPlayList.Item(i)

            ' get file_no
            myParts = szFileName.Split(vbTab)
            iFileNo = myParts(0)
            szFileName = myParts(1)

            If iFileNo = -1 Then
                szFPath = IIf(myParts.Length > 2, myParts(2), "")
            Else
                ' build path into szFN
                iIndex = filesView.Find(iFileNo)
                If iIndex < 0 Then
                    myMsgBox.Show("Can't find a record in the table for " & szFileName & ".  Skipping",
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Continue Do
                Else
                    szFPath = filesView(iIndex)("f_path")
                End If

                szFPath = szFPath & "\" & szFileName
            End If
            If Dir(szFPath) = "" Then
                myMsgBox.Show("Can't find show file " & szFileName & " on the disk", "Running a show",
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
                Continue Do
            End If
            PlayFile(szFPath)
        Loop

        'PPPres.Presentations.Open(szFileName)
        'Dim iWin = PPPres.Windows.Count
        'PPPres.Windows(iWin).ViewType = ppViewNormal
        'With PPPres.Presentations(szFileName)
        '    SSWin = .SlideShowSettings.Run()
        '    'System.Threading.Thread.Sleep(500)
        '    SSWin.Activate()
        '    SSWin.View.First()
        '    SetForegroundWindow(SSWin.HWND)
        '    Do
        '        If SSWin Is Nothing Then Exit Do
        '        Try
        '            If SSWin.Active Then System.Threading.Thread.Sleep(1000)
        '        Catch ex As Exception
        '            Exit Do
        '        End Try
        '    Loop

        '    ' in case the user closes Powerpoint before closing the show
        '    Try
        '        .Close()
        '    Catch ex As Exception

        '    End Try
        'End With

        'Try
        '    If PPPres IsNot Nothing Then
        '        PPPres.WindowState = PpWindowState.ppWindowMinimized
        '        PPPres.Quit()
        '    End If

        'Catch ex As Exception
        '    myMsgBox.Show("Error minimising the PowerPoint window. Do you have a licenced copy of MS Office?" &
        '            vbCrLf & "Please contact the programmer with this message:" & vbCrLf & ex.Message & vbCrLf &
        '            ex.StackTrace,
        '        "Presentation Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        'End Try

    End Sub
End Class
