using Maserati.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maserati.Controllers
{
    public class HomeController : Controller
    {
        //GET: /Home/
        private Models.ShopDbEntities1 db = new Models.ShopDbEntities1();
        public ActionResult Index()
        {
            var Items = db.Cars;
            return View(Items);
        }

        public ActionResult CarPage(int item_id)
        {
            var Item = db.Cars.FirstOrDefault(x => x.Id == item_id);
            if (Item == null)
            {
                return Content("<h1>Not Found</h1>");
            }
            return View(Item);
        }

        [ChildActionOnly]
        public ActionResult Nav()
        {
            var Items = db.Cars;
            string result = "";
            foreach (var item in Items)
            {
                result += "<li><a href='/Home/CarPage/?item_id=" + item.Id + "' title='" + item.Title + "'>" + item.Title + "</a></li>";
            }
            return Content(result);
        }

        [HttpGet]
        public ActionResult Form(int item_id=0)
        {
            ViewBag.Item = item_id;
            return PartialView();
        }

        [HttpPost]
        public string Form(string Name, string Tel, int Car)
        {
            Order order = new Order
            {
                UserName = Name,
                UserTel = Tel,
                CarId = Car,
                Status = "Создана"
            };
            db.Orders.Add(order);
            db.SaveChanges();
            return "Ваша заявка на автомобиль " + db.Cars.FirstOrDefault(x => x.Id == Car).Title + " создана.";
        }

        [ChildActionOnly]
        public string FormOption( int item_id)
        {
            var Items = db.Cars;
            string str = "";
            foreach (var item in Items)
            {
                if (item_id == item.Id)
                {
                    str += "<option value=" + item.Id + " selected>" + item.Title + "</option>";
                }
                else
                {
                    str += "<option value=" + item.Id + ">" + item.Title + "</option>";
                }
            }
            return str;
            
                
        }
    }
}