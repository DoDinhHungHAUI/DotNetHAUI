using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThucPhamSach.Areas.Admin.Controllers
{
    public class OrderManagerController : Controller
    {
        // GET: Admin/OrderManager
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail()
        {
            return View();
        }
    }
}