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
    public class DanhMucSanPhamsController : Controller
    {
        private thucphamsachDB db = new thucphamsachDB();

        // GET: Admin/DanhMucSanPhams
        public ActionResult Index()
        {
            var pageSize = Convert.ToInt32(ThucPhamSachEnum.PageSize);
            var currentPage = 1;
            var dmSanPhams = db.DanhMucSanPhams.OrderBy(sp => sp.TenDanhMuc).Skip(currentPage - 1).Take(pageSize);
            var totalRecord = db.DanhMucSanPhams.Count();
            double pageCount = (double)((decimal)totalRecord / Convert.ToDecimal(ThucPhamSachEnum.PageSize));
            ViewBag.PageCount = (int)Math.Ceiling(pageCount);
            return View(dmSanPhams.ToList());
        }

        // GET: Admin/DanhMucSanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMucSanPham danhMucSanPham = db.DanhMucSanPhams.Find(id);
            if (danhMucSanPham == null)
            {
                return HttpNotFound();
            }
            return View(danhMucSanPham);
        }

        // GET: Admin/DanhMucSanPhams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DanhMucSanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDanhMuc,TenDanhMuc,MoTa")] DanhMucSanPham danhMucSanPham)
        {
            if (ModelState.IsValid)
            {
                db.DanhMucSanPhams.Add(danhMucSanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(danhMucSanPham);
        }

        // GET: Admin/DanhMucSanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMucSanPham danhMucSanPham = db.DanhMucSanPhams.Find(id);
            if (danhMucSanPham == null)
            {
                return HttpNotFound();
            }
            return View(danhMucSanPham);
        }

        [HttpPost]
        public ActionResult SearchPaging(string txtSearch, int currentPage = 1)
        {
            var pageSize = Convert.ToInt32(ThucPhamSachEnum.PageSize);
            if (txtSearch != "")
            {
                var listSearchPaging = db.DanhMucSanPhams.Where(sp => sp.TenDanhMuc.ToUpper().Contains(txtSearch.ToUpper())).OrderBy(sp => sp.TenDanhMuc).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                var totalRecord = db.DanhMucSanPhams.Where(sp => sp.TenDanhMuc.ToUpper().Contains(txtSearch.ToUpper())).Count();

                int pageCount = (int)Math.Ceiling((double)((decimal)totalRecord / Convert.ToDecimal(pageSize)));

                var data = JsonConvert.SerializeObject(new { pageCount, listSearchPaging },
                    Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    });

                return Content(data, "application/json");
            }
            else
            {
                var listSearchPaging = db.DanhMucSanPhams.OrderBy(sp => sp.TenDanhMuc).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                var totalRecord = db.DanhMucSanPhams.Count();

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

        // POST: Admin/DanhMucSanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDanhMuc,TenDanhMuc,MoTa")] DanhMucSanPham danhMucSanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danhMucSanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(danhMucSanPham);
        }

        // GET: Admin/DanhMucSanPhams/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DanhMucSanPham danhMucSanPham = db.DanhMucSanPhams.Find(id);
        //    if (danhMucSanPham == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(danhMucSanPham);
        //}

        // POST: Admin/DanhMucSanPhams/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            DanhMucSanPham danhMucSanPham = db.DanhMucSanPhams.Find(id);
            db.DanhMucSanPhams.Remove(danhMucSanPham);
            db.SaveChanges();
            var listSearchPaging = db.DanhMucSanPhams.ToList();
            var totalRecord = db.DanhMucSanPhams.Count();
            int pageCount = (int)Math.Ceiling((double)((decimal)totalRecord / Convert.ToDecimal(ThucPhamSachEnum.PageSize)));
            var data = JsonConvert.SerializeObject(new { pageCount , listSearchPaging },
            Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
            return Content(data, "application/json");
            //return RedirectToAction("Index");
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
