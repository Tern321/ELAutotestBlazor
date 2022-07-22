using System;
using System.Text.Json;

namespace ELProxyClient
{
	public class ELPRequestClientMessageManager
	{
		ELPRequestClient client = new ELPRequestClient();

		public string IP = "46.49.118.172";
		public string clientId = "Example_c#_client_name";
		public string HttpApiKey = "ExampleHttpApi";
		public string SharedApiKey = "ExampleSharedApi";
		public int port = 5201;

		virtual public string respondToMessage(ELPClientRequestObject requestObject) {
			ELPClientResponseMessage responseMessage = new ELPClientResponseMessage(requestObject.requestInfo.RequestIndex, HttpApiKey);

			return responseMessage.updateResponse("ELPRequestClientMessageManager not implemented");
		}

		public void start(bool http, bool shared)
		{
			client.messageManager = this;
			client.StartClient(http,shared);
		}

		virtual public void gotSharedMessage(ELPClientResponseMessage message)
		{
			Console.WriteLine("gotSharedMessage");
			Console.WriteLine(message.Response);

			ELPClientResponseMessage responseMessage = new ELPClientResponseMessage("", HttpApiKey);
			responseMessage.MessageType = ELPRequestMessageType.sharedApiData;
			responseMessage.Response = "shared message from c#";
			responseMessage.ApiKey = SharedApiKey;
			client.sendDataAsMessage(responseMessage.ToJson());
			//sendSharedMessage(message: "shared message from c#")
		}


	}

}