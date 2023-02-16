using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prj.Models
{
	public class CVMDepEmp
	{
		public List<TableDoctors1081649> doctor { get; set; }
		public List<TablePatients1081649> patient { get; set; }
		public List<TableToors1081649> toor { get; set; }
		public List<TableToorman1081649> toorman { get; set; }
	}
}