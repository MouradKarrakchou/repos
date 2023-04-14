using System.Collections.Generic;
using System.Web.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;

namespace ServerForTuto2
{
    public class AgeController : ApiController
    {

        // GET api/age
        public string Get()
        {
            string json = JsonSerializer.Serialize(findAgeInfo(getServers()));
            return json;
            findAgeInfo(getServers());
            return "";
        }

        static HttpResponseHeaders findHeaders(string uri)
        {
            HttpResponseMessage response;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(uri);
            response = client.GetAsync("").Result;
            response.EnsureSuccessStatusCode();
            return (response.Headers);
        }

        static List<TimeSpan> getAllHeadersServer(List<string> servers)
        {
            List<TimeSpan> ages = new List<TimeSpan>();

            foreach (string server in servers)
            {
                try
                {
                    TimeSpan? age = findHeaders(server)?.Age;
                    if (age != null)
                    {
                        ages.Add(age.Value);
                    }
                }

                catch (Exception e)
                {
                    Console.WriteLine("Error: " + server + ": " + e.Message);
                }
            }
            return ages;
        }
        static dynamic findAgeInfo(List<string> servers)
        {

            List<TimeSpan> ages = getAllHeadersServer(servers);

            if (ages.Count > 0)
            {
                // Calculate the average age
                TimeSpan averageAge = TimeSpan.FromTicks((long)ages.Average(t => t.Ticks));
                var averageFormated= new
                {
                    hour = averageAge.Hours,
                    minute = averageAge.Minutes,
                    second = averageAge.Seconds
                };

                // Calculate the standard deviation
                double variance = ages.Average(t => Math.Pow(t.Ticks - averageAge.Ticks, 2));
                TimeSpan standardDeviation = TimeSpan.FromTicks((long)Math.Sqrt(variance));
                var standardDeviationFormated = new
                {
                    hour = standardDeviation.Hours,
                    minute = standardDeviation.Minutes,
                    second = standardDeviation.Seconds
                };
                return new
                {
                    average = averageFormated,
                    standardDeviation = standardDeviationFormated
                };
            }
            else
            {
                return null;
            }
        }



            static List<string> getServers()
        {
            List<string> servers = new List<string>();
            servers.Add("https://fr.wikipedia.org/wiki/Alpes_dinariques");
            servers.Add("https://fr.wikipedia.org/wiki/Massif_de_montagnes");
            servers.Add("https://fr.wikipedia.org/wiki/Pente_(topographie)");
            servers.Add("https://fr.wikipedia.org/wiki/Citadelles");
            servers.Add("https://fr.wikipedia.org/wiki/Sciences_humaines_et_sociales");
            servers.Add("https://fr.wikipedia.org/wiki/Histoire");
            servers.Add("https://fr.wikipedia.org/wiki/Octobre_1961");
            servers.Add("https://fr.wikipedia.org/wiki/Bruno_Faidutti");

            return servers;
        }
    }
}