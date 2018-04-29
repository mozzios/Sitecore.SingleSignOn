using Newtonsoft.Json;
using Sitecore.SingleSignOn.Models.Account;
using Sitecore.SingleSignOn.WebApp.Models;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Sitecore.SingleSignOn.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            try
            {
                HttpCookie authCookie = Request.Cookies["SitecoreCookie"];
                if (authCookie != null)
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                    var memberModel = JsonConvert.DeserializeObject<MemberModels>(authTicket.UserData);

                    MemberPrincipal principal = new MemberPrincipal(authTicket.Name, memberModel.LoginMethod);

                    principal.MemberId = memberModel.MemberId;
                    principal.Fullname = memberModel.Fullname;
                    principal.Username = memberModel.Username;

                    HttpContext.Current.User = principal;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
