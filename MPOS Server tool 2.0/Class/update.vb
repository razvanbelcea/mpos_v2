Imports MetroFramework
Imports System.IO


Public Class update
    Shared Sub app()
        Dim WbReq As New Net.WebClient
        WbReq.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials
        WbReq.Dispose()

        Dim result As Integer = MetroMessageBox.Show(MPOS.Main, "A new update is available. Please update in order to continue.", "Update Available", MessageBoxButtons.OK, MessageBoxIcon.Information)
        If result = DialogResult.No Then
        ElseIf result = DialogResult.OK Then
            Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("http://my-collaboration.metrogroup-networking.com/personal/r4_razvan_belcea/Shared%20Documents/Update.txt")
            request.Credentials = System.Net.CredentialCache.DefaultCredentials
            Dim response As System.Net.HttpWebResponse = request.GetResponse()
            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
            Dim newestversion As String = sr.ReadToEnd()
            Dim currentversion As String = Application.ProductVersion
            Try
                Dim source As New Uri("http://my-collaboration.metrogroup-networking.com/personal/r4_razvan_belcea/Shared%20Documents/MPOS%20Server%20Tool%20v1")
                Dim credentials As System.Net.NetworkCredential = System.Net.CredentialCache.DefaultNetworkCredentials
                My.Computer.Network.DownloadFile(source, Application.StartupPath + "/MPOS Server Tool V" & newestversion + ".exe", credentials, True, 60000I, False)
                If My.Computer.FileSystem.FileExists(Application.StartupPath + "/MPOS Server Tool V" & newestversion + ".exe") Then
                    Dim path As String = "oldversion.txt"

                    If Not File.Exists(path) Then
                        Using sw As StreamWriter = File.CreateText(path)
                            sw.WriteLine(currentversion)
                        End Using
                    End If

                    Main.x = True
                    MPOS.Main.Close()
                    System.Threading.Thread.Sleep(3000)
                    Process.Start(Application.StartupPath + "/MPOS Server Tool V" & newestversion + ".exe")
                End If
            Catch ex As Exception
                miniTool.balon(ex.Message + " Error Downloading update.")
                ' Logger.WriteToErrorLog(ex.Message, ex.StackTrace, "error")
            End Try
            sr.Close()
        End If
    End Sub
End Class
