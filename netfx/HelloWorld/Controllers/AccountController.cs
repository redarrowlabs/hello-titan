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
            return Redirect("https://test.redarrow.io/auth/logout");
        }
    }
}