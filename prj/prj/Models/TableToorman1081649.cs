//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace prj.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TableToorman1081649
    {
        public string 員工編號 { get; set; }
        public string 姓名 { get; set; }
        public string 月考績分數 { get; set; }
        public string Mail { get; set; }
        public Nullable<int> 負責醫生編號 { get; set; }
        public Nullable<System.DateTime> 雇用日期 { get; set; }
    
        public virtual TableDoctors1081649 TableDoctors1081649 { get; set; }
    }
}
