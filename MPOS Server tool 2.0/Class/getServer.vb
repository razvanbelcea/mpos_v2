Imports System.Xml
Imports System
Imports System.Threading
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Data.SQLite

Public Class getServer
    Public Shared Filepath As String = System.Windows.Forms.Application.StartupPath + "\Resources\utils.db3"
    Shared Sub populate(ByVal type As String, ByVal grup As Integer)
        Main.serverlist.Items.Clear()
        miniTool.ComboBox1.Items.Clear()

        Dim svl As String = "serverlist.xml"
        Dim readserver As New XmlTextReader(svl)
        Dim i As Integer = 0
        Dim s As String = ""
        readserver.ReadToFollowing(type)
        While readserver.Read()
            If readserver.NodeType = XmlNodeType.EndElement AndAlso readserver.Name = type Then
                Exit While
            End If
            Select Case readserver.NodeType
                Case XmlNodeType.Element
                    Select Case readserver.Name
                        Case "Country"
                            readserver.Read()
                            Main.serverlist.Items.Add(readserver.Value)
                            s = readserver.Value & " "
                        Case "Name"
                            readserver.Read()
                            Main.serverlist.Items(i).SubItems.Add(readserver.Value)
                            s = s & readserver.Value & " IP-"
                        Case "Ip"
                            readserver.Read()
                            Main.serverlist.Items(i).SubItems.Add(readserver.Value)
                            s = s & readserver.Value
                        Case "Location"
                            readserver.Read()
                            Main.serverlist.Items.Item(i).Group = Main.serverlist.Groups(grup)
                            s = readserver.Value & " " & s
                            miniTool.ComboBox1.Items.Add(s)
                            i = i + 1
                    End Select
            End Select
        End While
        readserver.Dispose()
        Main.ToolStripProgressBar1.Maximum = i
    End Sub
    Shared Sub populatefromdb(type As String, grup As Integer)
        Main.serverlist.Items.Clear()
        miniTool.ComboBox1.Items.Clear()

        Dim sqLconnect As New SQLiteConnection()
        Dim sqLcommand As SQLiteCommand
        Dim SQLstr As String = "select countrycode, servername, serverip, serverstatus, version, hfserver, hftill from ServerInformation where location='" + type + "'"
        sqLconnect.ConnectionString = "Data Source=" + Filepath + ";"
        sqLconnect.Open()
        sqLcommand = New SQLiteCommand(SQLstr, sqLconnect)

        Dim sqlReader As SQLiteDataReader = sqLcommand.ExecuteReader()
        Dim i = 0
        While sqlReader.Read()
            Main.serverlist.Items.Add(sqlReader(0))
            Main.serverlist.Items(i).SubItems.Add(sqlReader(1))
            Main.serverlist.Items(i).SubItems.Add(sqlReader(2))
            Main.serverlist.Items(i).SubItems.Add(sqlReader(3))
            Main.serverlist.Items(i).SubItems.Add("V:" + sqlReader(4) + "HF:" + sqlReader(5) + "/" + sqlReader(6))
            Main.serverlist.Items.Item(i).Group = Main.serverlist.Groups(grup)
            i = i + 1
        End While
    End Sub

End Class