Imports System.Data.SqlServerCe
Imports System.IO
Imports System.Xml

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
            Task.Factory.StartNew(Sub() utils.populateSDFdb)
          '  utils.populateSDFdb()
        End Sub

        Public Sub CreateDB()
            Dim doc As XmlDocument = New XmlDocument()
            Dim svl As String = "sqllist.xml"
            doc.Load(svl)
            Try
                If Not File.Exists("utils.sdf") Then
                    Dim connString As String = "Data Source='utils.sdf';"
                    Dim engine As New SqlCeEngine(connString)
                    engine.CreateDatabase()
                    Dim qatab As String = doc.SelectSingleNode("List/DBqueries/qatab").InnerText
                    Dim devtab As String = doc.SelectSingleNode("List/DBqueries/devtab").InnerText
                    Dim uattab As String = doc.SelectSingleNode("List/DBqueries/uattab").InnerText
                    Dim prodtab As String = doc.SelectSingleNode("List/DBqueries/prodtab").InnerText
                    Using con As New SqlCeConnection("Data Source=utils.sdf")
                        con.Open()
                        Using cmd As New SqlCeCommand(qatab, con)
                            cmd.ExecuteNonQuery()
                        End Using
                        Using cmd1 As New SqlCeCommand(devtab, con)
                            cmd1.ExecuteNonQuery()
                        End Using
                        Using cmd2 As New SqlCeCommand(uattab, con)
                            cmd2.ExecuteNonQuery()
                        End Using
                        Using cmd3 As New SqlCeCommand(prodtab, con)
                            cmd3.ExecuteNonQuery()
                        End Using
                    End Using
                End If
            Catch ex As Exception

            End Try
        End Sub

    End Class

End Namespace

