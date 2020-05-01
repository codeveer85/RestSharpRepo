using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpAutomation.Helpers.Request;
using RestSharpProject.Model.JsonModel;
using RestSharpProject.Model.XmlModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.PostEndPoint
{
    [TestClass]
    public class TestPostEndPoints
    {
        private string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add";
        private Random random = new Random();

        public IRestResponse RestClientHelper { get; private set; }

        [TestMethod]
        public void PostEndPointsWithJsonData()
        {
            int id = random.Next(100);
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
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(postUrl);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddJsonBody(jsonData);
            IRestResponse restResponse = restClient.Post(restRequest);
            if (restResponse.IsSuccessful)
            {
                Console.WriteLine((int)restResponse.StatusCode);
                Console.WriteLine(restResponse.Content);
                Assert.AreEqual(200, (int)restResponse.StatusCode);

            }
        }
        private Laptop GetLaptopObject()
        {
            Laptop laptop = new Laptop()
            {
                BrandName = "sample BrandName",
                LaptopName = "Sample Name",
                Id = ""+random.Next(1000)

            };
            RestSharpProject.Model.XmlModel.Features features = new RestSharpProject.Model.XmlModel.Features();
            List<string> featureList = new List<string>()
            {
                ("sample feature")
            };
            features.Feature = featureList;
            laptop.Features = features;
            return laptop;
        }
        [TestMethod]
        public void PostEndPointsWithModel()
        {
            int id = random.Next(100);            
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(postUrl);
            restRequest.AddHeader("Accept", "application/xml");
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.RequestFormat = DataFormat.Json;

            restRequest.AddBody(GetLaptopObject());
            IRestResponse restResponse = restClient.Post(restRequest);
            if (restResponse.IsSuccessful)
            {
                Console.WriteLine((int)restResponse.StatusCode);
                Console.WriteLine(restResponse.Content);
                Assert.AreEqual(200, (int)restResponse.StatusCode);

            }
        }
        [TestMethod]
        public void PostEndPointsusingHelper()
        {

            Dictionary<string, string> requestHeaders = new Dictionary<string, string>()
            {
                { "Accept", "application/json" },
                {"Content-Type", "application/json" }
            };
            IRestResponse<Laptop> restResponse = RestRequestHelper.PerformPostRequest<Laptop>(postUrl, requestHeaders, GetLaptopObject(), DataFormat.Json);

           if (restResponse.IsSuccessful)
            {
                Console.WriteLine((int)restResponse.StatusCode);
                Console.WriteLine(restResponse.Content);
                Assert.AreEqual(200, (int)restResponse.StatusCode);
                Assert.AreEqual("sample BrandName", restResponse.Data.BrandName);

            }
        }
        [TestMethod]
        public void PostEndPointusingXmlData()
        {
            int id = random.Next(1000);
            string xmlData = "<Laptop>" +
   "<BrandName>Alienware</BrandName>" +
   "<Features>" +
       "<Feature> 8th Generation Intel® Core™ i5 - 8300H </Feature>" +
            "<Feature> Windows 10 Home 64 - bit English </Feature>" +
                 "<Feature> NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                    "<Feature> 8GB, 2x4GB, DDR4, 2666MHz </Feature>" +
                  " </Features>" +
                   "<Id>" + id + "</Id>" +
                   "<LaptopName> Alienware M17 </LaptopName>" +
                  "</Laptop>";
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>()
            {
                { "Accept", "application/xml" },
                {"Content-Type", "application/xml" }
            };
            IRestResponse<Laptop> restResponse = RestRequestHelper.PerformPostRequest<Laptop>(postUrl, requestHeaders, xmlData, DataFormat.Xml);

           // if (restResponse.IsSuccessful)
            {
                Console.WriteLine((int)restResponse.StatusCode);
                Console.WriteLine(restResponse.Content);
                Assert.AreEqual(200, (int)restResponse.StatusCode);
                Assert.AreEqual("AlienWare", restResponse.Data.BrandName);

            }
        }
        [TestMethod]
        public void PostEndPointusingXmDataAndModel()
        {
            
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>()
            {
                { "Accept", "application/xml" },
                {"Content-Type", "application/xml" }
            };
            IRestResponse<Laptop> restResponse = RestRequestHelper.PerformPostRequest<Laptop>(postUrl, requestHeaders, GetLaptopObject(), DataFormat.Xml);

            // if (restResponse.IsSuccessful)
            {
                Console.WriteLine((int)restResponse.StatusCode);
                Console.WriteLine(restResponse.Content);
                Assert.AreEqual(200, (int)restResponse.StatusCode);
                Assert.AreEqual("sample BrandName", restResponse.Data.BrandName);

            }
        }
    }
}