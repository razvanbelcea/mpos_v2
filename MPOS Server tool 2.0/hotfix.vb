Imports System.Xml

Public Class hotfix

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
    '------------

    Private Sub getHotfixlist()
        MetroGrid1.Rows.Clear()
        Dim row As String() = New String() {"-", "-", "-", "-", "-"}
        Dim hPath As String = ""
        Dim baseLocation As String = "\\buk11fsr001\GRP_MSYS_MPOS_DELIVERY\pos\MPOSx Deliveries\" + MetroComboBox1.SelectedValue + "\Hotfix"
        If My.Computer.FileSystem.DirectoryExists(baseLocation) Then
            For Each Dir As String In My.Computer.FileSystem.GetDirectories(baseLocation)
                Dim dirInfo As New System.IO.DirectoryInfo(Dir)
                hPath = dirInfo.Name
                row(0) = hPath

                Dim doc As New XmlDocument
                doc.Load(Dir + "\" + "hotfix.xml")
                Dim ReturnValue As New List(Of String)
                For Each node As XmlNode In doc.SelectNodes("//Hotfix")
                    row(1) = node.Attributes("countries").Value
                    row(3) = node.Attributes("releasedBy").Value
                Next

                For Each node As XmlNode In doc.SelectNodes("//Target")
                    row(2) = node.Attributes("system").Value
                Next

                For Each node As XmlNode In doc.SelectNodes("//Activity")
                    Dim source As String
                    source = node.Attributes("type").Value
                    If source = "msi" Then
                        row(4) = "WN"
                    Else
                        row(4) = "Msys"
                    End If
                Next
                MetroGrid1.Rows.Add(row)
            Next
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim releases As New ArrayList
        Try
            For Each directory As String In My.Computer.FileSystem.GetDirectories("\\buk11fsr001\GRP_MSYS_MPOS_DELIVERY\pos\MPOSx Deliveries")
                Dim dirInfo As New System.IO.DirectoryInfo(directory)
                If dirInfo.Name.Contains("Release") Then
                    If My.Computer.FileSystem.DirectoryExists("\\buk11fsr001\GRP_MSYS_MPOS_DELIVERY\pos\MPOSx Deliveries\" + dirInfo.Name + "\Hotfix") Then
                        If My.Computer.FileSystem.GetDirectories("\\buk11fsr001\GRP_MSYS_MPOS_DELIVERY\pos\MPOSx Deliveries\" + dirInfo.Name + "\Hotfix").Count > 0 Then
                            releases.Add(dirInfo.Name)
                        End If
                    End If
                End If
            Next
            releases.Sort()
            MetroComboBox1.DataSource = releases
        Catch el As Exception
        End Try
    End Sub

    Private Sub metrocombobox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MetroComboBox1.TextChanged
        getHotfixlist()
    End Sub
End Class