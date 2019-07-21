using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;

namespace OsmanKURT.Common
{
    public static class ApiCaller
    {
        public static T Get<T>(ApiCallerDTO request)
        {
            return Request<T>(request, "GET");
        }

        public static T Post<T>(ApiCallerDTO request)
        {
            return Request<T>(request, "POST");
        }

        private static T Request<T>(ApiCallerDTO request, string method)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Url))
            {
                throw new Exception("Endpoint not found");
            }

            string args = method == "GET" ? request.RequestObject.ToString() : string.Empty;

            HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(request.Url + args);
            httpRequest.Method = method;
            HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            httpRequest.CachePolicy = noCachePolicy;
            httpRequest.ContentType = "application/json";

            if (request.RequestObject != null && method == "POST")
            {
                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(request.RequestObject);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }

            foreach (var header in request.Header)
            {
                httpRequest.Headers.Add(header.Key, header.Value);
            }
            //httpRequest.Timeout = 3000;

            string result = string.Empty;

            using (HttpWebResponse response = (HttpWebResponse)httpRequest.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                result = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
            }

            if (!string.IsNullOrWhiteSpace(result))
            {
                var resultObject = (JObject)JsonConvert.DeserializeObject(result);

                if (resultObject != null)
                {
                    return resultObject.ToObject<T>();
                }
            }

            return (T)Convert.ChangeType(null, typeof(T));
        }
    }
}
