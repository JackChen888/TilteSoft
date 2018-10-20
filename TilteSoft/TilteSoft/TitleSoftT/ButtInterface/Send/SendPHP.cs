using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitleSoftT.ButtInterface.Utility;
using TitleSoftT.ServiceProxy;

namespace TitleSoftT.ButtInterface.Send
{
    public  class SendPHP
    {

        public static bool GetSend(string url)
        {
            //string[] arrTemp = "abc,abcd,abcdef,abcdefg,abcdefgh".Split(',');

            string str = HttpHelp.SendGet(url);
            var model = JsonToModel.FromJSON<TitleInfoResponse>(str);
            if (model.status == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool PostSend(string url, string ids)
        {

            string str = HttpHelp.SendPost(url, ids);
            var model = JsonToModel.FromJSON<UpDataResponse>(str);
            if (model.status == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }







    }
}
