using System;
using ELProxyClient;

namespace BlazorTest3.Server.Controllers
{
    public class Model1
    {
        public static ELAutotestMessageManager messageManager = new ELAutotestMessageManager();
        public Model1()
        {
        }
        public static void init()
        {
            messageManager.start(true, true);
        }

    }
}

