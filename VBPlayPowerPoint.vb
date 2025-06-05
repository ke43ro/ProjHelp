Imports Microsoft.Office.Interop.PowerPoint
Imports Microsoft.Office.Interop.PowerPoint.PpViewType


Public Class VBPlayPowerPoint
    'Public Property PPPres As Microsoft.Office.Interop.PowerPoint.Application
    Private Declare Function SetForegroundWindow Lib "user32" (ByVal hWnd As IntPtr) As Integer
    'Private ReadOnly myMsgBox As DlgMsgBox = New DlgMsgBox
    Private ReadOnly myMsgBox As New DlgMsgBox

    Public Sub Run(Path As String)
        Dim SSWin As SlideShowWindow
        'Dim szFileName, szFPath As String, i, iFileNo, iIndex As Integer

        If Dir(Path) = "" Then
            myMsgBox.Show("Can't find show file " & Path & " on the disk", "Playing a PowerPoint",
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim PPPres = New Microsoft.Office.Interop.PowerPoint.Application
        Try
            PPPres.Visible = True

        Catch ex As Exception
            myMsgBox.Show("Error making the PowerPoint window visible. Do you have a licenced copy of MS Office?" &
                    vbCrLf & "Please contact the programmer with this message:" & vbCrLf & ex.Message & vbCrLf &
                    ex.StackTrace,
                "Presentation Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub

        End Try

        PPPres.Presentations.Open(Path)
        Dim iWin = PPPres.Windows.Count
        PPPres.Windows(iWin).ViewType = ppViewNormal
        With PPPres.Presentations(Path)
            SSWin = .SlideShowSettings.Run()
            'System.Threading.Thread.Sleep(500)
            SSWin.Activate()
            SSWin.View.First()
            Dim unused = SetForegroundWindow(SSWin.HWND)
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
