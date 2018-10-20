using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitleSoftT.Entity.Model;

namespace TitleSoftT.Entity
{
    public abstract class MessageBase
    {
        protected Model.ResKey_dbEntities DbContext;

        protected MessageBase()
        {
            DbContext = new ResKey_dbEntities();
            //DbContext.Database.Log = message =>
            // {
            //     CurrentLogger.Info($"{TransactionId} {message}");
            // };
        }


    }
}
