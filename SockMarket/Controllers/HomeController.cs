using System.Web.Mvc;

namespace SockMarket.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
    }
}