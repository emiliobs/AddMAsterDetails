using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AddMAsterDetails.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetProductCategories()
        {
            List<Category> categories = new List<AddMAsterDetails.Category>();

            using (MyDBEntities db = new MyDBEntities())
            {
                categories = db.Categories.OrderBy(c => c.CategoryNane).ToList();
            }

            return new JsonResult { Data = categories, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetProducts(int categoryId)
        {
            List<Product> Products = new List<AddMAsterDetails.Product>();

            using (MyDBEntities db = new MyDBEntities())
            {
                Products = db.Products.Where(c=>c.CategoryId.Equals(categoryId)).OrderBy(a=>a.ProductName).ToList();
            }

            return new JsonResult { Data = Products, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult Save(OrderMaster order)
        {
            bool status = false;
            DateTime dateOrg;

            var isValidDate = DateTime.TryParseExact(order.OrderDateString, "dd-mm-yyyy", null,
                System.Globalization.DateTimeStyles.None, out dateOrg);

            if (isValidDate)
            {
                order.OrderDate = dateOrg;
            }

            var isValidModel = TryUpdateModel(order);

            if (isValidModel)
            {
                using (MyDBEntities db = new MyDBEntities())
                {
                    db.OrderMasters.Add(order);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {

                    }

                    status = true;
                }
            }

            return new JsonResult { Data = new { status } };
        }
    }
}