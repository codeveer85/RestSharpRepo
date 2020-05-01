using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.DropBoxApi.ListFolderModel
{
   public class Sharing_info
    {
        public bool read_only { get; set; }
        public string parent_shared_folder_id { get; set; }
        public string modified_by { get; set; }
    }
}
