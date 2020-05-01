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

namespace RestSharpProject.DeleteEndPoints
{
    [TestClass]
    public class DeleteEndPoints
    {
        private string DeleteUrl = "http://localhost:8080/laptop-bag/webapi/api/delete/";
        private string getUrl = "http://localhost:8080/laptop-bag/webapi/api/find/";
        private string jsonMediaType = "application/json";
        private string xmlMediaType = "application/xml";
        private RestResponse restResponse;
        private RestResponse restResponseForGet;
        [TestMethod]
        public void DeleteEndPointTestusingDeleteAsync()
        {
            int id = 965;

            using (HttpClient httpclient = new HttpClient())
            {

                Task<HttpResponseMessage> httpResponse = httpclient.DeleteAsync(DeleteUrl + id);
                using (HttpResponseMessage httpResponseMessage = httpResponse.Result)
                {
                    restResponse = new RestResponse((int)httpResponseMessage.StatusCode, httpResponseMessage.Content.ReadAsStringAsync().Result);
                    Console.WriteLine(restResponse.ToString());
                    Assert.AreEqual(200, restResponse.StatusCode);

                }

            }
        }
        [TestMethod]
        public void DeleteEndPointTestusingClientHelper()
        {
            int id = 343;
            RestResponse restResponse = HttpClientHelper.PerformDeleteRequest(DeleteUrl + id, null);
            Assert.AreEqual(200, restResponse.StatusCode);
            RestResponse restResponseForGet = HttpClientHelper.PerformGetRequest(getUrl + id, null);
            Assert.AreEqual(404, restResponseForGet.StatusCode);
            

        }

    }
}
