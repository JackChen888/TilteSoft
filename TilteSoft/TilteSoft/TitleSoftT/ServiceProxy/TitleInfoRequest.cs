using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitleSoftT.ServiceProxy
{
    public class TitleInfoRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int? ID { get; set; }

        public int? IsHandle { get; set; }

        public int? IsOccupy { get; set; }

        public string UserNo { get; set; }

        public string UserName { get; set; }

        public string TitleName { get; set; }

        public int? IsStep { get; set; }
    }
}
