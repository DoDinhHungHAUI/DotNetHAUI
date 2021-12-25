using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ThucPhamSach.Infrastructure.Common;
using ThucPhamSach.Infrastructure.Core;
using ThucPhamSach.Models;

namespace ThucPhamSach.Controllers
{
    public class ProductController : Controller
    {
        // GET: Category
        thucphamsachDB db = new thucphamsachDB();
        public ActionResult ProductCategory(int? id , int page = 1, string sort = "")
        {
            int pageSize = 3;//ConfigHelper.pageSize;
            var sanPhams = db.SanPhams.Where(sp => sp.MaDanhMuc == id).OrderBy(item => item.TenSanPham).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            ViewBag.id = id;
            ViewBag.danhMucs = db.DanhMucSanPhams.Select(dm => new { MaDanhMuc = dm.MaDanhMuc , TenDanhMuc = dm.TenDanhMuc , SLSanPham = (db.SanPhams.Where(sp => sp.MaDanhMuc == dm.MaDanhMuc).Count())}).ToList();
            ViewBag.idProductCategory = id;
            int soLuong = db.SanPhams.Where(sp => sp.MaDanhMuc == id).Count();
            int totalPage = (int)Math.Ceiling((double)soLuong / pageSize);
            var paginationSet = new PaginationSet<SanPham>
            {
                Items = sanPhams,
                Count = soLuong,
                MaxPage = ConfigHelper.MaxPage,
                Page = page,
                TotalPages = totalPage
            };

            return View(paginationSet);
        }

        public ActionResult ProductDetail(int? id)
        {
            var sanPham = db.SanPhams.Where(sp => sp.MaSP == id).FirstOrDefault();
            if(sanPham != null)
            {
                ViewBag.sanPhamRecomment = db.SanPhams.Where(sp => sp.MaDanhMuc == sanPham.MaDanhMuc && sp.MaSP != sanPham.MaSP).ToList();
            }    

            return View(sanPham);
        }


        public ActionResult Search(string keyword, int page = 1, string sort = "")
        {
            int pageSize = ConfigHelper.pageSize;
            int totalRow = 0;
            var products = TimKiemSanPham(keyword, page, pageSize, sort, out totalRow);

            //var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            ViewBag.keyword = keyword;

            ViewBag.danhMucs = db.DanhMucSanPhams.Select(dm => new { MaDanhMuc = dm.MaDanhMuc, TenDanhMuc = dm.TenDanhMuc, SLSanPham = (db.SanPhams.Where(sp => sp.MaDanhMuc == dm.MaDanhMuc).Count()) }).ToList();

            var paginationSet = new PaginationSet<SanPham>
            {
                Items = products,
                Count = products.Count(),
                MaxPage = ConfigHelper.MaxPage,
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            //return View(paginationSet);

            return View(paginationSet);
        }

        private IEnumerable<SanPham> TimKiemSanPham(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = db.SanPhams.Where(item => item.TenSanPham.Contains(keyword));

            switch (sort)
            {
                case "giaban":
                    query = query.OrderBy(x => x.GiaBan);
                    break;
                //case "discount":
                //    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                //    break;
                //case "price":
                //    query = query.OrderBy(x => x.Price);
                //    break;
                default:
                    query = query.OrderBy(x => x.TenSanPham);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }





    }
}