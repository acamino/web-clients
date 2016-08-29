using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace HttpWebClientApproach
{
    internal class Contributor
    {
        public string Login { get; set; }
        public short Contributions { get; set; }

        public override string ToString()
        {
            return $"{Login, 20}: {Contributions} contributions";
        }
    }

    internal class Program
    {
        private static void Main()
        {
            var webRequest = WebRequest.Create("https://api.github.com/repos/twilio/twilio-csharp/contributors") as HttpWebRequest;
            if (webRequest == null)
            {
                return;
            }

            webRequest.ContentType = "application/json";
            webRequest.UserAgent = "Nothing";

            using (var s = webRequest.GetResponse().GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    var contributorsAsJson = sr.ReadToEnd();
                    var contributors = JsonConvert.DeserializeObject<List<Contributor>>(contributorsAsJson);
                    contributors.ForEach(Console.WriteLine);
                }
            }

            Console.ReadLine();
        }
    }
}
