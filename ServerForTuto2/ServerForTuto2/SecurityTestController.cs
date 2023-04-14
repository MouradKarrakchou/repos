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
    public class SecurityTestController : ApiController
    {

        // GET api/securitytest
        public string Get()
        {
            string json = JsonSerializer.Serialize(GetHeadersOccurence(Tools.getServers()));
            return json;
        }


        Dictionary<string, int> GetHeadersOccurence(List<string> servers)
        {
            Dictionary<string, int> headerCounts = new Dictionary<string, int>
            {
                {"Strict-Transport-Security", 0},
                {"Content-Security-Policy", 0},
                {"X-XSS-Protection", 0},
                {"X-Content-Type-Options", 0}
            };

            foreach (string server in servers)
            {
                try
                {
                    HttpResponseHeaders response = Tools.findHeaders(server);

                    if (response.Contains("Strict-Transport-Security"))
                        headerCounts["Strict-Transport-Security"]++;

                    if (response.Contains("Content-Security-Policy"))
                        headerCounts["Content-Security-Policy"]++;

                    if (response.Contains("X-XSS-Protection"))
                        headerCounts["X-XSS-Protection"]++;

                    if (response.Contains("X-Content-Type-Options"))
                        headerCounts["X-Content-Type-Options"]++;
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error: " + server + ": " + exception.Message);
                }
            }
            return headerCounts;
        }
    }
}