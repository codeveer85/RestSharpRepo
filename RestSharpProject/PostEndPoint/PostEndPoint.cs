using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharpProject.Helper.Authentication;
using RestSharpProject.Helper.Request;
using RestSharpProject.Helper.Response;
using RestSharpProject.Model;
using RestSharpProject.Model.JsonModel;
using RestSharpProject.Model.XmlModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RestSharpProject.PostEndPoint
{
    [TestClass]
   public class PostEndPoint
    {
        private Uri postUri = new Uri("http://localhost:8080/laptop-bag/webapi/api/add");
        private string getUrl = "http://localhost:8080/laptop-bag/webapi/api/find/";
        private string jsonMediaType = "application/json";
        private string xmlMediaType = "application/xml";
        private RestResponse restResponse;
        private RestResponse restResponseForGet;
        Random random = new Random();
        [TestMethod]
        public void PostEndPointTestusingPostAsync()
        {
           
            int id = random.Next(1001);
            string jsonData = "{" +
        "\"BrandName\": \"Animalware\"," +
        "\"Features\": {" +
                "\"Feature\": [" +
                    "\"8th Generation Intel® Core™ i5-8300H\"," +
                    "\"Windows 10 Home 64-bit English\"," +
                    "\"NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6\"," +
                    "\"8GB, 2x4GB, DDR4, 2666MHz\"" +
            "]" +
    "}," +
        "\"Id\":" + id +"," +
        "\"LaptopName\": \"Alienware M17\"" +
    "}";
            
           

            using (HttpClient httpclient = new HttpClient())
            {
                HttpContent httpcontent = new StringContent(jsonData, Encoding.UTF8, jsonMediaType);
               Task<HttpResponseMessage>httpResponse= httpclient.PostAsync(postUri, httpcontent);
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
                    Assert.AreEqual("Animalware", jsonRootObject.BrandName);

                }

            }

        }
        [TestMethod]
        public void PostEndPointTestUsingXml()
        {

            int id = random.Next(1001);
            string xmlData = "<Laptop>" + 
    "<BrandName>Alienware</BrandName>" +
    "<Features>" +
        "<Feature> 8th Generation Intel® Core™ i5 - 8300H </Feature>"+     
             "<Feature> Windows 10 Home 64 - bit English </Feature>" +          
                  "<Feature> NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +           
                     "<Feature> 8GB, 2x4GB, DDR4, 2666MHz </Feature>" +                
                   " </Features>" +                
                    "<Id>" + id +"</Id>"+                
                    "<LaptopName> Alienware M17 </LaptopName>" +
                   "</Laptop>";



            using (HttpClient httpclient = new HttpClient())
            {
                HttpContent httpcontent = new StringContent(xmlData, Encoding.UTF8, xmlMediaType);
                Task<HttpResponseMessage> httpResponse = httpclient.PostAsync(postUri, httpcontent);
                using (HttpResponseMessage httpResponseMessage = httpResponse.Result)
                {
                    restResponse = new RestResponse((int)httpResponseMessage.StatusCode, httpResponseMessage.Content.ReadAsStringAsync().Result);
                    Console.WriteLine(restResponse.ToString());
                    Assert.AreEqual(200, restResponse.StatusCode);
                    Assert.IsNotNull(restResponse.ResponseData, "Response data is not null/empty");
                    Task<HttpResponseMessage> httpGetResponse = httpclient.GetAsync(getUrl + id);
                    if (!httpGetResponse.Result.IsSuccessStatusCode)
                    {
                        Assert.Fail("Assert Failed");
                    }
                    restResponseForGet = new RestResponse((int)httpGetResponse.Result.StatusCode, httpGetResponse.Result.Content.ReadAsStringAsync().Result);
                    Console.WriteLine(httpGetResponse.Result.Content.ReadAsStringAsync().Result);
                    Laptop laptop = ResponseDataHelper.DeserializeXmlResponse<Laptop>(restResponseForGet.ResponseData);
                    //XmlSerializer serializer = new XmlSerializer(typeof(Laptop));
                    //TextReader textReader = new StringReader(restResponseForGet.ResponseData);
                    //Laptop laptop = (Laptop)serializer.Deserialize(textReader); 
                    Assert.AreEqual("Alienware", laptop.BrandName);
                }

            }

        }
        [TestMethod]
        public void PostEndPointTestusingSendAsync()
        {
            int id = random.Next(1001);
            string jsonData = "{" +
        "\"BrandName\": \"Animalware\"," +
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



            using (HttpClient httpClient = new HttpClient())
            {
                
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, postUri);
                httpClient.DefaultRequestHeaders.Add("accept", jsonMediaType);
                HttpContent httpContent = new StringContent(jsonData, Encoding.UTF8, jsonMediaType);
                httpRequestMessage.Content = httpContent;
               

                Task<HttpResponseMessage> httpResponse = httpClient.SendAsync(httpRequestMessage);
                using (HttpResponseMessage httpResponseMessage = httpResponse.Result)
                {
                    restResponse = new RestResponse((int)httpResponseMessage.StatusCode, httpResponseMessage.Content.ReadAsStringAsync().Result);
                    Console.WriteLine(restResponse.ToString());
                    Assert.AreEqual(200, restResponse.StatusCode);
                    Assert.IsNotNull(restResponse.ResponseData, "Response data is not null/empty");
                  

                }

            }

        }
        [TestMethod]
        public void PostEndPointTestusingPostHelper()
        {
            string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add";            
            int id = random.Next(1001);
            string jsonData = "{" +
       "\"BrandName\": \"Animalware\"," +
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
            Dictionary<String, string>httpHeaders = new Dictionary<string, string>();

            httpHeaders.Add("accept", "application/json");
            HttpContent httpContent = new StringContent(jsonData, Encoding.UTF8, jsonMediaType);          
            RestResponse restResponse = HttpClientHelper.PerformPostRequest(postUrl, httpContent, httpHeaders);
            Console.WriteLine(restResponse.ToString());
            JsonRootObject jsonRootObject = JsonConvert.DeserializeObject<JsonRootObject>(restResponse.ResponseData);
            Assert.AreEqual("Alienware M17", jsonRootObject.LaptopName);
           
        }
        [TestMethod]
        public void SecurePostEndPointTestusingPostHelper()
        {
            string postUrl = "http://localhost:8080/laptop-bag/webapi/secure/add";
            int id = random.Next(1001);
            string jsonData = "{" +
       "\"BrandName\": \"Animalware\"," +
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
            string authHeader = "Basic " + Base64AuthenticationHelper.GetBase64String("admin", "welcome");

            Dictionary<string, string> httpHeader = new Dictionary<string, string>();
            httpHeader.Add("Accept", "application/json");
            httpHeader.Add("Authorization", authHeader);
            HttpContent httpContent = new StringContent(jsonData, Encoding.UTF8, jsonMediaType);
            RestResponse restResponse = HttpClientHelper.PerformPostRequest(postUrl, httpContent, httpHeader);
            Console.WriteLine(restResponse.ToString());
            JsonRootObject jsonRootObject = JsonConvert.DeserializeObject<JsonRootObject>(restResponse.ResponseData);
            Assert.AreEqual("ware M17", jsonRootObject.LaptopName);

        }
    }
}
