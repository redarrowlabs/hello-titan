﻿using System.Linq;
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
                return View(new UserModel
                {
                    FirstName = ClaimsPrincipal.Current.FindFirst("given_name").Value,
                    LastName = ClaimsPrincipal.Current.FindFirst("family_name").Value
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