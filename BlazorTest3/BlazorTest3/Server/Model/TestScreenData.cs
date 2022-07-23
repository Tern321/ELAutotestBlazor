using System;
using System.Text.Json;

namespace BlazorTest3.Server.Model
{
    public class TestScreenData
    {
        public string testName { get; set; }
        public string testCaseId { get; set; }
        public string testRunId { get; set; }

        public string viewControllerName { get; set; }
        public string rotation { get; set; }
        public string screenBase64 { get; set; }
        public string viewControllerModelJson { get; set; }
        public string deviceId { get; set; }
        public string deviceModel { get; set; }
        public string lang { get; set; }

        public TestScreenData()
        {
        }
        public byte[] getScreenshot() {
            return Convert.FromBase64String(screenBase64);
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}

