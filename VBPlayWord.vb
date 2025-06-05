Imports Microsoft.Office.Interop.Word

Public Class VBPlayWord
    'Private myMsgBox = New DlgMsgBox

    Public Sub Run(Path As String, myScreen As Screen)
        Dim WordPres = New Microsoft.Office.Interop.Word.Application()
        With WordPres
            .Visible = True
            .Left = myScreen.Bounds.Left
            .Top = myScreen.Bounds.Top
            .Documents.Open(Path)
            .Windows(1).WindowState = WdWindowState.wdWindowStateMaximize
        End With

        Try
            ' Exception is thrown if the Word window is closed "externally"
            While WordPres.Windows.Count > 0
                System.Threading.Thread.Sleep(1000)
            End While
        Catch
        End Try
    End Sub
End Class
