using System;
using System.Text.RegularExpressions;

namespace BlazorTest3.Server.Model
{
    public class Util
    {
        public static string cleanFileName(string name)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9_ ]");
            return rgx.Replace(name, "").Replace(" ", "_");
        }

        public static string unicFileName()
        {
            Guid myuuid = Guid.NewGuid();
            return cleanFileName(myuuid.ToString());
        }
        public Util()
        {
        }
    }
}

