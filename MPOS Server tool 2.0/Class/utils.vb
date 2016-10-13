Imports System.Data.SqlClient
Imports System.Data.SqlServerCe

Public Class utils
    Private ReadOnly _logger As New ErrorLogger
    Public Sub WriteSDFdata(q As String)
        Try
            Using con As New SqlCeConnection("Data Source=utils.sdf")
                con.Open()
                Using cmd As New SqlCeCommand(q, con)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            _logger.WriteToErrorLog(ex.Message, ex.StackTrace, "WriteSDFdata to utils.sdf")
        End Try
    End Sub

    Public Function ReadSDFdata(q As String)
        Dim reader As SqlCeDataReader = Nothing
        Dim con As SqlCeConnection = Nothing
        Dim cmd As SqlCeCommand = Nothing
        Try
            con = New SqlCeConnection("Data Source=utils.sdf")
            con.Open()
            If con.State = ConnectionState.Open Then
                cmd = New SqlCeCommand(q, con)
                reader = cmd.ExecuteReader()
                Return reader
            End If
        Catch ex As Exception
            _logger.WriteToErrorLog(ex.Message, ex.StackTrace, "ReadSDFdata to utils.sdf")
        End Try
        Return Nothing
    End Function

    Public Shared Function readusidb(type As String) As List(Of Tuple(Of String, String, String))
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
    Public Shared Sub populateSDFdb()
        Dim listQa = readusidb("MPSQ").ToList
        Dim listDev = readusidb("MPSD").ToList
        Dim listUat = readusidb("MPSU").ToList
        Dim con As New utils

        For Each item In listQa
            Dim cmdstring As String = "insert into qatab values ('" + item.Item3.ToString + "','" + item.Item2.ToString + "','" + item.Item1.ToString + "','' ,'""','""','""','" + DateTime.Now.ToString() + "')"
            con.WriteSDFdata(cmdstring)
        Next
        For Each item In listDev
            Dim cmdstring As String = "insert into devtab values ('" + item.Item3.ToString + "','" + item.Item2.ToString + "','" + item.Item1.ToString + "','' ,'""','""','""','" + DateTime.Now.ToString() + "')"
            con.WriteSDFdata(cmdstring)
        Next
        For Each item In listUat
            Dim cmdstring As String = "insert into uattab values ('" + item.Item3.ToString + "','" + item.Item2.ToString + "','" + item.Item1.ToString + "','' ,'""','""','""','" + DateTime.Now.ToString() + "')"
            con.WriteSDFdata(cmdstring)
        Next


    End Sub

End Class
