using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoftwareProjectManagementSystemWebApp.Manager;
using SoftwareProjectManagementSystemWebApp.Models;
using SoftwareProjectManagementSystemWebApp.Utility;

namespace SoftwareProjectManagementSystemWebApp.Controllers
{
    public class UserController : Controller
    {
        private UserManager userManager;
        private DesignationManager designationManager;
        private AuthenticationManager authManager;
        
        public UserController()
        {
            userManager = new UserManager();
            designationManager = new DesignationManager();
            authManager = new AuthenticationManager();
        }

        // save user (for IT Admin) 
        [HttpGet]
        public ActionResult SaveUser()
        {
            if (CheckAuthITAdmin() == 0)
            {
                ViewBag.ErrorMessage = "Log in first";
                return RedirectToAction("LogIn","UserAuthentication");
            }
            else if (CheckAuthITAdmin() == 1)
            {
                User user = userManager.GetUserById(Convert.ToInt32(Session["UserId"]));

                ViewBag.UserName = user.Name;
                ViewBag.Designations = designationManager.GetDesignationForDropDown();
                return View();
            }
            else
            {
                return HttpNotFound();
            }
            
        }

        [HttpPost]
        public ActionResult SaveUser(User user)
        {
            if (CheckAuthITAdmin() == 0)
            {
                ViewBag.ErrorMessage = "Log in first";
                return RedirectToAction("LogIn", "UserAuthentication");
            }
            else if (CheckAuthITAdmin() == 1)
            {
                User userName = userManager.GetUserById(Convert.ToInt32(Session["UserId"]));

                ViewBag.UserName = userName.Name;

                if (ModelState.IsValid)
                {
                    user.ActionBy = "IT Admin";
                    user.ActionDone = ActionUtility.ActionInsert;
                    user.ActionTime = DateTime.Now.ToString();

                    int saved = userManager.SaveUser(user);

                    if (saved == 1)
                    {
                        ViewBag.Message = "Saved Successfully";

                        ModelState.Clear();
                        ViewBag.Designations = designationManager.GetDesignationForDropDown();
                        return View();
                    }
                    else if (saved == 2)
                    {
                        ViewBag.Message = "Email Must be Unique";

                        ViewBag.Designations = designationManager.GetDesignationForDropDown();
                        return View(user);
                    }
                    else
                    {
                        ViewBag.Message = "Save Failed";

                        ViewBag.Designations = designationManager.GetDesignationForDropDown();
                        return View(user);
                    }
                }
                else
                {
                    ViewBag.Message = "Fill up all fields";

                    ViewBag.Designations = designationManager.GetDesignationForDropDown();
                    return View(user);
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

         // view all users (for IT Admin)
        [HttpGet]
        public ActionResult ViewAllUsers()
        {
            if (CheckAuthITAdmin() == 0)
            {
                ViewBag.ErrorMessage = "Log in first";
                return RedirectToAction("LogIn", "UserAuthentication");
            }
            else if (CheckAuthITAdmin() == 1)
            {
                User user = userManager.GetUserById(Convert.ToInt32(Session["UserId"]));

                ViewBag.UserName = user.Name;
                ViewBag.ViewAllUsers = userManager.GetAllUsers();
                return View();
            }
            else
            {
                return HttpNotFound();
            }

            
        }

        //// reset password (for IT Admin)
        [HttpGet]
        public ActionResult ResetPassword(int id)
        {
            if (CheckAuthITAdmin() == 0)
            {
                ViewBag.ErrorMessage = "Log in first";
                return RedirectToAction("LogIn", "UserAuthentication");
            }
            else if (CheckAuthITAdmin() == 1)
            {
                User user = userManager.GetUserById(Convert.ToInt32(Session["UserId"]));

                ViewBag.UserName = user.Name;
                if (userManager.IsUserExists(id))
                {
                    int resetPassword = userManager.ResetPassword(id);

                    if (resetPassword == 1)
                    {
                        ViewBag.Message = "Successfully reset";
                    }
                    else
                    {
                        ViewBag.Message = "Reset Failed";
                    }

                    return View();
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        // update user (for IT Admin)
        [HttpGet]
        public ActionResult UpdateUser(int id)
        {
            if (CheckAuthITAdmin() == 0)
            {
                ViewBag.ErrorMessage = "Log in first";
                return RedirectToAction("LogIn", "UserAuthentication");
            }
            else if (CheckAuthITAdmin() == 1)
            {
                User userName = userManager.GetUserById(Convert.ToInt32(Session["UserId"]));

                ViewBag.UserName = userName.Name;
                if (userManager.IsUserExists(id))
                {
                    User user = userManager.GetUserById(id);

                    ViewBag.NowStatus = user.Status;
                    ViewBag.DesignationIndex = user.DesignationId;
                    ViewBag.Designations = designationManager.GetDesignationForDropDown();

                    return View(user);
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult UpdateUser(User user)
        {
            if (CheckAuthITAdmin() == 0)
            {
                ViewBag.ErrorMessage = "Log in first";
                return RedirectToAction("LogIn", "UserAuthentication");
            }
            else if (CheckAuthITAdmin() == 1)
            {
                User userName = userManager.GetUserById(Convert.ToInt32(Session["UserId"]));

                ViewBag.UserName = userName.Name;

                if (ModelState.IsValid)
                {
                    user.ActionBy = "IT Admin";
                    user.ActionDone = ActionUtility.ActionUpdate;
                    user.ActionTime = DateTime.Now.ToString();

                    int updated = userManager.UpdateUser(user);

                    if (updated == 1)
                    {
                        ViewBag.Message = "Successfull";
                    }
                    else if (updated == 2)
                    {
                        ViewBag.Message = "Email already exists";
                    }
                    else if (updated == 0)
                    {
                        ViewBag.Message = "Save Failed";
                    }

                    User userInfo = userManager.GetUserById(user.Id);

                    ViewBag.NowStatus = userInfo.Status;
                    ViewBag.DesignationIndex = userInfo.DesignationId;
                    ViewBag.Designations = designationManager.GetDesignationForDropDown();

                    return View(userInfo);

                }
                else
                {
                    ViewBag.Message = "Fill up all fields";

                    User userInfo = userManager.GetUserById(user.Id);

                    ViewBag.NowStatus = userInfo.Status;
                    ViewBag.DesignationIndex = userInfo.DesignationId;
                    ViewBag.Designations = designationManager.GetDesignationForDropDown();

                    return View(userInfo);
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        // check IT Admin for auth
        public int CheckAuthITAdmin()
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            if (authManager.CheckDesignation(userId) == 5)
            {
                return 0;
            }
            else if (authManager.CheckDesignation(userId) == 1)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
	}
}