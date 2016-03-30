Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Xml
Imports System.Net
Imports System.Management
Imports System.ServiceProcess
Imports System.ComponentModel
Imports System.Threading
Imports System.Threading.Thread
Imports Microsoft.Win32
Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Security.Permissions
Imports MetroFramework

Public Class Main

    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        MetroContextMenu1.Show(MetroButton1, 0, MetroButton1.Height)
    End Sub

    Private Sub DBQueriesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DBQueriesToolStripMenuItem.Click
        If MetroLabel15.Text = "ONLINE" Then
            DBQueries.ShowDialog()
            DBQueries.Dispose()
        Else
            miniTool.balon("Please select a server!")
        End If
    End Sub

    '==========================================================================================================================
    Public Shared UID As String = "TpAdmin"
    Public Shared PSW As String = "Cawt6__56UBn_szF8_10"
    Public Shared cred1 As String = "Integrated Security=SSPI"
    Public Shared cred2 As String = "Uid=" & UID & "; Password=" & PSW
    Public Shared cred As String '= cred1
    Public Shared svl As String = "serverlist.xml"
    Public Shared ffl As String = "folderlist.xml"
    Public Shared srl As String = "servicelist.xml"
    Public Shared cfl As String = "countlist.xml"
    Public Shared hff As String = "\c$\mgi\mposinstallstate.xml"
    Public Shared x As Boolean = False
    Public Shared w As Boolean = False
    Public Shared printeron As Boolean
    Public Shared qafolder As String
    Public Shared uatfolder As String
    Public Shared Logger As New ErrorLogger
    Dim anulareserver As CancellationTokenSource
    Dim anulareservice As CancellationTokenSource
    Dim anularetills As CancellationTokenSource
    Dim anulareoperators As CancellationTokenSource
    Dim cnt As String = ""

    '-----------------------------------------------------------------------------------------------------------------------------------------BEGIN form load/unload
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If x = False Then
            NotifyIcon1.Visible = True
            Me.Hide()
            miniTool.Show()
            e.Cancel = True
        End If
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MetroRadioButton1.Checked = True
        Control.CheckForIllegalCrossThreadCalls = False
        Timer1.Interval = 5000
        Timer1.Enabled = True
        Timer1.Start()

        Try
            Dim MyReg As RegistryKey
            Dim MyVal As Object
            MyReg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Microsoft\System\DNSClient", False)
            If (Not MyReg Is Nothing) Then
                MyVal = MyReg.GetValue("PrimaryDnsSuffix")
            Else
                MyVal = "not_ro"
            End If
            If MyVal = "not_ro" Then
                miniTool.balon("XML update not needed!")
            ElseIf MyVal <> "client.ro.r4.madm.net" Then
                miniTool.balon("XML update not needed!")
            Else
                XmlVersion("sqllist")
                XmlVersion("server")
                XmlVersion("folder")
                XmlVersion("service")
                If w = True Then
                    miniTool.balon("New XML files downloaded!!!")
                End If
            End If
            MyReg.Close()
        Catch a As Exception
            Logger.WriteToErrorLog(a.Message, a.StackTrace, "PrimaryDNS missing! ")
        End Try

        Me.Show()
        If Me.TopMost = False Then
            Me.TopMost = True
            Me.TopMost = False
        End If

        'taskserver()
        cleanshortc("desktop")
        checkforupdate()
    End Sub
    '-----------------------------------------------------------------------------------------------------------------------------------------END form load/unload
    '-----------------------------------------------------------------------------------------------------------------------------------------BEGIN read xml files
    'DESIGN AREA
    Private Sub PictureBox5_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox5.MouseMove
        PictureBox5.Image = My.Resources.settings_b
        MetroLabel13.Visible = True
    End Sub

    Private Sub PictureBox5_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox5.MouseLeave
        PictureBox5.Image = My.Resources.settings
        MetroLabel13.Visible = False
    End Sub
    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        Settings.ShowDialog()
        Settings.Dispose()
    End Sub

    Private Sub PictureBox7_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox7.MouseMove
        PictureBox7.Image = My.Resources.quit_b
        MetroLabel14.Visible = True
    End Sub

    Private Sub PictureBox7_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox7.MouseLeave
        PictureBox7.Image = My.Resources.quit
        MetroLabel14.Visible = False
    End Sub
    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        x = False
        Me.Close()
    End Sub
    Private Sub PictureBox8_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox8.MouseMove
        PictureBox8.Image = My.Resources.minimize_b
        MetroLabel18.Visible = True
    End Sub

    Private Sub PictureBox8_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox8.MouseLeave
        PictureBox8.Image = My.Resources.minimize
        MetroLabel18.Visible = False
    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    'END OF DESIGN AREA
    Private Sub readfolderlist(link)
        Dim readfolder As XmlTextReader = New XmlTextReader(ffl)
        Dim i As Integer = 0
        Dim env As String = ""

        If link = "QA" Then
            env = "Path"
        ElseIf link = "UAT" Then
            env = "UATPath"
        ElseIf link = "PROD" Then
            env = "UATPath"
        ElseIf link = "DEV" Then
            env = "Path"
        End If
        Try
            folderlist.Items.Clear()
            Do While (readfolder.Read())
                Select Case readfolder.NodeType
                    Case XmlNodeType.Element
                        Select Case readfolder.Name
                            Case "Name"
                                readfolder.Read()
                                folderlist.Items.Add(readfolder.Value)
                            Case env
                                readfolder.Read()
                                folderlist.Items(i).SubItems.Add("\\" + MetroLabel8.Text + readfolder.Value)
                                i = i + 1
                        End Select
                End Select
            Loop
            readfolder.Dispose()
        Catch e As Exception
            miniTool.balon("ReadFolderList: " + e.Message)
            Logger.WriteToErrorLog(e.Message, e.StackTrace, "error")
        End Try
    End Sub
    Private Sub readservicelist()
        Dim readservice As XmlTextReader = New XmlTextReader(srl)
        Dim i As Integer = 0
        servicelist.Items.Clear()
        Try
            Do While (readservice.Read())
                Select Case readservice.NodeType
                    Case XmlNodeType.Element
                        Select Case readservice.Name
                            Case "Service"
                                readservice.Read()
                                servicelist.Items.Add(readservice.Value)
                                If MetroLabel15.Text = "ONLINE" Then
                                    ccd.Visible = True
                                    'Label6.Visible = True
                                Else
                                    servicelist.Items(i).SubItems.Add("-")
                                    servicelist.Items(i).SubItems.Add("-")
                                End If
                                i = i + 1
                        End Select
                End Select
            Loop
            readservice.Dispose()
        Catch e As Exception
            miniTool.balon("ReadServiceList: " + e.Message)
            Logger.WriteToErrorLog(e.Message, e.StackTrace, "error")
        End Try
    End Sub
    '-----------------------------------------------------------------------------------------------------------------------------------------END read xml files
    '-----------------------------------------------------------------------------------------------------------------------------------------BEGIN server selection
    Private Sub serverlist_SelectedIndexChanged(sender As Object, e As EventArgs) Handles serverlist.SelectedIndexChanged
        Dim countryIP As String = ""
        For Each country As ListViewItem In serverlist.Items
            If country.Selected = True Then
                Dim countryName As String = country.SubItems(0).Text
                Try
                    Dim doc As XmlDocument = New XmlDocument()
                    doc.Load(svl)
                    If country.Group.Name = "ListViewGroup1" Then
                        Dim saveCountry As String = doc.SelectSingleNode("List/QA/Server[Country='" + countryName + "']/Ip").InnerText
                        countryIP = saveCountry.ToString()
                    ElseIf country.Group.Name = "ListViewGroup4" Then
                        Dim saveCountry As String = doc.SelectSingleNode("List/DEV/Server[Country='" + countryName + "']/Ip").InnerText
                        countryIP = saveCountry.ToString()
                    ElseIf country.Group.Name = "ListViewGroup2" Then
                        Dim saveCountry As String = doc.SelectSingleNode("List/UAT/Server[Country='" + countryName + "']/Ip").InnerText
                        countryIP = saveCountry.ToString()
                    ElseIf country.Group.Name = "ListViewGroup3" Then
                        Dim saveCountry As String = doc.SelectSingleNode("List/PROD/Server[Country='" + countryName + "']/Ip").InnerText
                        countryIP = saveCountry.ToString()
                    End If
                    If My.Computer.Network.Ping(countryIP.ToString()) Then
                        servicelist.Items.Clear()
                        'Button1.Visible = True
                        'Button6.Visible = True
                        MetroButton2.Visible = True
                        'Button7.Visible = True
                        viewserver()
                        statusserver()
                        For Each item As ListViewItem In serverlist.SelectedItems
                            If item.Group.Name = "ListViewGroup1" Then
                                readfolderlist("QA")
                            ElseIf item.Group.Name = "ListViewGroup4" Then
                                readfolderlist("DEV")
                            Else
                                readfolderlist("UAT")
                            End If
                        Next
                        ' Set the initial sorting type for the ListView. 
                        Me.tilllist.Sorting = System.Windows.Forms.SortOrder.None
                        ' Disable automatic sorting to enable manual sorting. 
                        AddHandler tilllist.ColumnClick, AddressOf Me.tilllist_ColumnClick
                        loaddatabase()
                        loadcounts()
                    End If
                Catch ex As Exception
                    Logger.WriteToErrorLog(ex.Message, ex.StackTrace, "error")
                End Try
            End If
        Next

    End Sub
    Private Sub statusserver()
        Try
            If My.Computer.Network.Ping(MetroLabel8.Text) Then
                MetroLabel15.Text = "ONLINE"
                MetroLabel15.ForeColor = Color.Green
                MetroLabel15.Style = MetroColorStyle.Green
            Else
                MetroLabel15.Text = "OFFLINE"
                MetroLabel15.ForeColor = Color.Red
                MetroLabel15.Style = MetroColorStyle.Red
                tpb.Visible = False
                '  tlb.Visible = False
            End If
        Catch e As Exception
            miniTool.balon("StatusServer: " + e.Message)
            Logger.WriteToErrorLog(e.Message, e.StackTrace, "error")
        End Try
    End Sub
    Private Sub viewserver()
        metro.Visible = False
        ' title.Visible = False
        folderlist.Visible = True
        status.Visible = True
        folder.Visible = True
        tills.Visible = True
        operators.Visible = True
        For Each item As ListViewItem In serverlist.Items
            If item.Selected = True Then
                MetroLabel8.Text = item.SubItems(2).Text
                MetroLabel7.Text = item.SubItems(1).Text
                ' MetroLabel9.Text = item.SubItems(0).Text
                Try
                    Dim arr As New ArrayList
                    Dim arr1, arr2, arr3 As New ArrayList
                    Dim conex1 As SqlConnection
                    Dim dat As SqlDataReader
                    Dim dat1, dat2, dat3 As SqlDataReader
                    Dim cmd, cmd2, cmd3 As SqlCommand
                    Dim cmd1 As SqlCommand
                    Dim t As Boolean = False
                    conex1 = New SqlConnection("Data Source=" & item.SubItems(2).Text & ";Database=TPCentralDB;" & cred & ";")
                    cmd = conex1.CreateCommand
                    cmd1 = conex1.CreateCommand
                    cmd2 = conex1.CreateCommand
                    cmd3 = conex1.CreateCommand
                    cmd.CommandText = "select top 1 szDatabaseVersionID from MGIDatabaseVersionUpdate order by szDatabaseInstallDate desc"
                    cmd1.CommandText = "select * from (select top 1 szPackageName from EUSoftwareVersion where szResult = 'Success' and szState = 'Finished' and szPackageName like 'Hotfix%' and szWorkstationID in (select top 10 szworkstationid from workstation order by lWorkstationNmbr desc)order by szTimestamp desc) a union select 'Hotfix_00'where (select COUNT(*) from EUSoftwareVersion where szResult = 'Success' and szState = 'Finished' and szPackageName like 'Hotfix%' and szWorkstationID in (select top 10 szworkstationid from workstation order by lWorkstationNmbr desc))=0"
                    cmd2.CommandText = "select top 1 szVersion from EUSoftwareVersion where szPackageName = 'Metro_Common_TPDotnetSetupPos'"
                    cmd3.CommandText = "select top 1 szCountryCode from MGIExternalStore"
                    conex1.Open()
                    If conex1.State = ConnectionState.Open Then
                        dat = cmd.ExecuteReader()
                        While dat.Read()
                            arr.Add(dat(0))
                        End While
                        dat.Close()
                        dat1 = cmd1.ExecuteReader()
                        While dat1.Read()
                            arr1.Add(dat1(0))
                        End While
                        dat1.Close()
                        dat2 = cmd2.ExecuteReader()
                        While dat2.Read()
                            arr2.Add(dat2(0))
                        End While
                        dat2.Close()
                        dat3 = cmd3.ExecuteReader()
                        While dat3.Read()
                            arr3.Add(dat3(0))
                        End While
                        dat3.Close()
                        conex1.Close()
                        t = True
                    ElseIf conex1.State = ConnectionState.Closed Then
                        miniTool.balon("ViewServer: Database offline...")
                    End If
                    MetroLabel11.Text = arr(0).ToString + " " + arr1(0).ToString
                    MetroLabel20.Text = arr2(0).ToString
                    MetroLabel9.Text = arr3(0).ToString
                Catch a As Exception
                    'Form7.balon(a.Message)
                    Logger.WriteToErrorLog(a.Message, a.StackTrace, "error")
                End Try
                Exit For
            End If
        Next
    End Sub
    '-----------------------------------------------------------------------------------------------------------------------------------------END server selection
    '-----------------------------------------------------------------------------------------------------------------------------------------BEGIN folder selection
    Private Sub folderlist_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles folderlist.ItemSelectionChanged
        Try
            For Each item As ListViewItem In folderlist.Items
                If item.Selected = True AndAlso My.Computer.FileSystem.DirectoryExists(item.SubItems(1).Text) Then
                    Process.Start("explorer.exe", item.SubItems(1).Text)
                    folderlist.Refresh()
                    Exit For
                ElseIf item.Selected = True AndAlso Not My.Computer.FileSystem.DirectoryExists(item.SubItems(1).Text) Then
                    miniTool.balon("Location not found ...")
                End If
            Next
        Catch ey As Exception
            miniTool.balon("FolderList: " + ey.Message)
            Logger.WriteToErrorLog(ey.Message, ey.StackTrace, "error")
        End Try
    End Sub
    '-----------------------------------------------------------------------------------------------------------------------------------------END folder selection
    '-----------------------------------------------------------------------------------------------------------------------------------------BEGIN load stuff
    Private Sub loaddatabase()
        operatorlist.Items.Clear()
        tilllist.Items.Clear()
        Dim con As SqlConnection
        con = New SqlConnection("Data Source=" & MetroLabel8.Text & ";Database=TPCentralDB;" & cred & ";")
        If MetroLabel15.Text = "ONLINE" Then
            Try
                con.Open()
            Catch e As Exception
                Logger.WriteToErrorLog(e.Message, e.StackTrace, "error")
            End Try
            If con.State = ConnectionState.Open Then
                con.Close()
                MetroLabel16.Text = "DB is ONLINE"
                MetroLabel16.ForeColor = Color.Green
                MetroLabel16.Style = MetroColorStyle.Green
                tasktills()
                taskoperators()
            Else
                MetroLabel16.Text = "DB is OFFLINE"
                MetroLabel16.ForeColor = Color.Red
                MetroLabel16.Style = MetroColorStyle.Red
                tpb.Visible = False
                '  tlb.Visible = False
            End If
        Else
            MetroLabel16.Text = "DB is OFF"
            MetroLabel16.ForeColor = Color.Red
            MetroLabel16.Style = MetroColorStyle.Red
            tpb.Visible = False
            '   tlb.Visible = False
        End If
    End Sub
    Private Sub loadoperators(tokenoperators As CancellationToken)
        Dim i As Integer = 0
        Dim con1 As SqlConnection
        Dim cmd1 As SqlCommand
        Dim dat1 As SqlDataReader
        Try
            con1 = New SqlConnection("Data Source=" & MetroLabel8.Text & ";Database=TPCentralDB;" & cred & ";")
            cmd1 = con1.CreateCommand
            con1.Open()
            cmd1.CommandText = "select lOperatorID,szname from OperatorProfileAffiliation as a join profile as b on a.lProfileID=b.lProfileID"
            dat1 = cmd1.ExecuteReader()
            While dat1.Read()
                If tokenoperators.IsCancellationRequested Then
                    tokenoperators.ThrowIfCancellationRequested()
                End If
                operatorlist.Items.Add(dat1(0))
                operatorlist.Items(i).SubItems.Add(dat1(1))
                i = i + 1
            End While
            dat1.Close()
            con1.Close()
        Catch e As Exception
            'Logger.WriteToErrorLog(e.Message, e.StackTrace, "error")
        End Try
    End Sub
    Private Sub loadtills(tokentills As CancellationToken)
        Dim ff As String
        Dim i As Integer = 0
        Dim arr As Array = {"-", "-", "-", "-", "-", "-", "-"}
        Dim con As SqlConnection
        Dim cmd As SqlCommand
        Dim dat As SqlDataReader
        Try
            tilllist.HeaderStyle = ColumnHeaderStyle.None
            con = New SqlConnection("Data Source=" & MetroLabel8.Text & ";Database=TPCentralDB;" & cred & ";")
            cmd = con.CreateCommand
            cmd.CommandText = "select szWorkstationID,lWorkstationNmbr, szWorkstationGroupID,lOperatorID from workstation left join operator on lLoggedOnWorkstationNmbr=lWorkstationNmbr where bisthickpos<>0"
            con.Open()
            dat = cmd.ExecuteReader()
            ' tpb.Visible = True === causes application to freeze/ fix needed 
            While dat.Read()
                arr = {"-", "-", "-", "-", "-", "-", "-"}
                If tokentills.IsCancellationRequested Then
                    tokentills.ThrowIfCancellationRequested()
                End If
                arr(0) = dat(0)
                arr(1) = dat(1)
                arr(2) = dat(2)
                If Not IsDBNull(dat(3)) Then
                    arr(3) = dat(3)
                End If
                Try
                    arr(4) = System.Net.Dns.GetHostEntry(arr(0) & ".MPOS.MADM.NET").AddressList(0).ToString()
                Catch e As Exception
                    Logger.WriteToErrorLog(e.Message, e.StackTrace, "error")
                End Try
                Try
                    If arr(4) <> "-" AndAlso My.Computer.Network.Ping(arr(4)) Then
                        arr(5) = "ON"
                    Else
                        arr(5) = "OFF"
                    End If
                Catch e As Exception
                    ' Logger.WriteToErrorLog(e.Message, e.StackTrace, "error")
                End Try
                If printeron = True Then
                    Try
                        For Each item As ListViewItem In serverlist.SelectedItems
                            If item.Group.Name = "ListViewGroup1" Then
                                ff = "\\" & arr(4) & "\e$\TpDotnet\cfg\Metro.MPOS.PrintProcessor." & MetroLabel9.Text & ".xml"
                            Else
                                ff = "\\" & arr(4) & "\TPServer\cfg\Metro.MPOS.PrintProcessor." & MetroLabel9.Text & ".xml"
                            End If

                            If arr(4) <> "-" AndAlso arr(5) = "ON" AndAlso My.Computer.FileSystem.FileExists(ff) Then
                                Dim readprinter As XmlTextReader = New XmlTextReader(ff)
                                Do While (readprinter.Read())
                                    Select Case readprinter.NodeType
                                        Case XmlNodeType.Element
                                            If readprinter.Name = "PrinterType" Then
                                                readprinter.Read()
                                                If readprinter.Value >= 0 And readprinter.Value <= 2 Then
                                                    arr(6) = readprinter.Value
                                                End If
                                            End If
                                    End Select
                                Loop
                                readprinter.Dispose()
                            End If
                        Next
                    Catch e As Exception
                        Logger.WriteToErrorLog(e.Message, e.StackTrace, "error")
                    End Try
                Else
                    arr(6) = "?"
                End If
                tilllist.Items.Add(i)
                If arr(5) = "ON" Then
                    tilllist.Items(i).ForeColor = Color.Green
                ElseIf arr(5) = "OFF" Then
                    tilllist.Items(i).ForeColor = Color.Red
                End If
                For Each item In arr
                    tilllist.Items(i).SubItems.Add(item)
                Next
                i = i + 1
            End While
            dat.Dispose()
            con.Dispose()
            tilllist.HeaderStyle = ColumnHeaderStyle.Clickable
        Catch e As Exception
            'Form7.balon(e.Message)
            ' Logger.WriteToErrorLog(e.Message, e.StackTrace, "error")
        End Try
        ' tpb.Visible = False === causes application to freeze/ fix needed 
    End Sub
    Private Sub loadcounts()
        Dim readcounts As XmlTextReader = New XmlTextReader(cfl)
        Dim con3 As New SqlConnection("Data Source=" & MetroLabel8.Text & ";Database=TPCentralDB;" & cred & ";")
        Dim cmd3 As New SqlCommand
        Dim dat3 As SqlDataReader
        cnt = vbTab & vbTab & vbTab & vbTab & vbTab & vbCrLf
        Try
            If MetroLabel15.Text = "ONLINE" Then
                con3.Open()
                cmd3 = con3.CreateCommand
                cmd3.CommandText = "select lRetailStoreID from workstation where szWorkstationID='" & MetroLabel7.Text & "'"
                dat3 = cmd3.ExecuteReader()
                dat3.Read()
                MetroLabel10.Text = dat3(0)
                dat3.Close()
                For Each item As ListViewItem In serverlist.Items
                    If item.Selected = True Then
                        Try
                            Do While (readcounts.Read())
                                Select Case readcounts.NodeType
                                    Case XmlNodeType.Element
                                        Select Case readcounts.Name
                                            Case "Name"
                                                readcounts.Read()
                                                cnt = cnt & readcounts.Value & " : "
                                            Case "Sql"
                                                readcounts.Read()
                                                cmd3 = con3.CreateCommand
                                                cmd3.CommandText = readcounts.Value
                                                dat3 = cmd3.ExecuteReader()
                                                dat3.Read()
                                                cnt = cnt & dat3(0) & vbCrLf
                                                dat3.Close()
                                        End Select
                                End Select
                            Loop
                            readcounts.Dispose()
                        Catch e As Exception
                            miniTool.balon(e.Message)
                        End Try
                        Exit For
                    End If
                Next
                cmd3.Dispose()
                con3.Close()
            End If
            cnt = cnt & vbCrLf & vbTab & vbTab & vbTab & vbTab & vbTab
        Catch ex As Exception
            miniTool.balon(ex.Message)
            Logger.WriteToErrorLog(ex.Message, ex.StackTrace, "error")
            cmd3.Dispose()
            con3.Close()
        End Try
    End Sub
    Private Sub loadservice(tokenservice As CancellationToken)
        Dim i As Integer = 0
        ccd.Visible = False
        ' Label6.Visible = False
        If MetroLabel15.Text = "ONLINE" Then
            ccd.Visible = True
            '  Label6.Visible = True
            For Each item As ListViewItem In servicelist.Items
                tokenservice.ThrowIfCancellationRequested()
                Try
                    Dim serv As New ServiceController(item.SubItems(0).Text, MetroLabel7.Text & ".mpos.madm.net")
                    item.SubItems.Add(serv.DisplayName)
                    item.SubItems.Add(serv.Status.ToString)
                    serv.Dispose()
                Catch a As Exception
                    Logger.WriteToErrorLog(a.Message, a.StackTrace, "error")
                    item.SubItems.Add("-")
                    item.SubItems.Add("-")
                End Try
            Next
            ccd.Visible = False
            '   Label6.Visible = False
        End If
    End Sub
    Private Sub loadping(tokenserver As CancellationToken)
        Dim i As Integer = 0
        Dim msg As String = ""
        ' Dim MyReg As RegistryKey
        ' Dim MyVal As Object
        ' sss.Visible = True
        Try
            For Each item As ListViewItem In serverlist.Items
                tokenserver.ThrowIfCancellationRequested()
                If My.Computer.Network.Ping(item.SubItems(2).Text) Then
                    item.ForeColor = Color.Green
                    item.SubItems.Add("ON")
                    Try
                        Dim arr As New ArrayList
                        Dim arr1 As New ArrayList
                        Dim conex1 As SqlConnection
                        Dim cmd, cmd1 As SqlCommand
                        Dim dat, dat1 As SqlDataReader
                        Dim t As Boolean = False

                        conex1 = New SqlConnection("Data Source=" & item.SubItems(2).Text & ";Database=TPCentralDB;" & cred & ";")
                        cmd = conex1.CreateCommand
                        cmd1 = conex1.CreateCommand
                        cmd.CommandText = "select top 1 szDatabaseVersionID from MGIDatabaseVersionUpdate order by szDatabaseInstallDate desc"
                        cmd1.CommandText = "select * from (select top 1 szPackageName from EUSoftwareVersion where szResult = 'Success' and szState = 'Finished' and szPackageName like 'Hotfix%' and szWorkstationID in (select top 10 szworkstationid from workstation order by lWorkstationNmbr desc)order by szTimestamp desc) a union select 'Hotfix_00'where (select COUNT(*) from EUSoftwareVersion where szResult = 'Success' and szState = 'Finished' and szPackageName like 'Hotfix%' and szWorkstationID in (select top 10 szworkstationid from workstation order by lWorkstationNmbr desc))=0"
                        conex1.Open()
                        If conex1.State = ConnectionState.Open Then
                            dat = cmd.ExecuteReader()
                            While dat.Read()
                                arr.Add(dat(0))
                            End While
                            dat.Close()
                            dat1 = cmd1.ExecuteReader()
                            While dat1.Read()
                                arr1.Add(dat1(0))
                            End While
                            dat.Close()
                            conex1.Close()
                            t = True
                        ElseIf conex1.State = ConnectionState.Closed Then
                            miniTool.balon("DB Offline")
                        End If
                        item.SubItems.Add(arr(0).ToString + " " + arr1(0).ToString)
                    Catch a As Exception
                        'Form7.balon(a.Message)
                        Logger.WriteToErrorLog(a.Message, a.StackTrace, "error")
                    End Try
                    item.SubItems.Add("-")
                    item.SubItems.Add("-")
                    item.SubItems.Add("-")
                Else
                    item.ForeColor = Color.Red
                    item.SubItems.Add("OFF")
                    item.SubItems.Add("-")
                    item.SubItems.Add("-")
                    item.SubItems.Add("-")
                    item.SubItems.Add("-")
                End If
                i = i + 1
                ToolStripProgressBar1.Value = i
            Next
            ToolStripProgressBar1.Value = 0
            'sss.Visible = False
        Catch ex As Exception
        End Try
    End Sub
    '-----------------------------------------------------------------------------------------------------------------------------------------END load stuff
    '-----------------------------------------------------------------------------------------------------------------------------------------BEGIN tasks 
    Private Sub taskserver(ByVal type, ByVal group)
        If anulareserver IsNot Nothing Then
            anulareserver.Cancel()
        End If
        ' readserverlist()
        getServer.populate(type, group)
        anulareserver = New CancellationTokenSource
        Dim tokenserver = anulareserver.Token
        Task.Factory.StartNew(Sub() loadping(tokenserver), tokenserver)
    End Sub
    Private Sub taskservice()
        If anulareservice IsNot Nothing Then
            anulareservice.Cancel()
        End If
        readservicelist()
        anulareservice = New CancellationTokenSource
        Dim tokenservice = anulareservice.Token
        Task.Factory.StartNew(Sub() loadservice(tokenservice), tokenservice)
    End Sub
    Private Sub taskoperators()
        If anulareoperators IsNot Nothing Then
            anulareoperators.Cancel()
        End If
        anulareoperators = New CancellationTokenSource
        Dim tokenoperators = anulareoperators.Token
        Task.Factory.StartNew(Sub() loadoperators(tokenoperators), tokenoperators)
    End Sub
    Private Sub tasktills()
        If anularetills IsNot Nothing Then
            anularetills.Cancel()
        End If
        anularetills = New CancellationTokenSource
        Dim tokentills = anularetills.Token
        Task.Factory.StartNew(Sub() loadtills(tokentills), tokentills)
    End Sub
    '-----------------------------------------------------------------------------------------------------------------------------------------END tasks
    '-----------------------------------------------------------------------------------------------------------------------------------------BEGIN menus
    Private Sub logoffoperator()
        Dim con As New SqlConnection("Data Source=" & MetroLabel8.Text & ";Database=TPCentralDB;" & cred & ";")
        Dim cmd As New SqlCommand
        Try
            con.Open()
            If MetroLabel15.Text = "ONLINE" Then
                For Each item As ListViewItem In tilllist.Items
                    If item.Selected = True Then
                        cmd = New SqlCommand("update operator set lLoggedOnWorkstationNmbr=0 where loperatorid=" & item.SubItems(4).Text, con)
                        Try
                            miniTool.balon("Operator has been forced logged off !" & vbCrLf & cmd.ExecuteNonQuery().ToString & " row/s have been updated")
                        Catch eg As Exception
                            miniTool.balon(eg.Message)
                            Logger.WriteToErrorLog(eg.Message, eg.StackTrace, "error")
                        End Try
                        cmd.Dispose()
                        Exit For
                    End If
                Next
            End If
            con.Close()
        Catch ex As Exception
            miniTool.balon(ex.Message)
            Logger.WriteToErrorLog(ex.Message, ex.StackTrace, "error")
        End Try
    End Sub
    Private Sub restarttill()
        For Each item As ListViewItem In tilllist.Items
            If item.Selected = True Then
                Try
                    Process.Start("cmd.exe", "/c shutdown -r -t 0 -m \\" & item.SubItems(5).Text)
                    miniTool.balon("Restart command has been sent!")
                Catch ed As Exception
                    miniTool.balon(ed.Message)
                    Logger.WriteToErrorLog(ed.Message, ed.StackTrace, "error")
                End Try
                Exit For
            End If
        Next
    End Sub
    Private Sub resetoperator()
        Dim con As New SqlConnection("Data Source=" & MetroLabel8.Text & ";Database=TPCentralDB;" & cred & ";")
        Dim cmd As New SqlCommand
        Try
            If MetroLabel15.Text = "ONLINE" Then
                con.Open()
                For Each item As ListViewItem In operatorlist.Items
                    If item.Selected = True AndAlso item.SubItems(0).Text <> "-" Then
                        cmd = New SqlCommand("update Operator set szSignOnPassword='PkaIqJt8znE=',szPasswordExpirationDate='20161111',bPasswordChangeFlag=0 where lOperatorID='" & item.SubItems(0).Text & "'", con)
                        Try
                            miniTool.balon("Operator password has been reset to : 123" & vbCrLf & cmd.ExecuteNonQuery().ToString & "  row/s have been updated")
                        Catch
                        End Try
                        cmd.Dispose()
                        Exit For
                    End If
                Next
                con.Close()
            End If
        Catch ew As Exception
            miniTool.balon(ew.Message)
            Logger.WriteToErrorLog(ew.Message, ew.StackTrace, "error")
        End Try
    End Sub
    Private Sub ResetOperatorPassword123ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetOperatorPassword123ToolStripMenuItem.Click
        resetoperator()
    End Sub
    Private Sub RefreshToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem2.Click
        loaddatabase()
    End Sub
    Private Sub RefreshDBToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshDBToolStripMenuItem.Click
        loaddatabase()
    End Sub
    Private Sub RestartTillToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartTillToolStripMenuItem.Click
        Dim result As Integer = MessageBox.Show("Are you sure you want to reboot the till ?", "Reboot till", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            restarttill()
        Else
        End If
    End Sub
    Private Sub ForceSignOutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ForceSignOutToolStripMenuItem.Click
        logoffoperator()
    End Sub
    Private Sub RefreshStatusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshStatusToolStripMenuItem.Click
        For Each item As ListViewItem In serverlist.SelectedItems
            If item.Group.Name = "ListViewGroup1" Then
                taskserver("QA", 0)
            ElseIf item.Group.Name = "ListViewGroup2" Then
                taskserver("UAT", 1)
            ElseIf item.Group.Name = "ListViewGroup4" Then
                taskserver("DEV", 3)
            ElseIf item.Group.Name = "ListViewGroup3" Then
                taskserver("PROD", 2)
            End If
        Next
    End Sub
    'Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
    '    'about.ShowDialog()
    'End Sub
    Private Sub RefreshToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem1.Click
        taskservice()
    End Sub
    Private Sub RestartServiceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartServiceToolStripMenuItem.Click
        Try
            For Each item As ListViewItem In servicelist.Items
                If item.Selected AndAlso MetroLabel15.Text = "ONLINE" Then
                    Dim serv As New ServiceController(item.SubItems(0).Text, MetroLabel7.Text & ".mpos.madm.net")
                    serv.Stop()
                    Sleep(200)
                    serv.Start()
                End If
            Next
            taskservice()
        Catch ed As Exception
            miniTool.balon(ed.Message)
            Logger.WriteToErrorLog(ed.Message, ed.StackTrace, "error")
        End Try
    End Sub
    Private Sub StartStopServiceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartStopServiceToolStripMenuItem.Click
        Try
            For Each item As ListViewItem In servicelist.Items
                If item.Selected AndAlso MetroLabel15.Text = "ONLINE" Then
                    Dim serv As New ServiceController(item.SubItems(0).Text, MetroLabel7.Text & ".mpos.madm.net")
                    If serv.Status = ServiceProcess.ServiceControllerStatus.Running Then
                        serv.Stop()
                    Else
                        serv.Start()
                    End If
                End If
            Next
            taskservice()
        Catch ed As Exception
            miniTool.balon(ed.Message)
            Logger.WriteToErrorLog(ed.Message, ed.StackTrace, "error")
        End Try
    End Sub
    Private Sub CreateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateToolStripMenuItem.Click
        Dim ff As String
        For Each item As ListViewItem In serverlist.SelectedItems
            If item.Group.Name = "ListViewGroup1" Then
                ff = "\\" & MetroLabel8.Text & "\e$\TpDotnet\cfg\Metro.Mpos.Router.xml"
            Else
                ff = "\\" & MetroLabel8.Text & "\TPServer\cfg\Metro.Mpos.Router.xml"
            End If
            If MetroLabel15.Text = "ONLINE" Then
                My.Computer.FileSystem.CopyFile("Metro.Mpos.Router.xml", ff, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
                My.Computer.FileSystem.WriteAllText(ff, My.Computer.FileSystem.ReadAllText(ff).Replace("XXX", MetroLabel8.Text), False)
                My.Computer.FileSystem.WriteAllText(ff, My.Computer.FileSystem.ReadAllText(ff).Replace("YYY", MetroLabel10.Text), False)
                System.Diagnostics.Process.Start("Notepad.Exe", ff)
            End If
        Next
    End Sub
    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click
        Dim ff As String
        For Each item As ListViewItem In serverlist.SelectedItems
            If item.Group.Name = "ListViewGroup1" Then
                ff = "\\" & MetroLabel8.Text & "\e$\TpDotnet\cfg\Metro.Mpos.Router.xml"
            Else
                ff = "\\" & MetroLabel8.Text & "\TPServer\cfg\Metro.Mpos.Router.xml"
            End If
            If MetroLabel15.Text = "ONLINE" AndAlso My.Computer.FileSystem.FileExists(ff) Then
                System.Diagnostics.Process.Start("Notepad.Exe", ff)
            Else
                miniTool.balon("File not found.")
            End If
        Next
    End Sub
    Private Sub NoneToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NoneToolStripMenuItem.Click
        Dim ff As String
        For Each item As ListViewItem In tilllist.Items
            If item.Selected AndAlso MetroLabel15.Text = "ONLINE" Then
                ff = "\\" & item.SubItems(5).Text & "\e$\TpDotnet\cfg\Metro.MPOS.PrintProcessor." & MetroLabel9.Text & ".xml"
                My.Computer.FileSystem.WriteAllText(ff, My.Computer.FileSystem.ReadAllText(ff).Replace("<PrinterType>" & item.SubItems(7).Text & "</PrinterType>", "<PrinterType>0</PrinterType>"), False)
                miniTool.balon("Printer set to none")
            End If
        Next
    End Sub
    Private Sub MatrixToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MatrixToolStripMenuItem.Click
        Dim ff As String
        For Each item As ListViewItem In tilllist.Items
            If item.Selected AndAlso MetroLabel15.Text = "ONLINE" Then
                ff = "\\" & item.SubItems(5).Text & "\e$\TpDotnet\cfg\Metro.MPOS.PrintProcessor." & MetroLabel9.Text & ".xml"
                My.Computer.FileSystem.WriteAllText(ff, My.Computer.FileSystem.ReadAllText(ff).Replace("<PrinterType>" & item.SubItems(7).Text & "</PrinterType>", "<PrinterType>1</PrinterType>"), False)
                miniTool.balon("Printer set to Matrix.")
            End If
        Next
    End Sub
    Private Sub LaserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaserToolStripMenuItem.Click
        Dim ff As String
        For Each item As ListViewItem In tilllist.Items
            If item.Selected AndAlso MetroLabel15.Text = "ONLINE" Then
                ff = "\\" & item.SubItems(5).Text & "\e$\TpDotnet\cfg\Metro.MPOS.PrintProcessor." & MetroLabel9.Text & ".xml"
                My.Computer.FileSystem.WriteAllText(ff, My.Computer.FileSystem.ReadAllText(ff).Replace("<PrinterType>" & item.SubItems(7).Text & "</PrinterType>", "<PrinterType>2</PrinterType>"), False)
                miniTool.balon("Printer set to Laser.")
            End If
        Next
    End Sub
    Private Sub GetServerLogsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GetServerLogsToolStripMenuItem.Click
        miniTool.moveserverlog(0)
    End Sub
    Private Sub GetLogsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GetLogsToolStripMenuItem.Click
        miniTool.movetilllog(0)
    End Sub
    Private Sub MSTSCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MstscToolStripMenuItem.Click
        System.Diagnostics.Process.Start("mstsc.exe", "/v " & MetroLabel8.Text)
    End Sub
    Private Sub ContextMenuStrip1_Closing(sender As Object, e As ToolStripDropDownClosingEventArgs) Handles ContextMenuStrip1.Closing
        SetPrinterToolStripMenuItem.Visible = False
        ForceSignOutToolStripMenuItem.Visible = False
        RestartTillToolStripMenuItem.Visible = False
        OpenInSCCMToolStripMenuItem1.Visible = False
        MstscToolStripMenuItem.Visible = False
        GetLogsToolStripMenuItem.Visible = False
    End Sub
    Private Sub ContextMenuStrip1_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStrip1.Opening
        Try
            NoneToolStripMenuItem.Checked = False
            MatrixToolStripMenuItem.Checked = False
            LaserToolStripMenuItem.Checked = False
            SetPrinterToolStripMenuItem.Visible = False
            ForceSignOutToolStripMenuItem.Visible = False
            RestartTillToolStripMenuItem.Visible = False
            OpenInSCCMToolStripMenuItem1.Visible = False
            MstscToolStripMenuItem1.Visible = False
            GetLogsToolStripMenuItem.Visible = False
            For Each item As ListViewItem In tilllist.Items
                If item.Selected AndAlso MetroLabel15.Text = "ONLINE" Then
                    If item.SubItems(5).Text <> "-" AndAlso item.SubItems(6).Text = "ON" Then
                        RestartTillToolStripMenuItem.Visible = True
                        OpenInSCCMToolStripMenuItem1.Visible = True
                        MstscToolStripMenuItem1.Visible = True
                        GetLogsToolStripMenuItem.Visible = True
                    ElseIf item.SubItems(5).Text <> "-" Then
                        RestartTillToolStripMenuItem.Visible = True
                        OpenInSCCMToolStripMenuItem1.Visible = True
                        MstscToolStripMenuItem1.Visible = True
                        GetLogsToolStripMenuItem.Visible = True
                    End If
                    If item.SubItems(4).Text <> "-" Then
                        ForceSignOutToolStripMenuItem.Visible = True
                    End If
                    If item.SubItems(7).Text = "-" Then
                        SetPrinterToolStripMenuItem.Visible = False
                    ElseIf item.SubItems(7).Text = "?" Then
                        SetPrinterToolStripMenuItem.Visible = False
                    Else
                        SetPrinterToolStripMenuItem.Visible = True
                    End If
                    If item.SubItems(7).Text = "0" Then
                        NoneToolStripMenuItem.Checked = True
                        MatrixToolStripMenuItem.Checked = False
                        LaserToolStripMenuItem.Checked = False
                    ElseIf item.SubItems(7).Text = "1" Then
                        NoneToolStripMenuItem.Checked = False
                        MatrixToolStripMenuItem.Checked = True
                        LaserToolStripMenuItem.Checked = False
                    ElseIf item.SubItems(7).Text = "2" Then
                        NoneToolStripMenuItem.Checked = False
                        MatrixToolStripMenuItem.Checked = False
                        LaserToolStripMenuItem.Checked = True
                    End If
                    Exit For
                End If
            Next
        Catch ex As Exception
            miniTool.balon(ex.Message)
            Logger.WriteToErrorLog(ex.Message, ex.StackTrace, "error")
        End Try
    End Sub
    Private Sub OpenMPOSToolAppToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenMPOSToolAppToolStripMenuItem.Click
        miniTool.Hide()
        Me.Show()
        If Me.TopMost = False Then
            Me.TopMost = True
            Me.TopMost = False
        End If
    End Sub
    Private Sub OpenInSCCMToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles OpenInSCCMToolStripMenuItem1.Click
        Try
            For Each item As ListViewItem In tilllist.Items
                If item.Selected = True Then
                    If Environment.Is64BitOperatingSystem = False Then
                        Process.Start("C:\Program Files\Microsoft Configuration Manager\AdminConsole\bin\i386\CmRcViewer.exe", item.SubItems(5).Text)
                    Else
                        Process.Start("C:\Program Files (x86)\Microsoft Configuration Manager\AdminConsole\bin\i386\CmRcViewer.exe", item.SubItems(5).Text)
                    End If
                    Exit For
                End If
            Next
        Catch ed As Exception
            miniTool.balon(ed.Message)
            Logger.WriteToErrorLog(ed.Message, ed.StackTrace, "error")
        End Try
    End Sub
    Private Sub ExitMPOSToolAppToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitMPOSToolAppToolStripMenuItem.Click
        x = True
        Me.Close()
    End Sub
    Private Sub OpenInSCCMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenInSCCMToolStripMenuItem.Click
        Try
            For Each item As ListViewItem In serverlist.Items
                If item.Selected = True Then
                    If Environment.Is64BitOperatingSystem = False Then
                        Process.Start("C:\Program Files\Microsoft Configuration Manager\AdminConsole\bin\i386\CmRcViewer.exe", item.SubItems(5).Text)
                    Else
                        Process.Start("C:\Program Files (x86)\Microsoft Configuration Manager\AdminConsole\bin\i386\CmRcViewer.exe", item.SubItems(5).Text)
                    End If
                    Exit For
                End If
            Next
        Catch ed As Exception
            miniTool.balon(ed.Message)
            Logger.WriteToErrorLog(ed.Message, ed.StackTrace, "error")
        End Try
    End Sub
    Private Sub ContextMenuStrip4_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStrip4.Opening
        Try
            OpenInSCCMToolStripMenuItem.Visible = False
            GetServerLogsToolStripMenuItem.Visible = False
            MstscToolStripMenuItem.Visible = False
            For Each item As ListViewItem In serverlist.Items
                If item.Selected = True And item.SubItems.Count > 3 Then
                    If item.SubItems(3).Text = "ON" Then
                        OpenInSCCMToolStripMenuItem.Visible = True
                        GetServerLogsToolStripMenuItem.Visible = True
                        MstscToolStripMenuItem.Visible = True
                        Exit For
                    End If
                End If
            Next
        Catch ed As Exception
            miniTool.balon(ed.Message)
            Logger.WriteToErrorLog(ed.Message, ed.StackTrace, "error")
        End Try
    End Sub
    Private Sub ContextMenuStrip2_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStrip2.Opening
        ResetOperatorPassword123ToolStripMenuItem.Visible = False
        For Each item As ListViewItem In operatorlist.Items
            If item.Selected Then
                ResetOperatorPassword123ToolStripMenuItem.Visible = True
                Exit For
            End If
        Next
    End Sub
    Private Sub ContextMenuStrip3_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStrip3.Opening
        Try
            RestartServiceToolStripMenuItem.Visible = False
            StartStopServiceToolStripMenuItem.Visible = False
            For Each item As ListViewItem In servicelist.Items
                If item.Selected And MetroLabel15.Text = "ONLINE" And item.SubItems.Count > 1 Then
                    If item.SubItems(2).Text <> "-" Then
                        RestartServiceToolStripMenuItem.Visible = True
                        StartStopServiceToolStripMenuItem.Visible = True
                        Exit For
                    End If
                End If
            Next
        Catch ed As Exception
            miniTool.balon(ed.Message)
            Logger.WriteToErrorLog(ed.Message, ed.StackTrace, "error")
        End Try
    End Sub
    '-----------------------------------------------------------------------------------------------------------------------------------------END menus
    '-----------------------------------------------------------------------------------------------------------------------------------------BEGIN minimize
    Private Sub NotifyIcon1_MouseClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If Me.Visible Then
                Me.Hide()
                miniTool.Show()
            Else
                miniTool.Hide()
                Me.Show()
                If Me.TopMost = False Then
                    Me.TopMost = True
                    Me.TopMost = False
                End If
            End If
        End If
    End Sub
    '-----------------------------------------------------------------------------------------------------------------------------------------END minimize
    '-----------------------------------------------------------------------------------------------------------------------------------------BEGIN hover
    Private Sub ContextMenuStrip6_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStrip6.Opening
        Dim ff As String
        ff = "\\" & MetroLabel8.Text & "\e$\TpDotnet\cfg\Metro.Mpos.Router.xml"
        If MetroLabel15.Text = "ONLINE" AndAlso My.Computer.FileSystem.FileExists(ff) Then
            CreateToolStripMenuItem.Text = "Overwrite"
        Else
            CreateToolStripMenuItem.Text = "Create"
        End If
    End Sub
    Private Sub MetroLabel7_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles MetroLabel7.LinkClicked
        My.Computer.Clipboard.SetText(MetroLabel9.Text & " : " & MetroLabel7.Text & " : " & MetroLabel8.Text)
    End Sub
    Private Sub MetroLabel8_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles MetroLabel8.LinkClicked
        System.Diagnostics.Process.Start("mstsc.exe", "/v " & MetroLabel8.Text)

    End Sub
    '-----------------------------------------------------------------------------------------------------------
    Public Sub ActualVersion()
        Dim cversion As String = Application.ProductVersion
        MetroLabel17.Text = "Version : " & cversion.ToString
    End Sub
    Shared Sub DeleteOldVersion()
        Dim smallArr As New ArrayList()
        Dim path As String = "oldversion.txt"
        Dim execurrentversion As String = Application.ProductVersion
        Try
            If My.Computer.FileSystem.FileExists("oldversion.txt") Then
                Using sr As StreamReader = File.OpenText(path)
                    Do While sr.Peek() >= 0
                        smallArr.Add(sr.ReadLine())
                    Loop
                End Using
                If execurrentversion > smallArr(0) Then
                    If My.Computer.FileSystem.FileExists(Application.StartupPath + "/MPOS Server Tool V" & smallArr(0) + ".exe") Then
                        My.Computer.FileSystem.DeleteFile(Application.StartupPath + "/MPOS Server Tool V" & smallArr(0) + ".exe")
                        My.Computer.FileSystem.DeleteFile("oldversion.txt")
                    ElseIf My.Computer.FileSystem.FileExists(Application.StartupPath + "/MPOS Server tool 2.0.exe") Then
                        My.Computer.FileSystem.DeleteFile(Application.StartupPath + "/MPOS Server tool 2.0.exe")
                        My.Computer.FileSystem.DeleteFile("oldversion.txt")
                    End If
                End If
            End If
        Catch ex As Exception
            miniTool.balon(ex.Message)
            Logger.WriteToErrorLog(ex.Message, ex.StackTrace, "error")
        End Try
    End Sub
    Public Function ResourceExists(location As Uri) As Boolean
        If (Not String.Equals(location.Scheme, Uri.UriSchemeHttp, StringComparison.InvariantCultureIgnoreCase)) And (Not String.Equals(location.Scheme, Uri.UriSchemeHttps, StringComparison.InvariantCultureIgnoreCase)) Then
            Throw New NotSupportedException("URI scheme is not supported")
        End If

        Dim request = Net.WebRequest.Create(location)
        request.Method = "HEAD"

        Try
            Using response = request.GetResponse
                Return DirectCast(response, Net.HttpWebResponse).StatusCode = Net.HttpStatusCode.OK
            End Using
        Catch ex As Net.WebException
            Select Case DirectCast(ex.Response, Net.HttpWebResponse).StatusCode
                Case Net.HttpStatusCode.NotFound
                    Return False
                Case Else
                    Throw
            End Select
            Logger.WriteToErrorLog(ex.Message, ex.StackTrace, "error")
        End Try
    End Function
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        DeleteOldVersion()
        Timer1.Stop()
    End Sub
    Public Sub XmlVersion(files)

        Dim ttr As String = ""
        Dim link As String = ""

        If files = "server" Then
            ttr = Main.svl
            link = "\\buk11fsr001\GRP_MSYS_MPOS_DELIVERY\pos\Users\public\serverlist.xml"
            'Dim link As New Uri("http://my-collaboration.metrogroup-networking.com/personal/r4_razvan_belcea/Shared%20Documents/serverlist.xml")
        ElseIf files = "folder" Then
            ttr = Main.ffl
            link = "\\buk11fsr001\GRP_MSYS_MPOS_DELIVERY\pos\Users\public\folderlist.xml"
            'Dim link As New Uri("http://my-collaboration.metrogroup-networking.com/personal/r4_razvan_belcea/Shared%20Documents/folderlist.xml")
        ElseIf files = "service" Then
            ttr = Main.srl
            link = "\\buk11fsr001\GRP_MSYS_MPOS_DELIVERY\pos\Users\public\servicelist.xml"
            'Dim link As New Uri("http://my-collaboration.metrogroup-networking.com/personal/r4_razvan_belcea/Shared%20Documents/servicelist.xml")
        ElseIf files = "sqllist" Then
            ttr = "sqllist.xml"
            link = "\\buk11fsr001\GRP_MSYS_MPOS_DELIVERY\pos\Users\public\sqllist.xml"
            'Dim link As New Uri("http://my-collaboration.metrogroup-networking.com/personal/r4_razvan_belcea/Shared%20Documents/sqllist.xml")
        End If

        Dim xmlread As XmlTextReader = New XmlTextReader(ttr)
        Dim myArr As New ArrayList()
        Try
            Do While (xmlread.Read())
                Select Case xmlread.NodeType
                    Case XmlNodeType.Element
                        Select Case xmlread.Name
                            Case "Version"
                                xmlread.Read()
                                myArr.Add(xmlread.Value)
                        End Select
                End Select
            Loop
            xmlread.Close()
            xmlread.Dispose()
        Catch ex As Exception
            Logger.WriteToErrorLog(ex.Message, ex.StackTrace, "error")
        End Try

        Dim reader As XmlTextReader = New XmlTextReader(link)
        If My.Computer.FileSystem.FileExists(link) Then
            Do While reader.Read()
                Select Case reader.NodeType
                    Case XmlNodeType.Element
                        Select Case reader.Name
                            Case "Version"
                                reader.Read()
                                myArr.Add(reader.Value)
                        End Select
                End Select
            Loop
        Else
            myArr.Add(xmlread.Value)
            MsgBox("Invalid address at " + link)
        End If
        If myArr(0).ToString < myArr(1).ToString Then
            Dim credentials As System.Net.NetworkCredential = System.Net.CredentialCache.DefaultNetworkCredentials
            Try
                If My.Computer.FileSystem.FileExists(ttr) Then
                    My.Computer.FileSystem.DeleteFile(ttr)
                End If
                My.Computer.Network.DownloadFile(link,
                                                ttr,
                                                "", "", False, 500, True)
            Catch ex As Exception
                miniTool.balon(ex.Message + " Error Downloading update.")
                Logger.WriteToErrorLog(ex.Message, ex.StackTrace, "error")
            End Try
            w = True
        End If
    End Sub

    Private Sub MetroButton2_Click(sender As Object, e As EventArgs) Handles MetroButton2.Click
        taskservice()
        MetroButton2.Hide()
    End Sub

    Public Sub cleanshortc(path)
        Try
            Dim Directory As String = CreateObject("WScript.Shell").Specialfolders(10)
            If path = "desktop" Then
                Directory = CreateObject("WScript.Shell").Specialfolders(10)
            ElseIf path = "startup" Then
                Directory = CreateObject("WScript.Shell").SpecialFolders("Startup")
            End If
            For Each filename As String In IO.Directory.GetFiles(Directory, "*", IO.SearchOption.AllDirectories)
                If Microsoft.VisualBasic.Right(filename, 4) = ".lnk" Then
                    If InStr(filename, "MPOS Tool") > 1 Then
                        My.Computer.FileSystem.DeleteFile(filename)
                    End If
                End If
            Next
            createshortc(Directory & "\MPOS Tool.lnk", "MPOS")
        Catch ex As Exception
            miniTool.balon(ex.Message)
            Logger.WriteToErrorLog(ex.Message, ex.StackTrace, "error")
        End Try
    End Sub
    Public Sub createshortc(FileName, Title)
        Try
            Dim shortc As Object = CreateObject("WScript.Shell").CreateShortcut(FileName)
            shortc.TargetPath = Application.ExecutablePath
            shortc.WindowStyle = 1I
            shortc.Description = Title
            shortc.WorkingDirectory = Application.StartupPath
            shortc.IconLocation = Application.ExecutablePath & ", 0"
            shortc.Arguments = String.Empty
            shortc.Save()
        Catch ex As Exception
            miniTool.balon(ex.Message)
            Logger.WriteToErrorLog(ex.Message, ex.StackTrace, "error")
        End Try
    End Sub

    ' TILL LIST COMPARER
    Dim sortColumn As Integer = -1
    Private Sub tilllist_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs)

        ' Determine whether the column is the same as the last column clicked.
        If e.Column <> sortColumn Then
            ' Set the sort column to the new column.
            sortColumn = e.Column
            ' Set the sort order to ascending by default.
            tilllist.Sorting = System.Windows.Forms.SortOrder.Ascending
        Else
            ' Determine what the last sort order was and change it.
            If tilllist.Sorting = System.Windows.Forms.SortOrder.Ascending Then
                tilllist.Sorting = System.Windows.Forms.SortOrder.Descending
            Else
                tilllist.Sorting = System.Windows.Forms.SortOrder.Ascending
            End If
        End If
        ' Call the sort method to manually sort.
        tilllist.Sort()
        ' Set the ListViewItemSorter property to a new ListViewItemComparer
        ' object.
        tilllist.ListViewItemSorter = New ListViewItemComparer(e.Column,
                                                         tilllist.Sorting)
    End Sub

    ' Implements the manual sorting of items by column.
    Class ListViewItemComparer
        Implements IComparer
        Private col As Integer
        Private order As System.Windows.Forms.SortOrder

        Public Sub New()
            col = 0
            order = System.Windows.Forms.SortOrder.Ascending
        End Sub

        Public Sub New(column As Integer, order As System.Windows.Forms.SortOrder)
            col = column
            Me.order = order
        End Sub

        Public Function Compare(x As Object, y As Object) As Integer _
                            Implements System.Collections.IComparer.Compare
            Dim returnVal As Integer = -1
            returnVal = [String].Compare(CType(x,
                            ListViewItem).SubItems(col).Text,
                            CType(y, ListViewItem).SubItems(col).Text)
            ' Determine whether the sort order is descending.
            If order = System.Windows.Forms.SortOrder.Descending Then
                ' Invert the value returned by String.Compare.
                returnVal *= -1
            End If

            Return returnVal
        End Function
    End Class

    Private Sub MSTSCToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles MstscToolStripMenuItem1.Click
        System.Diagnostics.Process.Start("mstsc.exe", "/v " & tilllist.SelectedItems.Item(0).SubItems(5).Text)
    End Sub

    Private Sub LinkLabel1_Click(sender As Object, e As EventArgs) Handles LinkLabel1.Click
        ContextMenuStrip6.Show(Cursor.Position)
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        x = True
        Me.Close()
    End Sub

    Private Sub MetroRadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles MetroRadioButton1.CheckedChanged
        taskserver("QA", 0)
    End Sub

    Private Sub MetroRadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles MetroRadioButton2.CheckedChanged
        taskserver("UAT", 1)
    End Sub

    Private Sub MetroRadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles MetroRadioButton3.CheckedChanged
        taskserver("DEV", 3)
    End Sub

    Private Sub MetroRadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles MetroRadioButton4.CheckedChanged
        taskserver("PROD", 2)
    End Sub

    Private Sub BarcodeGeneratorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarcodeGeneratorToolStripMenuItem.Click
        If MetroLabel15.Text = "ONLINE" Then
            barcodeG.ShowDialog()
            barcodeG.Dispose()
        Else
            miniTool.balon("Please select a server!")
        End If
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub DiscountTableToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DiscountTableToolStripMenuItem.Click
        Try
            If MetroLabel15.Text = "ONLINE" Then
                DiscountExtract.getem()
            Else
                miniTool.balon("Please select a server!")
            End If
        Catch ex As Exception
            Logger.WriteToErrorLog(ex.Message, ex.StackTrace, "error")
        End Try
    End Sub

    Private Sub MetroLabel12_MouseMove(sender As Object, e As MouseEventArgs) Handles MetroLabel12.MouseMove
        MetroToolTip1.Show(cnt, MetroLabel12)
    End Sub

    Private Sub HotfixesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HotfixesToolStripMenuItem.Click
        hotfix.ShowDialog()
    End Sub

    Private Sub checkforupdate()
        Try
            Dim WbReq As New Net.WebClient
            WbReq.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials
            WbReq.Dispose()

            Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("http://my-collaboration.metrogroup-networking.com/personal/r4_razvan_belcea/Shared%20Documents/Update.txt")
            request.Credentials = System.Net.CredentialCache.DefaultCredentials
            Dim response As System.Net.HttpWebResponse = request.GetResponse()
            Dim sr As IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
            Dim exenewestversion As String = sr.ReadToEnd()
            Dim execurrentversion As String = System.Windows.Forms.Application.ProductVersion
            If execurrentversion < exenewestversion Then
                updateapp.app()
            Else
                ActualVersion()
            End If
            sr.Close()
        Catch ex As Exception
            miniTool.balon("CheckForUpdates: " + ex.Message)
        End Try
    End Sub
End Class
