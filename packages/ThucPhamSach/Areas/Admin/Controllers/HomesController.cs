using System.Linq;
using System.Web.Mvc;
using ThucPhamSach.Models;

namespace ThucPhamSach.Areas.Admin.Controllers
{
    public class HomesController : Controller
    {
        private thucphamsachDB db = new thucphamsachDB();
        // GET: Admin/Admin
        public ActionResult Index()
        {
            if (Session["idUser"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("DangNhap", "Homes");
            }
        }
        public ActionResult DangNhap()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(string email, string matkhau)
        {
            if (ModelState.IsValid)
            {
                var data = db.TaiKhoans.Where(s => s.Email == email && s.MatKhau == matkhau && s.MaQuyen == 1).ToList();
                if (data.Count() > 0)
                {
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["HoTen"] = data.FirstOrDefault().HoTen;
                    Session["idUser"] = data.FirstOrDefault().MaTK;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Đăng Nhập Thất Bại";
                    return View();
                }
            }
            return View();
        }

        public ActionResult DangXuat()
        {
            Session.Clear();//remove session
            return RedirectToAction("DangNhap", "Homes");
        }
    }
}