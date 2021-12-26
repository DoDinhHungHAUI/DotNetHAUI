using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ThucPhamSach.Infrastructure.Core;
using ThucPhamSach.Models;

namespace ThucPhamSach.Controllers
{
    public class HomeController : Controller
    {
        thucphamsachDB db = new thucphamsachDB();

        public static string CartSession = "cartSesstion";

        public ActionResult Index()
        {

            var sanpham = db.SanPhams.OrderBy(sp => sp.TenSanPham).Take(4).ToList();

            return View(sanpham);
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
                var data = db.TaiKhoans.Where(s => s.Email == email && s.MatKhau == matkhau).ToList();
                if (data.Count() > 0)
                {
                    //add session

                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["HoTen"] = data.FirstOrDefault().HoTen;
                    Session["MatKhau"] = data.FirstOrDefault().MatKhau;
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


        public ActionResult DangKy()
        {
            return View();
        }


        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(TaiKhoan _user)
        {
            if (ModelState.IsValid)
            {
                var check = db.TaiKhoans.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.MaQuyen = 1;
                    var MaTK = db.TaiKhoans.OrderByDescending(x => x.MaTK).First().MaTK;
                    _user.MaTK = MaTK + 1;


                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.TaiKhoans.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("DangNhap");
                }
                else
                {
                    ViewBag.error = "Email đã tồn tại";
                    return View();
                }
            }
            return View();
        }

        public ActionResult SuaThongTinTaiKhoan(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.SingleOrDefault(m => m.MaTK == id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaThongTinTaiKhoan([Bind(Include = "MaTK,HoTen,Ten,Email,DienThoai,DiaChi")] TaiKhoan taiKhoan)
        {
            //if (ModelState.IsValid)
            //{
            //    taiKhoan.MaTK = 5;
            //    taiKhoan.MaQuyen = 1;
            //    //taiKhoan.ConfirmPassword = taiKhoan.MatKhau;

            //    db.Entry(taiKhoan).State = EntityState.Modified;
            //    db.Configuration.ValidateOnSaveEnabled = false;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //taiKhoan.MaTK = 5;
            taiKhoan.MaQuyen = 0;
            //taiKhoan.ConfirmPassword = taiKhoan.MatKhau;

            var taiKhoanSave = db.TaiKhoans.Where(t => t.MaTK == taiKhoan.MaTK).FirstOrDefault();
            taiKhoanSave.HoTen = taiKhoan.HoTen;
            taiKhoanSave.Ten = taiKhoan.Ten;
            taiKhoanSave.Email = taiKhoan.Email;
            taiKhoanSave.DienThoai = taiKhoan.DienThoai;
            taiKhoanSave.DiaChi = taiKhoan.DiaChi;
            db.Entry(taiKhoanSave).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            return RedirectToAction("Index");

            //return View(taiKhoan);
        }
        public ActionResult SuaMatKhau(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.SingleOrDefault(m => m.MaTK == id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaMatKhau([Bind(Include = "MaTK,ConfirmPassword")] TaiKhoan taiKhoan)
        {
            var taiKhoanSave = db.TaiKhoans.Where(t => t.MaTK == taiKhoan.MaTK).FirstOrDefault();

            taiKhoanSave.MatKhau = taiKhoan.ConfirmPassword;
            db.Entry(taiKhoanSave).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult ChiTiet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.SingleOrDefault(m => m.MaTK == id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        public PartialViewResult Header()
        {
            var danhmucs = db.DanhMucSanPhams.ToList();
            return PartialView(danhmucs);
        }

        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return PartialView(list);
        }

        public ActionResult ChangePasswordUser()
        {
            return View();
        }

        public ActionResult DangXuat()
        {
            Session.Clear();//remove session
            return RedirectToAction("DangNhap", "Home");
        }
    }
}