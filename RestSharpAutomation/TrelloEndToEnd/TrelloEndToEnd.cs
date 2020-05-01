using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.TrelloEndToEnd
{
    [TestClass]
    public class TrelloEndToEndTesting
    {
        private const string key = "c9e3f41728315fd26e83d90056dca166";
        private string token = "cb4004f66e054f266e10311e7204deac8aa6d5f175fff873b22b5ad8a36647ea";
        [TestMethod]
        public void TrelloEndToEndTest()
        {
            IRestClient restClient = new RestClient();
        }
    }
}
