using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.DropBoxApi.ListFolderModel
{
    public class Entry
    { public string Invalid_Name_tag { get; set; }
    public string name { get; set; }
    public string id { get; set; }
    public DateTime client_modified { get; set; }
    public DateTime server_modified { get; set; }
    public string rev { get; set; }
    public int size { get; set; }
    public string path_lower { get; set; }
    public string path_display { get; set; }
    public Sharing_info sharing_info { get; set; }
    public bool is_downloadable { get; set; }
    public IList<Property_groups> property_groups { get; set; }
    public bool has_explicit_shared_members { get; set; }
    public string content_hash { get; set; }
    public File_lock_info file_lock_info { get; set; }
}
}
