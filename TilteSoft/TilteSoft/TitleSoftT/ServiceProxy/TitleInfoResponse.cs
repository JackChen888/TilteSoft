using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitleSoftT.Models;

namespace TitleSoftT.ServiceProxy
{
    public class TitleInfoResponse
    {
        public int status { get; set; }

        public string msg { get; set; }

        public List<TitleInfoModel> data { get; set; }
    }



    public class TitleInfoModel
    {
        public string search_id { get; set; }

        public string search_txt { set; get; }

    }
    public class TitleInfoListResponse
    {
        public int RowCount { get; set; }

        public int PageSize { get; set; }

        public MainWindowModel[] mainWindowModels { get; set; }

        public int PageMax { get; set; }

    }

}
