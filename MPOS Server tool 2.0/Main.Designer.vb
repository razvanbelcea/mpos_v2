<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
    Inherits MetroFramework.Forms.MetroForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("QA", System.Windows.Forms.HorizontalAlignment.Center)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("UAT", System.Windows.Forms.HorizontalAlignment.Center)
        Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("PROD", System.Windows.Forms.HorizontalAlignment.Center)
        Dim ListViewGroup4 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("DEV", System.Windows.Forms.HorizontalAlignment.Center)
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.MetroRadioButton4 = New MetroFramework.Controls.MetroRadioButton()
        Me.MetroRadioButton3 = New MetroFramework.Controls.MetroRadioButton()
        Me.MetroRadioButton2 = New MetroFramework.Controls.MetroRadioButton()
        Me.MetroRadioButton1 = New MetroFramework.Controls.MetroRadioButton()
        Me.status = New System.Windows.Forms.GroupBox()
        Me.MetroLabel12 = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel8 = New System.Windows.Forms.LinkLabel()
        Me.MetroLabel7 = New System.Windows.Forms.LinkLabel()
        Me.MetroButton2 = New MetroFramework.Controls.MetroButton()
        Me.ccd = New MetroFramework.Controls.MetroProgressSpinner()
        Me.MetroLabel16 = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel15 = New MetroFramework.Controls.MetroLabel()
        Me.servicelist = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ContextMenuStrip3 = New MetroFramework.Controls.MetroContextMenu(Me.components)
        Me.RestartServiceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StartStopServiceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MetroLabel11 = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel10 = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel9 = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel6 = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel5 = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel4 = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel3 = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel2 = New MetroFramework.Controls.MetroLabel()
        Me.tills = New System.Windows.Forms.GroupBox()
        Me.tpb = New MetroFramework.Controls.MetroProgressSpinner()
        Me.tilllist = New System.Windows.Forms.ListView()
        Me.nr = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Tillname = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tillno = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Tilltype = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Signon = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TillIP = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Tillstatus = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.printertype = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ContextMenuStrip1 = New MetroFramework.Controls.MetroContextMenu(Me.components)
        Me.OpenInSCCMToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MstscToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestartTillToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ForceSignOutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GetLogsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetPrinterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NoneToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MatrixToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LaserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshDBToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.metro = New System.Windows.Forms.PictureBox()
        Me.LinkLabel1 = New MetroFramework.Controls.MetroLink()
        Me.folderlist = New System.Windows.Forms.ListView()
        Me.operators = New System.Windows.Forms.GroupBox()
        Me.operatorlist = New System.Windows.Forms.ListView()
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ContextMenuStrip2 = New MetroFramework.Controls.MetroContextMenu(Me.components)
        Me.ResetOperatorPassword123ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripProgressBar1 = New MetroFramework.Controls.MetroProgressBar()
        Me.MetroLabel1 = New MetroFramework.Controls.MetroLabel()
        Me.serverlist = New System.Windows.Forms.ListView()
        Me.country = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.servername = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ip = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.statuss = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.vers = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ContextMenuStrip4 = New MetroFramework.Controls.MetroContextMenu(Me.components)
        Me.OpenInSCCMToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MstscToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GetServerLogsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshStatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MetroLabel13 = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel14 = New MetroFramework.Controls.MetroLabel()
        Me.MetroContextMenu1 = New MetroFramework.Controls.MetroContextMenu(Me.components)
        Me.DBQueriesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BarcodeGeneratorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DiscountExtractToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DiscountTableToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InputTableToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HotfixesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FavLinksToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MetroButton1 = New MetroFramework.Controls.MetroButton()
        Me.MetroStyleManager1 = New MetroFramework.Components.MetroStyleManager(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip7 = New MetroFramework.Controls.MetroContextMenu(Me.components)
        Me.OpenMPOSToolAppToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitMPOSToolAppToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MetroLabel17 = New MetroFramework.Controls.MetroLabel()
        Me.ContextMenuStrip6 = New MetroFramework.Controls.MetroContextMenu(Me.components)
        Me.RouterxmlToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MetroLabel18 = New MetroFramework.Controls.MetroLabel()
        Me.folder = New System.Windows.Forms.GroupBox()
        Me.MetroToolTip1 = New MetroFramework.Components.MetroToolTip()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.GroupBox1.SuspendLayout()
        Me.status.SuspendLayout()
        Me.ContextMenuStrip3.SuspendLayout()
        Me.tills.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.metro, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.operators.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.ContextMenuStrip4.SuspendLayout()
        Me.MetroContextMenu1.SuspendLayout()
        CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip7.SuspendLayout()
        Me.ContextMenuStrip6.SuspendLayout()
        Me.folder.SuspendLayout()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.MetroRadioButton4)
        Me.GroupBox1.Controls.Add(Me.MetroRadioButton3)
        Me.GroupBox1.Controls.Add(Me.MetroRadioButton2)
        Me.GroupBox1.Controls.Add(Me.MetroRadioButton1)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.GroupBox1.Location = New System.Drawing.Point(24, 64)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(467, 46)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Environment"
        '
        'MetroRadioButton4
        '
        Me.MetroRadioButton4.AutoSize = True
        Me.MetroRadioButton4.Location = New System.Drawing.Point(257, 24)
        Me.MetroRadioButton4.Name = "MetroRadioButton4"
        Me.MetroRadioButton4.Size = New System.Drawing.Size(54, 15)
        Me.MetroRadioButton4.Style = MetroFramework.MetroColorStyle.Purple
        Me.MetroRadioButton4.TabIndex = 3
        Me.MetroRadioButton4.Text = "PROD"
        Me.MetroRadioButton4.UseSelectable = True
        Me.MetroRadioButton4.UseStyleColors = True
        '
        'MetroRadioButton3
        '
        Me.MetroRadioButton3.AutoSize = True
        Me.MetroRadioButton3.Location = New System.Drawing.Point(196, 24)
        Me.MetroRadioButton3.Name = "MetroRadioButton3"
        Me.MetroRadioButton3.Size = New System.Drawing.Size(44, 15)
        Me.MetroRadioButton3.Style = MetroFramework.MetroColorStyle.Pink
        Me.MetroRadioButton3.TabIndex = 2
        Me.MetroRadioButton3.Text = "DEV"
        Me.MetroRadioButton3.UseSelectable = True
        Me.MetroRadioButton3.UseStyleColors = True
        '
        'MetroRadioButton2
        '
        Me.MetroRadioButton2.AutoSize = True
        Me.MetroRadioButton2.Location = New System.Drawing.Point(135, 24)
        Me.MetroRadioButton2.Name = "MetroRadioButton2"
        Me.MetroRadioButton2.Size = New System.Drawing.Size(46, 15)
        Me.MetroRadioButton2.Style = MetroFramework.MetroColorStyle.Magenta
        Me.MetroRadioButton2.TabIndex = 1
        Me.MetroRadioButton2.Text = "UAT"
        Me.MetroRadioButton2.UseSelectable = True
        Me.MetroRadioButton2.UseStyleColors = True
        '
        'MetroRadioButton1
        '
        Me.MetroRadioButton1.AutoSize = True
        Me.MetroRadioButton1.Location = New System.Drawing.Point(80, 24)
        Me.MetroRadioButton1.Name = "MetroRadioButton1"
        Me.MetroRadioButton1.Size = New System.Drawing.Size(40, 15)
        Me.MetroRadioButton1.Style = MetroFramework.MetroColorStyle.Green
        Me.MetroRadioButton1.TabIndex = 0
        Me.MetroRadioButton1.Text = "QA"
        Me.MetroRadioButton1.UseSelectable = True
        Me.MetroRadioButton1.UseStyleColors = True
        '
        'status
        '
        Me.status.Controls.Add(Me.MetroLabel12)
        Me.status.Controls.Add(Me.MetroLabel8)
        Me.status.Controls.Add(Me.MetroLabel7)
        Me.status.Controls.Add(Me.MetroButton2)
        Me.status.Controls.Add(Me.ccd)
        Me.status.Controls.Add(Me.MetroLabel16)
        Me.status.Controls.Add(Me.MetroLabel15)
        Me.status.Controls.Add(Me.servicelist)
        Me.status.Controls.Add(Me.MetroLabel11)
        Me.status.Controls.Add(Me.MetroLabel10)
        Me.status.Controls.Add(Me.MetroLabel9)
        Me.status.Controls.Add(Me.MetroLabel6)
        Me.status.Controls.Add(Me.MetroLabel5)
        Me.status.Controls.Add(Me.MetroLabel4)
        Me.status.Controls.Add(Me.MetroLabel3)
        Me.status.Controls.Add(Me.MetroLabel2)
        Me.status.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.status.Location = New System.Drawing.Point(497, 64)
        Me.status.Name = "status"
        Me.status.Size = New System.Drawing.Size(508, 286)
        Me.status.TabIndex = 2
        Me.status.TabStop = False
        Me.status.Text = "Server/Service Status"
        Me.status.Visible = False
        '
        'MetroLabel12
        '
        Me.MetroLabel12.AutoSize = True
        Me.MetroLabel12.Location = New System.Drawing.Point(320, 93)
        Me.MetroLabel12.Name = "MetroLabel12"
        Me.MetroLabel12.Size = New System.Drawing.Size(89, 19)
        Me.MetroLabel12.TabIndex = 33
        Me.MetroLabel12.Text = "Database info"
        '
        'MetroLabel8
        '
        Me.MetroLabel8.AutoSize = True
        Me.MetroLabel8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MetroLabel8.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.MetroLabel8.LinkColor = System.Drawing.Color.Black
        Me.MetroLabel8.Location = New System.Drawing.Point(79, 49)
        Me.MetroLabel8.Name = "MetroLabel8"
        Me.MetroLabel8.Size = New System.Drawing.Size(19, 15)
        Me.MetroLabel8.TabIndex = 32
        Me.MetroLabel8.TabStop = True
        Me.MetroLabel8.Text = "..."
        '
        'MetroLabel7
        '
        Me.MetroLabel7.AutoSize = True
        Me.MetroLabel7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MetroLabel7.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.MetroLabel7.LinkColor = System.Drawing.Color.Black
        Me.MetroLabel7.Location = New System.Drawing.Point(79, 31)
        Me.MetroLabel7.Name = "MetroLabel7"
        Me.MetroLabel7.Size = New System.Drawing.Size(19, 15)
        Me.MetroLabel7.TabIndex = 31
        Me.MetroLabel7.TabStop = True
        Me.MetroLabel7.Text = "..."
        '
        'MetroButton2
        '
        Me.MetroButton2.Location = New System.Drawing.Point(7, 150)
        Me.MetroButton2.Name = "MetroButton2"
        Me.MetroButton2.Size = New System.Drawing.Size(494, 130)
        Me.MetroButton2.Style = MetroFramework.MetroColorStyle.Orange
        Me.MetroButton2.TabIndex = 14
        Me.MetroButton2.Text = "Get services"
        Me.MetroButton2.UseSelectable = True
        Me.MetroButton2.UseStyleColors = True
        '
        'ccd
        '
        Me.ccd.Location = New System.Drawing.Point(464, 242)
        Me.ccd.Maximum = 100
        Me.ccd.Name = "ccd"
        Me.ccd.Size = New System.Drawing.Size(30, 28)
        Me.ccd.TabIndex = 13
        Me.ccd.UseSelectable = True
        Me.ccd.Value = 20
        '
        'MetroLabel16
        '
        Me.MetroLabel16.AutoSize = True
        Me.MetroLabel16.Location = New System.Drawing.Point(320, 64)
        Me.MetroLabel16.Name = "MetroLabel16"
        Me.MetroLabel16.Size = New System.Drawing.Size(93, 19)
        Me.MetroLabel16.TabIndex = 12
        Me.MetroLabel16.Text = "DB is OFFLINE"
        Me.MetroLabel16.UseStyleColors = True
        '
        'MetroLabel15
        '
        Me.MetroLabel15.AutoSize = True
        Me.MetroLabel15.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.MetroLabel15.FontWeight = MetroFramework.MetroLabelWeight.Bold
        Me.MetroLabel15.Location = New System.Drawing.Point(320, 31)
        Me.MetroLabel15.Name = "MetroLabel15"
        Me.MetroLabel15.Size = New System.Drawing.Size(83, 25)
        Me.MetroLabel15.TabIndex = 11
        Me.MetroLabel15.Text = "OFFLINE"
        Me.MetroLabel15.UseStyleColors = True
        '
        'servicelist
        '
        Me.servicelist.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.servicelist.ContextMenuStrip = Me.ContextMenuStrip3
        Me.servicelist.Location = New System.Drawing.Point(7, 124)
        Me.servicelist.Name = "servicelist"
        Me.servicelist.Size = New System.Drawing.Size(494, 156)
        Me.servicelist.TabIndex = 10
        Me.servicelist.UseCompatibleStateImageBehavior = False
        Me.servicelist.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Service"
        Me.ColumnHeader1.Width = 205
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Service Name"
        Me.ColumnHeader2.Width = 191
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Status"
        '
        'ContextMenuStrip3
        '
        Me.ContextMenuStrip3.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        Me.ContextMenuStrip3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.ContextMenuStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RestartServiceToolStripMenuItem, Me.StartStopServiceToolStripMenuItem, Me.RefreshToolStripMenuItem1})
        Me.ContextMenuStrip3.Name = "ContextMenuStrip3"
        Me.ContextMenuStrip3.Size = New System.Drawing.Size(168, 70)
        Me.ContextMenuStrip3.Theme = MetroFramework.MetroThemeStyle.Dark
        Me.ContextMenuStrip3.UseStyleColors = True
        '
        'RestartServiceToolStripMenuItem
        '
        Me.RestartServiceToolStripMenuItem.Name = "RestartServiceToolStripMenuItem"
        Me.RestartServiceToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.RestartServiceToolStripMenuItem.Text = "Restart Service"
        '
        'StartStopServiceToolStripMenuItem
        '
        Me.StartStopServiceToolStripMenuItem.Name = "StartStopServiceToolStripMenuItem"
        Me.StartStopServiceToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.StartStopServiceToolStripMenuItem.Text = "Start/Stop Service"
        '
        'RefreshToolStripMenuItem1
        '
        Me.RefreshToolStripMenuItem1.Name = "RefreshToolStripMenuItem1"
        Me.RefreshToolStripMenuItem1.Size = New System.Drawing.Size(167, 22)
        Me.RefreshToolStripMenuItem1.Text = "Refresh Services"
        '
        'MetroLabel11
        '
        Me.MetroLabel11.AutoSize = True
        Me.MetroLabel11.Location = New System.Drawing.Point(80, 102)
        Me.MetroLabel11.Name = "MetroLabel11"
        Me.MetroLabel11.Size = New System.Drawing.Size(18, 19)
        Me.MetroLabel11.TabIndex = 9
        Me.MetroLabel11.Text = "..."
        '
        'MetroLabel10
        '
        Me.MetroLabel10.AutoSize = True
        Me.MetroLabel10.Location = New System.Drawing.Point(80, 83)
        Me.MetroLabel10.Name = "MetroLabel10"
        Me.MetroLabel10.Size = New System.Drawing.Size(18, 19)
        Me.MetroLabel10.TabIndex = 8
        Me.MetroLabel10.Text = "..."
        '
        'MetroLabel9
        '
        Me.MetroLabel9.AutoSize = True
        Me.MetroLabel9.Location = New System.Drawing.Point(80, 64)
        Me.MetroLabel9.Name = "MetroLabel9"
        Me.MetroLabel9.Size = New System.Drawing.Size(18, 19)
        Me.MetroLabel9.TabIndex = 7
        Me.MetroLabel9.Text = "..."
        '
        'MetroLabel6
        '
        Me.MetroLabel6.AutoSize = True
        Me.MetroLabel6.Location = New System.Drawing.Point(16, 102)
        Me.MetroLabel6.Name = "MetroLabel6"
        Me.MetroLabel6.Size = New System.Drawing.Size(55, 19)
        Me.MetroLabel6.TabIndex = 4
        Me.MetroLabel6.Text = "Version:"
        '
        'MetroLabel5
        '
        Me.MetroLabel5.AutoSize = True
        Me.MetroLabel5.Location = New System.Drawing.Point(16, 83)
        Me.MetroLabel5.Name = "MetroLabel5"
        Me.MetroLabel5.Size = New System.Drawing.Size(43, 19)
        Me.MetroLabel5.TabIndex = 3
        Me.MetroLabel5.Text = "Store:"
        '
        'MetroLabel4
        '
        Me.MetroLabel4.AutoSize = True
        Me.MetroLabel4.Location = New System.Drawing.Point(16, 64)
        Me.MetroLabel4.Name = "MetroLabel4"
        Me.MetroLabel4.Size = New System.Drawing.Size(58, 19)
        Me.MetroLabel4.TabIndex = 2
        Me.MetroLabel4.Text = "Country:"
        '
        'MetroLabel3
        '
        Me.MetroLabel3.AutoSize = True
        Me.MetroLabel3.Location = New System.Drawing.Point(16, 45)
        Me.MetroLabel3.Name = "MetroLabel3"
        Me.MetroLabel3.Size = New System.Drawing.Size(23, 19)
        Me.MetroLabel3.TabIndex = 1
        Me.MetroLabel3.Text = "IP:"
        '
        'MetroLabel2
        '
        Me.MetroLabel2.AutoSize = True
        Me.MetroLabel2.Location = New System.Drawing.Point(16, 26)
        Me.MetroLabel2.Name = "MetroLabel2"
        Me.MetroLabel2.Size = New System.Drawing.Size(48, 19)
        Me.MetroLabel2.TabIndex = 0
        Me.MetroLabel2.Text = "Name:"
        '
        'tills
        '
        Me.tills.Controls.Add(Me.tpb)
        Me.tills.Controls.Add(Me.tilllist)
        Me.tills.Controls.Add(Me.metro)
        Me.tills.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.tills.Location = New System.Drawing.Point(497, 357)
        Me.tills.Name = "tills"
        Me.tills.Size = New System.Drawing.Size(507, 234)
        Me.tills.TabIndex = 3
        Me.tills.TabStop = False
        Me.tills.Text = "Tills"
        Me.tills.Visible = False
        '
        'tpb
        '
        Me.tpb.Enabled = False
        Me.tpb.Location = New System.Drawing.Point(464, 193)
        Me.tpb.Maximum = 100
        Me.tpb.Name = "tpb"
        Me.tpb.Size = New System.Drawing.Size(30, 28)
        Me.tpb.TabIndex = 14
        Me.tpb.UseSelectable = True
        Me.tpb.Value = 20
        Me.tpb.Visible = False
        '
        'tilllist
        '
        Me.tilllist.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.nr, Me.Tillname, Me.tillno, Me.Tilltype, Me.Signon, Me.TillIP, Me.Tillstatus, Me.printertype})
        Me.tilllist.ContextMenuStrip = Me.ContextMenuStrip1
        Me.tilllist.Cursor = System.Windows.Forms.Cursors.Hand
        Me.tilllist.FullRowSelect = True
        Me.tilllist.Location = New System.Drawing.Point(6, 25)
        Me.tilllist.MultiSelect = False
        Me.tilllist.Name = "tilllist"
        Me.tilllist.ShowGroups = False
        Me.tilllist.Size = New System.Drawing.Size(495, 203)
        Me.tilllist.TabIndex = 1
        Me.tilllist.UseCompatibleStateImageBehavior = False
        Me.tilllist.View = System.Windows.Forms.View.Details
        '
        'nr
        '
        Me.nr.Width = 0
        '
        'Tillname
        '
        Me.Tillname.Text = "Till Name"
        Me.Tillname.Width = 120
        '
        'tillno
        '
        Me.tillno.Text = "No."
        Me.tillno.Width = 40
        '
        'Tilltype
        '
        Me.Tilltype.Text = "Till Type"
        '
        'Signon
        '
        Me.Signon.Text = "Op."
        Me.Signon.Width = 30
        '
        'TillIP
        '
        Me.TillIP.Text = "Till IP"
        Me.TillIP.Width = 90
        '
        'Tillstatus
        '
        Me.Tillstatus.Text = "Status"
        Me.Tillstatus.Width = 35
        '
        'printertype
        '
        Me.printertype.Text = "Printer"
        Me.printertype.Width = 50
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        Me.ContextMenuStrip1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenInSCCMToolStripMenuItem1, Me.MstscToolStripMenuItem1, Me.RestartTillToolStripMenuItem, Me.ForceSignOutToolStripMenuItem, Me.GetLogsToolStripMenuItem, Me.SetPrinterToolStripMenuItem, Me.RefreshDBToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(153, 158)
        Me.ContextMenuStrip1.Theme = MetroFramework.MetroThemeStyle.Dark
        Me.ContextMenuStrip1.UseStyleColors = True
        '
        'OpenInSCCMToolStripMenuItem1
        '
        Me.OpenInSCCMToolStripMenuItem1.Name = "OpenInSCCMToolStripMenuItem1"
        Me.OpenInSCCMToolStripMenuItem1.Size = New System.Drawing.Size(152, 22)
        Me.OpenInSCCMToolStripMenuItem1.Text = "Open in SCCM"
        '
        'MstscToolStripMenuItem1
        '
        Me.MstscToolStripMenuItem1.Name = "MstscToolStripMenuItem1"
        Me.MstscToolStripMenuItem1.Size = New System.Drawing.Size(152, 22)
        Me.MstscToolStripMenuItem1.Text = "mstsc"
        '
        'RestartTillToolStripMenuItem
        '
        Me.RestartTillToolStripMenuItem.Name = "RestartTillToolStripMenuItem"
        Me.RestartTillToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.RestartTillToolStripMenuItem.Text = "Restart Till"
        '
        'ForceSignOutToolStripMenuItem
        '
        Me.ForceSignOutToolStripMenuItem.Name = "ForceSignOutToolStripMenuItem"
        Me.ForceSignOutToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ForceSignOutToolStripMenuItem.Text = "Force SignOff"
        '
        'GetLogsToolStripMenuItem
        '
        Me.GetLogsToolStripMenuItem.Name = "GetLogsToolStripMenuItem"
        Me.GetLogsToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.GetLogsToolStripMenuItem.Text = "Get Till Logs"
        '
        'SetPrinterToolStripMenuItem
        '
        Me.SetPrinterToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NoneToolStripMenuItem, Me.MatrixToolStripMenuItem, Me.LaserToolStripMenuItem})
        Me.SetPrinterToolStripMenuItem.Name = "SetPrinterToolStripMenuItem"
        Me.SetPrinterToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.SetPrinterToolStripMenuItem.Text = "Set Printer"
        '
        'NoneToolStripMenuItem
        '
        Me.NoneToolStripMenuItem.Name = "NoneToolStripMenuItem"
        Me.NoneToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.NoneToolStripMenuItem.Text = "0 : None"
        '
        'MatrixToolStripMenuItem
        '
        Me.MatrixToolStripMenuItem.Name = "MatrixToolStripMenuItem"
        Me.MatrixToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.MatrixToolStripMenuItem.Text = "1 : Matrix"
        '
        'LaserToolStripMenuItem
        '
        Me.LaserToolStripMenuItem.Name = "LaserToolStripMenuItem"
        Me.LaserToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.LaserToolStripMenuItem.Text = "2 : Laser"
        '
        'RefreshDBToolStripMenuItem
        '
        Me.RefreshDBToolStripMenuItem.Name = "RefreshDBToolStripMenuItem"
        Me.RefreshDBToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.RefreshDBToolStripMenuItem.Text = "Refresh DB"
        '
        'metro
        '
        Me.metro.Location = New System.Drawing.Point(391, 57)
        Me.metro.Name = "metro"
        Me.metro.Size = New System.Drawing.Size(178, 50)
        Me.metro.TabIndex = 15
        Me.metro.TabStop = False
        Me.metro.Visible = False
        '
        'LinkLabel1
        '
        Me.LinkLabel1.Location = New System.Drawing.Point(11, 252)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(143, 19)
        Me.LinkLabel1.TabIndex = 1
        Me.LinkLabel1.Text = "Metro.MPOS.Router.xml"
        Me.LinkLabel1.UseSelectable = True
        '
        'folderlist
        '
        Me.folderlist.Cursor = System.Windows.Forms.Cursors.Hand
        Me.folderlist.Location = New System.Drawing.Point(6, 22)
        Me.folderlist.Name = "folderlist"
        Me.folderlist.Size = New System.Drawing.Size(154, 249)
        Me.folderlist.TabIndex = 0
        Me.folderlist.UseCompatibleStateImageBehavior = False
        Me.folderlist.View = System.Windows.Forms.View.List
        Me.folderlist.Visible = False
        '
        'operators
        '
        Me.operators.Controls.Add(Me.operatorlist)
        Me.operators.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.operators.Location = New System.Drawing.Point(1011, 357)
        Me.operators.Name = "operators"
        Me.operators.Size = New System.Drawing.Size(166, 234)
        Me.operators.TabIndex = 5
        Me.operators.TabStop = False
        Me.operators.Text = "Operators"
        Me.operators.Visible = False
        '
        'operatorlist
        '
        Me.operatorlist.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader5, Me.ColumnHeader6})
        Me.operatorlist.ContextMenuStrip = Me.ContextMenuStrip2
        Me.operatorlist.Cursor = System.Windows.Forms.Cursors.Hand
        Me.operatorlist.FullRowSelect = True
        Me.operatorlist.Location = New System.Drawing.Point(6, 25)
        Me.operatorlist.Name = "operatorlist"
        Me.operatorlist.Size = New System.Drawing.Size(154, 203)
        Me.operatorlist.TabIndex = 2
        Me.operatorlist.UseCompatibleStateImageBehavior = False
        Me.operatorlist.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Op."
        Me.ColumnHeader5.Width = 30
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Profile"
        Me.ColumnHeader6.Width = 75
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        Me.ContextMenuStrip2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ResetOperatorPassword123ToolStripMenuItem, Me.RefreshToolStripMenuItem2})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(183, 48)
        Me.ContextMenuStrip2.Theme = MetroFramework.MetroThemeStyle.Dark
        Me.ContextMenuStrip2.UseStyleColors = True
        '
        'ResetOperatorPassword123ToolStripMenuItem
        '
        Me.ResetOperatorPassword123ToolStripMenuItem.Name = "ResetOperatorPassword123ToolStripMenuItem"
        Me.ResetOperatorPassword123ToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.ResetOperatorPassword123ToolStripMenuItem.Text = "Reset Password : 123"
        '
        'RefreshToolStripMenuItem2
        '
        Me.RefreshToolStripMenuItem2.Name = "RefreshToolStripMenuItem2"
        Me.RefreshToolStripMenuItem2.Size = New System.Drawing.Size(182, 22)
        Me.RefreshToolStripMenuItem2.Text = "Refresh DB"
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Location = New System.Drawing.Point(24, 628)
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(440, 23)
        Me.ToolStripProgressBar1.TabIndex = 6
        '
        'MetroLabel1
        '
        Me.MetroLabel1.AutoSize = True
        Me.MetroLabel1.Location = New System.Drawing.Point(24, 573)
        Me.MetroLabel1.Name = "MetroLabel1"
        Me.MetroLabel1.Size = New System.Drawing.Size(100, 19)
        Me.MetroLabel1.TabIndex = 7
        Me.MetroLabel1.Text = "Loading servers"
        '
        'serverlist
        '
        Me.serverlist.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.serverlist.AutoArrange = False
        Me.serverlist.BackColor = System.Drawing.Color.White
        Me.serverlist.BackgroundImageTiled = True
        Me.serverlist.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.country, Me.servername, Me.ip, Me.statuss, Me.vers})
        Me.serverlist.ContextMenuStrip = Me.ContextMenuStrip4
        Me.serverlist.Cursor = System.Windows.Forms.Cursors.Hand
        Me.serverlist.Font = New System.Drawing.Font("Arial Narrow", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.serverlist.FullRowSelect = True
        Me.serverlist.GridLines = True
        ListViewGroup1.Header = "QA"
        ListViewGroup1.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center
        ListViewGroup1.Name = "ListViewGroup1"
        ListViewGroup2.Header = "UAT"
        ListViewGroup2.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center
        ListViewGroup2.Name = "ListViewGroup2"
        ListViewGroup3.Header = "PROD"
        ListViewGroup3.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center
        ListViewGroup3.Name = "ListViewGroup3"
        ListViewGroup4.Header = "DEV"
        ListViewGroup4.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center
        ListViewGroup4.Name = "ListViewGroup4"
        Me.serverlist.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2, ListViewGroup3, ListViewGroup4})
        Me.serverlist.HideSelection = False
        Me.serverlist.LabelWrap = False
        Me.serverlist.Location = New System.Drawing.Point(24, 116)
        Me.serverlist.MultiSelect = False
        Me.serverlist.Name = "serverlist"
        Me.serverlist.Size = New System.Drawing.Size(467, 475)
        Me.serverlist.TabIndex = 1
        Me.serverlist.TileSize = New System.Drawing.Size(100, 100)
        Me.serverlist.UseCompatibleStateImageBehavior = False
        Me.serverlist.View = System.Windows.Forms.View.Details
        '
        'country
        '
        Me.country.Text = "Country"
        Me.country.Width = 77
        '
        'servername
        '
        Me.servername.Text = "Server Name"
        Me.servername.Width = 111
        '
        'ip
        '
        Me.ip.Text = "IP"
        Me.ip.Width = 74
        '
        'statuss
        '
        Me.statuss.Text = "Status"
        Me.statuss.Width = 42
        '
        'vers
        '
        Me.vers.Text = "Version"
        Me.vers.Width = 120
        '
        'ContextMenuStrip4
        '
        Me.ContextMenuStrip4.AllowMerge = False
        Me.ContextMenuStrip4.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        Me.ContextMenuStrip4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.ContextMenuStrip4.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenInSCCMToolStripMenuItem, Me.MstscToolStripMenuItem, Me.GetServerLogsToolStripMenuItem, Me.RefreshStatusToolStripMenuItem})
        Me.ContextMenuStrip4.Name = "ContextMenuStrip4"
        Me.ContextMenuStrip4.Size = New System.Drawing.Size(156, 92)
        Me.ContextMenuStrip4.Theme = MetroFramework.MetroThemeStyle.Dark
        Me.ContextMenuStrip4.UseStyleColors = True
        '
        'OpenInSCCMToolStripMenuItem
        '
        Me.OpenInSCCMToolStripMenuItem.Name = "OpenInSCCMToolStripMenuItem"
        Me.OpenInSCCMToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.OpenInSCCMToolStripMenuItem.Text = "Open in SCCM"
        '
        'MstscToolStripMenuItem
        '
        Me.MstscToolStripMenuItem.Name = "MstscToolStripMenuItem"
        Me.MstscToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.MstscToolStripMenuItem.Text = "mstsc"
        '
        'GetServerLogsToolStripMenuItem
        '
        Me.GetServerLogsToolStripMenuItem.Name = "GetServerLogsToolStripMenuItem"
        Me.GetServerLogsToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.GetServerLogsToolStripMenuItem.Text = "Get Server Logs"
        '
        'RefreshStatusToolStripMenuItem
        '
        Me.RefreshStatusToolStripMenuItem.Name = "RefreshStatusToolStripMenuItem"
        Me.RefreshStatusToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.RefreshStatusToolStripMenuItem.Text = "Refresh Servers"
        '
        'MetroLabel13
        '
        Me.MetroLabel13.AutoSize = True
        Me.MetroLabel13.FontSize = MetroFramework.MetroLabelSize.Small
        Me.MetroLabel13.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.MetroLabel13.ForeColor = System.Drawing.Color.Orange
        Me.MetroLabel13.Location = New System.Drawing.Point(1035, 52)
        Me.MetroLabel13.Name = "MetroLabel13"
        Me.MetroLabel13.Size = New System.Drawing.Size(49, 15)
        Me.MetroLabel13.Style = MetroFramework.MetroColorStyle.Green
        Me.MetroLabel13.TabIndex = 16
        Me.MetroLabel13.Text = "Settings"
        Me.MetroLabel13.UseStyleColors = True
        Me.MetroLabel13.Visible = False
        '
        'MetroLabel14
        '
        Me.MetroLabel14.AutoSize = True
        Me.MetroLabel14.FontSize = MetroFramework.MetroLabelSize.Small
        Me.MetroLabel14.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.MetroLabel14.ForeColor = System.Drawing.Color.Orange
        Me.MetroLabel14.Location = New System.Drawing.Point(1141, 52)
        Me.MetroLabel14.Name = "MetroLabel14"
        Me.MetroLabel14.Size = New System.Drawing.Size(30, 15)
        Me.MetroLabel14.Style = MetroFramework.MetroColorStyle.Red
        Me.MetroLabel14.TabIndex = 17
        Me.MetroLabel14.Text = "Quit"
        Me.MetroLabel14.UseStyleColors = True
        Me.MetroLabel14.Visible = False
        '
        'MetroContextMenu1
        '
        Me.MetroContextMenu1.AccessibleRole = System.Windows.Forms.AccessibleRole.Window
        Me.MetroContextMenu1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.MetroContextMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DBQueriesToolStripMenuItem, Me.BarcodeGeneratorToolStripMenuItem, Me.DiscountExtractToolStripMenuItem, Me.HotfixesToolStripMenuItem, Me.FavLinksToolStripMenuItem, Me.ToolStripSeparator1, Me.ToolStripMenuItem1})
        Me.MetroContextMenu1.Name = "MetroContextMenu1"
        Me.MetroContextMenu1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.MetroContextMenu1.Size = New System.Drawing.Size(173, 142)
        Me.MetroContextMenu1.Style = MetroFramework.MetroColorStyle.Orange
        Me.MetroContextMenu1.Theme = MetroFramework.MetroThemeStyle.Light
        Me.MetroContextMenu1.UseStyleColors = True
        '
        'DBQueriesToolStripMenuItem
        '
        Me.DBQueriesToolStripMenuItem.Name = "DBQueriesToolStripMenuItem"
        Me.DBQueriesToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.DBQueriesToolStripMenuItem.Text = "DB Queries"
        '
        'BarcodeGeneratorToolStripMenuItem
        '
        Me.BarcodeGeneratorToolStripMenuItem.Name = "BarcodeGeneratorToolStripMenuItem"
        Me.BarcodeGeneratorToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.BarcodeGeneratorToolStripMenuItem.Text = "Barcode Generator"
        '
        'DiscountExtractToolStripMenuItem
        '
        Me.DiscountExtractToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DiscountTableToolStripMenuItem, Me.InputTableToolStripMenuItem})
        Me.DiscountExtractToolStripMenuItem.Name = "DiscountExtractToolStripMenuItem"
        Me.DiscountExtractToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.DiscountExtractToolStripMenuItem.Text = "DB table to XLS"
        '
        'DiscountTableToolStripMenuItem
        '
        Me.DiscountTableToolStripMenuItem.Name = "DiscountTableToolStripMenuItem"
        Me.DiscountTableToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.DiscountTableToolStripMenuItem.Text = "Discount tables"
        '
        'InputTableToolStripMenuItem
        '
        Me.InputTableToolStripMenuItem.Name = "InputTableToolStripMenuItem"
        Me.InputTableToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.InputTableToolStripMenuItem.Text = "Select table"
        '
        'HotfixesToolStripMenuItem
        '
        Me.HotfixesToolStripMenuItem.Name = "HotfixesToolStripMenuItem"
        Me.HotfixesToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.HotfixesToolStripMenuItem.Text = "Hotfixes"
        '
        'FavLinksToolStripMenuItem
        '
        Me.FavLinksToolStripMenuItem.Name = "FavLinksToolStripMenuItem"
        Me.FavLinksToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.FavLinksToolStripMenuItem.Text = "Fav Links"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(169, 6)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(172, 22)
        Me.ToolStripMenuItem1.Text = "Quit"
        '
        'MetroButton1
        '
        Me.MetroButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.MetroButton1.Location = New System.Drawing.Point(79, 17)
        Me.MetroButton1.Name = "MetroButton1"
        Me.MetroButton1.Size = New System.Drawing.Size(173, 36)
        Me.MetroButton1.Style = MetroFramework.MetroColorStyle.Orange
        Me.MetroButton1.TabIndex = 18
        Me.MetroButton1.Text = "Main Menu"
        Me.MetroButton1.UseSelectable = True
        Me.MetroButton1.UseStyleColors = True
        '
        'MetroStyleManager1
        '
        Me.MetroStyleManager1.Owner = Me
        Me.MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Orange
        '
        'Timer1
        '
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.ContextMenuStrip = Me.ContextMenuStrip7
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "MPOS Server Tool Application"
        Me.NotifyIcon1.Visible = True
        '
        'ContextMenuStrip7
        '
        Me.ContextMenuStrip7.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenMPOSToolAppToolStripMenuItem, Me.ExitMPOSToolAppToolStripMenuItem})
        Me.ContextMenuStrip7.Name = "ContextMenuStrip7"
        Me.ContextMenuStrip7.Size = New System.Drawing.Size(125, 48)
        Me.ContextMenuStrip7.UseStyleColors = True
        '
        'OpenMPOSToolAppToolStripMenuItem
        '
        Me.OpenMPOSToolAppToolStripMenuItem.Name = "OpenMPOSToolAppToolStripMenuItem"
        Me.OpenMPOSToolAppToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.OpenMPOSToolAppToolStripMenuItem.Text = "Maximize"
        '
        'ExitMPOSToolAppToolStripMenuItem
        '
        Me.ExitMPOSToolAppToolStripMenuItem.Name = "ExitMPOSToolAppToolStripMenuItem"
        Me.ExitMPOSToolAppToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.ExitMPOSToolAppToolStripMenuItem.Text = "Exit"
        '
        'MetroLabel17
        '
        Me.MetroLabel17.AutoSize = True
        Me.MetroLabel17.Location = New System.Drawing.Point(1011, 598)
        Me.MetroLabel17.Name = "MetroLabel17"
        Me.MetroLabel17.Size = New System.Drawing.Size(90, 19)
        Me.MetroLabel17.TabIndex = 19
        Me.MetroLabel17.Text = "MetroLabel17"
        '
        'ContextMenuStrip6
        '
        Me.ContextMenuStrip6.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RouterxmlToolStripMenuItem, Me.RefreshToolStripMenuItem})
        Me.ContextMenuStrip6.Name = "ContextMenuStrip6"
        Me.ContextMenuStrip6.Size = New System.Drawing.Size(150, 48)
        Me.ContextMenuStrip6.Theme = MetroFramework.MetroThemeStyle.Dark
        Me.ContextMenuStrip6.UseStyleColors = True
        '
        'RouterxmlToolStripMenuItem
        '
        Me.RouterxmlToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem, Me.CreateToolStripMenuItem})
        Me.RouterxmlToolStripMenuItem.Name = "RouterxmlToolStripMenuItem"
        Me.RouterxmlToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
        Me.RouterxmlToolStripMenuItem.Text = "Router.xml"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'CreateToolStripMenuItem
        '
        Me.CreateToolStripMenuItem.Name = "CreateToolStripMenuItem"
        Me.CreateToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.CreateToolStripMenuItem.Text = "Create/Overwrite"
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
        Me.RefreshToolStripMenuItem.Text = "Refresh Folder"
        '
        'MetroLabel18
        '
        Me.MetroLabel18.AutoSize = True
        Me.MetroLabel18.FontSize = MetroFramework.MetroLabelSize.Small
        Me.MetroLabel18.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.MetroLabel18.ForeColor = System.Drawing.Color.Orange
        Me.MetroLabel18.Location = New System.Drawing.Point(1081, 52)
        Me.MetroLabel18.Name = "MetroLabel18"
        Me.MetroLabel18.Size = New System.Drawing.Size(56, 15)
        Me.MetroLabel18.Style = MetroFramework.MetroColorStyle.Yellow
        Me.MetroLabel18.TabIndex = 24
        Me.MetroLabel18.Text = "Minimize"
        Me.MetroLabel18.UseStyleColors = True
        Me.MetroLabel18.Visible = False
        '
        'folder
        '
        Me.folder.Controls.Add(Me.LinkLabel1)
        Me.folder.Controls.Add(Me.folderlist)
        Me.folder.Location = New System.Drawing.Point(1011, 73)
        Me.folder.Name = "folder"
        Me.folder.Size = New System.Drawing.Size(166, 277)
        Me.folder.TabIndex = 34
        Me.folder.TabStop = False
        Me.folder.Text = "Folder"
        Me.folder.Visible = False
        '
        'MetroToolTip1
        '
        Me.MetroToolTip1.Style = MetroFramework.MetroColorStyle.Blue
        Me.MetroToolTip1.StyleManager = Nothing
        Me.MetroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light
        Me.MetroToolTip1.UseAnimation = False
        Me.MetroToolTip1.UseFading = False
        '
        'PictureBox8
        '
        Me.PictureBox8.Image = Global.MPOS.My.Resources.Resources.minimize
        Me.PictureBox8.Location = New System.Drawing.Point(1083, 12)
        Me.PictureBox8.Margin = New System.Windows.Forms.Padding(0)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(48, 36)
        Me.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox8.TabIndex = 23
        Me.PictureBox8.TabStop = False
        '
        'PictureBox7
        '
        Me.PictureBox7.Image = Global.MPOS.My.Resources.Resources.quit
        Me.PictureBox7.Location = New System.Drawing.Point(1131, 12)
        Me.PictureBox7.Margin = New System.Windows.Forms.Padding(0)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(48, 36)
        Me.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox7.TabIndex = 22
        Me.PictureBox7.TabStop = False
        '
        'PictureBox5
        '
        Me.PictureBox5.Image = Global.MPOS.My.Resources.Resources.settings
        Me.PictureBox5.Location = New System.Drawing.Point(1036, 12)
        Me.PictureBox5.Margin = New System.Windows.Forms.Padding(0)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(48, 36)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox5.TabIndex = 20
        Me.PictureBox5.TabStop = False
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.ClientSize = New System.Drawing.Size(1200, 659)
        Me.ControlBox = False
        Me.Controls.Add(Me.folder)
        Me.Controls.Add(Me.MetroLabel18)
        Me.Controls.Add(Me.PictureBox8)
        Me.Controls.Add(Me.PictureBox7)
        Me.Controls.Add(Me.PictureBox5)
        Me.Controls.Add(Me.MetroLabel17)
        Me.Controls.Add(Me.MetroButton1)
        Me.Controls.Add(Me.MetroLabel14)
        Me.Controls.Add(Me.MetroLabel13)
        Me.Controls.Add(Me.serverlist)
        Me.Controls.Add(Me.MetroLabel1)
        Me.Controls.Add(Me.ToolStripProgressBar1)
        Me.Controls.Add(Me.operators)
        Me.Controls.Add(Me.tills)
        Me.Controls.Add(Me.status)
        Me.Controls.Add(Me.GroupBox1)
        Me.DisplayHeader = False
        Me.DoubleBuffered = False
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Main"
        Me.Padding = New System.Windows.Forms.Padding(20, 30, 20, 20)
        Me.Resizable = False
        Me.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow
        Me.Style = MetroFramework.MetroColorStyle.Orange
        Me.Text = "MPOS Server"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.status.ResumeLayout(False)
        Me.status.PerformLayout()
        Me.ContextMenuStrip3.ResumeLayout(False)
        Me.tills.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.metro, System.ComponentModel.ISupportInitialize).EndInit()
        Me.operators.ResumeLayout(False)
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.ContextMenuStrip4.ResumeLayout(False)
        Me.MetroContextMenu1.ResumeLayout(False)
        CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip7.ResumeLayout(False)
        Me.ContextMenuStrip6.ResumeLayout(False)
        Me.folder.ResumeLayout(False)
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents status As System.Windows.Forms.GroupBox
    Friend WithEvents tills As System.Windows.Forms.GroupBox
    Friend WithEvents operators As System.Windows.Forms.GroupBox
    Friend WithEvents ToolStripProgressBar1 As MetroFramework.Controls.MetroProgressBar
    Friend WithEvents MetroLabel1 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroRadioButton4 As MetroFramework.Controls.MetroRadioButton
    Friend WithEvents MetroRadioButton3 As MetroFramework.Controls.MetroRadioButton
    Friend WithEvents MetroRadioButton2 As MetroFramework.Controls.MetroRadioButton
    Friend WithEvents MetroRadioButton1 As MetroFramework.Controls.MetroRadioButton
    Friend WithEvents servicelist As System.Windows.Forms.ListView
    Friend WithEvents MetroLabel11 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroLabel10 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroLabel9 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroLabel6 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroLabel5 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroLabel4 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroLabel3 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroLabel2 As MetroFramework.Controls.MetroLabel
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents tilllist As System.Windows.Forms.ListView
    Friend WithEvents nr As System.Windows.Forms.ColumnHeader
    Friend WithEvents Tillname As System.Windows.Forms.ColumnHeader
    Friend WithEvents tillno As System.Windows.Forms.ColumnHeader
    Friend WithEvents Tilltype As System.Windows.Forms.ColumnHeader
    Friend WithEvents Signon As System.Windows.Forms.ColumnHeader
    Friend WithEvents TillIP As System.Windows.Forms.ColumnHeader
    Friend WithEvents Tillstatus As System.Windows.Forms.ColumnHeader
    Friend WithEvents printertype As System.Windows.Forms.ColumnHeader
    Friend WithEvents operatorlist As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents serverlist As System.Windows.Forms.ListView
    Friend WithEvents country As System.Windows.Forms.ColumnHeader
    Friend WithEvents servername As System.Windows.Forms.ColumnHeader
    Friend WithEvents ip As System.Windows.Forms.ColumnHeader
    Friend WithEvents statuss As System.Windows.Forms.ColumnHeader
    Friend WithEvents vers As System.Windows.Forms.ColumnHeader
    Friend WithEvents MetroLabel13 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroLabel14 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroContextMenu1 As MetroFramework.Controls.MetroContextMenu
    Friend WithEvents DBQueriesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BarcodeGeneratorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DiscountExtractToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MetroButton1 As MetroFramework.Controls.MetroButton
    Friend WithEvents MetroStyleManager1 As MetroFramework.Components.MetroStyleManager
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents folderlist As System.Windows.Forms.ListView
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents ContextMenuStrip7 As MetroFramework.Controls.MetroContextMenu
    Friend WithEvents OpenMPOSToolAppToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitMPOSToolAppToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MetroLabel16 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroLabel15 As MetroFramework.Controls.MetroLabel
    Friend WithEvents ccd As MetroFramework.Controls.MetroProgressSpinner
    Friend WithEvents tpb As MetroFramework.Controls.MetroProgressSpinner
    Friend WithEvents MetroButton2 As MetroFramework.Controls.MetroButton
    Friend WithEvents metro As System.Windows.Forms.PictureBox
    Friend WithEvents ContextMenuStrip1 As MetroFramework.Controls.MetroContextMenu
    Friend WithEvents OpenInSCCMToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MstscToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RestartTillToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ForceSignOutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GetLogsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SetPrinterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NoneToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MatrixToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LaserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshDBToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip2 As MetroFramework.Controls.MetroContextMenu
    Friend WithEvents ResetOperatorPassword123ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip4 As MetroFramework.Controls.MetroContextMenu
    Friend WithEvents OpenInSCCMToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MstscToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GetServerLogsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshStatusToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MetroLabel17 As MetroFramework.Controls.MetroLabel
    Friend WithEvents ContextMenuStrip6 As MetroFramework.Controls.MetroContextMenu
    Friend WithEvents RouterxmlToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip3 As MetroFramework.Controls.MetroContextMenu
    Friend WithEvents RestartServiceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StartStopServiceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LinkLabel1 As MetroFramework.Controls.MetroLink
    Friend WithEvents MetroLabel8 As System.Windows.Forms.LinkLabel
    Friend WithEvents MetroLabel7 As System.Windows.Forms.LinkLabel
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox7 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox8 As System.Windows.Forms.PictureBox
    Friend WithEvents MetroLabel18 As MetroFramework.Controls.MetroLabel
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DiscountTableToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InputTableToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents folder As System.Windows.Forms.GroupBox
    Friend WithEvents HotfixesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FavLinksToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MetroLabel12 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroToolTip1 As MetroFramework.Components.MetroToolTip
End Class
