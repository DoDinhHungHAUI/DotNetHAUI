using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ThucPhamSach.Areas.Admin.Assets.Enum;
using ThucPhamSach.Models;

namespace ThucPhamSach.Areas.Admin.Controllers
{
    public class SanPhamsController : Controller
    {
        private thucphamsachDB db = new thucphamsachDB();

        // GET: Admin/SanPhams
        public ActionResult Index()
        {
            var pageSize = Convert.ToInt32(ThucPhamSachEnum.PageSize);
            var currentPage = 1;
            var sanPhams = db.SanPhams.Include(s => s.DanhMucSanPham).OrderBy(sp => sp.TenSanPham).Skip(currentPage - 1).Take(pageSize);

            var totalRecord = db.SanPhams.Count();
            double pageCount = (double)((decimal)totalRecord / Convert.ToDecimal(ThucPhamSachEnum.PageSize));
            ViewBag.PageCount = (int)Math.Ceiling(pageCount);
       
            return View(sanPhams.ToList());
        }

        // GET: Admin/SanPhams/Details/5
        public ActionResult ChiTiet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        [HttpPost]
        public ActionResult SearchPaging(string txtSearch , int currentPage = 1)
        {
            var pageSize = Convert.ToInt32(ThucPhamSachEnum.PageSize);
            if (txtSearch != "")
            {
                var listSearchPaging = db.SanPhams.Where(sp => sp.TenSanPham.ToUpper().Contains(txtSearch.ToUpper())).OrderBy(sp => sp.TenSanPham).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                var totalRecord = db.SanPhams.Where(sp => sp.TenSanPham.ToUpper().Contains(txtSearch.ToUpper())).Count();

                int pageCount = (int)Math.Ceiling((double)((decimal)totalRecord / Convert.ToDecimal(pageSize)));

                var data = JsonConvert.SerializeObject(new { pageCount , listSearchPaging },
                    Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    });

                return Content(data, "application/json");
            }
            else
            {
                var listSearchPaging = db.SanPhams.OrderBy(sp => sp.TenSanPham).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                var totalRecord = db.SanPhams.Count();

                int pageCount = (int)Math.Ceiling((double)((decimal)totalRecord / Convert.ToDecimal(pageSize)));

                var data = JsonConvert.SerializeObject(new { pageCount, listSearchPaging },
                    Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    });

                return Content(data, "application/json");
            }
        }

        // GET: Admin/SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucSanPhams, "MaDanhMuc", "TenDanhMuc");
            return View();
        }

        // POST: Admin/SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSanPham,MoTa,NhaCungCap,GiaBan,Anh,MaDanhMuc,SoLuong")] SanPham sanPham)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var f = Request.Files["ImageFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string UploadPath = Server.MapPath("~/wwwroot/Images/" + FileName);
                        f.SaveAs(UploadPath);
                        sanPham.Anh = FileName;
                    }

                    db.SanPhams.Add(sanPham);
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
                //ViewBag.MaDanhMuc = new SelectList(db.DanhMucSanPhams, "MaDanhMuc", "TenDanhMuc", sanPham.MaDanhMuc);
                //return View(sanPham);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi Sửa dữ liệu !" + ex.Message;
                return View(sanPham);

            }
        }

        // GET: Admin/SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucSanPhams, "MaDanhMuc", "TenDanhMuc", sanPham.MaDanhMuc);
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSanPham,MoTa,NhaCungCap,GiaBan,Anh,MaDanhMuc,SoLuong")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                var f = Request.Files["ImageFile"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Server.MapPath("~/wwwroot/Images/" + FileName);
                    f.SaveAs(UploadPath);
                    sanPham.Anh = FileName;
                }

                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucSanPhams, "MaDanhMuc", "TenDanhMuc", sanPham.MaDanhMuc);
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SanPham sanPham = db.SanPhams.Find(id);
        //    if (sanPham == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sanPham);
        //}

        // POST: Admin/SanPhams/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            var listSearchPaging = db.SanPhams.ToList();

            var totalRecord = db.SanPhams.Count();
            int pageCount = (int)Math.Ceiling((double)((decimal)totalRecord / Convert.ToDecimal(ThucPhamSachEnum.PageSize)));

            var data = JsonConvert.SerializeObject(new { pageCount , listSearchPaging },
            Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });

            return Content(data, "application/json");

            //return Json(new { data = listSp }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {   
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
