using System;
using System.Collections.Generic;
using RestSharp;

namespace RestSharpApproach
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
            var client = new RestClient("https://api.github.com");

            var request = new RestRequest("repos/twilio/twilio-csharp/contributors", Method.GET);
            // Add HTTP headers
            request.AddHeader("User-Agent", "Nothing");

            // Execute the request and automatically deserialize the result.
            var contributors = client.Execute<List<Contributor>>(request);
            contributors.Data.ForEach(Console.WriteLine);

            Console.ReadLine();
        }
    }
}
