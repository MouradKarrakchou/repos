using System.Collections.Generic;
using System.Web.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ServerForTuto2
{
    public class TimeResponseController : ApiController
    {

        // GET api/values
        public string Get()
        {
            string json = JsonSerializer.Serialize(averageResponseTime(Tools.getServers()));
            return json;
        }


        static Dictionary<string, TimeSpan> averageResponseTime(List<string> servers)
        {
            Dictionary<string, TimeSpan> responseTimes = new Dictionary<string, TimeSpan>();

            foreach (string server in servers)
            {
                try
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(server);
                    var totalResponseTime = TimeSpan.Zero;
                    for (int i = 0; i < 5; i++)
                    {
                        var start = DateTime.UtcNow;
                        var response = client.GetAsync("").Result;
                        response.EnsureSuccessStatusCode();
                        var end = DateTime.UtcNow;
                        var responseTime = end - start;
                        totalResponseTime += responseTime;
                    }
                    var averageResponseTime = TimeSpan.FromTicks(totalResponseTime.Ticks / 5);
                    responseTimes[server] = averageResponseTime;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + server + ": " + e.Message);
                }
            }
            return responseTimes;
        }
    }
}
