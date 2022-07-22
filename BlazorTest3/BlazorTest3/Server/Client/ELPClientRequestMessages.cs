using System;
using System.Text;
using System.Text.Json;

namespace ELProxyClient
{
    public class ELPRequestMessage
    {
        public ELPRequestMessage(string clientId, string requestIndex, string apiKey, string messageType)
        {
            ClientId = clientId;
            RequestIndex = requestIndex;
            ApiKey = apiKey;
            MessageType = messageType;
        }

        public string ClientId { get; set; }
        public string RequestIndex { get; set; }
        public string ApiKey { get; set; }
        public string MessageType { get; set; }

        public string response { get; set; }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public class ApiKeyMessage
        {
            public string apiKey { get; set; }
        }

        public bool isSharedApiMessage()
        {
            return MessageType == ELPRequestMessageType.sharedApiMessage;
        }

        public bool isApiKeyMessage()
        {
            return MessageType == ELPRequestMessageType.apiMessage;
        }
    }

    public class ELPClientResponseMessage
    {
        public string RequestIndex { get; set; }
        public string ApiKey { get; set; }
        public string Response { get; set; }
        public string MessageType { get; set; }

        public ELPClientResponseMessage() { }

        public ELPClientResponseMessage(string requestIndex, string apiKey)
        {
            RequestIndex = requestIndex;
            ApiKey = apiKey;
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public string updateResponse(byte[] response)
        {
            this.Response = Convert.ToBase64String(response);
            return this.ToJson();
        }

        public string updateResponse(string response)
        {
            return updateResponse(Encoding.UTF8.GetBytes(response));
        }
    }

    public class ELPRequestInfo
    {
        public string RequestIndex { get; set; }
        public string Path { get; set; }
    }

    public class ELPClientRequestObject
    {
        public ELPRequestInfo requestInfo;
        public byte[] bodyBytes;

        public string requestBody()
        {
            return Encoding.UTF8.GetString(bodyBytes);
        }

        public ELPClientRequestObject(string message)
        {
            var arr = message.Split(ELProxyUtils.middleOfMessage);
            bodyBytes = Convert.FromBase64String(arr[1]);

            ELProxyUtils.messageToString(arr[1]);
            requestInfo = System.Text.Json.JsonSerializer.Deserialize<ELPRequestInfo>(ELProxyUtils.messageToString(arr[0]));
        }
    }
}

