using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace ELProxyClient
{
    public class ELPRequestMessageType
    {
        public const string apiMessage = "apiMessage";
        public const string sharedApiMessage = "sharedApiMessage";

        public const string requestMessage = "requestMessage";
        public const string sharedApiData = "sharedApiData";
    }

    public class ELPRequestClient
    {
        private ManualResetEvent connectDone = new ManualResetEvent(false);

        public ELPRequestClientMessageManager messageManager;
        Socket client;

        public void StartClient(bool http, bool shared)
        {
            Console.WriteLine("ELPRequestClient start");
            try
            {
                IPAddress ipAddr = IPAddress.Parse(messageManager.IP);
                IPEndPoint remoteEP = new IPEndPoint(ipAddr, messageManager.port);

                client = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();

                if (http)
                {
                    Console.WriteLine("Connected http client " + messageManager.HttpApiKey + " " + messageManager.clientId + " " + messageManager.IP + ":" + messageManager.port);
                    sendDataAsMessage(apiMessage(messageManager.clientId));
                }
                if (shared)
                {
                    Console.WriteLine("Connected shared tcp  " + messageManager.SharedApiKey + " " + messageManager.clientId + " " + messageManager.IP + ":" + messageManager.port);
                    sendDataAsMessage(sharedApiMessage(messageManager.clientId));
                }


                Receive(client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public string apiMessage(string clientId)
        {
            var message = new ELPRequestMessage(clientId, "", messageManager.HttpApiKey, ELPRequestMessageType.apiMessage);
            return message.ToJson();
        }

        public string sharedApiMessage(string clientId)
        {
            var message = new ELPRequestMessage(clientId, "", messageManager.SharedApiKey, ELPRequestMessageType.sharedApiMessage);
            return message.ToJson();
        }

        public void sendDataAsMessage(string data)
        {
            Send(client, ELProxyUtils.stringToMessage(data));
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void Receive(Socket client)
        {
            try
            {
                ELPStateObject state = new ELPStateObject();
                state.workSocket = client;

                client.BeginReceive(state.buffer, 0, ELPStateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void ReadCallback(IAsyncResult ar) // this can be done better
        {
            ELPStateObject state = (ELPStateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            state.bytesRead = handler.EndReceive(ar);
            if (state.bytesRead > 0)
            {
                state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, state.bytesRead));
                string data = state.sb.ToString();
                state.sb.Clear();

                while (data.IndexOf(ELProxyUtils.endOfMessage) > -1)
                {
                    var index = data.IndexOf(ELProxyUtils.endOfMessage);
                    string content = data.Substring(0, index);
                    data = data.Substring(index + 1);

                    if (content.Length == 0) { continue; }

                    if (content.Contains(ELProxyUtils.middleOfMessage))
                    {
                        var requestContent = new ELPClientRequestObject(content);
                        var response = messageManager.respondToMessage(requestContent);
                        Send(state.workSocket, ELProxyUtils.stringToMessage(response));
                    }
                    else
                    {
                        var json = ELProxyUtils.messageToString(content);
                        ELPClientResponseMessage message = System.Text.Json.JsonSerializer.Deserialize<ELPClientResponseMessage>(json);
                        messageManager.gotSharedMessage(message);
                    }
                }
                state.sb.Append(data);
            }
            handler.BeginReceive(state.buffer, 0, ELPStateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
        }

        private void Send(Socket client, String data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}