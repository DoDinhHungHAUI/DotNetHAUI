using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ThucPhamSach.Infrastructure.Core;
using ThucPhamSach.Models;

namespace ThucPhamSach.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        private const string cartSession = "cartSesstion";

        private thucphamsachDB thucphamsachDB = new thucphamsachDB();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = Session[cartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public ActionResult AddToCart(int maSp, int soLuong)
        {
            var product = thucphamsachDB.SanPhams.Find(maSp);
            var cart = Session[cartSession];

            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.sanPham.MaSP == maSp))
                {

                    foreach (var item in list)
                    {
                        if (item.sanPham.MaSP == maSp)
                        {
                            item.soLuong += soLuong;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var item = new CartItem();
                    item.sanPham = product;
                    item.soLuong = soLuong;
                    list.Add(item);
                }
                Session[cartSession] = list;
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new CartItem();
                item.sanPham = product;
                item.soLuong = soLuong;
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào session
                Session[cartSession] = list;
            }
            return RedirectToAction("Index");
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[cartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.sanPham.MaSP == item.sanPham.MaSP);
                if (jsonItem != null)
                {
                    item.soLuong = jsonItem.soLuong;
                }
            }
            Session[cartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Delete(int id)
        {
            var sessionCart = (List<CartItem>)Session[cartSession];
            sessionCart.RemoveAll(x => x.sanPham.MaSP == id);
            Session[cartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
    }
}