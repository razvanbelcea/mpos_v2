Imports System.Xml
Imports System
Imports System.Threading
Imports System.Collections.Generic

Public Class getServer

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
End Class