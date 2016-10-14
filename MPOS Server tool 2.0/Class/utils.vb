Imports System.Data.SqlClient
Imports System.Threading
Imports System.Data.SQLite
Imports System.IO
Imports System.Xml

Public Class utils
    Private ReadOnly _logger As New ErrorLogger
    Public Shared Filepath As String = System.Windows.Forms.Application.StartupPath + "\Resources\utils.db3"
    Public Shared Sub CreateDb()
        Splash.Copyright.Text = "Creating local database..."
        Dim doc = New XmlDocument()
        Dim svl = "sqllist.xml"
        Dim filepath As String = System.Windows.Forms.Application.StartupPath + "\Resources\utils.db3"
        doc.Load(svl)
        Try
            If Not File.Exists(filepath) Then
                Dim connString = "Data Source=" + filepath + ";"
                SQLiteConnection.CreateFile(filepath)
                Dim serverinfo As String = doc.SelectSingleNode("List/DBqueries/ServerInformation").InnerText
                Using con As New SQLiteConnection(connString)
                    con.Open()
                    Using cmd As New SQLiteCommand(serverinfo, con)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            End If
            Splash.loading = 1
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub WriteSdFdata(q As String)
        Try
            Using con As New SQLiteConnection("Data Source=" + Filepath + ";")
                con.Open()
                Using cmd As New SQLiteCommand(q, con)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            _logger.WriteToErrorLog(ex.Message, ex.StackTrace, "WriteSDFdata to utils.sdf")
        End Try
    End Sub

    Public Function ReadSdFdata(q As String)
        Dim reader As SQLiteDataReader = Nothing
        Dim con As SQLiteConnection = Nothing
        Dim cmd As SQLiteCommand = Nothing
        Try
            con = New SQLiteConnection("Data Source=" + Filepath + ";")
            con.Open()
            If con.State = ConnectionState.Open Then
                cmd = New SQLiteCommand(q, con)
                reader = cmd.ExecuteReader()
                Return reader
            End If
        Catch ex As Exception
            _logger.WriteToErrorLog(ex.Message, ex.StackTrace, "ReadSDFdata to utils.sdf")
        End Try
        Return Nothing
    End Function

    Public Shared Function Readusidb(type As String) As List(Of Tuple(Of String, String, String))
        Dim tuple As New List(Of Tuple(Of String, String, String))
        Dim sqlconnection As New SqlConnection
        Dim cmd As SqlCommand
        Dim reader As SqlDataReader
        Dim constring As String = "Data Source=10.21.22.136;Database=mposx_usi;User ID=DevReadOnly;Password=Pa$$w0rd;"
        Dim strCommand As String
        If type = "MPSQ" Then
            strCommand = "select a.MachineNameAlias,c.MyRegionCode,m.IPAddress from dbo.MachineNameAliases a join Machines m ON a.MAC=m.MAC join Stores s ON m.MyStoreID=s.ID join Cities c on c.CityCode=s.MyCityCode where (a.MachineNameAlias like '%" + type + "%')"
        ElseIf type = "MPSD" Then
            strCommand = "select a.MachineNameAlias,c.MyRegionCode,m.IPAddress from dbo.MachineNameAliases a join Machines m ON a.MAC=m.MAC join Stores s ON m.MyStoreID=s.ID join Cities c on c.CityCode=s.MyCityCode where (a.MachineNameAlias like '%MPSD%')"
        ElseIf type = "MPSU" Then
            strCommand = "select a.MachineNameAlias,c.MyRegionCode,m.IPAddress from dbo.MachineNameAliases a join Machines m ON a.MAC=m.MAC join Stores s ON m.MyStoreID=s.ID join Cities c on c.CityCode=s.MyCityCode where (a.MachineNameAlias like '%MPSU%')"
        End If

        Try
            sqlconnection = New SqlConnection(constring)
            sqlconnection.Open()
            If sqlconnection.State = ConnectionState.Open Then
                cmd = New SqlCommand(strCommand, sqlconnection)
                reader = cmd.ExecuteReader()
                While reader.Read()
                    tuple.Add(New Tuple(Of String, String, String)(reader(0), reader(1), reader(2)))
                End While
                Return tuple
            End If
        Catch ex As Exception

        End Try
        Return Nothing
    End Function
    Public Shared Sub PopulateSdFdb()
        Splash.Copyright.Text = "Preparing the server list..."
        Try
            Dim listQa = Readusidb("MPSQ").ToList
            Dim listDev = Readusidb("MPSD").ToList
            Dim listUat = Readusidb("MPSU").ToList
            Dim con As New utils

            For Each item In listQa
                Dim cmdstring As String = "insert into ServerInformation values ('" + item.Item3.ToString + "','" + item.Item2.ToString + "','" + item.Item1.ToString + "','' ,'""','""','""','QA','" + DateTime.Now.ToString() + "')"
                con.WriteSdFdata(cmdstring)
            Next
            For Each item In listDev
                Dim cmdstring As String = "insert into ServerInformation values ('" + item.Item3.ToString + "','" + item.Item2.ToString + "','" + item.Item1.ToString + "','' ,'""','""','""','DEV','" + DateTime.Now.ToString() + "')"
                con.WriteSdFdata(cmdstring)
            Next
            For Each item In listUat
                Dim cmdstring As String = "insert into ServerInformation values ('" + item.Item3.ToString + "','" + item.Item2.ToString + "','" + item.Item1.ToString + "','' ,'""','""','""','UAT','" + DateTime.Now.ToString() + "')"
                con.WriteSdFdata(cmdstring)
            Next
            Splash.loading = Splash.loading+1
        Catch ex As Exception

        End Try

    End Sub
    Public Shared Sub Updateserverinfo()
        Splash.Copyright.Text = "Gathering information from all servers..."
        Dim SQLstr As String = "select serverip from ServerInformation"
        Dim iplist As New List(Of String)
        Dim arr, arr1, arr2, arr3 As New ArrayList

        Try
            Dim sqLconnect As New SQLiteConnection()
            Dim sqLcommand As SQLiteCommand
            sqLconnect.ConnectionString = "Data Source=" + Filepath + ";"
            sqLconnect.Open()
            sqLcommand = New SQLiteCommand(SQLstr, sqLconnect)

            Dim sqlReader As SQLiteDataReader = sqLcommand.ExecuteReader()

            While sqlReader.Read()
                arr3.Add(sqlReader(0))
            End While
            iplist = arr3.Cast(Of String)().ToList()

            Parallel.ForEach(iplist,
                             Sub(item)
                                 If My.Computer.Network.Ping(item) Then

                                     Dim db As New SqldbManager(item,,, 1)
                                     Dim szDatabaseVersionId As SqlDataReader = db.ReadSqlData("select top 1 szDatabaseVersionID from MGIDatabaseVersionUpdate order by szDatabaseInstallDate desc, szDatabaseInstallTime desc")

                                     While szDatabaseVersionId.Read()
                                         arr.Add(szDatabaseVersionId(0))
                                     End While
                                     szDatabaseVersionId.Close()

                                     Dim euSoftwareVersionServer As SqlDataReader = db.ReadSqlData("select * from (select top 1 szVersion from EUSoftwareVersion where szPackageName like 'Hotfix%' and szWorkstationID like '%MPS%' order by szTimestamp desc) a union select '000' where (select count(*) szVersion from EUSoftwareVersion where szPackageName like 'Hotfix%' and szWorkstationID like '%MPS%')=0")

                                     While euSoftwareVersionServer.Read()
                                         arr1.Add(euSoftwareVersionServer(0))
                                     End While
                                     euSoftwareVersionServer.Close()

                                     Dim euSoftwareVersionTill As SqlDataReader = db.ReadSqlData("select * from (select top 1 szVersion from EUSoftwareVersion where szPackageName like 'Hotfix%' and szWorkstationID like '%MPC%' order by szTimestamp desc) a union select '000' where (select count(*) szVersion from EUSoftwareVersion where szPackageName like 'Hotfix%' and szWorkstationID like '%MPC%')=0")

                                     While euSoftwareVersionTill.Read()
                                         arr2.Add(euSoftwareVersionTill(0))
                                     End While
                                     euSoftwareVersionTill.Close()

                                     Dim serverhf As String
                                     Dim shortshf As String = arr1(0).ToString.Substring(arr1(0).ToString.Length - 3)

                                     If shortshf.StartsWith(".") Then
                                         serverhf = "0" + shortshf.Substring(shortshf.Length - 2)
                                     ElseIf shortshf.Contains(".") Then
                                         serverhf = "00" + shortshf.Substring(shortshf.Length - 1)
                                     Else
                                         serverhf = shortshf
                                     End If


                                     Dim tillhf As String
                                     Dim tillshf As String = arr2(0).ToString.Substring(arr2(0).ToString.Length - 3)

                                     If tillshf.StartsWith(".") Then
                                         tillhf = "0" + tillshf.Substring(tillshf.Length - 2)
                                     ElseIf tillshf.Contains(".") Then
                                         tillhf = "00" + tillshf.Substring(tillshf.Length - 1)
                                     Else
                                         tillhf = tillshf
                                     End If

                                     Dim querry As String = "update ServerInformation set serverstatus='ON',version=" + arr(0).ToString + ",hfserver=" + serverhf + ",hftill=" + tillhf + " where serverip=" + item + ""
                                     Dim conn As New utils
                                     conn.WriteSdFdata(querry)

                                 End If
                             End Sub)
            Splash.loading = Splash.loading+1
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub

End Class
