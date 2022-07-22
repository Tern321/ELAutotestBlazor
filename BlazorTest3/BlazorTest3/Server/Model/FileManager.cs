using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using BlazorTest3.Server.Model;

namespace ELProxyClient
{
	public class FileManager
	{
        //static string ForumSourceFilesPath = "/Users/evgeniiloshchenko/Documents/testApi/ForumSourceFiles/";
        static string hostedFilesPath = "/Users/evgeniiloshchenko/Documents/hostedApiFiles/autotestBackend/";// /testName/

        public static void saveStandardFile(string testName, string testCase, string deviceType, string lang, byte[] image)
        {

            testName = Util.cleanFileName(testName);
            testCase = Util.cleanFileName(testCase);
            deviceType = Util.cleanFileName(deviceType);
            lang = Util.cleanFileName(lang);

            var path = hostedFilesPath + "Tests/" + testName + "/" + testCase + "/" + deviceType + "/" + lang + ".jpg";

            System.IO.Directory.CreateDirectory(hostedFilesPath + "Tests/" + testName + "/" + testCase + "/" + deviceType + "/");
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.BaseStream.Write(image, 0, image.Length);
            }
        }
        public FileManager()
		{
			// example files for tests
			// recived files from tests
		}

        public static byte[] readFile(string filePath)
        {
            return System.IO.File.ReadAllBytes(filePath);
        }

        public static byte[] readUserFile(string fileName) { 
            return readFile(hostedFilesPath + Regex.Replace(fileName, "[^A-Za-z0-9_\\.]", ""));
        }

        public static string saveUserFile(byte[] bytes, string fileName)
        {
            var filePath = System.Guid.NewGuid() + "_" + fileName;
            var unicFileName = Regex.Replace(filePath, "[^A-Za-z0-9_\\.]", ""); // not safe
            Console.WriteLine("writeFile " + hostedFilesPath + unicFileName);
            Console.WriteLine(unicFileName);

            using (StreamWriter sw = new StreamWriter(hostedFilesPath + unicFileName))
            {
                sw.BaseStream.Write(bytes, 0, bytes.Length);
            }
            return unicFileName;
        }
    }
}

