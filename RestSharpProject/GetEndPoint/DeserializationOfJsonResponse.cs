using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
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

namespace RestSharpProject.GetEndPoint
{
   [TestClass]
   public class DeserializationOfJsonResponse
    {
        [TestMethod]
       public void DeserializationOfJsonResponseTest()
        {
            Uri getUri = new Uri("http://localhost:8080/laptop-bag/webapi/api/all");
            using (HttpClient httpclient = new HttpClient())
            {
                using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
                {
                    httpRequestMessage.Method = HttpMethod.Get;
                    httpRequestMessage.RequestUri = getUri;
                    httpRequestMessage.Headers.Add("accept", "application/json");

                    Task<HttpResponseMessage> httpResponse = httpclient.SendAsync(httpRequestMessage); 
                    using (HttpResponseMessage httpResponseMessage = httpResponse.Result)
                    {
                       
                        RestResponse restResponse = new RestResponse((int)httpResponseMessage.StatusCode, httpResponseMessage.Content.ReadAsStringAsync().Result);
                        //Console.WriteLine(restResponse.ToString());
                        List<JsonRootObject> jsonRootObject= JsonConvert.DeserializeObject<List<JsonRootObject>>(httpResponseMessage.Content.ReadAsStringAsync().Result);
                        Assert.AreEqual("Alienware", jsonRootObject[0].BrandName);
                        Console.WriteLine("Brandname=> {0}", jsonRootObject[0].BrandName);
                   }
                }




            }

        }
        [TestMethod]
        public void DeserializationOfXmlResponseTest()
        {
            Uri getUri = new Uri("http://localhost:8080/laptop-bag/webapi/api/all");
            using (HttpClient httpclient = new HttpClient())
            {
                using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
                {
                    httpRequestMessage.Method = HttpMethod.Get;
                    httpRequestMessage.RequestUri = getUri;
                    httpRequestMessage.Headers.Add("accept", "application/xml");

                    Task<HttpResponseMessage> httpResponse = httpclient.SendAsync(httpRequestMessage);
                    using (HttpResponseMessage httpResponseMessage = httpResponse.Result)
                    {

                        RestResponse restResponse = new RestResponse((int)httpResponseMessage.StatusCode, httpResponseMessage.Content.ReadAsStringAsync().Result);
                        Console.WriteLine(restResponse.ToString());
                        XmlSerializer serializer = new XmlSerializer(typeof(LaptopDetailss));
                        TextReader textReader = new StringReader(restResponse.ResponseData);
                        LaptopDetailss xmlData =(LaptopDetailss)serializer.Deserialize(textReader);
                        // Console.WriteLine(xmlData.ToString());
                        Assert.AreEqual(200, restResponse.StatusCode);
                        Assert.AreEqual("Alienware", xmlData.Laptop[0].BrandName);                     

                    }
                }




            }

        }
    }
}
