Imports System.Data.SqlClient

Public Class SqldbManager

    Private _connection As SqlConnection
    Private ReadOnly _logger As New ErrorLogger
    Const Cred As String = "Integrated Security=SSPI"

    Public Sub New(datasource As String,
                       Optional database As String = "TPCentralDB",
                       Optional credentials As String = Cred,
                       Optional timeout As Integer = 30)
        Connect(datasource, database, credentials, timeout)
    End Sub

    Public Sub Connect(datasource As String,
                       Optional database As String = "TPCentralDB",
                       Optional credentials As String = Cred,
                       Optional timeout As Integer = 30)

        Try

            _connection = New SqlConnection("Data Source=" &
                                datasource &
                                ";Database=" & database & ";" &
                                credentials &
                                ";" &
                                "Connect Timeout=" & timeout)

            _connection.Open()

        Catch ex As Exception
            _logger.WriteToErrorLog(ex.Message, ex.StackTrace, "Connecting to database.")

            _connection = Nothing
        End Try

    End Sub

    Public Function ReadSqlData(q As String) As SqlDataReader

        Dim cmd As SqlCommand
        Dim reader As SqlDataReader

        Try
            cmd = New SqlCommand(q, _connection)
            If _connection.State = ConnectionState.Open Then
                reader = cmd.ExecuteReader()
                Return reader
            End If
        Catch ex As Exception
            _logger.WriteToErrorLog(ex.Message, ex.StackTrace, "Reading database.")
        End Try
        Return Nothing
    End Function

End Class
