using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpAutomation.Helpers.Request;
using RestSharpProject.Model.XmlModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.DeleteEndPoint
{
    [TestClass]
   public class DeleteEndPoint
    {
        private Random random = new Random();
        private string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add";
        private string deleteUrl = "http://localhost:8080/laptop-bag/webapi/api/delete/";
        private string getUrl = "http://localhost:8080/laptop-bag/webapi/api/find/";
        [TestMethod]
        public void TestDeleteEndPoint()
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
            
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>()
            {
                { "Accept", "application/json" },
                {"Content-Type", "application/json" }
            };
            IRestResponse<Laptop> postResponse = RestRequestHelper.PerformPostRequest<Laptop>(postUrl, requestHeaders, jsonData, DataFormat.Json);

            IRestResponse<Laptop> getResponseBeforeDelete = RestRequestHelper.PerformGetRequest<Laptop>(getUrl + id, requestHeaders);
            Console.WriteLine("Brandname =>" + getResponseBeforeDelete.Data.BrandName);
            requestHeaders["Accept"]= "*/*"; //This Header is needed to receive the response from Delete request.
            IRestResponse deleteResponse = RestRequestHelper.PerformDeleteRequest(deleteUrl+id, requestHeaders);
            Assert.AreEqual(200, (int)deleteResponse.StatusCode);
             deleteResponse = RestRequestHelper.PerformDeleteRequest(deleteUrl + id, requestHeaders);
            Assert.AreEqual(404, (int)deleteResponse.StatusCode);

        }
    }
}
