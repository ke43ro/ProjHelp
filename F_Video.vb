Imports LibVLCSharp.Shared


Public Class F_Video
    Private Declare Function SetForegroundWindow Lib "user32" (ByVal hWnd As IntPtr) As Integer
    Private VideoMedia As Media, ScreenLocation As Rectangle, BPauseEach As Boolean = False
    Private ReadOnly myMsgBox As New DlgMsgBox
    Private _mp As MediaPlayer

    Private Sub F_Video_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'myMsgBox.Show("On Load: Top: " & Top & "; Left: " & Left &
        '    "; SL.Top: " & ScreenLocation.Top & "; SL.Left: " & ScreenLocation.Left)

        Top = ScreenLocation.Top
        Left = ScreenLocation.Left

        ' These options don't seem to take effect although illegal ones cause an exception
        'Dim myMediaOptions() As String = {":mouse-events", ":keyboard-events"}

        ' Options applied here don't seem to take effect so I removed them
        Dim libVLC As New LibVLC()

        _mp = New MediaPlayer(libVLC) With {
            .EnableKeyInput = True,
            .EnableMouseInput = True
        }
        AddHandler _mp.EndReached, AddressOf MediaPlayer_EndReached
        'myMsgBox.Show("add mediaplayer")
        With VideoView1
            .MediaPlayer = _mp
            .Visible = True
            .Focus()
        End With

        'myMsgBox.Show("about to play")
        _mp.Play(VideoMedia)
        'myMsgBox.Show("maximise form")
        WindowState = FormWindowState.Maximized
        'myMsgBox.Show("set foreground")
        Dim unused = SetForegroundWindow(Handle)
        'myMsgBox.Show("end form load - Top: " & Top & "; Left: " & Left)
    End Sub


    Private Sub CloseForm()
        If Me.InvokeRequired Then
            Me.Invoke(New Action(AddressOf CloseForm))
            Return
        End If
        Close()
    End Sub


    Friend Sub LoadMedia(myMedia As Media)
        VideoMedia = myMedia
    End Sub


    Friend Sub LoadBounds(myBounds As Rectangle)
        ' Public Sub New(x As Integer, y As Integer, width As Integer, height As Integer)
        With myBounds
            'myMsgBox.Show("LoadBounds: Top: " & .Top & "; Left: " & .Left &
            '    "; Width: " & .Width & "; Height: " & .Height)
            ScreenLocation = New Rectangle(.Left, .Top, .Width, .Height)
        End With
        'myMsgBox.Show("ScreenLocation: Top: " & ScreenLocation.Top & "; Left: " & ScreenLocation.Left &
        '        "; Width: " & ScreenLocation.Width & "; Height: " & ScreenLocation.Height)
    End Sub


    Friend Sub LoadPause(BPause As Boolean)
        BPauseEach = BPause
    End Sub


    Private Sub VideoView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles VideoView1.KeyPress
        Select Case e.KeyChar
            ' Include b to support clicker
            Case "p", "b"
                If _mp.IsPlaying Then
                    _mp.Pause()
                Else
                    _mp.Play()
                End If

            Case "r"
                _mp.SeekTo(New TimeSpan(0))

            Case "s"
                _mp.Stop()
                Close()

            Case Else
                Exit Sub
        End Select

        e.Handled = True
    End Sub


    Private Sub VideoView1_KeyDown(sender As Object, e As KeyEventArgs) Handles VideoView1.KeyDown
        Select Case e.KeyCode
            Case Keys.Next
                If _mp.State = VLCState.Ended Then
                    CloseForm()
                Else
                    _mp.Stop()
                    Close()
                End If

            Case Keys.PageUp
                _mp.SeekTo(New TimeSpan(0))

            Case Else
                Exit Sub
        End Select

        e.SuppressKeyPress = True
    End Sub


    Private Sub MediaPlayer_EndReached(sender As Object, e As EventArgs)
        If Not BPauseEach Then CloseForm()
    End Sub
End Class