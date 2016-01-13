Imports System.Xml
Imports System
Imports System.IO

Public Class Settings

    'DESIGN AREA
    Private Sub PictureBox6_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox6.MouseMove
        PictureBox6.Image = My.Resources.back_b
        MetroLabel1.Visible = True
    End Sub
    Private Sub PictureBox6_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox6.MouseLeave
        PictureBox6.Image = My.Resources.back
        MetroLabel1.Visible = False
    End Sub
    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        Me.Close()
        Me.Dispose()
    End Sub
    'DESIGN AREA END

    Public Shared servicedisabled As Boolean = False
    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToolTip1.SetToolTip(MetroLabel5, "Leave this unchecked to improve the till loading time!")
        ToolTip1.SetToolTip(MetroToggle5, "Leave this unchecked to improve the till loading time!")
        'Button2.Enabled = False
        CheckSettings()
    End Sub

    Private Sub MetroToggle1_CheckedChanged(sender As Object, e As EventArgs) Handles MetroToggle1.CheckedChanged
        If MetroToggle1.Checked = True Then
            Main.TopMost = True
            Button2.Enabled = True
        Else
            Main.TopMost = False
            Button2.Enabled = True
        End If
    End Sub

    Private Sub MetroToggle2_CheckedChanged(sender As Object, e As EventArgs) Handles MetroToggle2.CheckedChanged
        If MetroToggle2.Checked = True Then
            Main.cred = Main.cred1
            Button2.Enabled = True
        Else
            Main.cred = Main.cred2
            Button2.Enabled = True
        End If
    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Dispose()
    End Sub

    Private Sub MetroToggle3_CheckedChanged(sender As Object, e As EventArgs) Handles MetroToggle3.CheckedChanged
        If MetroToggle3.Checked = True Then
            ServiceModuleDisable()
            Button2.Enabled = True
        ElseIf MetroToggle3.Checked = False Then
            ServiceModuleEnable()
            Button2.Enabled = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If My.Computer.FileSystem.FileExists("config.ini") Then
            ConfigIniUpdate()
            Me.Close()
        Else
            ConfigIniCreate()
            Me.Close()
        End If
    End Sub
    Public Sub ServiceModuleDisable()
        Main.servicelist.Hide()
        Main.MetroButton2.Hide()
        Main.tills.Height = 392
        Main.status.Height = 125
        Main.tills.Location = New Point(497, 199)
        Main.tilllist.Height = 361
        Main.tilllist.Location = New Point(6, 25)
        '    Main.tlb.Location = New Point(356, 340)
        Main.tpb.Location = New Point(464, 350)
        servicedisabled = True
    End Sub
    Public Sub ServiceModuleEnable()
        Main.servicelist.Show()
        Main.MetroButton2.Show()
        Main.tills.Height = 234
        Main.status.Height = 286
        Main.tills.Location = New Point(497, 357)
        Main.tilllist.Height = 203
        Main.tilllist.Location = New Point(6, 25)
        '  Main.tlb.Location = New Point(356, 169)
        Main.tpb.Location = New Point(464, 193)
        servicedisabled = False
    End Sub
    Public Sub ConfigIniCreate()
        Dim config As New IniFile
        If MetroToggle1.Checked = True Then
            config.AddSection("Settings").AddKey("AlwaysOnTop").Value = "1"
        ElseIf MetroToggle1.Checked = False Then
            config.AddSection("Settings").AddKey("AlwaysOnTop").Value = "0"
        End If
        If MetroToggle2.Checked = True Then
            config.AddSection("Settings").AddKey("SSPI").Value = "1"
        ElseIf MetroToggle2.Checked = False Then
            config.AddSection("Settings").AddKey("SSPI").Value = "0"
        End If
        If MetroToggle3.Checked = True Then
            config.AddSection("Settings").AddKey("ServiceModule").Value = "1"
        ElseIf MetroToggle3.Checked = False Then
            config.AddSection("Settings").AddKey("ServiceModule").Value = "0"
        End If
        If MetroToggle4.Checked = True Then
            config.AddSection("Settings").AddKey("Startup").Value = "1"
        ElseIf MetroToggle4.Checked = False Then
            config.AddSection("Settings").AddKey("Startup").Value = "0"
        End If
        If MetroToggle5.Checked = True Then
            config.AddSection("Settings").AddKey("printeron").Value = "1"
        ElseIf MetroToggle5.Checked = False Then
            config.AddSection("Settings").AddKey("printeron").Value = "0"
        End If
        config.Save("config.ini")
        miniTool.balon("Settings saved!")
    End Sub
    Public Sub ConfigIniUpdate()
        Dim config As New IniFile
        config.Load("config.ini")

        If MetroToggle1.Checked = True Then
            config.SetKeyValue("Settings", "AlwaysOnTop", "1")
        ElseIf MetroToggle1.Checked = False Then
            config.SetKeyValue("Settings", "AlwaysOnTop", "0")
        End If
        If MetroToggle2.Checked = True Then
            config.SetKeyValue("Settings", "SSPI", "1")
        ElseIf MetroToggle2.Checked = False Then
            config.SetKeyValue("Settings", "SSPI", "0")
        End If
        If MetroToggle3.Checked = True Then
            config.SetKeyValue("Settings", "ServiceModule", "1")
        ElseIf MetroToggle3.Checked = False Then
            config.SetKeyValue("Settings", "ServiceModule", "0")
        End If
        If MetroToggle4.Checked = True Then
            config.SetKeyValue("Settings", "Startup", "1")
        ElseIf MetroToggle4.Checked = False Then
            config.SetKeyValue("Settings", "Startup", "0")
        End If
        If MetroToggle5.Checked = True Then
            config.SetKeyValue("Settings", "printeron", "1")
        ElseIf MetroToggle5.Checked = False Then
            config.SetKeyValue("Settings", "printeron", "0")
        End If
        config.Save("config.ini")
        miniTool.balon("Settings saved!")
    End Sub
    Public Sub CheckSettings()
        Try
            If My.Computer.FileSystem.FileExists("config.ini") Then
                Dim config As New IniFile
                config.Load("config.ini")

                If config.GetKeyValue("Settings", "AlwaysOnTop") = "1" Then
                    MetroToggle1.Checked = True
                Else
                    MetroToggle1.Checked = False
                End If

                If config.GetKeyValue("Settings", "SSPI") = "1" Then
                    MetroToggle2.Checked = True
                Else
                    MetroToggle2.Checked = False
                End If

                If config.GetKeyValue("Settings", "ServiceModule") = "1" Then
                    MetroToggle3.Checked = True
                Else
                    MetroToggle3.Checked = False
                End If
                If config.GetKeyValue("Settings", "Startup") = "1" Then
                    MetroToggle4.Checked = True
                Else
                    MetroToggle4.Checked = False
                End If
                If config.GetKeyValue("Settings", "printeron") = "1" Then
                    MetroToggle5.Checked = True
                Else
                    MetroToggle5.Checked = False
                End If
            Else
                MetroToggle2.Checked = True
                miniTool.balon("Missing settings file!")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Metrotoggle4_CheckedChanged(sender As Object, e As EventArgs) Handles MetroToggle4.CheckedChanged
        Dim Directory As String = CreateObject("WScript.Shell").SpecialFolders("Startup")
        If MetroToggle4.Checked = True Then
            Main.cleanshortc("startup")
            Button2.Enabled = True
        Else
            For Each filename As String In IO.Directory.GetFiles(Directory, "*", IO.SearchOption.AllDirectories)
                If Microsoft.VisualBasic.Right(filename, 4) = ".lnk" Then
                    If My.Computer.FileSystem.FileExists(filename) Then
                        My.Computer.FileSystem.DeleteFile(filename)
                    End If
                    Button2.Enabled = True
                End If
            Next
        End If
    End Sub

    Private Sub MetroToggle5_CheckedChanged(sender As Object, e As EventArgs) Handles MetroToggle5.CheckedChanged
        If MetroToggle5.Checked = True Then
            Main.printeron = True
            Button2.Enabled = True
        Else
            Main.printeron = False
            Button2.Enabled = True
        End If
    End Sub
End Class