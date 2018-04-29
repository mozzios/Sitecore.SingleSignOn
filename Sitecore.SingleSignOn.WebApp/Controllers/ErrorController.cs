using System.Web.Mvc;

namespace Sitecore.SingleSignOn.WebApp.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}