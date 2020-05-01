using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.DropBoxApi.ListFolderModel
{
    public class Property_groups
    {
        public string template_id { get; set; }
        public IList<Fields> fields { get; set; }

    }
}
