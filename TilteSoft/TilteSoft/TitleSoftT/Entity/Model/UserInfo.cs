//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace TitleSoftT.Entity.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserInfo
    {
        public int ID { get; set; }
        public int TitleInfoID { get; set; }
        public string UserName { get; set; }
        public string UserNo { get; set; }
        public Nullable<int> IsEnable { get; set; }
        public Nullable<int> IsStep { get; set; }
    
        public virtual TitleInfo TitleInfo { get; set; }
    }
}
