using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RestSharpProject.Helper.Response
{
    public class ResponseDataHelper
    {
        public static T DeserializeJsonResponse<T>(string responseData) where T: class
        {
            return JsonConvert.DeserializeObject<T>(responseData);
        }

        public static T DeserializeXmlResponse<T>(string responseData) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            TextReader textReader = new StringReader(responseData);
            return (T)serializer.Deserialize(textReader);
        }

    }
}
