using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharpProject.Helper.Request;
using RestSharpProject.Helper.Response;
using RestSharpProject.Model;
using RestSharpProject.Model.JsonModel;
using RestSharpProject.Model.XmlModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpProject.PutEndPoints
{
    [TestClass]
    public class PutEndPoints
    {
        private string putUrl = "http://localhost:8080/laptop-bag/webapi/api/update";
        private string getUrl = "http://localhost:8080/laptop-bag/webapi/api/find/";
        private string jsonMediaType = "application/json";
        private string xmlMediaType = "application/xml";
        private RestResponse restResponse;
        private RestResponse restResponseForGet;
              [TestMethod]
        public void PutEndPointTestusingPuttAsync()
        {
            int id = 965;
            string jsonData = "{" +
        "\"BrandName\": \"Gadhaware\"," +
        "\"Features\": {" +
                "\"Feature\": [" +
                    "\"8th Generation Intel® Core™ i5-8300H\"," +
                    "\"Windows 10 Home 64-bit English\"," +
                    "\"NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6\"," +
                    "\"8GB, 2x4GB, DDR4, 2666MHz\"" +
            "]" +
    "}," +
        "\"Id\":" + id + "," +
        "\"LaptopName\": \"Alienware M17\"" +
    "}";



            using (HttpClient httpclient = new HttpClient())
            {
                HttpContent httpcontent = new StringContent(jsonData, Encoding.UTF8, jsonMediaType);
                Task<HttpResponseMessage> httpResponse = httpclient.PutAsync(putUrl, httpcontent);
                using (HttpResponseMessage httpResponseMessage = httpResponse.Result)
                {
                    restResponse = new RestResponse((int)httpResponseMessage.StatusCode, httpResponseMessage.Content.ReadAsStringAsync().Result);
                    Console.WriteLine(restResponse.ToString());
                    Assert.AreEqual(200, restResponse.StatusCode);
                    Assert.IsNotNull(restResponse.ResponseData, "Response data is not null/empty");
                    httpclient.DefaultRequestHeaders.Add("Accept", jsonMediaType);
                    Task<HttpResponseMessage> httpGetResponse = httpclient.GetAsync(getUrl + id);
                    restResponseForGet = new RestResponse((int)httpGetResponse.Result.StatusCode, httpGetResponse.Result.Content.ReadAsStringAsync().Result);
                    Console.WriteLine(httpGetResponse.Result.Content.ReadAsStringAsync().Result);
                    JsonRootObject jsonRootObject = ResponseDataHelper.DeserializeJsonResponse<JsonRootObject>(restResponseForGet.ResponseData);
                    Assert.AreEqual(200, restResponseForGet.StatusCode);
                    Assert.AreEqual("Gadhaware", jsonRootObject.BrandName);

                }

            }

        }
        [TestMethod]
        public void PutEndPointTestusingClientHelper()
        {
            int id = 965;
            string jsonData = "{" +
        "\"BrandName\": \"Ramware\"," +
        "\"Features\": {" +
                "\"Feature\": [" +
                    "\"8th Generation Intel® Core™ i5-8300H\"," +
                    "\"Windows 10 Home 64-bit English\"," +
                    "\"NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6\"," +
                    "\"8GB, 2x4GB, DDR4, 2666MHz\"" +
            "]" +
    "}," +
        "\"Id\":" + id + "," +
        "\"LaptopName\": \"Alienware M17\"" +
    "}";



            
            HttpContent httpcontent = new StringContent(jsonData, Encoding.UTF8, jsonMediaType);
            RestResponse restResponse = HttpClientHelper.PerformPutRequest(putUrl, jsonData, jsonMediaType, null);
            Assert.AreEqual(200, restResponse.StatusCode);
            RestResponse restResponseForGet = HttpClientHelper.PerformGetRequest(getUrl + id, null);
            Laptop laptop = ResponseDataHelper.DeserializeXmlResponse<Laptop>(restResponseForGet.ResponseData);                
            Assert.AreEqual(200, restResponseForGet.StatusCode);
            Assert.AreEqual("Ramware", laptop.BrandName);

                

            }

        }
    }

