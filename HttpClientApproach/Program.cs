using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HttpClientApproach
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
            List<Contributor> contributors;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.github.com");
                client.DefaultRequestHeaders.Add("User-Agent", "Anything");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("repos/twilio/twilio-csharp/contributors").Result;
                response.EnsureSuccessStatusCode();
                contributors = response.Content.ReadAsAsync<List<Contributor>>().Result;
            }

            contributors.ForEach(Console.WriteLine);
            Console.ReadLine();
        }
    }
}
