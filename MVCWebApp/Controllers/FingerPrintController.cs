using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWebApp.Controllers
{
    public class FingerPrintController : Controller
    {

		MVCAuthEntities _base;
        // GET: FingerPrint
        public ActionResult Index()
        {
            return View();
        }
		[AllowAnonymous]
		[HttpGet]
		public JsonResult ValidateDeviceID(string deviceID)
		{
			_base = new MVCAuthEntities();
			var userDevice = _base.UserDevices.Where(u => u.DeviceID == deviceID && u.IsActive == true && u.DeleteFlag == false).FirstOrDefault();
			if (userDevice == null)
			{
				return Json(new { IsEnabledUser = "False", UserName = string.Empty }, "application/json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
			}

			return Json(new { IsEnabledUser = "True", UserName = userDevice.AspNetUser.UserName }, "application/json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult AddUpdateUserDevice(Guid? userDeviceID, string deviceID = null, string deviceName = null)
		{
			try
			{
				//int x = 5;
				//x = x / 0;
				_base = new MVCAuthEntities();
				UserDevice userDevice = null;
				if (userDeviceID.HasValue)
				{
					//Remove Active Device
					userDevice = _base.UserDevices.Where(i => i.UserDeviceID == userDeviceID).FirstOrDefault();
					userDevice.IsActive = false;
					_base.SaveChanges();
					return Json(new { UserDeviceID = userDevice.UserDeviceID, DeviceID = userDevice.DeviceID, DeviceName = userDevice.DeviceName });
				}

				userDevice = _base.UserDevices.Where(i => i.DeviceID == deviceID && i.DeviceName == deviceName && i.DeleteFlag == false && i.IsActive == false).FirstOrDefault();
				//Update Record to Active
				if (userDevice != null)
				{
					userDevice.IsActive = true;
					_base.SaveChanges();
					return Json(new { UserDeviceID = userDevice.UserDeviceID, DeviceID = userDevice.DeviceID, DeviceName = userDevice.DeviceName });
				}

				//Insert New Record
				userDevice = new UserDevice()
				{
					UserDeviceID =  Guid.NewGuid(),
					UserID = User.Identity.GetUserId(),
					DeviceID = deviceID,
					DeviceName = deviceName,
					IsActive = true,
					DeleteFlag = false
				};
				_base.UserDevices.Add(userDevice);
				_base.SaveChanges();
				return Json(new { UserDeviceID = userDevice.UserDeviceID, DeviceID = userDevice.DeviceID, DeviceName = userDevice.DeviceName });
			}
			catch (Exception ex)
			{
				//LogUtil.LogExceptionMessage("InsertUserDevice", ex, LogInfo.GetCurrent());
				throw ex;
			}
		}
	}
}