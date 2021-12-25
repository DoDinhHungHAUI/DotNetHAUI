using System.Linq;
using System.Web.Mvc;
using ThucPhamSach.Models;

namespace ThucPhamSach.Controllers
{
    public class HomeController : Controller
    {
        thucphamsachDB db = new thucphamsachDB();

        public ActionResult Index()
        {

            var sanpham = db.SanPhams.OrderBy(sp => sp.TenSanPham).Take(4).ToList();

            return View(sanpham);
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult ChangeInfoUser()
        {
            return View();
        }

        public PartialViewResult Header()
        {
            var danhmucs = db.DanhMucSanPhams.ToList();
            return PartialView(danhmucs);
        }
     
        public ActionResult ChangePasswordUser()
        {
            return View();
        }


    }
}