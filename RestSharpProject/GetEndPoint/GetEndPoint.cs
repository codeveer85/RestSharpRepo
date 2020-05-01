using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharpProject.Helper.Authentication;
using RestSharpProject.Helper.Request;
using RestSharpProject.Model;
using RestSharpProject.Model.JsonModel;

namespace RestSharpProject.GetEndPoint
{
    [TestClass]
    public class GetEndPoint
    {
        [TestMethod]
        public void TestGetAllEndPoint()
        {
            HttpClient httpclient = new HttpClient();
            var getUrl = "http://localhost:8080/laptop-bag/webapi/api/all";
            Task<HttpResponseMessage> httpResponse = httpclient.GetAsync(getUrl); //Return Type is of Type task so we will use Result property of Task to get the ResponseMessage.
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            HttpStatusCode statusCode = httpResponseMessage.StatusCode;
            HttpContent responseContent= httpResponseMessage.Content;
            Task<string> content = responseContent.ReadAsStringAsync();
            string getContent = content.Result;

            //Console.WriteLine(httpResponseMessage.ToString());
            //Console.WriteLine("StatusCode=>" + statusCode);
            //Console.WriteLine("StatusCode=>" + (int)statusCode);
            //Console.WriteLine(getContent);
            Console.WriteLine("StatusCode=>{0} Content=> {1}", (int)httpResponse.Result.StatusCode, httpResponse.Result.Content.ReadAsStringAsync().Result);// Geeting the content and Statuscode in one line code.


            httpclient.Dispose();
        }
        [TestMethod]
        public void TestGetAllEndPointwithUri()
        {
            HttpClient httpclient = new HttpClient();
            
            var getUrl = "http://localhost:8080/laptop-bag/webapi/api/all";
            Uri getUri = new Uri(getUrl);
            //Task<HttpResponseMessage> httpResponse = httpclient.GetAsync(getUri);
            //httpResponse.
            //Console.WriteLine(httpResponse.Sta)

            httpclient.Dispose();
        }
        [TestMethod]
        public void TestGetAllEndPointInJsonFormat()
        {
            HttpClient httpclient = new HttpClient();
            var getUrl = "http://localhost:8080/laptop-bag/webapi/api/all";
            MediaTypeWithQualityHeaderValue acceptHeader = new MediaTypeWithQualityHeaderValue("application/json");
            //httpclient.DefaultRequestHeaders.Add("accept", "application/xml");
            httpclient.DefaultRequestHeaders.Accept.Add(acceptHeader);
            Task<HttpResponseMessage> httpResponse = httpclient.GetAsync(getUrl); //Return Type is of Type task so we will use Result property of Task to get the ResponseMessage.
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            HttpStatusCode statusCode = httpResponseMessage.StatusCode;
            HttpContent responseContent = httpResponseMessage.Content;
            Task<string> content = responseContent.ReadAsStringAsync();
            string getContent = content.Result;

            //Console.WriteLine(httpResponseMessage.ToString());
            //Console.WriteLine("StatusCode=>" + statusCode);
            //Console.WriteLine("StatusCode=>" + (int)statusCode);
            //Console.WriteLine(getContent);
            Console.WriteLine("StatusCode=>{0} Content=> {1}", (int)httpResponse.Result.StatusCode, httpResponse.Result.Content.ReadAsStringAsync().Result);// Geeting the content and Statuscode in one line code.


            httpclient.Dispose();
        }
        [TestMethod]
        public void TestGetAllEndPointWithSendAsyncMethod()
        {
            HttpClient httpclient = new HttpClient();
            var getUrl = "http://localhost:8080/laptop-bag/webapi/api/all";
            Uri getUri = new Uri(getUrl);
            httpclient.DefaultRequestHeaders.Add("accept", "application/json");
            //MediaTypeWithQualityHeaderValue acceptHeader = new MediaTypeWithQualityHeaderValue("application/json");            
            //httpclient.DefaultRequestHeaders.Accept.Add(acceptHeader);
            //HttpMethod getMethod = new HttpMethod("Get");
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Get;
            httpRequestMessage.RequestUri = getUri;
           // httpRequestMessage.Headers.Add("accept", "application/json");

            Task<HttpResponseMessage> httpResponse = httpclient.SendAsync(httpRequestMessage); //Return Type is of Type task so we will use Result property of Task to get the ResponseMessage.
          

           
            Console.WriteLine("StatusCode=>{0} Content=> {1}", (int)httpResponse.Result.StatusCode, httpResponse.Result.Content.ReadAsStringAsync().Result);// Geeting the content and Statuscode in one line code.


            httpclient.Dispose();
        }
        [TestMethod]
        public void TestGetAllEndPointWithSendAsyncMethodandUsing()// While we use Using in that case we do not need to call the dispose() method and Using can only be used with classes Extending Idesposable
        {
            
           
            Uri getUri = new Uri("http://localhost:8080/laptop-bag/webapi/api/all");
            using (HttpClient httpclient = new HttpClient())
            {
                using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
                {
                    httpRequestMessage.Method = HttpMethod.Get;
                    httpRequestMessage.RequestUri = getUri;
                    httpRequestMessage.Headers.Add("accept", "application/json");

                    Task<HttpResponseMessage> httpResponse = httpclient.SendAsync(httpRequestMessage); //Return Type is of Type task so we will use Result property of Task to get the ResponseMessage.
                    using (HttpResponseMessage httpResponseMessage = httpResponse.Result)
                    {
                        Console.WriteLine("StatusCode=>{0} Content=> {1}", (int)httpResponseMessage.StatusCode, httpResponseMessage.Content.ReadAsStringAsync().Result);
                        RestResponse restResponse = new RestResponse((int)httpResponseMessage.StatusCode, httpResponseMessage.Content.ReadAsStringAsync().Result);
                        Console.WriteLine(restResponse.ToString());
                       
                    }
                }

               
             

            }
        }
        [TestMethod]
        public void TestGetAllEndPointusingGetHelper()
        {
           
            var getUrl = "http://localhost:8080/laptop-bag/webapi/api/find/1";
            Dictionary<string, string> httpHeader = new Dictionary<string, string>();
            httpHeader.Add("Accept","application/json");
            RestResponse restResponse = HttpClientHelper.PerformGetRequest(getUrl, httpHeader);

            Console.WriteLine(restResponse.ToString());
            JsonRootObject JsonData = JsonConvert.DeserializeObject<JsonRootObject>(restResponse.ResponseData);
            Assert.AreEqual("Alienware", JsonData.BrandName);
        }
        [TestMethod]
        public void TestSecureGetAllEndPointusingGetHelper()
        {

            var getUrl = "http://localhost:8080/laptop-bag/webapi/delay/all";
            string authHeader = "Basic " + Base64AuthenticationHelper.GetBase64String("admin", "welcome");
            
            Dictionary<string, string> httpHeader = new Dictionary<string, string>();
            httpHeader.Add("Accept", "application/json");
            //httpHeader.Add("Authorization", "Basic YWRtaW46d2VsY29tZQ==");
           // httpHeader.Add("Authorization", authHeader);
            RestResponse restResponse = HttpClientHelper.PerformGetRequest(getUrl, httpHeader);
            Console.WriteLine(restResponse.ToString());
            List<JsonRootObject> JsonData = JsonConvert.DeserializeObject<List<JsonRootObject>>(restResponse.ResponseData);
            Assert.AreEqual("Alienware", JsonData[0].BrandName);
        }

        [TestMethod]
        public void TestGetAllEndPoint_Async()
        {


            Task T1 = new Task(GetAllEndPointAction());
            T1.Start();
            //Task T2 = new Task(GetAllEndPointAction());
            //T2.Start();
            //Task T3 = new Task(GetAllEndPointAction());
            //T3.Start();
            //Task T4 = new Task(GetAllEndPointAction());
            //T4.Start();
            T1.Wait();
            //T2.Wait();
            //T3.Wait();
            //T4.Wait();


        }
        private Action GetAllEndPointAction()
        {
            var getUrl = "http://localhost:8080/laptop-bag/webapi/delay/all";

            Dictionary<string, string> httpHeader = new Dictionary<string, string>();
            httpHeader.Add("Accept", "application/json");
            return new Action(() =>
            {
               RestResponse restResponse = HttpClientHelper.PerformGetRequest(getUrl, httpHeader);
                Assert.AreEqual(200, restResponse.StatusCode);
                
            });

        }
        [TestMethod]
        public void GetEndpointTrst()
        {
            var getUrl = "http://localhost:8080/laptop-bag/webapi/api/all";

            Dictionary<string, string> httpHeader = new Dictionary<string, string>();
            httpHeader.Add("Accept", "application/json");
            
                RestResponse restResponse = HttpClientHelper.PerformGetRequest(getUrl, httpHeader);
                Assert.AreEqual(201, restResponse.StatusCode);

            

        }
        
    }
}