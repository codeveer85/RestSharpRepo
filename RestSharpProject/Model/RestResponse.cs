using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpProject.Model
{
    public class RestResponse
    {
        public int StatusCode{get; }
        public string ResponseData { get; }
        public RestResponse(int statuscode, string responseData)
        {
            StatusCode = statuscode;
            ResponseData = responseData;

        }
        public override string ToString()
        {
            return string.Format("StatusCode: {0} ResponseData: {1}", StatusCode, ResponseData);
        }

    }
}
