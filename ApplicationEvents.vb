Imports System.Threading

Namespace My
    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Public Event ThreadException(sender As Object, e As ThreadExceptionEventArgs)
        Private ReadOnly myMsgBox As New DlgMsgBox

        Private Sub MyApplication_Startup(sender As Object, e As ApplicationServices.StartupEventArgs) Handles Me.Startup
            AddHandler Application.ThreadException, AddressOf Application_ThreadException
            AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf CurrentDomain_UnhandledException
        End Sub

        Private Sub Application_ThreadException(sender As Object, e As Threading.ThreadExceptionEventArgs)
            MyMsgBox.Show("Unhandled UI Exception: " & e.Exception.Message)
            ' Optionally log or handle further
        End Sub

        Private Sub CurrentDomain_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs)
            Dim ex As Exception = TryCast(e.ExceptionObject, Exception)
            MyMsgBox.Show("Unhandled Non-UI Exception: " & ex?.Message)
            ' Optionally log or handle further
        End Sub
    End Class
End Namespace
