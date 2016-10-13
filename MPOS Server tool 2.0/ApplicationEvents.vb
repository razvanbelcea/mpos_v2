Imports System.Data.SQLite
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
           ' Task.Factory.StartNew(Sub() utils.populateSDFdb)
           ' CreateDb()
           ' utils.populateSDFdb()
            utils.Updateserverinfo()
        End Sub
           Public Sub CreateDb()
        Dim doc = New XmlDocument()
        Dim svl = "sqllist.xml"
        Dim filepath As String = System.Windows.Forms.Application.StartupPath + "\Resources\utils.db3"
        doc.Load(svl)
        Try
            If Not File.Exists(filepath) Then
                Dim connString = "Data Source="+filepath+";"
                SQLiteConnection.CreateFile(filepath)
                Dim serverinfo As String = doc.SelectSingleNode("List/DBqueries/ServerInformation").InnerText
                Using con As New SQLiteConnection(connString)
                    con.Open()
                    Using cmd As New SQLiteCommand(serverinfo, con)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            End If
        Catch ex As Exception
                MessageBox.Show(ex.Message)
        End Try
    End Sub
    End Class
End Namespace

