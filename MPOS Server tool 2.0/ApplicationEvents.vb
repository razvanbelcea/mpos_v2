Imports System.Data.SqlServerCe
Imports System.IO

Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(sender As Object, e As ApplicationServices.StartupEventArgs) Handles Me.Startup
            MPOS.Main.ActualVersion()
            MPOS.Main.ShowInTaskbar = True
            MPOS.Settings.CheckSettings()
            CreateDB()
        End Sub

        Public Sub CreateDB()

            If Not File.Exists("utils.sdf") Then
                Dim connString As String = "Data Source='utils.sdf'; Password=123;"
                Dim engine As New SqlCeEngine(connString)
                engine.CreateDatabase()

                Dim cmdstring As String = "CREATE TABLE cacheversion
(
	serverip nCHAR(15) NOT NULL PRIMARY KEY, 
    version ntext NULL
)
"

                Using con As New SqlCeConnection("Data Source=utils.sdf;Password=123")
                    con.Open()
                    Using cmd As New SqlCeCommand(cmdstring, con)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            End If

        End Sub

    End Class

End Namespace

