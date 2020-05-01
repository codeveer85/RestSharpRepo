using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpAutomation.Helpers.Request;
using RestSharpProject.Model.XmlModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.PutEndPoints
{
    [TestClass]
     public class TestPutEndPoints
    {
        private Random random= new Random();
        private string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add";
        private string putUrl = "http://localhost:8080/laptop-bag/webapi/api/update";
        private string getUrl = "http://localhost:8080/laptop-bag/webapi/api/find/";
        [TestMethod]
        public void PuttEndPointsWithJsonData()
        {
            int id = random.Next(100);
            string jsonData = "{" +
        "\"BrandName\": \"AnimalWare\"," +
        "\"Features\": {" +
                "\"Feature\": [" +
                    "\"8th Generation Intel® Core™ i5-8300H\"," +
                    "\"Windows 10 Home 64-bit English\"," +
                    "\"NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6\"," +
                    "\"8GB, 2x4GB, DDR4, 2666MHz\"" +
            "]" +
    "}," +
        "\"Id\":" + id + "," +
        "\"LaptopName\": \"LionWare M17\"" +
    "}"; 
            string jsonDataForPut = "{" +
         "\"BrandName\": \"LionWare\"," +
         "\"Features\": {" +
                 "\"Feature\": [" +
                     "\"8th Generation Intel® Core™ i5-8300H\"," +
                     "\"Windows 10 Home 64-bit English\"," +
                     "\"NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6\"," +
                     "\"8GB, 2x4GB, DDR4, 2666MHz\"" +
             "]" +
     "}," +
         "\"Id\":" + id + "," +
         "\"LaptopName\": \"AlienWare M17\"" +
     "}";
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>()
            {
                { "Accept", "application/json" },
                {"Content-Type", "application/json" }
            };
            IRestResponse<Laptop> postResponse = RestRequestHelper.PerformPostRequest<Laptop>(postUrl, requestHeaders, jsonData, DataFormat.Json);
           
                IRestResponse<Laptop> getResponseBeforePut = RestRequestHelper.PerformGetRequest<Laptop>(getUrl + postResponse.Data.Id, requestHeaders);
                Console.WriteLine("Brandname =>" + getResponseBeforePut.Data.BrandName);
                IRestResponse<Laptop> putResponse = RestRequestHelper.PerformPutRequest<Laptop>(putUrl, requestHeaders, jsonDataForPut, DataFormat.Json);
                Assert.AreEqual("LionWare", putResponse.Data.BrandName);
                IRestResponse<Laptop> getResponseafterPut = RestRequestHelper.PerformGetRequest<Laptop>(getUrl + postResponse.Data.Id, requestHeaders);
                Console.WriteLine("LaptopName =>" + getResponseBeforePut.Data.LaptopName);
            
        }

       
    }
}
