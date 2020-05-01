using RestSharpProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpProject.Helper.Request
{
   public class HttpClientHelper
    {
        private static HttpClient httpClient;
        private static HttpRequestMessage httpRequestMessage;
        private static RestResponse restResponse;
        private static HttpClient AddHeadersAndCreateHttpClient(Dictionary<string, string> httpHeaders)
        {
            httpClient = new HttpClient();
            if (httpHeaders != null) {
                foreach (string key in httpHeaders.Keys)
                {
                    httpClient.DefaultRequestHeaders.Add(key, httpHeaders[key]);
                }
            }
            
            return httpClient;
        }
        private static HttpRequestMessage CreateHttpRequestMessage(string requestUrl, HttpMethod httpMethod, HttpContent httpContent)
        {
            httpRequestMessage = new HttpRequestMessage(httpMethod, requestUrl);
            if (!(httpRequestMessage.Method==HttpMethod.Get))
                httpRequestMessage.Content = httpContent;

            return httpRequestMessage;
        }
        
        private static RestResponse SendHttpRequest(string requestUrl, HttpMethod httpMethod, HttpContent httpContent, Dictionary<string, string> httpHeaders)
        {
            HttpClient httpClient = AddHeadersAndCreateHttpClient(httpHeaders);
            HttpRequestMessage httpRequestMessage = CreateHttpRequestMessage(requestUrl, httpMethod, httpContent);

            try
            {
                Task<HttpResponseMessage> httpResponseMessage = httpClient.SendAsync(httpRequestMessage);
                restResponse = new RestResponse((int)httpResponseMessage.Result.StatusCode, 
                    httpResponseMessage.Result.Content.ReadAsStringAsync().Result);
                
            }
            catch (Exception ex)
            {
                restResponse = new RestResponse(500, ex.Message);

            }
            finally
            {
                httpRequestMessage?.Dispose();
                httpClient?.Dispose();

            }
            return restResponse;
        }

        public static RestResponse PerformGetRequest(string requestUrl, Dictionary<string, string>httpHeaders)
        {
            return SendHttpRequest(requestUrl, HttpMethod.Get, null, httpHeaders);
        }
        public static RestResponse PerformPostRequest(string requestUrl, HttpContent httpContent, Dictionary<string, string> httpHeaders)
        {
            return SendHttpRequest(requestUrl, HttpMethod.Post, httpContent, httpHeaders);
        }
        public static RestResponse PerformPostRequest(string requestUrl, string data, string mediaType, Dictionary<string, string> httpHeaders)
        {
            HttpContent httpContent = new StringContent(data, Encoding.UTF8, mediaType);
            return SendHttpRequest(requestUrl, HttpMethod.Post, httpContent, httpHeaders);
        }
        public static RestResponse PerformPutRequest(string requestUrl, string data, string mediaType, Dictionary<string, string> httpHeaders)
        {
            HttpContent httpContent = new StringContent(data, Encoding.UTF8, mediaType);
            return SendHttpRequest(requestUrl, HttpMethod.Put, httpContent, httpHeaders);
        }
        public static RestResponse PerformDeleteRequest(string requestUrl, Dictionary<string, string> httpHeaders)
        {
            return SendHttpRequest(requestUrl, HttpMethod.Delete, null, null);
        }
    }

}
