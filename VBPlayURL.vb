Public Class VBPlayURL
    Public Sub Run(urlFilePath As String)
        Dim url As String = ""
        For Each line As String In IO.File.ReadAllLines(urlFilePath)
            If line.StartsWith("URL=", StringComparison.OrdinalIgnoreCase) Then
                url = line.Substring(4)
                Exit For
            End If
        Next
        If Not String.IsNullOrEmpty(url) Then
            Process.Start(url)
        End If
    End Sub

End Class
