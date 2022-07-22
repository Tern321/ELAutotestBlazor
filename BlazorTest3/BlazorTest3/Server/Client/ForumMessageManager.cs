using System;
using System.Text.Json;
using BlazorTest3.Server.Model;
using ELProxyClient;

namespace ELProxyClient
{

	public class ELAutotestMessageManager: ELPRequestClientMessageManager
	{
		static public long lastChangeTimeValue = 0;

		public ELAutotestMessageManager()
		{
			bool openPorts = true;
			if (openPorts)
			{
				this.IP = "192.168.1.10";
				this.port = 5201;
			}
			else
			{
				this.IP = "127.0.0.1";
				this.port = 5301;
			}

			this.SharedApiKey = "iosAutotestSharedApi";
			this.HttpApiKey = "iosAutotestApi";
			this.clientId = "c#_AutotestBackend_client1";
		}

		//http://localhost:5204/forumApi/FAHome?foo=bar
		public static string page(ELPClientRequestObject requestObject)
		{
			return ELProxyUtils.pathElement(requestObject, 2);
		}

		override public string respondToMessage(ELPClientRequestObject requestObject)
		{
			ELPClientResponseMessage responseMessage = new ELPClientResponseMessage(requestObject.requestInfo.RequestIndex, HttpApiKey);
			var page = ELAutotestMessageManager.page(requestObject);

			return responseMessage.updateResponse("not implemented");
		}

        public override void gotSharedMessage(ELPClientResponseMessage message)
        {
			Console.WriteLine("gotSharedMessage");
			//base.gotSharedMessage(message);

			//ELPClientResponseMessage message = System.Text.Json.JsonSerializer.Deserialize<ELPClientResponseMessage>(json);
			TestScreenData testData = System.Text.Json.JsonSerializer.Deserialize<TestScreenData>(message.Response);
			//Console.WriteLine(testData.deviceModel);
			FileManager.saveStandardFile(testData.testName, testData.testCaseId, testData.deviceModel, testData.lang, testData.getScreenshot());

		}
    }
}

