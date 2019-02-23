using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using SoftwareProjectManagementSystemWebApp.Manager;
using SoftwareProjectManagementSystemWebApp.Models;
using SoftwareProjectManagementSystemWebApp.Utility;

namespace SoftwareProjectManagementSystemWebApp.Controllers
{
    public class ProjectController : Controller
    {
        private ProjectManager projectManager;
        private UserManager userManager;
        private TaskManager taskManager;
        private AuthenticationManager authManager;

        private int count = 0;
        public ProjectController()
        {
            projectManager = new ProjectManager();
            userManager = new UserManager();
            taskManager = new TaskManager();
            authManager = new AuthenticationManager();
        }

        // add files (for Project Manager)
        [HttpGet]
        public ActionResult AddProject()
        {
            if (CheckAuthProjectManager() == 0)
            {
                ViewBag.ErrorMessage = "Log in first";
                return RedirectToAction("LogIn","UserAuthentication");
            }
            else if (CheckAuthProjectManager() == 1)
            {
                User user = userManager.GetUserById(Convert.ToInt32(Session["UserId"]));

                ViewBag.UserName = user.Name;
                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult AddProject(ProjectDetail projectDetails, List<HttpPostedFileBase> files)
        {
            if (CheckAuthProjectManager() == 0)
            {
                ViewBag.ErrorMessage = "Log in first";
                return RedirectToAction("LogIn", "UserAuthentication");
            }
            else if (CheckAuthProjectManager() == 1)
            {
                User user = userManager.GetUserById(Convert.ToInt32(Session["UserId"]));

                ViewBag.UserName = user.Name;

                if (ModelState.IsValid)
                {
                    DateTime date1 = DateTime.Parse(projectDetails.StartDate);
                    DateTime date2 = DateTime.Parse(projectDetails.EndDate);

                    if (date2 <= date1)
                    {
                        ViewBag.Response = "Invalid Start and End Date";
                        return View(projectDetails);
                    }
                    else
                    {
                        if (files[0] != null)
                        {
                            projectDetails.ActionBy = Convert.ToInt32(Session["UserId"]);
                            projectDetails.ActionDone = ActionUtility.ActionInsert;
                            projectDetails.ActionTime = DateTime.Now.ToString();

                            projectDetails.ProjectCode = Guid.NewGuid().ToString();

                            int projectId = projectManager.SaveProject(projectDetails);

                            if (projectId > 0)
                            {
                                foreach (HttpPostedFileBase file in files)
                                {
                                    FileUrl fileUrl = new FileUrl();

                                    fileUrl.ProjectId = projectId;
                                    fileUrl.FileName = file.FileName;
                                    fileUrl.FileLocation = "~/Files/" + projectDetails.ProjectCode + "_" + file.FileName;
                                    fileUrl.ActionBy = Convert.ToInt32(Session["UserId"]);
                                    fileUrl.ActionDone = ActionUtility.ActionInsert;
                                    fileUrl.ActionTime = DateTime.Now.ToString();

                                    bool fileUrlSaved = projectManager.SaveFileUrl(fileUrl);

                                    if (fileUrlSaved)
                                    {
                                        var path = Path.Combine(Server.MapPath("~/Files/"),
                                            projectDetails.ProjectCode + "_" + file.FileName);
                                        file.SaveAs(path);
                                        count++;
                                    }
                                    else
                                    {
                                        count = 0;
                                    }
                                }

                                if (count == files.Count())
                                {
                                    ViewBag.Response = "Project Files Uploaded Successfully";
                                    ModelState.Clear();
                                    return View();
                                }
                                else
                                {
                                    ViewBag.Response = "Project Files Upload Failed";
                                    return View(projectDetails);
                                }
                            }
                            else
                            {
                                ViewBag.Response = "Failed to Upload Project";
                                return View(projectDetails);
                            }
                        }
                        else
                        {
                            ViewBag.Response = "Browse files";
                            return View(projectDetails);
                        }
                    }
                }
                else
                {
                    ViewBag.Response = "Fill up all fields correctly";
                    return View(projectDetails);
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        // view all project for update (for Project Manager)
        [HttpGet]
        public ActionResult ViewAllProject()
        {
            if (CheckAuthProjectManager() == 0)
            {
                ViewBag.ErrorMessage = "Log in first";
                return RedirectToAction("LogIn", "UserAuthentication");
            }
            else if (CheckAuthProjectManager() == 1)
            {
                User user = userManager.GetUserById(Convert.ToInt32(Session["UserId"]));

                ViewBag.UserName = user.Name;
                ViewBag.Projects = projectManager.GetAllProjects();
                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

        // update project info (for Project Manager)
        [HttpGet]
        public ActionResult UpdateProjectDetail(int projectId)
        {
            if (CheckAuthProjectManager() == 0)
            {
                ViewBag.ErrorMessage = "Log in first";
                return RedirectToAction("LogIn", "UserAuthentication");
            }
            else if (CheckAuthProjectManager() == 1)
            {
                User user = userManager.GetUserById(Convert.ToInt32(Session["UserId"]));

                ViewBag.UserName = user.Name;

                if (projectManager.IsProjectExistsById(projectId))
                {
                    ProjectDetail projectDetail = projectManager.GetProjectDetailsById(projectId);
                    ViewBag.Files = projectManager.GetProjectFilesForDropDown(projectId);

                    return View(projectDetail);
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
        public ActionResult UpdateProjectDetail(ProjectDetail projectDetail, int selectedFile, HttpPostedFileBase file)
        {
            if (CheckAuthProjectManager() == 0)
            {
                ViewBag.ErrorMessage = "Log in first";
                return RedirectToAction("LogIn", "UserAuthentication");
            }
            else if (CheckAuthProjectManager() == 1)
            {
                User user = userManager.GetUserById(Convert.ToInt32(Session["UserId"]));

                ViewBag.UserName = user.Name;

                if (projectManager.IsProjectExistsById(projectDetail.Id))
                {
                    if (ModelState.IsValid)
                    {
                        DateTime date1 = DateTime.Parse(projectDetail.StartDate);
                        DateTime date2 = DateTime.Parse(projectDetail.EndDate);

                        if (date2 <= date1)
                        {
                            ViewBag.Response = "Invalid Start and End Date";

                            ProjectDetail projectDetailModel = projectManager.GetProjectDetailsById(projectDetail.Id);
                            ViewBag.Files = projectManager.GetProjectFilesForDropDown(projectDetail.Id);
                            return View(projectDetailModel);
                        }
                        else
                        {
                            if (file != null && selectedFile != 0)
                            {
                                projectDetail.ActionBy = Convert.ToInt32(Session["UserId"]);
                                projectDetail.ActionDone = ActionUtility.ActionUpdate;
                                projectDetail.ActionTime = DateTime.Now.ToString();
                                int updateDetailsCheck = projectManager.UpdateProjectDetails(projectDetail);

                                FileUrl fileUrl = projectManager.GetProjectFilesByFileUrlId(selectedFile);

                                fileUrl.FileName = file.FileName;
                                fileUrl.FileLocation = "~/Files/" + projectDetail.ProjectCode + "_" + file.FileName;
                                fileUrl.ActionBy = Convert.ToInt32(Session["UserId"]);
                                fileUrl.ActionDone = ActionUtility.ActionUpdate;
                                fileUrl.ActionTime = DateTime.Now.ToString();

                                bool updateFileUrl = projectManager.UpdateFileUrl(fileUrl);

                                if (updateFileUrl && updateDetailsCheck == 1)
                                {
                                    var path = Path.Combine(Server.MapPath("~/Files/"),
                                    projectDetail.ProjectCode + "_" + file.FileName);
                                    file.SaveAs(path);

                                    ViewBag.Response = "Successfully Updated";
                                }
                                else if (updateFileUrl && updateDetailsCheck == 2)
                                {
                                    var path = Path.Combine(Server.MapPath("~/Files/"),
                                    projectDetail.ProjectCode + "_" + file.FileName);
                                    file.SaveAs(path);

                                    ViewBag.Response = "Successfully Updated File but Details remains same";

                                }
                                else if (!updateFileUrl && updateDetailsCheck == 1)
                                {
                                    ViewBag.Response = "Failed Updated File but Details Updated Successfully";
                                }
                                else if (updateFileUrl && updateDetailsCheck == 0)
                                {
                                    var path = Path.Combine(Server.MapPath("~/Files/"),
                                    projectDetail.ProjectCode + "_" + file.FileName);
                                    file.SaveAs(path);

                                    ViewBag.Response = "Success to Update files but failed to update details";
                                }
                                else
                                {
                                    ViewBag.Response = "Not Updated any thing";
                                }

                                ProjectDetail projectDetailModel = projectManager.GetProjectDetailsById(projectDetail.Id);
                                ViewBag.Files = projectManager.GetProjectFilesForDropDown(projectDetail.Id);

                                return View(projectDetailModel);
                            }
                            else
                            {
                                projectDetail.ActionBy = Convert.ToInt32(Session["UserId"]);
                                projectDetail.ActionDone = ActionUtility.ActionUpdate;
                                projectDetail.ActionTime = DateTime.Now.ToString();
                                int updateDetailsCheck = projectManager.UpdateProjectDetails(projectDetail);

                                if (updateDetailsCheck == 2)
                                {
                                    ViewBag.Response = "Remaining Same";
                                }
                                else if (updateDetailsCheck == 1)
                                {
                                    ViewBag.Response = "Details information Updated without files";
                                }
                                else
                                {
                                    ViewBag.Response = "Failed to Update";
                                }

                                ProjectDetail projectDetailModel = projectManager.GetProjectDetailsById(projectDetail.Id);
                                ViewBag.Files = projectManager.GetProjectFilesForDropDown(projectDetail.Id);
                                return View(projectDetailModel);
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Response = "Fill up all fields correctly";

                        ProjectDetail projectDetailModel = projectManager.GetProjectDetailsById(projectDetail.Id);
                        ViewBag.Files = projectManager.GetProjectFilesForDropDown(projectDetail.Id);
                        return View(projectDetailModel);
                    }
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

        // assign user to project
        [HttpGet]
        public ActionResult AssignUser()
        {
            if (CheckAuthProjectManager() == 0)
            {
                ViewBag.ErrorMessage = "Log in first";
                return RedirectToAction("LogIn", "UserAuthentication");
            }
            else if (CheckAuthProjectManager() == 1)
            {
                User user = userManager.GetUserById(Convert.ToInt32(Session["UserId"]));

                ViewBag.UserName = user.Name;
                ViewBag.Projects = projectManager.GetAllProjectsForDropDown();
                ViewBag.Users = userManager.GetAllUsersFromDropDown();

                ViewBag.AssignedUser = projectManager.GetAssignedUser();
                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult AssignUser(AssignUser assignUser)
        {
            if (CheckAuthProjectManager() == 0)
            {
                ViewBag.ErrorMessage = "Log in first";
                return RedirectToAction("LogIn", "UserAuthentication");
            }
            else if (CheckAuthProjectManager() == 1)
            {
                User user = userManager.GetUserById(Convert.ToInt32(Session["UserId"]));

                ViewBag.UserName = user.Name;

                if (ModelState.IsValid)
                {
                    assignUser.ActionBy = Convert.ToInt32(Session["UserId"]);
                    assignUser.ActionDone = ActionUtility.ActionInsert;
                    assignUser.ActionTime = DateTime.Now.ToString();

                    ViewBag.Response = projectManager.SaveAssignUser(assignUser);

                    ViewBag.Projects = projectManager.GetAllProjectsForDropDown();
                    ViewBag.Users = userManager.GetAllUsersFromDropDown();
                    ViewBag.AssignedUser = projectManager.GetAssignedUser();

                    return View();
                }
                else
                {
                    ViewBag.Response = "Select All correctly";
                    ViewBag.Projects = projectManager.GetAllProjectsForDropDown();
                    ViewBag.Users = userManager.GetAllUsersFromDropDown();
                    ViewBag.AssignedUser = projectManager.GetAssignedUser();
                    return View();

                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        // view all project by user involve (for all) 
        [HttpGet]
        public ActionResult ViewProjectInvolve()
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
                int userId = Convert.ToInt32(Session["UserId"]);
                ViewBag.Projects = projectManager.GetProjectInvolved(userId);
                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

        // view project details by project id
        [HttpGet]
        public ActionResult ViewProjectDetails(int projectId)
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

                if (projectManager.IsProjectExistsById(projectId))
                {
                    ViewBag.ProjectDetails = projectManager.GetProjectDetailsById(projectId);
                    ViewBag.ProjectFiles = projectManager.GetProjectFiles(projectId);
                    ViewBag.GetAssignedMembers = projectManager.GetAssignedByProjectId(projectId);
                    ViewBag.TaskList = taskManager.GetTasksByProjectId(projectId);

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

        // download file
        public FileResult DownloadFile(string fileName, string url)
        {
            return File(url, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        // check auth
        public int CheckAuthProjectManager()
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            if (authManager.CheckDesignation(userId) == 5)
            {
                return 0;
            }
            else if (authManager.CheckDesignation(userId) == 2)
            {
                return 1;
            }
            else
            {
                return 2;
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