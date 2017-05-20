using System;
using System.Web;
using System.Web.Mvc;

namespace HelloWorld.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // GET: /Account/Login
        public ActionResult Login(string returnUrl)
        {
            return RedirectToAction("Index", "Home");
        }

        // POST: /Account/LogOff
        [HttpPost]
        public ActionResult LogOff()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("https://test.redarrow.io/auth/logout");
        }
    }
}