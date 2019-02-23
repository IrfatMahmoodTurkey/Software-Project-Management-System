using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoftwareProjectManagementSystemWebApp.Manager;
using SoftwareProjectManagementSystemWebApp.Models;
using SoftwareProjectManagementSystemWebApp.Models.ViewModels;
using SoftwareProjectManagementSystemWebApp.Utility;

namespace SoftwareProjectManagementSystemWebApp.Controllers
{
    public class TaskController : Controller
    {
        private ProjectManager projectManager;
        private TaskManager taskManager;
        private AuthenticationManager authManager;
        private UserManager userManager;

        public TaskController()
        {
            projectManager = new ProjectManager();
            taskManager = new TaskManager();
            authManager = new AuthenticationManager();
            userManager = new UserManager();
        }

        // 
        [HttpGet]
        public ActionResult AddTask()
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
        public ActionResult AddTask(Task task)
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
                    task.AssignedById = Convert.ToInt32(Session["UserId"]);
                    task.ActionTime = DateTime.Now.ToString();
                    task.ActionDone = ActionUtility.ActionInsert;

                    ViewBag.Response = taskManager.AddTask(task);

                    ViewBag.Projects = projectManager.GetAllProjectsForDropDown();
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ViewBag.Response = "Fill up all fields correctly";
                    ViewBag.Projects = projectManager.GetAllProjectsForDropDown();
                    return View(task);
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        // get user by project
        public JsonResult GetUserByProject(int projectId)
        {
            return Json(projectManager.GetUserByProject(projectId));
        }

        // update task
        [HttpGet]
        public ActionResult UpdateTask(int taskId)
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

                if (taskManager.IsTaskExists(taskId))
                {
                    TaskListViewModel taskById = taskManager.GetTasksByTaskId(taskId);

                    Task task = new Task();
                    task.Id = taskById.Id;
                    task.ProjectId = taskById.ProjectId;
                    task.Description = taskById.Description;
                    task.DueDate = taskById.DueDate;
                    task.Priority = taskById.Priority;
                    task.AssignedToId = taskById.AssignedToId;

                    ViewBag.ProjectName = taskById.ProjectName;
                    ViewBag.UserName = taskById.AssignedTo;
                    ViewBag.SelectedIndex = taskById.Priority;

                    return View(task);
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
        public ActionResult UpdateTask(Task task)
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
                    task.AssignedById = Convert.ToInt32(Session["UserId"]);
                    task.ActionDone = ActionUtility.ActionUpdate;
                    task.ActionTime = DateTime.Now.ToString();

                    ViewBag.Response = taskManager.UpdateTask(task);


                    TaskListViewModel taskById = taskManager.GetTasksByTaskId(task.Id);

                    task.Id = taskById.Id;
                    task.ProjectId = taskById.ProjectId;
                    task.Description = taskById.Description;
                    task.DueDate = taskById.DueDate;
                    task.Priority = taskById.Priority;
                    task.AssignedToId = taskById.AssignedToId;

                    ViewBag.ProjectName = taskById.ProjectName;
                    ViewBag.UserName = taskById.AssignedTo;
                    ViewBag.SelectedIndex = taskById.Priority;

                    return View(task);
                }
                else
                {
                    ViewBag.Response = "Fill Up all fields correctly";

                    TaskListViewModel taskById = taskManager.GetTasksByTaskId(task.Id);

                    task.Id = taskById.Id;
                    task.ProjectId = taskById.ProjectId;
                    task.Description = taskById.Description;
                    task.DueDate = taskById.DueDate;
                    task.Priority = taskById.Priority;
                    task.AssignedToId = taskById.AssignedToId;

                    ViewBag.ProjectName = taskById.ProjectName;
                    ViewBag.UserName = taskById.AssignedTo;
                    ViewBag.SelectedIndex = taskById.Priority;

                    return View(task);
                }
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