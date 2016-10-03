Imports System.Threading
Imports System.Net.Sockets
Imports System.Text

Public Class frmSlashTellClient

    Private co_Client As TcpClient
    Private ci_PortNumber As Integer = 9077
    Private cs_IPAddress As String = "127.0.0.1"
    Private cs_User As String = "[Bart Simpson]"
    Private cs_Message As String


    Private ReturningMessage As String
    ' Private InfCounter As Integer
    Private c_bKeepListening As Boolean
    Private ServerStream As NetworkStream

    Private ClientThread As Threading.Thread

    Private Sub PostToClientConsole(PostMe As String)

        Dim mo_s As String = PostMe
        'PostMe &= DateTime.Now.ToString("hh:mm:ss") & " " & cs_User & " " & PostMe
        txtClientConsole.AppendText(mo_s & vbCrLf)
        'txtClientConsole.Text &= vbCrLf
        'txtClientConsole.Text &= DateTime.Now.ToString("hh:mm:ss") & " " & cs_User & " " & PostMe
    End Sub

    Private Sub btnConnect_Click(sender As System.Object, e As System.EventArgs) Handles btnConnect.Click
        Connect()
    End Sub

    Private Function SetPortNumber() As Integer
        ci_PortNumber = nudPortNumber.Value
        Return ci_PortNumber
    End Function

    Private Function SetUserName() As String

        If txtUserName.Text <> "" Then
            cs_User = txtUserName.Text
        Else
            cs_User = "Lemming"
            txtUserName.Text = cs_User
        End If

        Return cs_User
    End Function

    Private Function InitTcpClient() As TcpClient
        co_Client = New TcpClient()
        'Later on pull ip and user name from the form.
        Return co_Client
    End Function

    Private Sub Connect()

        If c_bKeepListening Then
            PostToClientConsole("Disconnecting: " & ci_PortNumber)
            c_bKeepListening = False
            Me.btnConnect.Text = "Connect"
        Else
            InitTcpClient()
            SetUserName()
            SetPortNumber()

            c_bKeepListening = True
            ClientThread = New Threading.Thread(AddressOf ClientListeningLoop)
            ClientThread.Start()

            co_Client.Connect(cs_IPAddress, ci_PortNumber)
            Transmit(cs_User)
            PostToClientConsole("Using IP: " & cs_IPAddress)
            PostToClientConsole("Connection Established on port: " & ci_PortNumber)
            Me.btnConnect.Text = "Disconnect"
        End If

    End Sub

    Private Function GetMessage() As String
        cs_Message = txtMessage.Text
        txtMessage.Text = ""
        Return cs_Message
    End Function

    Private Sub Transmit(Username As String)
        'Pull directly from form and stuff into string until I find a more elegant way to handle this.
        'Dim mo_StringToSend As String = DateTime.Now.ToString("hh:mm:ss") & " " & cs_User & ": "
        'mo_StringToSend = mo_StringToSend & GetMessage()

        'If mo_StringToSend = "" Then
        '    mo_StringToSend &= " "
        'End If

        Dim mo_Stream As NetworkStream = co_Client.GetStream()

        If mo_Stream.CanRead And mo_Stream.CanWrite Then
            'Send a string to the server.
            Dim mo_OutgoingBytes As [Byte]() = Encoding.ASCII.GetBytes(Username)
            mo_Stream.Write(mo_OutgoingBytes, 0, mo_OutgoingBytes.Length)
            mo_Stream.Flush()

            'Get the echo from server
            'Dim mo_IncomingBytes(co_Client.ReceiveBufferSize) As Byte
            'mo_Stream.Read(mo_IncomingBytes, 0, co_Client.ReceiveBufferSize)
            'Dim mo_IncomingString As String = Encoding.ASCII.GetString(mo_IncomingBytes)
            'PostToClientConsole(mo_IncomingString)

        End If

    End Sub

    Private Sub Transmit()
        'Pull directly from form and stuff into string until I find a more elegant way to handle this.
        Dim mo_StringToSend As String = DateTime.Now.ToString("hh:mm:ss") & " " & cs_User & ": "
        mo_StringToSend = mo_StringToSend & GetMessage()

        If mo_StringToSend = "" Then
            mo_StringToSend &= " "
        End If

        Dim mo_Stream As NetworkStream = co_Client.GetStream()

        If mo_Stream.CanRead And mo_Stream.CanWrite Then
            'Send a string to the server.
            Dim mo_OutgoingBytes As [Byte]() = Encoding.ASCII.GetBytes(mo_StringToSend)
            mo_Stream.Write(mo_OutgoingBytes, 0, mo_OutgoingBytes.Length)
            mo_Stream.Flush()

            'Get the echo from server
            'Dim mo_IncomingBytes(co_Client.ReceiveBufferSize) As Byte
            'mo_Stream.Read(mo_IncomingBytes, 0, co_Client.ReceiveBufferSize)
            'Dim mo_IncomingString As String = Encoding.ASCII.GetString(mo_IncomingBytes)
            'PostToClientConsole(mo_IncomingString)

        End If

    End Sub

    Private Sub ClientListeningLoop()
        Dim mo_bytes(1024) As [Byte]
        Dim mo_data As [String] = Nothing
        Dim mo_Stream As NetworkStream
        Dim mo_i As Int32

        Do While c_bKeepListening
            mo_Stream = co_Client.GetStream()
            mo_i = mo_Stream.Read(mo_bytes, 0, mo_bytes.Length)
            mo_data = System.Text.Encoding.ASCII.GetString(mo_bytes, 0, mo_i)

            ReturningMessage = mo_data
            msg()
            'Dim buffsize As Integer
            'Dim inStream(1024) As Byte
            'buffsize = co_Client.ReceiveBufferSize()
            'ServerStream.Read(inStream, 0, buffsize)
            'Dim IncomingData As String = Encoding.ASCII.GetString(inStream)
            'PostToClientConsole(IncomingData)
        Loop

        MsgBox("Exiting ReadLoop")

    End Sub

    Private Sub msg()
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf msg))
        Else
            txtClientConsole.AppendText(ReturningMessage)
            txtClientConsole.Text &= vbCrLf
            'PostToClientConsole(ReturningMessage)
        End If
    End Sub

    Private Sub btnSend_Click(sender As System.Object, e As System.EventArgs) Handles btnSend.Click
        Transmit()
    End Sub

    Private Sub txtMessage_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtMessage.KeyPress
        If e.KeyChar = Chr(13) Then
            Transmit()
        End If
    End Sub

End Class
