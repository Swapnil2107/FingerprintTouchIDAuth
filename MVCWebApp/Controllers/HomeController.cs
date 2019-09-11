using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWebApp.Controllers
{
	public class HomeController : Controller
	{
		MVCAuthEntities _dbcontext;
		public HomeController()
		{
			_dbcontext = new MVCAuthEntities();
		}
		public ActionResult Index()
		{

			var userDevices = (from ud in this._dbcontext.UserDevices
							   join u in this._dbcontext.AspNetUsers on ud.UserID equals u.Id
							   where ud.DeleteFlag == false
								   && ud.IsActive == true
							   select ud).ToList();

			return View(userDevices);
		}

		public ActionResult Delete(Guid id)
		{

			var deletedevice = _dbcontext.UserDevices.FirstOrDefault(i => i.UserDeviceID == id);
			_dbcontext.UserDevices.Remove(deletedevice);
			_dbcontext.SaveChanges();

			ViewBag.Message = "Your application description page.";

			return View("index");
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}