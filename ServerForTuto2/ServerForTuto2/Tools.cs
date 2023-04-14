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
    class Tools
    {
        public static List<string> getServers()
        {
            List<string> servers = new List<string>();
            servers.Add("http://www.youtube.com");
            servers.Add("https://www.google.fr/");
            servers.Add("https://learn.microsoft.com/en-us/dotnet/api/system.net.http.headers.httpresponseheaders?view=netframework-4.8");
            servers.Add("https://fr.wikipedia.org/wiki/Sciences_humaines_et_sociales");
            servers.Add("https://www.amazon.fr/");
            servers.Add("http://www.tigli.fr/");


            return servers;
        }


        public static HttpResponseHeaders findHeaders(string uri)
        {
            HttpResponseMessage response;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(uri);
            response = client.GetAsync("").Result;
            response.EnsureSuccessStatusCode();
            return (response.Headers);
        }

    }
}
