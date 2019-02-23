using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoftwareProjectManagementSystemWebApp.Manager;
using SoftwareProjectManagementSystemWebApp.Models;

namespace SoftwareProjectManagementSystemWebApp.Controllers
{
    public class HomeController : Controller
    {
        private UserManager userManager;

        public HomeController()
        {
            userManager = new UserManager();
        }

        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["UserId"]) == 0)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.Designation = userManager.GetDesignationByUserId(Convert.ToInt32(Session["UserId"]));
                User user = userManager.GetUserById(Convert.ToInt32(Session["UserId"]));

                ViewBag.UserName = user.Name;
                return View();    
            }
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}