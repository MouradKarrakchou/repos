using System.Collections.Generic;
using System.Web.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.IO;
using System.Reflection;

namespace ServerForTuto2
{
    public class CacheController : ApiController
    {

        // GET api/cache
        public string Get()
        {
            string json = JsonSerializer.Serialize(getCacheControlDirectives(Tools.getServers()));
            return json;
        }


        static Dictionary<string, int> getCacheControlDirectives(List<string> servers)
        {
            var directiveCounts = new Dictionary<string, int>();
            string[] directivesToCheck = { "no-store", "must-revalidate", "no-cache", "max-age=0", "no-transform", "public", "private", "proxy-revalidate", "max-stale", "min-fresh", "only-if-cached", "no-cache=", "no-cache=*" };

            foreach (string server in servers)
            {
                try
                {
                    CacheControlHeaderValue cacheControl = Tools.findHeaders(server).CacheControl;
                    if (cacheControl != null)
                    {
                        foreach (string directiveName in directivesToCheck)
                        {
                            if (cacheControl.ToString().Contains(directiveName))
                            {
                                if (directiveCounts.ContainsKey(directiveName))
                                {
                                    directiveCounts[directiveName]++;
                                }
                                else
                                {
                                    directiveCounts[directiveName] = 1;
                                }
                            }
                        }
                    }
                }
                catch (WebException exception)
                {
                    Console.WriteLine("Error: " + server + ": " + exception.Message);
                }
            }

            return directiveCounts;
        }
    }
}
