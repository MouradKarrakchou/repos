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
    public class ValuesController : ApiController
    {

        // GET api/values
        public string Get()
        {
            string json = JsonSerializer.Serialize(findEachOccurence(Tools.getServers()));
            return json;
        }

        static List<string> getAllHeadersServer(List<string> servers)
        {
            List<string> serverTypes = new List<string>();

            foreach (string server in servers)
            {
                try
                {

                    string serverType = Tools.findHeaders(server).Server.ToString();
                    if (!String.IsNullOrEmpty(serverType))
                    {
                        serverTypes.Add(serverType);
                    }
                }

                catch (WebException exception)
                {
                    Console.WriteLine("Error: " + server + ": " + exception.Message);
                }
            }
            return serverTypes;
        }
        static Dictionary<string, int> findEachOccurence(List<string> servers)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (string str in getAllHeadersServer(servers))
            {
                if (dict.ContainsKey(str))
                {
                    dict[str]++;
                }
                else
                {
                    dict.Add(str, 1);
                }
            }

            return dict;
        }
       
    }
}