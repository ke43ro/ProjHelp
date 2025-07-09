Imports Microsoft.Office.Interop.PowerPoint
Imports Microsoft.Office.Interop.PowerPoint.PpViewType


Public Class VBPlayPowerPoint
    Private Declare Function SetForegroundWindow Lib "user32" (ByVal hWnd As IntPtr) As Integer
    Private ReadOnly myMsgBox As New DlgMsgBox
    Private ReadOnly isDebug As Boolean = My.Settings.Debug

    Public Sub Run(Path As String)
        Dim SSWin As SlideShowWindow

        If isDebug Then
            myMsgBox.Show("Running PowerPoint file " & Path, "Playing a PowerPoint", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        If Dir(Path, FileAttribute.Normal) = "" Then
            myMsgBox.Show("Can't find show file " & Path & " on the disk", "Playing a PowerPoint",
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim PPPres As Application
        Dim attempts As Integer = 0
        Const maxAttempts As Integer = 3

        Do
            Try
                PPPres = New Application With {
            .Visible = True
        }
                Exit Do
            Catch ex As Exception
                attempts += 1
                If attempts >= maxAttempts Then
                    myMsgBox.Show("Error making the PowerPoint window visible. Do you have a licenced copy of MS Office?" &
                vbCrLf & "Please copy the message below and paste it into an email to the programmer: ke43ro@gmail.com" &
                vbCrLf & ex.Message & vbCrLf & ex.StackTrace,
                "Presentation Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
                System.Threading.Thread.Sleep(1000) ' Wait 1 second before retrying
            End Try
        Loop
        'Try
        '    PPPres = New Application With {
        '        .Visible = True
        '    }

        'Catch ex As Exception
        '    myMsgBox.Show("Error making the PowerPoint window visible. Do you have a licenced copy of MS Office?" &
        '            vbCrLf & "Please copy the message below and paste it into an email to the programmer: ke43ro@gmail.com" &
        '            vbCrLf & ex.Message & vbCrLf & ex.StackTrace,
        '        "Presentation Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub

        'End Try

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
                    vbCrLf & "Please copy the message below and paste it into an email to the programmer: ke43ro@gmail.com" &
                    vbCrLf & ex.Message & vbCrLf & ex.StackTrace,
                "Presentation Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End Try
    End Sub
End Class
