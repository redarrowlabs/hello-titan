using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using HelloWorld.Models;
using IdentityModel.Client;

namespace HelloWorld.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
            {
                var userInfoClient = new UserInfoClient("https://sandbox.redarrow.io/auth/connect/userinfo");

                var userInfo = await userInfoClient.GetAsync(ClaimsPrincipal.Current.FindFirst("access_token").Value);

                return View(new UserModel
                {
                    FirstName = userInfo.Claims.First(x => x.Type == "given_name").Value,
                    LastName = userInfo.Claims.First(x => x.Type == "family_name").Value
                });
            }
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