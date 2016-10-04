Imports System
Imports System.Collections.Generic
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text

Public Class frmSlashTellServer

#Region "Declaration"
    Private ci_PortNumber As Integer = 9077
    Private co_ServerSocket As TcpListener
    Private co_ClientSocket As TcpClient
#End Region

    Private Sub PostToServerConsole(PostMe As String)
        PostMe = txtServerConsole.Text
        PostMe &= DateTime.Now.ToString("hh:mm:ss") & " " & "[System] " & PostMe
        txtServerConsole.AppendText(PostMe & Environment.NewLine)
        'txtServerConsole.Text &= DateTime.Now.ToString("hh:mm:ss") & " " & "[System] " & PostMe & vbCrLf
    End Sub

    Private Sub btnLaunch_Click(sender As System.Object, e As System.EventArgs) Handles btnLaunch.Click
        StartServer()
    End Sub

    Private Function InitializeServerSocket() As TcpListener
        co_ServerSocket = New TcpListener(IPAddress.Any, ci_PortNumber)
        Return co_ServerSocket
    End Function

    Private Sub StartServer()
        Try

            'InitializeServerSocket()



            co_ServerSocket = New TcpListener(IPAddress.Any, ci_PortNumber)
            
            'Open the server socket
            co_ServerSocket.Start()

            PostToServerConsole("Server Started on port: " & ci_PortNumber)
            PostToServerConsole("Waiting for connection...")

            Dim mo_bytes(1024) As [Byte]
            Dim mo_data As [String] = Nothing

            While True


                'Open the client socket.
                co_ClientSocket = co_ServerSocket.AcceptTcpClient()
                PostToServerConsole("Connection established...")

                mo_data = Nothing
                'Open stream
                Dim mo_Stream As NetworkStream = co_ClientSocket.GetStream()
                Dim mo_i As Int32
                mo_i = mo_Stream.Read(mo_bytes, 0, mo_bytes.Length)

                While (mo_i <> 0) 'Infinite loop which listens for requests from client-side

                    'Translate data into ascii string.
                    mo_data = System.Text.Encoding.ASCII.GetString(mo_bytes, 0, mo_i)
                    PostToServerConsole("Client sent: " & [String].Format(mo_data))
                    Dim mo_msg As [Byte]() = System.Text.Encoding.ASCII.GetBytes(mo_data)

                    'Respond
                    mo_Stream.Write(mo_msg, 0, mo_msg.Length)
                    PostToServerConsole("Sent to client: " & [String].Format(mo_data))

                    mo_i = mo_Stream.Read(mo_bytes, 0, mo_bytes.Length)
                End While

            End While

        Catch exxx As SocketException
            PostToServerConsole("She's dead, Jim...")
        End Try
    End Sub
   
End Class
