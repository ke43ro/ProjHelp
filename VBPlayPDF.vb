Public Class VBPlayPDF
    Public Sub Run(Path As String)

        Dim myProc = New Process
        With myProc
            .StartInfo.FileName = Path '"C:\Program Files\Adobe\Acrobat DC\Acrobat\Acrobat.exe", Path
            .StartInfo.UseShellExecute = True
            .StartInfo.WindowStyle = ProcessWindowStyle.Normal
            .Start()
            .WaitForExit()
        End With
    End Sub

End Class
