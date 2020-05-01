using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.Helpers.Request
{
    public class RestRequestHelper
    {
        private static IRestClient CreatetRestClient()
        {
            return new RestClient();
        }
        private static IRestRequest CreateRestRequest(string url, Method method, Dictionary<string, string> headers, object body, DataFormat dataFormat)
        {
            IRestRequest restRequest = new RestRequest()
            {
                Method = method,
                Resource = url
            };
            if(body!= null) {
                restRequest.RequestFormat = dataFormat;
                switch (dataFormat)
                {
                    case DataFormat.Json:
                        restRequest.AddBody(body);
                        break;
                    case DataFormat.Xml:
                        restRequest.XmlSerializer = new RestSharp.Serializers.DotNetXmlSerializer(); //We need to serialize the data if Data Format is in XMl and body is in xml Model
                        restRequest.AddParameter("XmlBody", body.GetType().Equals(typeof(string)) ? body : restRequest.XmlSerializer.Serialize(body) , ParameterType.RequestBody); // Add Body Doesn't work with XML Data so we need to use AddParameter method.
                        break;                   
                }
                              
            }
            
           
            if (headers != null) {
                foreach (string key in headers.Keys)
                {
                    restRequest.AddHeader(key, headers[key]);
                }
            }
            
            return restRequest;
        }

        private static IRestResponse SendRequest(IRestRequest restRequest)
        {
            var restClient = CreatetRestClient();
            IRestResponse restResponse = restClient.Execute(restRequest);
            
            return restResponse;
        }

       
        private static IRestResponse<T> SendRequest<T>(IRestRequest restRequest) where T : new()
        {
            var restClient = CreatetRestClient();
            IRestResponse<T> restResponse = restClient.Execute<T>(restRequest);
            if(restResponse.ContentType.Equals("application/xml"))  //Deserialize if content type is XML
            {
                var deserializer = new RestSharp.Deserializers.DotNetXmlDeserializer();
                restResponse.Data = deserializer.Deserialize<T>(restResponse);
            }
            return restResponse;
        }

        public static IRestResponse PerformGetRequest(string requestUrl, Dictionary<string, string> HttpHeaders)
        {
            IRestRequest request = CreateRestRequest(requestUrl, Method.GET, HttpHeaders, null, DataFormat.None);
            IRestResponse restResponse = SendRequest(request);
            //IRestResponse<T> restResponseDeserialized = SendRequest<T>(request);
            return restResponse;
        }
        public static IRestResponse<T> PerformGetRequest<T>(string requestUrl, Dictionary<string, string> HttpHeaders) where T: new()
        {
            IRestRequest request = CreateRestRequest(requestUrl, Method.GET, HttpHeaders,null, DataFormat.None);
           
            IRestResponse<T> restResponseDeserialized = SendRequest<T>(request);
            return restResponseDeserialized;
        }
        public static IRestResponse<T> PerformPostRequest<T>(string requestUrl, Dictionary<string, string> HttpHeaders, Object body, DataFormat dataFormat) where T : new()
        {
            IRestRequest request = CreateRestRequest(requestUrl, Method.POST, HttpHeaders, body, dataFormat);

            IRestResponse<T> restResponseDeserialized = SendRequest<T>(request);
            return restResponseDeserialized;
        }
         public static IRestResponse PerformPostRequest(string requestUrl, Dictionary<string, string> HttpHeaders, Object body, DataFormat dataFormat) 
        {
            IRestRequest request = CreateRestRequest(requestUrl, Method.POST, HttpHeaders, body, dataFormat);

            IRestResponse restResponse = SendRequest(request);
            return restResponse;
        }
        public static IRestResponse<T> PerformPutRequest<T>(string requestUrl, Dictionary<string, string> HttpHeaders, Object body, DataFormat dataFormat) where T : new()
        {
            IRestRequest request = CreateRestRequest(requestUrl, Method.PUT, HttpHeaders, body, dataFormat);

            IRestResponse<T> restResponseDeserialized = SendRequest<T>(request);
            return restResponseDeserialized;
        }
        public static IRestResponse PerformPutRequest(string requestUrl, Dictionary<string, string> HttpHeaders, Object body, DataFormat dataFormat)
        {
            IRestRequest request = CreateRestRequest(requestUrl, Method.PUT, HttpHeaders, body, dataFormat);

            IRestResponse restResponse = SendRequest(request);
            return restResponse;
        }
        public static IRestResponse PerformDeleteRequest(string requestUrl, Dictionary<string, string> httpHeaders)
        {
            IRestRequest request = CreateRestRequest(requestUrl, Method.DELETE, httpHeaders, null, DataFormat.None);

            IRestResponse restResponse = SendRequest(request);
            return restResponse;
        }

    }
}