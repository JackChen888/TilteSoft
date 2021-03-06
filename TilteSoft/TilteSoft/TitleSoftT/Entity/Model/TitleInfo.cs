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
    
    public partial class TitleInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TitleInfo()
        {
            this.BaiDuInfo = new HashSet<BaiDuInfo>();
            this.UserInfo = new HashSet<UserInfo>();
        }
    
        public int ID { get; set; }
        public int OldID { get; set; }
        public string OldTitle { get; set; }
        public string NewTitle { get; set; }
        public string UpTime { get; set; }
        public Nullable<int> IsHandle { get; set; }
        public Nullable<int> IsAdd { get; set; }
        public string ModificationTime { get; set; }
        public Nullable<int> IsOccupy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiDuInfo> BaiDuInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserInfo> UserInfo { get; set; }
    }
}
