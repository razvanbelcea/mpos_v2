Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Management.Automation
Imports System.Management.Automation.Runspaces
Imports System.Threading
Imports Microsoft.Office.Interop.Outlook

Public Class VMSnapshot

    'DESIGN AREA
    Private Sub PictureBox6_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox6.MouseMove
        PictureBox6.Image = My.Resources.back_b
        MetroLabel2.Visible = True
    End Sub
    Private Sub PictureBox6_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox6.MouseLeave
        PictureBox6.Image = My.Resources.back
        MetroLabel2.Visible = False
    End Sub
    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        Me.Close()
        Me.Dispose()
    End Sub
    'DESIGN AREA END

    Private Sub VMSnapshot_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fillcombobox()
        MetroGrid2.Columns.Clear()
    End Sub
    Private Sub populatenewgrid()

        Try
            Dim xdoc As XDocument
            xdoc = XDocument.Load(System.Windows.Forms.Application.StartupPath + "\Resources\vmlist.xml")

            If File.Exists(System.Windows.Forms.Application.StartupPath + "\Resources\vmlist.xml") Then
                Dim list = xdoc.Descendants("Property").ToList()
                Dim tuple As New List(Of Tuple(Of String, String, String))

                Dim x As Integer = 0
                While x < list.Count - 1
                    tuple.Add(New Tuple(Of String, String, String)(list(x).Value, list(x + 1).Value, list(x + 2).Value))
                    x = x + 3
                End While

                Dim result = From item In tuple
                             Where item.Item1 = ComboBox1.SelectedValue
                             Select item

                MetroGrid2.DataSource = result.ToList()
                MetroGrid2.Columns(0).HeaderText = "Server name"
                MetroGrid2.Columns(1).HeaderText = "Snapshot name"
                MetroGrid2.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                MetroGrid2.Columns(2).HeaderText = "Created at"

                Dim btn As New DataGridViewButtonColumn()
                MetroGrid2.Columns.Add(btn)
                btn.HeaderText = "Action"
                btn.Text = "Delete"
                btn.Name = "btn"
                btn.FlatStyle = FlatStyle.Flat
                btn.UseColumnTextForButtonValue = True

                If Environment.UserName = "razvan.belcea" Or Environment.UserName = "dragos.rosca" Then
                    Dim btn_mount As New DataGridViewButtonColumn()
                    MetroGrid2.Columns.Add(btn_mount)
                    btn_mount.HeaderText = "Action"
                    btn_mount.Text = "Mount"
                    btn_mount.Name = "btn_mount"
                    btn_mount.FlatStyle = FlatStyle.Flat
                    btn_mount.UseColumnTextForButtonValue = True
                End If

            End If
        Catch ex As System.Exception
            miniTool.balon(ex.Message)
        End Try

    End Sub

    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        Dim scriptpath As String = System.Windows.Forms.Application.StartupPath + "\Resources\hey.ps1"
        Dim taskOne = Task.Factory.StartNew(Sub() RunScript("", scriptpath))
        taskOne.Wait() ' Blocks until task is complete        
    End Sub

    Private Sub RunScript(argument As String, type As String)
        Process.Start("powershell", "-noexit -ExecutionPolicy ByPass -file """ + type + """ -user r4\razvan.belcea -password Thinkpad11 ")
    End Sub
    Private Sub createsnap(argument As String, type As String)
        Dim myValue As String = InputBox("Hello, gorgeous", "Had a good day ?", "Please enter name for the snapshot")
        If myValue <> "" Then
            Dim filename As String = """" + argument + "_" + myValue + """"
            Process.Start("powershell", "-noexit -ExecutionPolicy ByPass -file """ + type + """ -user r4\razvan.belcea -password Thinkpad11 -server " + argument + " -name " + filename)
        End If
    End Sub
    Private Sub deletevm(type As String, snapname As String, vmname As String)
        Process.Start("powershell", "-noexit -ExecutionPolicy ByPass -file """ + type + """ -user r4\razvan.belcea -password Thinkpad11 -snapname " + snapname + " -vmname " + vmname)
    End Sub

    Private Sub fillcombobox()
        Dim xdoc As XDocument
        xdoc = XDocument.Load("serverlist.xml")
        If File.Exists("serverlist.xml") Then
            Dim studentQuery = From student In xdoc.Descendants("Server")
                               Where student.Element("Location") = "QA"
                               Select student.Element("Name").Value

            Dim serverlist As New List(Of String)
            Dim x As Integer = 0
            While x < studentQuery.Count - 1
                serverlist.Add(studentQuery(x).ToString())
                x = x + 1
            End While

            ComboBox1.DataSource = studentQuery.ToList()
        Else
            miniTool.balon("Snapshot file not generated. Please generate the file.")
        End If

        Dim filename As String = System.Windows.Forms.Application.StartupPath + "\Resources\vmlist.xml"
        If File.Exists(filename) Then
            Dim creation As String = File.GetLastWriteTime(filename)
            MetroLabel1.Text = "Last update of snapshot file: " + creation
        Else
            miniTool.balon("Snapshot file not generated. Please generate the file.")
            MetroLabel1.Text = "Snapshot file not generated. Please generate the file."
        End If
    End Sub

    Private Sub MetroButton3_Click(sender As Object, e As EventArgs) Handles MetroButton3.Click
        Dim scriptpath As String = System.Windows.Forms.Application.StartupPath + "\Resources\createS.ps1"
        Dim taskOne = Task.Factory.StartNew(Sub() createsnap(ComboBox1.SelectedValue, scriptpath))
        taskOne.Wait() ' Blocks until task is complete     
    End Sub

    Private Sub MetroComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        MetroGrid2.Columns.Clear()
        populatenewgrid()
    End Sub
    Private Sub MetroGrid2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles MetroGrid2.CellClick
        If e.ColumnIndex = 3 Then
            Dim vmname As String = """" + MetroGrid2.Rows(e.RowIndex.ToString()).Cells(0).Value + """"
            Dim snamshotname As String = """" + MetroGrid2.Rows(e.RowIndex.ToString()).Cells(1).Value + """"
            Dim scriptpath As String = System.Windows.Forms.Application.StartupPath + "\Resources\deletevm.ps1"
            Dim taskOne = Task.Factory.StartNew(Sub() deletevm(scriptpath, snamshotname, vmname))
            taskOne.Wait() ' Blocks until task is complete
        End If
    End Sub
End Class