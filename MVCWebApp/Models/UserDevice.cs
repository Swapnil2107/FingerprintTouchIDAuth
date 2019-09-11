using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebApp.Models
{
	public class UserDevice
	{
		public System.Guid UserDeviceID { get; set; }
		public string UserID { get; set; }
		public string DeviceID { get; set; }
		public string DeviceName { get; set; }
		public bool IsActive { get; set; }
		public Nullable<bool> DeleteFlag { get; set; }
		public Nullable<short> RowVer { get; set; }
		public Nullable<System.Guid> CreatedBy { get; set; }
		public Nullable<System.DateTime> CreatedOn { get; set; }
		public Nullable<System.Guid> ModifiedBy { get; set; }
		public Nullable<System.DateTime> ModifiedOn { get; set; }
	}
}