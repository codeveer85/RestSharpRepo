using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Authenticators;
using RestSharpAutomation.Helpers.Request;
using RestSharpProject.Model.JsonModel;
using RestSharpProject.Model.XmlModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.RestGetEndPoint
{[TestClass]
    public class TestGetEndPoint
    {
        private string getUrl = "http://localhost:8080/laptop-bag/webapi/api/all";
        private string secureUrl = "http://localhost:8080/laptop-bag/webapi/secure/all";
        [TestMethod]
        public void TestGetUsingRestSharp()
        {
            IRestClient restClient = new RestClient(); //Creating The Rest Client.
            IRestRequest restRequest = new RestRequest(getUrl); // Create the Request.
            restRequest.AddHeader("Accept", "application/xml");
            IRestResponse restResponse= restClient.Get(restRequest); // Send the request using RestClient.
            Console.WriteLine((int)restResponse.StatusCode);
            Console.WriteLine(restResponse.Content);
        }
        [TestMethod]
        public void TestGetUsingRestSharpXmlDeserialization()
        {
            IRestClient restClient = new RestClient(); 
            IRestRequest restRequest = new RestRequest(getUrl); 
            restRequest.AddHeader("Accept", "application/xml");
            var dotNetXmlSerializer = new RestSharp.Deserializers.DotNetXmlDeserializer(); //XML Deserialization Step:1
            IRestResponse restResponse = restClient.Get(restRequest); // 
            if (restResponse.IsSuccessful)
            {
                Console.WriteLine((int)restResponse.StatusCode);
                Assert.AreEqual(200, (int)restResponse.StatusCode);
                LaptopDetailss data = dotNetXmlSerializer.Deserialize<LaptopDetailss>(restResponse);//XML Deserialization Step:2
                Assert.AreEqual("Alienware M17", data.Laptop[1].LaptopName);
            }

        }
        [TestMethod]
        public void TestGetUsingRestSharpJsonDeserialization()
        {
            IRestClient restClient = new RestClient(); 
            IRestRequest restRequest = new RestRequest(getUrl); 
            restRequest.AddHeader("Accept", "application/json");
            IRestResponse<List<JsonRootObject>> restResponse = restClient.Get<List<JsonRootObject>>(restRequest); 
            if (restResponse.IsSuccessful)
            {
                Console.WriteLine((int)restResponse.StatusCode);
                Assert.AreEqual(200, (int)restResponse.StatusCode);
                List<JsonRootObject> data = restResponse.Data;
                JsonRootObject laptop = data.Find((x) =>
                {
                    return x.Id == 761;  //passing Anonymous function to Find method of List class.
                });
                Assert.AreEqual("Alienware M17", laptop.LaptopName);
            }

        }
         [TestMethod]
        public void TestGetUsingExecute()
        {
            IRestClient restClient = new RestClient(); 
            IRestRequest restRequest = new RestRequest(getUrl); 
            restRequest.AddHeader("Accept", "application/json");
            restRequest.Method = Method.GET;
            IRestResponse<List<JsonRootObject>> restResponse = restClient.Execute<List<JsonRootObject>>(restRequest);
            
            if (restResponse.IsSuccessful)
            {
                Console.WriteLine((int)restResponse.StatusCode);
                Assert.AreEqual(200, (int)restResponse.StatusCode);
                List<JsonRootObject> data = restResponse.Data;
                JsonRootObject laptop = data.Find((x) =>
                {
                    return x.Id == 761; 
                });
                Assert.AreEqual("Alienwaree M17", laptop.LaptopName);
            }

        }
        [TestMethod]
        public void TestGetUsingRestRequestHelper()
        {
            Dictionary<string, string> httpHeaders = new Dictionary<string, string>();
            httpHeaders.Add("Accept", "application/json");
            IRestResponse restResponse= RestRequestHelper.PerformGetRequest(getUrl, httpHeaders);
           
            if (restResponse.IsSuccessful)
            {
                Console.WriteLine((int)restResponse.StatusCode);
                Console.WriteLine(restResponse.Content);
                Assert.AreEqual(200, (int)restResponse.StatusCode);
            }

        }
        [TestMethod]
        public void TestGetUsingRestRequestHelperDeserialiZerJsonData()
        {
            Dictionary<string, string> httpHeaders = new Dictionary<string, string>();
            httpHeaders.Add("Accept", "application/json");  //Deserializing JSON data
            
            IRestResponse<List<JsonRootObject>> restResponse = RestRequestHelper.PerformGetRequest<List<JsonRootObject>>(getUrl, httpHeaders);

            if (restResponse.IsSuccessful)
            {
                Console.WriteLine((int)restResponse.StatusCode);
                Console.WriteLine(restResponse.Content);
                Assert.AreEqual(200, (int)restResponse.StatusCode);
                List<JsonRootObject> data = restResponse.Data;
                JsonRootObject laptop = data.Find((x) =>
                {
                    return x.Id == 761;
                });
                Assert.AreEqual("Alienware M17", laptop.LaptopName);
            }

        }
        [TestMethod]
        public void TestGetUsingRestRequestHelperDeserialiZerXmlnData()
        {
            Dictionary<string, string> httpHeaders = new Dictionary<string, string>();
            
            httpHeaders.Add("Accept", "application/xml");   
            IRestResponse<LaptopDetailss> restResponse = RestRequestHelper.PerformGetRequest<LaptopDetailss>(getUrl, httpHeaders);

            if (restResponse.IsSuccessful)
            {
                Console.WriteLine((int)restResponse.StatusCode);
                Console.WriteLine(restResponse.Content);
                Assert.AreEqual(200, (int)restResponse.StatusCode);
                LaptopDetailss data = restResponse.Data;
                
                Assert.AreEqual("Alienware M17", data.Laptop[2].LaptopName);
            }

        }
        
        [TestMethod]
        public void TestGetUsingRestSharp_Authentication()
        {
            IRestClient restClient = new RestClient(); //Creating The Rest Client.
            restClient.Authenticator = new HttpBasicAuthenticator("admin", "welcome");
            IRestRequest restRequest = new RestRequest(secureUrl); // Create the Request.
            restRequest.AddHeader("Accept", "application/xml");
            IRestResponse restResponse = restClient.Get(restRequest); // Send the request using RestClient.
            Assert.AreEqual(200, (int)restResponse.StatusCode);
            Console.WriteLine(restResponse.Content);
        }
    }
}
