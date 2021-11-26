using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThucPhamSach.Areas.Admin.Controllers
{
    public class UserManagerController : Controller
    {
        // GET: Admin/UserManager
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
    }
}