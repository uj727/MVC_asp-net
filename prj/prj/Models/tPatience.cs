using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prj.Models
{
	public class tPatience
	{
        [DisplayName("看診編號")]
        [Required(ErrorMessage = "不可空白")]
        public string 看診編號 { get; set; }

        [DisplayName("身分證字號")]
        [Required(ErrorMessage = "不可空白")]
        [StringLength(10, ErrorMessage = "為10個字元")]
        public string 身分證字號 { get; set; }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "不可空白")]
        public string 姓名 { get; set; }

        [DisplayName("看診原因")]
        [Required(ErrorMessage = "不可空白")]
        public string 看診原因 { get; set; }

        [DisplayName("看診日期")]
        [DataType(DataType.Date, ErrorMessage = "必須為日期格式")]
        public Nullable<System.DateTime> 看診日期 { get; set; }

        [DisplayName("負責醫生編號")]
        [Range(1,3, ErrorMessage = "必須介於1~3")]
        public Nullable<int> 負責醫生編號 { get; set; }

    }
}