using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpProject.Helper.Authentication
{
    public class Base64AuthenticationHelper
    {
        public static string GetBase64String(string username, string password)
        {
            string auth = username + ":" + password;
            Byte[] inArray = System.Text.Encoding.UTF8.GetBytes(auth);

            return System.Convert.ToBase64String(inArray);
        }
    }
}
