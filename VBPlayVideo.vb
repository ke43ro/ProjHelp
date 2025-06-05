'Imports VideoLAN.LibVLC.Windows

Public Class VBPlayVideo
    Private Declare Function SetForegroundWindow Lib "user32" (ByVal hWnd As IntPtr) As Integer
    Private _mp As LibVLCSharp.Shared.MediaPlayer
    Private ReadOnly myMsgBox As New DlgMsgBox

    Public Sub Run(Path As String)
        Dim libVLC As New LibVLCSharp.Shared.LibVLC({"--mouse-events", "--keyboard-events", "--video", "--embedded-video"})
        Dim myVideo As LibVLCSharp.WinForms.VideoView

        If Dir(Path) = "" Then
            myMsgBox.Show("Can't find video file " & Path & " on the disk", "Playing a video",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        F_Video.Show()
        myVideo = F_Video.VideoView1

        Dim myMediaOptions() As String = {"--mouse-events", "--keyboard-events", "--video", ":embedded-video"}
        '":fullscreen", ":embedded-video", ":video-on-top", ":key-play-pause=b", ":key-vol-mute=m"
        Dim myUri As New Uri(Path)
        _mp = New LibVLCSharp.Shared.MediaPlayer(libVLC) With {
            .EnableKeyInput = True,
            .EnableMouseInput = True
        }

        With myVideo
            .MediaPlayer = _mp
            .Visible = True
        End With

        _mp.Play(New LibVLCSharp.Shared.Media(libVLC, myUri, myMediaOptions))


        F_Video.WindowState = FormWindowState.Maximized
        Dim unused = SetForegroundWindow(F_Video.Handle)
        'Dim unused = SetForegroundWindow(_mp.Hwnd)
        'Dim unused = SetForegroundWindow(myVideo.Handle)


        WaitTillEnded(_mp)

        F_Video.Close()

    End Sub


    Sub WaitTillEnded(myPlayer As LibVLCSharp.Shared.MediaPlayer)
        Threading.Thread.Sleep(2000)
        Do While myPlayer.IsPlaying
            Threading.Thread.Sleep(1000)
        Loop
    End Sub
End Class
