using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoftwareProjectManagementSystemWebApp.Manager;
using SoftwareProjectManagementSystemWebApp.Models;

namespace SoftwareProjectManagementSystemWebApp.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private AuthenticationManager authManager;

        public UserAuthenticationController()
        {
            authManager = new AuthenticationManager();
        }

        // log in
        [HttpGet]
        public ActionResult LogIn()
        {
            Session["UserId"] = 0;
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(User user)
        {
            if (user.Email != null || user.Password != null)
            {
                if (authManager.LogIn(user))
                {
                    Session["UserId"] = authManager.GetUserByEmailAndPassword(user);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Session["UserId"] = 0;
                    ViewBag.ErrorMessage = "Email or Password is invalid";
                    return View();
                }
            }
            else
            {
                Session["UserId"] = 0;
                ViewBag.ErrorMessage = "Fill up all fields correctly";
                return View();
            }
        }

        // log out
        public ActionResult LogOut()
        {
            ViewBag.ErrorMessage = "Log Out Successfully";
            Session["UserId"] = 0;
            return RedirectToAction("LogIn", "UserAuthentication");
        }
	}
}