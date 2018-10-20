using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitleSoftT.ServiceProxy
{
    public class SavaPicRequest
    {
        public int UserInfoID { get; set; }

        public string BaiDuUrl { get; set; }

        public int IsEnable { get; set; }

        public List<string> BaiDuUrlList { get; set; } 

        public int TitleInfoID { get; set; }
    }
}
