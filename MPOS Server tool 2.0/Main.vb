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
            DbQueries.ShowDialog()
            DbQueries.Dispose()
        Else
            miniTool.balon("Please select a server!")
        End If
    End Sub

    '==========================================================================================================================
    Public Shared Uid As String = "TpAdmin"
    Public Shared Psw As String = "Cawt6__56UBn_szF8_10"
    Public Shared Cred1 As String = "Integrated Security=SSPI"
    Public Shared Cred2 As String = "Uid=" & Uid & "; Password=" & Psw
    Public Shared Cred As String '= cred1
    Public Shared Svl As String = "serverlist.xml"
    Public Shared Ffl As String = "folderlist.xml"
    Public Shared Srl As String = "servicelist.xml"
    Public Shared Cfl As String = "countlist.xml"
    Public Shared Hff As String = "\c$\mgi\mposinstallstate.xml"
    Public Shared X As Boolean = False
    Public Shared W As Boolean = False
    Public Shared Printeron As Boolean
    Public Shared Virtualon As Boolean
    Public Shared Qafolder As String
    Public Shared Uatfolder As String
    Public Shared Logger As New ErrorLogger
    Dim _anulareserver As CancellationTokenSource
    Dim _anulareservice As CancellationTokenSource
    Dim _anularetills As CancellationTokenSource
    Dim _anulareoperators As CancellationTokenSource
    Dim _cnt As String = ""

    '-----------------------------------------------------------------------------------------------------------------------------------------BEGIN form load/unload
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If X = False Then
            NotifyIcon1.Visible = True
            Hide()
            miniTool.Show()
            e.Cancel = True
        End If
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MetroRadioButton1.Checked = True
        CheckForIllegalCrossThreadCalls = False
        Autoupdaterexists()
        Try
            Dim myReg As RegistryKey
            Dim myVal As Object
            myReg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Microsoft\System\DNSClient", False)
            If (Not myReg Is Nothing) Then
                myVal = myReg.GetValue("PrimaryDnsSuffix")
            Else
                myVal = "not_ro"
            End If
            If myVal = "not_ro" Then
                miniTool.balon("XML update not needed!")
            ElseIf myVal <> "client.ro.r4.madm.net" Then
                miniTool.balon("XML update not needed!")
            Else
                XmlVersion("sqllist")
                XmlVersion("server")
                XmlVersion("folder")
                XmlVersion("service")
                If W = True Then
                    miniTool.balon("New XML files downloaded!!!")
                End If
            End If
            myReg.Close()
        Catch a As Exception
            Logger.WriteToErrorLog(a.Message, a.StackTrace, "PrimaryDNS missing! ")
        End Try
        Show()
        If TopMost = False Then
            TopMost = True
            TopMost = False
        End If
        Cleanshortc("desktop")
        If Settings.MetroToggle6.Checked = True Then
            Settings.ServiceModuleEnable()
            Settings.Button2.Enabled = True
        ElseIf Settings.MetroToggle6.Checked = False Then
            Settings.ServiceModuleDisable()
            Settings.Button2.Enabled = True
        End If


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
        X = False
        Close()
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
        WindowState = FormWindowState.Minimized
    End Sub

    'END OF DESIGN AREA
    Private Sub Readfolderlist(link)
        Dim readfolder As XmlTextReader = New XmlTextReader(Ffl)
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
    Private Sub Readservicelist()
        Dim readservice As XmlTextReader = New XmlTextReader(Srl)
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
        Dim countryIp As String = ""
        For Each country As ListViewItem In serverlist.Items
            If country.Selected = True Then
                Dim countryName As String = country.SubItems(0).Text
                Try
                    Dim doc As XmlDocument = New XmlDocument()
                    doc.Load(Svl)
                    If country.Group.Name = "ListViewGroup1" Then
                        Dim saveCountry As String = doc.SelectSingleNode("List/QA/Server[Country='" + countryName + "']/Ip").InnerText
                        countryIp = saveCountry.ToString()
                    ElseIf country.Group.Name = "ListViewGroup4" Then
                        Dim saveCountry As String = doc.SelectSingleNode("List/DEV/Server[Country='" + countryName + "']/Ip").InnerText
                        countryIp = saveCountry.ToString()
                    ElseIf country.Group.Name = "ListViewGroup2" Then
                        Dim saveCountry As String = doc.SelectSingleNode("List/UAT/Server[Country='" + countryName + "']/Ip").InnerText
                        countryIp = saveCountry.ToString()
                    ElseIf country.Group.Name = "ListViewGroup3" Then
                        Dim saveCountry As String = doc.SelectSingleNode("List/PROD/Server[Country='" + countryName + "']/Ip").InnerText
                        countryIp = saveCountry.ToString()
                    End If
                    If My.Computer.Network.Ping(countryIp.ToString()) Then
                        servicelist.Items.Clear()
                        'Button1.Visible = True
                        'Button6.Visible = True
                        MetroButton2.Visible = True
                        'Button7.Visible = True
                        Viewserver()
                        Statusserver()
                        For Each item As ListViewItem In serverlist.SelectedItems
                            If item.Group.Name = "ListViewGroup1" Then
                                Readfolderlist("QA")
                            ElseIf item.Group.Name = "ListViewGroup4" Then
                                Readfolderlist("DEV")
                            Else
                                Readfolderlist("UAT")
                            End If
                        Next
                        ' Set the initial sorting type for the ListView. 
                        tilllist.Sorting = Windows.Forms.SortOrder.None
                        ' Disable automatic sorting to enable manual sorting. 
                        AddHandler tilllist.ColumnClick, AddressOf tilllist_ColumnClick
                        Loaddatabase()
                        Loadcounts()
                    End If
                Catch ex As Exception
                    Logger.WriteToErrorLog(ex.Message, ex.StackTrace, "error")
                End Try
            End If
        Next

    End Sub
    Private Sub Statusserver()
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
    Private Sub Viewserver()
        metro.Visible = False
        folderlist.Visible = True
        status.Visible = True
        folder.Visible = True
        tills.Visible = True
        operators.Visible = True
        For Each item As ListViewItem In serverlist.Items
            If item.Selected = True Then
                MetroLabel8.Text = item.SubItems(2).Text
                MetroLabel7.Text = item.SubItems(1).Text
                Try
                    Dim arr, arr1, arr2, arr3, arr4 As New ArrayList

                    Dim db As New SqldbManager(MetroLabel8.Text,,, 1)
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
                        arr4.Add(euSoftwareVersionTill(0))
                    End While
                    euSoftwareVersionTill.Close()

                    Dim szPackageName As SqlDataReader = db.ReadSqlData("select top 1 szVersion from EUSoftwareVersion where szPackageName = 'Metro_Common_TPDotnetSetupPos' order by szTimestamp desc")

                    While szPackageName.Read()
                        arr2.Add(szPackageName(0))
                    End While
                    szPackageName.Close()

                    Dim szCountryCode As SqlDataReader = db.ReadSqlData("select top 1 szCountryCode from MGIExternalStore")

                    While szCountryCode.Read()
                        arr3.Add(szCountryCode(0))
                    End While
                    szCountryCode.Close()

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
                    Dim tillshf As String = arr4(0).ToString.Substring(arr4(0).ToString.Length - 3)

                    If tillshf.StartsWith(".") Then
                        tillhf = "0" + tillshf.Substring(tillshf.Length - 2)
                    ElseIf tillshf.Contains(".") Then
                        tillhf = "00" + tillshf.Substring(tillshf.Length - 1)
                    Else
                        tillhf = tillshf
                    End If

                    MetroLabel23.Text = serverhf
                    MetroLabel24.Text = tillhf
                    MetroLabel11.Text = arr(0).ToString
                    MetroLabel20.Text = arr2(0).ToString
                    MetroLabel9.Text = arr3(0).ToString
                Catch a As Exception
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
    Private Sub Loaddatabase()
        operatorlist.Items.Clear()
        tilllist.Items.Clear()
        Dim con As SqlConnection
        con = New SqlConnection("Data Source=" & MetroLabel8.Text & ";Database=TPCentralDB;" & Cred & ";")
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
                Tasktills()
                Taskoperators()
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
    Private Sub Loadoperators(tokenoperators As CancellationToken)
        Dim i As Integer = 0
        Try

            Dim db As New SqldbManager(MetroLabel8.Text,,, 1)
            Dim loadOperator As SqlDataReader = db.ReadSqlData("select  e.lOperatorID, e.szName, e.szSignOnPassword, ISNULL(f.szLastUpdLocal,'null') as szLastUpdLocal from (select d.lOperatorID, d.szName, c.szSignOnPassword, c.szLastUpdLocal from (select lOperatorID,szname from OperatorProfileAffiliation as a join profile as b on a.lProfileID=b.lProfileID) d join Operator as c on d.lOperatorID = c.lOperatorID ) e full outer join (select * from OperatorPasswordHistory where szLastUpdLocal in (select MAX(szLastUpdLocal) from OperatorPasswordHistory group by lOperatorID)) as f on e.lOperatorID = f.lOperatorID")

            While loadOperator.Read()

                If tokenoperators.IsCancellationRequested Then
                    tokenoperators.ThrowIfCancellationRequested()
                End If

                If loadOperator(2) = "PkaIqJt8znE=" Then
                    Dim cryptpass As String = "******"
                    operatorlist.Items.Add(loadOperator(0))
                    operatorlist.Items(i).SubItems.Add(loadOperator(1))
                    operatorlist.Items(i).SubItems.Add(cryptpass)
                    i = i + 1
                Else
                    operatorlist.Items.Add(loadOperator(0))
                    operatorlist.Items(i).SubItems.Add(loadOperator(1))
                    operatorlist.Items(i).SubItems.Add(loadOperator(3))
                    i = i + 1
                End If

            End While
            loadOperator.Close()
        Catch e As Exception
            Logger.WriteToErrorLog(e.Message, e.StackTrace, "error")
        End Try
    End Sub
    Private Sub Loadtills(tokentills As CancellationToken)
        Dim ff As String
        Dim i As Integer = 0
        Dim arr As Array = {"-", "-", "-", "-", "-", "-", "-"}
        Try
            tilllist.HeaderStyle = ColumnHeaderStyle.None

            Dim db As New SqldbManager(MetroLabel8.Text,,, 1)
            Dim wks As String = "select szWorkstationID,lWorkstationNmbr, szWorkstationGroupID,lOperatorID from workstation left join operator on lLoggedOnWorkstationNmbr=lWorkstationNmbr where bisthickpos<>0"
            Dim wksNovirtual As String = "select szWorkstationID,lWorkstationNmbr, szWorkstationGroupID,lOperatorID from workstation left join operator on lLoggedOnWorkstationNmbr=lWorkstationNmbr where bisthickpos<>0 and szWorkstationID not like '%MPV%' and szWorkstationID not like '%LAGO%'"
            Dim rightone As String
            If Virtualon = True Then
                rightone = wks
            Else
                rightone = wksNovirtual
            End If
            Dim workstation As SqlDataReader = db.ReadSqlData(rightone)

            While workstation.Read()
                arr = {"-", "-", "-", "-", "-", "-", "-"}
                If tokentills.IsCancellationRequested Then
                    tokentills.ThrowIfCancellationRequested()
                End If
                arr(0) = workstation(0)
                arr(1) = workstation(1)
                arr(2) = workstation(2)
                If Not IsDBNull(workstation(3)) Then
                    arr(3) = workstation(3)
                End If
                Try
                    arr(4) = Dns.GetHostEntry(arr(0) & ".MPOS.MADM.NET").AddressList(0).ToString()
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
                If Printeron = True Then
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
            workstation.Close()
            tilllist.HeaderStyle = ColumnHeaderStyle.Clickable
        Catch e As Exception
            'Form7.balon(e.Message)
            ' Logger.WriteToErrorLog(e.Message, e.StackTrace, "error")
        End Try
        ' tpb.Visible = False === causes application to freeze/ fix needed 
    End Sub
    Private Sub Loadcounts()
        Dim readcounts As XmlTextReader = New XmlTextReader(Cfl)
        _cnt = vbTab & vbTab & vbTab & vbTab & vbTab & vbCrLf
        Try
            If MetroLabel15.Text = "ONLINE" Then
                Dim db As New SqldbManager(MetroLabel8.Text,,, 1)
                Dim wksquery As String = "select lRetailStoreID from workstation where szWorkstationID='" & MetroLabel7.Text & "'"
                Dim readworkstation As SqlDataReader = db.ReadSqlData(wksquery)
                While readworkstation.Read()
                    MetroLabel10.Text = readworkstation(0)
                End While
                For Each item As ListViewItem In serverlist.Items
                    If item.Selected = True Then
                        Try
                            Do While (readcounts.Read())
                                Select Case readcounts.NodeType
                                    Case XmlNodeType.Element
                                        Select Case readcounts.Name
                                            Case "Name"
                                                readcounts.Read()
                                                _cnt = _cnt & readcounts.Value & " : "
                                            Case "Sql"
                                                readcounts.Read()
                                                wksquery = readcounts.Value
                                                'dim readmeagain as SqlDataReader = db.ReadSqlData(wksquery)
                                                '_cnt = _cnt & readmeagain(0) & vbCrLf
                                                'readmeagain.Close()
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
                readworkstation.Close()
            End If
            _cnt = _cnt & vbCrLf & vbTab & vbTab & vbTab & vbTab & vbTab
        Catch ex As Exception
            Logger.WriteToErrorLog(ex.Message, ex.StackTrace, "error")
        End Try
    End Sub
    Private Sub Loadservice(tokenservice As CancellationToken)
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

    Private Sub updateserverlist(tokenserver As CancellationToken)
        Dim i As Integer = 0
        Dim connString As String = "Data Source='utils.sdf';"
        Dim sList As New List(Of ListViewItem)
        For Each item As ListViewItem In serverlist.Items
            sList.Add(item)
        Next
        Try
            Parallel.ForEach(sList,
                        Sub(item)
                            If My.Computer.Network.Ping(item.SubItems(2).Text) Then
                                item.ForeColor = Color.Green
                                item.SubItems.Add("ON")
                                Try
                                    Dim arr, arr1, arr2 As New ArrayList

                                    Dim db As New SqldbManager(item.SubItems(2).Text,,, 1)
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

                                    item.SubItems.Add(arr(0).ToString + " " + "HF: " + serverhf + "/" + tillhf)

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
                        End Sub)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Taskserver(ByVal type As String, ByVal group As Integer, ByVal refresh As Boolean)
        If _anulareserver IsNot Nothing Then
            _anulareserver.Cancel()
        End If
        ' readserverlist()
        getServer.populate(type, group)
        _anulareserver = New CancellationTokenSource
        Dim tokenserver = _anulareserver.Token
        Task.Factory.StartNew(Sub() updateserverlist(tokenserver), tokenserver)
    End Sub
    Private Sub Taskservice()
        If _anulareservice IsNot Nothing Then
            _anulareservice.Cancel()
        End If
        Readservicelist()
        _anulareservice = New CancellationTokenSource
        Dim tokenservice = _anulareservice.Token
        Task.Factory.StartNew(Sub() Loadservice(tokenservice), tokenservice)
    End Sub
    Private Sub Taskoperators()
        If _anulareoperators IsNot Nothing Then
            _anulareoperators.Cancel()
        End If
        _anulareoperators = New CancellationTokenSource
        Dim tokenoperators = _anulareoperators.Token
        Task.Factory.StartNew(Sub() Loadoperators(tokenoperators), tokenoperators)
    End Sub
    Private Sub Tasktills()
        If _anularetills IsNot Nothing Then
            _anularetills.Cancel()
        End If
        _anularetills = New CancellationTokenSource
        Dim tokentills = _anularetills.Token
        Task.Factory.StartNew(Sub() Loadtills(tokentills), tokentills)
    End Sub
    '-----------------------------------------------------------------------------------------------------------------------------------------END tasks
    '-----------------------------------------------------------------------------------------------------------------------------------------BEGIN menus
    Private Sub Logoffoperator()
        Dim con As New SqlConnection("Data Source=" & MetroLabel8.Text & ";Database=TPCentralDB;" & Cred & ";")
        Try
            con.Open()
            If MetroLabel15.Text = "ONLINE" Then
                For Each item As ListViewItem In tilllist.Items
                    If item.Selected = True Then
                        Dim cmd As SqlCommand = New SqlCommand("update operator set lLoggedOnWorkstationNmbr=0 where loperatorid=" & item.SubItems(4).Text, con)
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
    Private Sub Restarttill()
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
    Private Sub Resetoperator()
        Dim con As New SqlConnection("Data Source=" & MetroLabel8.Text & ";Database=TPCentralDB;" & Cred & ";")
        Try
            If MetroLabel15.Text = "ONLINE" Then
                con.Open()
                For Each item As ListViewItem In operatorlist.Items
                    If item.Selected = True AndAlso item.SubItems(0).Text <> "-" Then
                        Dim cmd As SqlCommand = New SqlCommand("update Operator set szSignOnPassword='PkaIqJt8znE=',szPasswordExpirationDate='20161111',bPasswordChangeFlag=0 where lOperatorID='" & item.SubItems(0).Text & "'", con)
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
    Private Sub Changetillmode(tilltype As String)
        Dim con As New SqlConnection("Data Source=" & MetroLabel8.Text & ";Database=TPCentralDB;" & Cred & ";")
        Try
            con.Open()
            For Each item As ListViewItem In tilllist.Items
                If item.Selected = True AndAlso item.SubItems(6).Text <> "-" Then
                    Dim cmd As SqlCommand = New SqlCommand("update Workstation set szWorkstationGroupID = '" + tilltype + "' where szWorkstationID = '" + item.SubItems(1).Text + "'", con)
                    cmd.ExecuteNonQuery()
                End If
            Next
        Catch ex As Exception
            miniTool.balon(ex.Message)
            Logger.WriteToErrorLog(ex.Message, ex.StackTrace, "error")
        End Try

    End Sub
    Private Sub ResetOperatorPassword123ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetOperatorPassword123ToolStripMenuItem.Click
        Resetoperator()
    End Sub
    Private Sub RefreshToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem2.Click
        Loaddatabase()
    End Sub
    Private Sub RefreshDBToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshDBToolStripMenuItem.Click
        Loaddatabase()
    End Sub
    Private Sub RestartTillToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartTillToolStripMenuItem.Click
        Dim result As Integer = MessageBox.Show("Are you sure you want to reboot the till ?", "Reboot till", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            Restarttill()
        Else
        End If
    End Sub
    Private Sub ForceSignOutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ForceSignOutToolStripMenuItem.Click
        Logoffoperator()
    End Sub
    Private Sub RefreshStatusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshStatusToolStripMenuItem.Click
        For Each item As ListViewItem In serverlist.SelectedItems
            If item.Group.Name = "ListViewGroup1" Then
                Taskserver("QA", 0, True)
            ElseIf item.Group.Name = "ListViewGroup2" Then
                Taskserver("UAT", 1, True)
            ElseIf item.Group.Name = "ListViewGroup4" Then
                Taskserver("DEV", 3, True)
            ElseIf item.Group.Name = "ListViewGroup3" Then
                Taskserver("PROD", 2, True)
            End If
        Next
    End Sub
    'Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
    '    'about.ShowDialog()
    'End Sub
    Private Sub RefreshToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem1.Click
        Taskservice()
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
            Taskservice()
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
            Taskservice()
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
        Process.Start("mstsc.exe", "/v " & MetroLabel8.Text)
    End Sub
    Private Sub ContextMenuStrip1_Closing(sender As Object, e As ToolStripDropDownClosingEventArgs) Handles ContextMenuStrip1.Closing
        SetPrinterToolStripMenuItem.Visible = False
        ForceSignOutToolStripMenuItem.Visible = False
        RestartTillToolStripMenuItem.Visible = False
        OpenInSCCMToolStripMenuItem1.Visible = False
        MstscToolStripMenuItem.Visible = False
        GetLogsToolStripMenuItem.Visible = False
        ChangeTillModeToolStripMenuItem.Visible = False
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
            ChangeTillModeToolStripMenuItem.Visible = False
            For Each item As ListViewItem In tilllist.Items
                If item.Selected AndAlso MetroLabel15.Text = "ONLINE" Then
                    If item.SubItems(5).Text <> "-" AndAlso item.SubItems(6).Text = "ON" Then
                        RestartTillToolStripMenuItem.Visible = True
                        OpenInSCCMToolStripMenuItem1.Visible = True
                        MstscToolStripMenuItem1.Visible = True
                        GetLogsToolStripMenuItem.Visible = True
                        ChangeTillModeToolStripMenuItem.Visible = True
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
        Show()
        If TopMost = False Then
            TopMost = True
            TopMost = False
        End If
    End Sub
    Private Sub OpenInSCCMToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles OpenInSCCMToolStripMenuItem1.Click
        Try
            For Each item As ListViewItem In From item1 As ListViewItem In tilllist.Items Where item1.Selected = True
                If Environment.Is64BitOperatingSystem = False Then
                    Process.Start("C:\Program Files\Microsoft Configuration Manager\AdminConsole\bin\i386\CmRcViewer.exe", item.SubItems(5).Text)
                Else
                    Process.Start("C:\Program Files (x86)\Microsoft Configuration Manager\AdminConsole\bin\i386\CmRcViewer.exe", item.SubItems(5).Text)
                End If
                Exit For
            Next
        Catch ed As Exception
            miniTool.balon(ed.Message)
            Logger.WriteToErrorLog(ed.Message, ed.StackTrace, "error")
        End Try
    End Sub
    Private Sub ExitMPOSToolAppToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitMPOSToolAppToolStripMenuItem.Click
        X = True
        Close()
    End Sub
    Private Sub OpenInSCCMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenInSCCMToolStripMenuItem.Click
        Try
            For Each item As ListViewItem In From item1 As ListViewItem In serverlist.Items Where item1.Selected = True
                If Environment.Is64BitOperatingSystem = False Then
                    Process.Start("C:\Program Files\Microsoft Configuration Manager\AdminConsole\bin\i386\CmRcViewer.exe", item.SubItems(2).Text)
                Else
                    Process.Start("C:\Program Files (x86)\Microsoft Configuration Manager\AdminConsole\bin\i386\CmRcViewer.exe", item.SubItems(2).Text)
                End If
                Exit For
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
            If (From item As ListViewItem In serverlist.Items Where item.Selected = True And item.SubItems.Count > 3).Any(Function(item) item.SubItems(3).Text = "ON") Then
                OpenInSCCMToolStripMenuItem.Visible = True
                GetServerLogsToolStripMenuItem.Visible = True
                MstscToolStripMenuItem.Visible = True
            End If
        Catch ed As Exception
            miniTool.balon(ed.Message)
            Logger.WriteToErrorLog(ed.Message, ed.StackTrace, "error")
        End Try
    End Sub
    Private Sub ContextMenuStrip2_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStrip2.Opening
        ResetOperatorPassword123ToolStripMenuItem.Visible = False
        For Each item As ListViewItem In From item1 As ListViewItem In operatorlist.Items Where item1.Selected
            ResetOperatorPassword123ToolStripMenuItem.Visible = True
            Exit For
        Next
    End Sub
    Private Sub ContextMenuStrip3_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStrip3.Opening
        Try
            RestartServiceToolStripMenuItem.Visible = False
            StartStopServiceToolStripMenuItem.Visible = False
            If (From item As ListViewItem In servicelist.Items Where item.Selected And MetroLabel15.Text = "ONLINE" And item.SubItems.Count > 1).Any(Function(item) item.SubItems(2).Text <> "-") Then
                RestartServiceToolStripMenuItem.Visible = True
                StartStopServiceToolStripMenuItem.Visible = True
            End If
        Catch ed As Exception
            miniTool.balon(ed.Message)
            Logger.WriteToErrorLog(ed.Message, ed.StackTrace, "error")
        End Try
    End Sub
    '-----------------------------------------------------------------------------------------------------------------------------------------END menus
    '-----------------------------------------------------------------------------------------------------------------------------------------BEGIN minimize
    Private Sub NotifyIcon1_MouseClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseClick
        If e.Button = MouseButtons.Left Then
            If Visible Then
                Hide()
                miniTool.Show()
            Else
                miniTool.Hide()
                Show()
                If TopMost = False Then
                    TopMost = True
                    TopMost = False
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
        Process.Start("mstsc.exe", "/v " & MetroLabel8.Text)

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

        Dim request = WebRequest.Create(location)
        request.Method = "HEAD"

        Try
            Using response = request.GetResponse
                Return DirectCast(response, HttpWebResponse).StatusCode = HttpStatusCode.OK
            End Using
        Catch ex As WebException
            Select Case DirectCast(ex.Response, HttpWebResponse).StatusCode
                Case HttpStatusCode.NotFound
                    Return False
                Case Else
                    Throw
            End Select
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
            ttr = Svl
            link = "\\buk11fsr001\GRP_MSYS_MPOS_DELIVERY\pos\Users\public\serverlist.xml"
            'Dim link As New Uri("http://my-collaboration.metrogroup-networking.com/personal/r4_razvan_belcea/Shared%20Documents/serverlist.xml")
        ElseIf files = "folder" Then
            ttr = Ffl
            link = "\\buk11fsr001\GRP_MSYS_MPOS_DELIVERY\pos\Users\public\folderlist.xml"
            'Dim link As New Uri("http://my-collaboration.metrogroup-networking.com/personal/r4_razvan_belcea/Shared%20Documents/folderlist.xml")
        ElseIf files = "service" Then
            ttr = Srl
            link = "\\buk11fsr001\GRP_MSYS_MPOS_DELIVERY\pos\Users\public\servicelist.xml"
            'Dim link As New Uri("http://my-collaboration.metrogroup-networking.com/personal/r4_razvan_belcea/Shared%20Documents/servicelist.xml")
        ElseIf files = "sqllist" Then
            ttr = "sqllist.xml"
            link = "\\buk11fsr001\GRP_MSYS_MPOS_DELIVERY\pos\Users\public\sqllist.xml"
            'Dim link As New Uri("http://my-collaboration.metrogroup-networking.com/personal/r4_razvan_belcea/Shared%20Documents/sqllist.xml")
        End If

        Dim xmlread = New XmlTextReader(ttr)
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
            W = True
        End If
    End Sub

    Private Sub MetroButton2_Click(sender As Object, e As EventArgs) Handles MetroButton2.Click
        Taskservice()
        MetroButton2.Hide()
    End Sub

    Public Sub Cleanshortc(path)
        Try
            Dim directory As String = CreateObject("WScript.Shell").Specialfolders(10)
            If path = "desktop" Then
                directory = CreateObject("WScript.Shell").Specialfolders(10)
            ElseIf path = "startup" Then
                directory = CreateObject("WScript.Shell").SpecialFolders("Startup")
            End If
            For Each filename As String In From filename1 In IO.Directory.GetFiles(directory, "*", SearchOption.AllDirectories) Where Microsoft.VisualBasic.Right(filename1, 4) = ".lnk" Where InStr(filename1, "MPOS Tool") > 1
                My.Computer.FileSystem.DeleteFile(filename)
            Next
            Createshortc(directory & "\MPOS Tool.lnk", "MPOS")
        Catch ex As Exception
            miniTool.balon(ex.Message)
            Logger.WriteToErrorLog(ex.Message, ex.StackTrace, "error")
        End Try
    End Sub
    Public Sub Createshortc(fileName, title)
        Try
            Dim shortc As Object = CreateObject("WScript.Shell").CreateShortcut(fileName)
            shortc.TargetPath = Application.ExecutablePath
            shortc.WindowStyle = 1I
            shortc.Description = title
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
    Dim _sortColumn As Integer = -1
    Private Sub tilllist_ColumnClick(sender As Object, e As ColumnClickEventArgs)

        ' Determine whether the column is the same as the last column clicked.
        If e.Column <> _sortColumn Then
            ' Set the sort column to the new column.
            _sortColumn = e.Column
            ' Set the sort order to ascending by default.
            tilllist.Sorting = Windows.Forms.SortOrder.Ascending
        Else
            ' Determine what the last sort order was and change it.
            If tilllist.Sorting = Windows.Forms.SortOrder.Ascending Then
                tilllist.Sorting = Windows.Forms.SortOrder.Descending
            Else
                tilllist.Sorting = Windows.Forms.SortOrder.Ascending
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
        Private ReadOnly _col As Integer
        Private ReadOnly _order As Windows.Forms.SortOrder

        Public Sub New()
            _col = 0
            _order = Windows.Forms.SortOrder.Ascending
        End Sub

        Public Sub New(column As Integer, order As Windows.Forms.SortOrder)
            _col = column
            _order = order
        End Sub

        Public Function Compare(x As Object, y As Object) As Integer _
                            Implements IComparer.Compare
            Dim returnVal As Integer = -1
            returnVal = [String].Compare(CType(x,
                            ListViewItem).SubItems(_col).Text,
                            CType(y, ListViewItem).SubItems(_col).Text)
            ' Determine whether the sort order is descending.
            If _order = Windows.Forms.SortOrder.Descending Then
                ' Invert the value returned by String.Compare.
                returnVal *= -1
            End If

            Return returnVal
        End Function
    End Class

    Private Sub MSTSCToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles MstscToolStripMenuItem1.Click
        Process.Start("mstsc.exe", "/v " & tilllist.SelectedItems.Item(0).SubItems(5).Text)
    End Sub

    Private Sub LinkLabel1_Click(sender As Object, e As EventArgs) Handles LinkLabel1.Click
        ContextMenuStrip6.Show(Cursor.Position)
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        X = True
        Close()
    End Sub

    Private Sub MetroRadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles MetroRadioButton1.CheckedChanged
        Dim tokenserver As CancellationToken
        If MetroRadioButton1.Checked Then
            Taskserver("QA", 0, False)
        Else
            tokenserver.ThrowIfCancellationRequested()
        End If
    End Sub

    Private Sub MetroRadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles MetroRadioButton2.CheckedChanged
        Dim tokenserver As CancellationToken
        If MetroRadioButton2.Checked Then
            Taskserver("UAT", 1, False)
        Else
            tokenserver.ThrowIfCancellationRequested()
        End If
    End Sub

    Private Sub MetroRadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles MetroRadioButton3.CheckedChanged
        Dim tokenserver As CancellationToken
        If MetroRadioButton3.Checked Then
            Taskserver("DEV", 3, False)
        Else
            tokenserver.ThrowIfCancellationRequested()
        End If
    End Sub

    Private Sub MetroRadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles MetroRadioButton4.CheckedChanged
        Dim tokenserver As CancellationToken
        If MetroRadioButton4.Checked Then
            Taskserver("PROD", 2, False)
        Else
            tokenserver.ThrowIfCancellationRequested()
        End If
    End Sub

    Private Sub BarcodeGeneratorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarcodeGeneratorToolStripMenuItem.Click
        If MetroLabel15.Text = "ONLINE" Then
            barcodeG.ShowDialog()
            barcodeG.Dispose()
        Else
            miniTool.balon("Please select a server!")
        End If
    End Sub

    Private Sub DiscountTableToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DiscountTableToolStripMenuItem.Click
        Try
            If MetroLabel15.Text = "ONLINE" Then
                DiscountExtract.Getem()
            Else
                miniTool.balon("Please select a server!")
            End If
        Catch ex As Exception
            Logger.WriteToErrorLog(ex.Message, ex.StackTrace, "error")
        End Try
    End Sub

    Private Sub MetroLabel12_MouseMove(sender As Object, e As MouseEventArgs) Handles MetroLabel12.MouseMove
        ' MetroToolTip1.Show(_cnt, MetroLabel12)
    End Sub

    Private Shared Sub HotfixesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HotfixesToolStripMenuItem.Click
        hotfix.ShowDialog()
    End Sub

    Private Shared Sub Autoupdaterexists()
        Try
            Dim wbReq As New Net.WebClient
            wbReq.Proxy.Credentials = CredentialCache.DefaultCredentials
            wbReq.Dispose()
            Const filetoget As String = "http://sunt.pro/m_update/AutoUpdater.exe"
            Dim filedownloaded As String = Application.StartupPath + " \AutoUpdater.exe"
            If My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "\Resources") Then
                If Not File.Exists(Application.StartupPath + "\AutoUpdater.exe") Then
                    My.Computer.Network.DownloadFile(filetoget, filedownloaded)
                End If
            Else
                My.Computer.FileSystem.CreateDirectory(Application.StartupPath + " \Resources\")
                If Not File.Exists(Application.StartupPath + "\AutoUpdater.exe") Then
                    My.Computer.Network.DownloadFile(filetoget, filedownloaded)
                End If
            End If
        Catch ex As Exception
            Logger.WriteToErrorLog(ex.Message, ex.StackTrace, "error")
        End Try
    End Sub
    Private Sub CheckForUpdatesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckForUpdatesToolStripMenuItem.Click
        Try
            X = True
            Process.Start(Application.StartupPath + "\AutoUpdater.exe")
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroTextBox1_TextChanged(sender As Object, e As EventArgs) Handles MetroTextBox1.TextChanged

        'Dim list As List(Of ListViewItem) = (From item In serverlist.Items.OfType(Of ListViewItem)()
        '                                     Where item.SubItems(0).Text.Contains(MetroTextBox1.Text.ToUpper())
        '                                     Select item).ToList
        'If list.Count = 0 Then
        '    serverlist.Items.Add(New ListViewItem("Your search returned no results"))
        '    'MsgBox("Your search returned no results")
        'Else
        '    'MsgBox(list.Count)
        'End If

        'Dim i As Integer
        'serverlist.Items.Clear()
        'For Each item In list
        '    If MetroRadioButton1.Checked = True Then
        '        item.Group = serverlist.Groups("ListViewGroup1")
        '        serverlist.Items.Add(item)
        '    ElseIf MetroRadioButton2.Checked = True Then
        '        item.Group = serverlist.Groups("ListViewGroup2")
        '        serverlist.Items.Add(item)
        '    ElseIf MetroRadioButton3.Checked = True Then
        '        item.Group = serverlist.Groups("ListViewGroup4")
        '        serverlist.Items.Add(item)
        '    ElseIf MetroRadioButton4.Checked = True Then
        '        item.Group = serverlist.Groups("ListViewGroup3")
        '        serverlist.Items.Add(item)
        '    End If
        'Next

        'If MetroTextBox1.Text = "" AndAlso MetroRadioButton1.Checked = True Then
        '    Taskserver("QA", 0, False)
        'ElseIf MetroTextBox1.Text = "" AndAlso MetroRadioButton2.Checked Then
        '    Taskserver("UAT", 1, False)
        'ElseIf MetroTextBox1.Text = "" AndAlso MetroRadioButton3.Checked Then
        '    Taskserver("DEV", 3, False)
        'ElseIf MetroTextBox1.Text = "" AndAlso MetroRadioButton4.Checked Then
        '    Taskserver("PROD", 2, False)

        'To find an item using text
        'In my case, I use their to find product id.
        'End If

        Dim itm As ListViewItem
        Dim i As Integer

        For i = 0 To serverlist.Items.Count - 1
            'itm = serverlist.SelectedItems(i)
            'Console.WriteLine(ContactFirstColumnHeader = {1} ContactLastColumnHeader = {2},itm.SubItems(1),itm.SubItems(2))
            serverlist.Items(i).Selected = False
            serverlist.Items(i).BackColor = Color.White
        Next

        With serverlist
            itm = .FindItemWithText(MetroTextBox1.Text, True, 0, True)


            If Not itm Is Nothing Then
                .Items.Item(itm.Index).BackColor = Color.LightGray
                .Items.Item(itm.Index).EnsureVisible()
            Else
                '   MsgBox("No Record Found!")
                For i = 0 To serverlist.Items.Count - 1
                    serverlist.Items(i).Selected = False
                    serverlist.Items(i).BackColor = Color.White
                Next
                .Items(0).EnsureVisible()
                .Items.Item(0).BackColor = Color.LemonChiffon
                MetroTextBox1.SelectionStart = 0
                MetroTextBox1.Focus()
            End If
        End With
        itm = Nothing
    End Sub

    Private Sub SalesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalesToolStripMenuItem.Click
        Dim item As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
        If item IsNot Nothing Then
            Changetillmode(item.Text)
            Loaddatabase()
            miniTool.balon("Till mode changed to " & item.Text)
        End If
    End Sub

    Private Sub WholeSalesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WholeSalesToolStripMenuItem.Click
        Dim item As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
        If item IsNot Nothing Then
            Changetillmode(item.Text)
            Loaddatabase()
            miniTool.balon("Till mode changed to " & item.Text)
        End If
    End Sub

    Private Sub RegularSalesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegularSalesToolStripMenuItem.Click
        Dim item As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
        If item IsNot Nothing Then
            Changetillmode(item.Text)
            Loaddatabase()
            miniTool.balon("Till mode changed to " & item.Text)
        End If
    End Sub

    Private Sub ReturnToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturnToolStripMenuItem.Click
        Dim item As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
        If item IsNot Nothing Then
            Changetillmode(item.Text)
            Loaddatabase()
            miniTool.balon("Till mode changed to " & item.Text)
        End If
    End Sub

    Private Sub DepositToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DepositToolStripMenuItem.Click
        Dim item As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
        If item IsNot Nothing Then
            Changetillmode(item.Text)
            Loaddatabase()
            miniTool.balon("Till mode changed to " & item.Text)
        End If
    End Sub

    Private Sub CorrectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CorrectionToolStripMenuItem.Click
        Dim item As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
        If item IsNot Nothing Then
            Changetillmode(item.Text)
            Loaddatabase()
            miniTool.balon("Till mode changed to " & item.Text)
        End If
    End Sub

    Private Sub CODGUIToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CODGUIToolStripMenuItem.Click
        Dim item As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
        If item IsNot Nothing Then
            Changetillmode(item.Text)
            Loaddatabase()
            miniTool.balon("Till mode changed to " & item.Text)
        End If
    End Sub

    Private Sub AdviceNoteCreationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdviceNoteCreationToolStripMenuItem.Click
        Dim item As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
        If item IsNot Nothing Then
            Changetillmode(item.Text)
            Loaddatabase()
            miniTool.balon("Till mode changed to " & item.Text)
        End If
    End Sub

    Private Sub VMSnapshotToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VMSnapshotToolStripMenuItem.Click
        VMSnapshot.ShowDialog()
    End Sub

    'Private Sub MetroTextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MetroTextBox1.KeyPress
    '    if e.KeyChar = ChrW(Keys.Enter) Then
    '                Dim itm As ListViewItem
    '    With serverlist
    '        itm = .FindItemWithText(MetroTextBox1.Text, False, 0, True)
    '        If Not itm Is Nothing Then
    '            .Items.Item(itm.Index).Selected = True
    '            .Items.Item(itm.index).EnsureVisible()
    '            dim tmpColor as Color
    '            tmpColor = Color.Coral
    '            .Items.Item(itm.index).BackColor = tmpColor
    '        End If
    '    End With
    '    itm = Nothing

    '    End If
    'End Sub


End Class
