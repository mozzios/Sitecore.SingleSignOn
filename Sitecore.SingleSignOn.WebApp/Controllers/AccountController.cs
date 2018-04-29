using Newtonsoft.Json;
using Sitecore.SingleSignOn.Bridge.Services;
using Sitecore.SingleSignOn.Models.Account;
using Sitecore.SingleSignOn.Utility.Constant;
using Sitecore.SingleSignOn.Utility.Enums;
using Sitecore.SingleSignOn.Utility.Logging;
using Sitecore.SingleSignOn.Utility.Security;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Sitecore.SingleSignOn.WebApp.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Logoff();
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ILoginServices loginService = new LoginServices();

                    IEncryptionHelper encyptionHelper = new EncryptionHelper();
                    model.Password = encyptionHelper.EncryptString(model.Password, EncryptionTypeEnums.Member);
                    MemberModels memberModel = loginService.Authenticate(model);

                    if (memberModel == null)
                    {
                        ModelState.AddModelError("", ErrorMessages.UsernamePasswordInvalid);
                        return View(model);
                    }

                    string userData = JsonConvert.SerializeObject(memberModel);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, model.Username, DateTime.Now, DateTime.Now.AddMinutes(30), false, userData);
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie authCookie = new HttpCookie("SitecoreCookie", encryptedTicket);
                    Response.Cookies.Add(authCookie);
                }
                else
                {
                    return View(model);
                }
                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog(StaticKeyHelper.WebApplication, StaticKeyHelper.Login, ex.Message);
                ModelState.AddModelError("", ErrorMessages.TechnicalIssues);
                return RedirectToAction("Login", "Account");
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IRegisterServices registerService = new RegisterServices();
                    if (registerService.IsUsernameExist(model))
                    {
                        ModelState.AddModelError("", ErrorMessages.UsernameTaken);
                        return View(model);
                    }

                    IEncryptionHelper encyptionHelper = new EncryptionHelper();
                    model.Password = encyptionHelper.EncryptString(model.Password, EncryptionTypeEnums.Member);

                    if (registerService.Register(model))
                        return RedirectToAction("Login", "Account");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(StaticKeyHelper.WebApplication, StaticKeyHelper.Register, ex.Message);
                ModelState.AddModelError("", ErrorMessages.TechnicalIssues);
                return RedirectToAction("Login", "Account");
            }
        }

        [AllowAnonymous]
        public ActionResult Logoff()
        {
            HttpCookie cookie = new HttpCookie("SitecoreCookie", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}