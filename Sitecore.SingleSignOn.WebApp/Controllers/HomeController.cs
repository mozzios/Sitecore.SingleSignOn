using Sitecore.SingleSignOn.WebApp.Helper;
using System.Web.Mvc;

namespace Sitecore.SingleSignOn.WebApp.Controllers
{
    [AuthorizeHelper]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}