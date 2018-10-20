using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitleSoftT.Models;

namespace TitleSoftT.ServiceProxy
{
    public class BaiDuInfoResponse
    {

        public int ID
        {
            get; set;
        }

        public int OldID
        {
            get; set;
        }

        public string OldTitle
        {
            get; set;
        }

        public string NewTitle
        {
            get; set;
        }

        public string UpTime
        {
            get; set;
        }

        public int? IsHandle
        {
            get; set;
        }

        public int? IsAdd
        {
            get; set;
        }

        public string ModificationTime
        {
            get; set;
        }

        public List<PictureControlModel> PicModelList
        {
            get; set;
        }

        public string UserName { get; set; }

        public string UserNo { get; set; }

    }




}
