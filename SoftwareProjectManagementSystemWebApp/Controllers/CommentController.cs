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
    public class CommentController : Controller
    {
        private ProjectManager projectManager;
        private TaskManager taskManager;
        private CommentManager commentManager;
        private AuthenticationManager authManager;
        private UserManager userManager;

        public CommentController()
        {
            projectManager = new ProjectManager();
            taskManager = new TaskManager();
            commentManager = new CommentManager();
            authManager = new AuthenticationManager();
            userManager = new UserManager();
        }

        // add comment
        [HttpGet]
        public ActionResult AddComment()
        {
            if (CheckAuthProjectManagerAndOther() == 0)
            {
                ViewBag.ErrorMessage = "Log in first";
                return RedirectToAction("LogIn", "UserAuthentication");
            }
            else if (CheckAuthProjectManagerAndOther() == 1)
            {
                User user = userManager.GetUserById(Convert.ToInt32(Session["UserId"]));

                ViewBag.UserName = user.Name;
                ViewBag.Projects = projectManager.GetAllProjectsForDropDown();
                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult AddComment(Comment comment)
        {
            if (CheckAuthProjectManagerAndOther() == 0)
            {
                ViewBag.ErrorMessage = "Log in first";
                return RedirectToAction("LogIn", "UserAuthentication");
            }
            else if (CheckAuthProjectManagerAndOther() == 1)
            {
                User user = userManager.GetUserById(Convert.ToInt32(Session["UserId"]));

                ViewBag.UserName = user.Name;

                if (ModelState.IsValid)
                {
                    comment.ActionById = Convert.ToInt32(Session["UserId"]);
                    comment.ActionDone = ActionUtility.ActionInsert;
                    comment.ActionTime = DateTime.Now.ToString();

                    ViewBag.Response = commentManager.AddComment(comment);

                    ViewBag.Projects = projectManager.GetAllProjectsForDropDown();
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ViewBag.Response = "Fill up all fields correctly";

                    ViewBag.Projects = projectManager.GetAllProjectsForDropDown();
                    return View(comment);
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        // get task by project id
        public JsonResult GetTaskByProjectId(int projectId)
        {
            return Json(taskManager.GetTasksByProjectId(projectId));
        }

        // view comment
        [HttpGet]
        public ActionResult ViewComment(int taskId)
        {
            if (CheckAuthProjectManagerAndOther() == 0)
            {
                ViewBag.ErrorMessage = "Log in first";
                return RedirectToAction("LogIn", "UserAuthentication");
            }
            else if (CheckAuthProjectManagerAndOther() == 1)
            {
                User user = userManager.GetUserById(Convert.ToInt32(Session["UserId"]));

                ViewBag.UserName = user.Name;
                ViewBag.Comments = commentManager.GetAllCommentByTaskId(taskId);

                ViewBag.ProjectName = taskManager.GetProjectNameByTaskId(taskId);
                ViewBag.TaskName = taskManager.GetTaskNameByTaskId(taskId);

                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

        // check auth with project manager and other
        public int CheckAuthProjectManagerAndOther()
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            if (authManager.CheckDesignation(userId) == 5)
            {
                return 0;
            }
            else if (authManager.CheckDesignation(userId) == 2 || authManager.CheckDesignation(userId) == 3)
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