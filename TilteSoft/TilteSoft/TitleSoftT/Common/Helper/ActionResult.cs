using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitleSoftT.Common.Helper
{
    public sealed class ActionResult<TResult> : ActionResult
          where TResult : class
    {
        public new TResult Result
        {
            get;
            set;
        }
    }

    public class ActionResult
    {
        public bool Result { get; set; }

        public bool HasError
        {
            get
            {
                return !string.IsNullOrWhiteSpace(ErrorMessage);
            }
        }

        public string ErrorMessage
        {
            get;
            set;
        }

        public ActionResult(string errorMessage = null)
        {
            ErrorMessage = errorMessage;
        }
    }
}
