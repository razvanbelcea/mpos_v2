Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Management.Automation
Imports System.Management.Automation.Runspaces
Imports System.Threading

Public Class VMSnapshot
    Private Sub VMSnapshot_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim xdoc As XDocument
        xdoc = XDocument.Load("serverlist.xml")
        If File.exists("serverlist.xml") Then
        Dim studentQuery = From student In xdoc.Descendants("Server") _
                           Where student.Element("Location")="QA"
                           Select New With _
                           { _
                             .Name = student.Element("Name").Value, _
                             .Country = student.Element("Country").Value _
                           }
        MetroGrid1.AutoGenerateColumns = True
        MetroGrid1.DataSource = studentQuery.ToList()

                    Dim btn As New DataGridViewButtonColumn()
        MetroGrid1.Columns.Add(btn)
        btn.HeaderText = "Create Snapshot"
        btn.Text = "Create"
        btn.Name = "btn"
            btn.FlatStyle = FlatStyle.Flat
        btn.UseColumnTextForButtonValue = True

                        Else 
            miniTool.balon("Snapshot file not generated. Please generate the file.")
            End if

        Dim filename As String = Application.StartupPath + "\Resources\test.xml"
        If File.Exists(filename) Then
        Dim creation As String = File.GetCreationTime(filename)
        MetroLabel1.Text = "Last update of snapshot file: " + creation
            Else 
            miniTool.balon("Snapshot file not generated. Please generate the file.")
            MetroLabel1.Text = "Snapshot file not generated. Please generate the file."
            End if

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub compare()
        for each row as DataGridViewRow in MetroGrid1.Rows
                    Dim xdoc As XDocument
        xdoc = XDocument.Load("\Resources\test.xml")
        If File.exists("\Resources\test.xml") Then
        Dim snapshots = From student In xdoc.Descendants("Object") _
                           Select New With _
                           { _
                             .Name = student.Attribute("Name").Value _
                           }
                           MsgBox(snapshots.GetEnumerator())
                           end if
        Next
    End Sub

    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        dim scriptpath as String = Application.StartupPath + "\Resources\hey.ps1"
        Dim taskOne = Task.Factory.StartNew(Sub() RunScript("",scriptpath))
        ' taskOne.Wait() ' Blocks until task is complete
    End Sub

    Private sub RunScript(argument As String, type As String)
        Process.Start("powershell", "-noexit -ExecutionPolicy ByPass -file """+type+""" ")
    End sub
    Private sub createsnap(argument As String, type As string)
         Process.Start("powershell", "-noexit -ExecutionPolicy ByPass -file """+type+""" -server " + argument + " -name " + argument)
    End sub

    Private Sub MetroButton2_Click(sender As Object, e As EventArgs) Handles MetroButton2.Click
        compare()
    End Sub
        Private Sub MetroGrid1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MetroGrid1.CellClick
        dim row as DataGridViewRow
        row = MetroGrid1.Rows(e.RowIndex)
        dim scriptpath as String = Application.StartupPath + "\Resources\createS.ps1"
        If e.ColumnIndex = 4 Then
            createsnap(row.Cells(2).Value,scriptpath)
        End If
    End Sub
End Class