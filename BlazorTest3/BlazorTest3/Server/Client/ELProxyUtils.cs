using System;
using System.Text;
using System.Net.Sockets;

namespace ELProxyClient
{
	public class ELProxyUtils
	{
        public const string middleOfMessage = "!";
        public const string endOfMessage = "\n";

        public static string stringToMessage(string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str)) + ELProxyUtils.endOfMessage;
        }

        public static string messageToString(string base64str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64str));
        }

        public static string pathElement(ELPClientRequestObject requestObject, int index)
        {
            // http://localhost:5204/forumApi/?page=home&val2=qwer
            var elements = requestObject.requestInfo.Path.Split("/");
            if (elements.Length > index)
            {
                return elements[index].Split(new char[] { '?', '&' })[0];
            }
            return "";
        }

        public static string typeNameString(string page)
        {
            return "ELProxyClient." + page + ", " + System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
        }
    }

    public class ELPStateObject
    {
        public static int BufferSize = 1024 * 1024 * 20;
        public byte[] buffer = new byte[BufferSize];

        public StringBuilder sb = new StringBuilder();
        public Socket workSocket = null;
        public int bytesRead = 0;
    }

}