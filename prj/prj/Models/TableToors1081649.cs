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
    
    public partial class TableToors1081649
    {
        public int 用品編號 { get; set; }
        public string 用品 { get; set; }
        public Nullable<int> 供應商代號 { get; set; }
        public Nullable<int> 負責醫生編號 { get; set; }
        public string 單位數量 { get; set; }
        public Nullable<decimal> 價格 { get; set; }
        public Nullable<short> 庫存量 { get; set; }
        public Nullable<short> 已訂購量 { get; set; }
        public Nullable<short> 預計能使用天數 { get; set; }
        public bool 報廢 { get; set; }
    
        public virtual TableDoctors1081649 TableDoctors1081649 { get; set; }
    }
}