using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpAutomation.DropBoxApi.ListFolderModel;
using RestSharpAutomation.Helpers.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation
{
    [TestClass]
    public class TestDropBoxApi
    {
        private const String EndPointUrl = "https://api.dropboxapi.com/2/files/list_folder";
        private const string accessToken = "T8Hz5VwFWMAAAAAAAAAADPztZ0-6SOO5WNDt-x3Qg8u8LyzE0CH38iwjacWuks4g";



        [TestMethod]
        public void FolderListTest()
        {
            string body = "{\"path\": \"\",\"recursive\": false,\"include_media_info\": false,\"include_deleted\": false,\"include_has_explicit_shared_members\": false,\"include_mounted_folders\": true,\"include_non_downloadable_files\": true}"
;
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest()
            {
                Resource = EndPointUrl
            };
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", "Bearer " + accessToken);
            restRequest.AddJsonBody(body);
            IRestResponse <RootObject> restResponse = restClient.Post<RootObject>(restRequest);
           
           Console.WriteLine("statusCode=>" + restResponse.StatusCode);
            Console.WriteLine(restResponse.Data.entries[0].id);
            Assert.AreEqual(200, (int)restResponse.StatusCode);
            
            
           
       
        }
    }
}
