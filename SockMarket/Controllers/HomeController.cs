using SockMarket.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SockMarket.Controllers
{
    public class HomeController : Controller
    {

        private MarketContext db = new MarketContext();

        public ActionResult Index()
        {
            return View(db.Companies.ToList());
        }
    }
}