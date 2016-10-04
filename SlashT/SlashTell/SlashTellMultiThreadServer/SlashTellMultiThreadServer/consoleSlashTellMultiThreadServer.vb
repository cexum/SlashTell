Imports System.Net.Sockets
Imports System.Net
Imports System.Text

Module consoleSlashTellMultiThreadServer

    Dim ci_PortNumber As Integer = 9077
    Dim co_ServerSocket As TcpListener
    Dim co_ClientSocket As TcpClient
    Dim testClientsList As New Hashtable
    Dim co_ClientsList As New Hashtable
    Dim co_Counter As Integer = 0

    Sub Main()
        InitServer()
    End Sub

    Private Sub InitServer()

        'InitializeServerSocket()
        Console.WriteLine("Init...")
        co_ServerSocket = New TcpListener(IPAddress.Any, ci_PortNumber)

        'Open the server socket
        co_ServerSocket.Start()
        Console.WriteLine("Server started on: " & ci_PortNumber)
        Console.WriteLine("Waiting for connection")


        Dim mo_bytes(1024) As [Byte]
        Dim mo_data As [String] = Nothing
        Dim mo_Stream As NetworkStream
        Dim mo_i As Int32

        While True
            co_Counter += 1
            'Open the client socket.
            co_ClientSocket = co_ServerSocket.AcceptTcpClient()
            Console.WriteLine("Incomming connection number: " & co_Counter)

            mo_Stream = co_ClientSocket.GetStream()
            mo_i = mo_Stream.Read(mo_bytes, 0, mo_bytes.Length)
            mo_data = System.Text.Encoding.ASCII.GetString(mo_bytes, 0, mo_i)
            mo_data = "@@@@@" & mo_data & " has connected@@@@@"
            BroadCastToClients(mo_data, co_Counter)
            'co_ClientsList(co_Counter) = co_ClientSocket

            'Thread
            Dim NewClient As New HandleClient
            NewClient.StartClientThread(co_ClientSocket, co_Counter)

        End While

    End Sub

    Private Sub BroadCastToClients(ByVal Message As String)

        Dim Item As DictionaryEntry
        Dim BroadcastSocket As TcpClient
        Dim BroadcastStream As NetworkStream
        Dim BroadcastBytes As [Byte]()

        For Each Item In testClientsList
            BroadcastSocket = CType(Item.Value, TcpClient)
            BroadcastStream = BroadcastSocket.GetStream()
            BroadcastBytes = Encoding.ASCII.GetBytes(Message)
            BroadcastStream.Write(BroadcastBytes, 0, BroadcastBytes.Length)
            BroadcastStream.Flush()
        Next

    End Sub

    Private Sub BroadCastToClients(UserName As String, count As Integer)

        Dim Item As DictionaryEntry
        Dim BroadcastSocket As TcpClient
        Dim BroadcastStream As NetworkStream
        Dim BroadcastBytes As [Byte]()

        For Each Item In testClientsList
            BroadcastSocket = CType(Item.Value, TcpClient)
            BroadcastStream = BroadcastSocket.GetStream()
            BroadcastBytes = Encoding.ASCII.GetBytes(UserName)
            BroadcastStream.Write(BroadcastBytes, 0, BroadcastBytes.Length)
            BroadcastStream.Flush()
        Next

    End Sub

    Public Class HandleClient

        Dim co_ClientSocket As TcpClient
        Dim ClientNumber As String
        'Dim co_ClientsList As Hashtable

        Public Sub StartClientThread(ByRef inClientSocket As TcpClient, inClientNumber As String)

            Me.co_ClientSocket = inClientSocket
            'Me.co_ClientsList = inClientList
            Me.ClientNumber = inClientNumber

            testClientsList.Add(ClientNumber, co_ClientSocket)

            Dim Thread As Threading.Thread = New Threading.Thread(AddressOf Chat)
            Thread.Start()

        End Sub

        Private Sub Chat()

            Dim RequestCount As Integer

            'Declared outside the while loop to reduce overhead
            Dim mo_bytes(1024) As [Byte]
            Dim mo_data As [String] = Nothing
            Dim mo_Stream As NetworkStream
            Dim mo_i As Int32

            '2016.09.29 ge -- Moved these to inside the loop
            'Open stream
            'mo_Stream = co_ClientSocket.GetStream()
            'mo_i = mo_Stream.Read(mo_bytes, 0, mo_bytes.Length)

            While (True) 'Infinite loop which listens for requests from client-side

                Try
                    mo_Stream = co_ClientSocket.GetStream()
                    mo_i = mo_Stream.Read(mo_bytes, 0, mo_bytes.Length)

                    'Translate data into ascii string.
                    mo_data = System.Text.Encoding.ASCII.GetString(mo_bytes, 0, mo_i)
                    Console.WriteLine([String].Format(mo_data))

                    BroadCastToClients(mo_data)

                    'Old mechanism which only echoes to discrete client threads, not to all client threads.

                    'Dim mo_msg As [Byte]() = System.Text.Encoding.ASCII.GetBytes(mo_data)
                    ''Respond
                    'mo_Stream.Write(mo_msg, 0, mo_msg.Length)
                    'Console.WriteLine("Sent to client: " & [String].Format(mo_data))
                    '' mo_i = mo_Stream.Read(mo_bytes, 0, mo_bytes.Length)
                    'mo_Stream.Flush()

                Catch exx As IO.IOException

                Catch exxx As SocketException
                    Console.WriteLine("She's dead, Jim...")
                End Try

            End While

        End Sub

    End Class

End Module
