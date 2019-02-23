using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoftwareProjectManagementSystemWebApp.Gateaway;
using SoftwareProjectManagementSystemWebApp.Models;
using SoftwareProjectManagementSystemWebApp.Models.ViewModels;

namespace SoftwareProjectManagementSystemWebApp.Manager
{
    public class ProjectManager
    {
        private ProjectGateway projectGateway;
        private TaskGateway taskGateway;

        public ProjectManager()
        {
            projectGateway = new ProjectGateway();
            taskGateway = new TaskGateway();
        }

        // save project
        public int SaveProject(ProjectDetail projectDetail)
        {
            if (projectGateway.IfAnyEntryExists(projectDetail))
            {
                ProjectDetail projectDetailModel = projectGateway.GetProjectInfoByOtherInfo(projectDetail);
                return projectDetailModel.Id;
            }
            else
            {
                int rowsAffected = projectGateway.SaveProject(projectDetail);

                if (rowsAffected > 0)
                {
                    ProjectDetail projectDetailModel =
                        projectGateway.GetProjectInfoByOtherInfo(projectDetail);
                    return projectDetailModel.Id;
                }
                else
                {
                    return 0;
                }
            }
        }


        // save file url
        public bool SaveFileUrl(FileUrl fileUrl)
        {
            int rowsAffected = projectGateway.UploadFileUrl(fileUrl);

            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // get all projects
        public List<ProjectDetailViewModel> GetAllProjects()
        {
            return projectGateway.GetAllProjects();
        }

        // get all projects for dropdown
        public List<SelectListItem> GetAllProjectsForDropDown()
        {
            List<ProjectDetailViewModel> models = GetAllProjects();

            List<SelectListItem> selectListItems =
                models.ConvertAll(c => new SelectListItem() {Text = c.ProjectName, Value = c.Id.ToString()});

            List<SelectListItem> mainSelectListItems = new List<SelectListItem>();
            mainSelectListItems.Add(new SelectListItem(){Text = "-- Select Project --", Value = ""});

            mainSelectListItems.AddRange(selectListItems);

            return mainSelectListItems;
        } 
 
        // get project files
        public List<FileUrl> GetProjectFiles(int projectId)
        {
            return projectGateway.GetProjectFiles(projectId);
        }

        // get project files for dropdown
        public List<SelectListItem> GetProjectFilesForDropDown(int projectId)
        {
            List<FileUrl> files = GetProjectFiles(projectId);

            List<SelectListItem> selectListItems =
                files.ConvertAll(f => new SelectListItem() {Text = f.FileName, Value = f.Id.ToString()});

            List<SelectListItem> mainSelectListItems = new List<SelectListItem>();
            mainSelectListItems.Add(new SelectListItem(){Text = "--Select one Project File which you want to Update", Value = 0.ToString()});

            mainSelectListItems.AddRange(selectListItems);
            return mainSelectListItems;
        }
 
        // get project info by project id
        public ProjectDetail GetProjectDetailsById(int projectId)
        {
            return projectGateway.GetInfoByProjectId(projectId);
        }

        // check is project exists by Id
        public bool IsProjectExistsById(int projectId)
        {
            return projectGateway.IsProjectExistsById(projectId);
        }

        // get project file by fileUrl id
        public FileUrl GetProjectFilesByFileUrlId(int id)
        {
            return projectGateway.GetProjectFilesByFileUrlId(id);
        }

        // update project details
        public int UpdateProjectDetails(ProjectDetail projectDetail)
        {
            if (projectGateway.IfAnyEntryExists(projectDetail))
            {
                return 2;
            }
            else
            {
                int rowsAffected = projectGateway.UpdateProjectDetails(projectDetail);

                if (rowsAffected > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        // update file url
        public bool UpdateFileUrl(FileUrl fileUrl)
        {
            int rowsAffected = projectGateway.UpdateFileUrl(fileUrl);

            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // assign user to project
        public string SaveAssignUser(AssignUser assignUser)
        {
            if (projectGateway.IsUserAlreadyAssigned(assignUser))
            {
                return "Already Assigned";
            }
            else
            {
                int rowsAffected = projectGateway.SaveAssignUser(assignUser);

                if (rowsAffected > 0)
                {
                    return "Assigned Successfull";
                }
                else
                {
                    return "Assigned Failed";
                }
            }
        }

        // get assigned user
        public List<AssignResourcePersonViewModel> GetAssignedUser()
        {
            return projectGateway.GetAssignedUser();
        }

        // get project involve
        public List<ProjectInvolveViewModel> GetProjectInvolved(int userId)
        {
            List<ProjectInvolveViewModel> models = projectGateway.GetAssignedProjectIdByUserId(userId);
            List<ProjectInvolveViewModel> newModels = new List<ProjectInvolveViewModel>();

            foreach (ProjectInvolveViewModel result in models)
            {
                result.NoOfMembers = projectGateway.TotalMembersById(result.ProjectId);
                result.ProjectShortName = GenerateSortName(result.ProjectName);
                result.NoOfTasks = taskGateway.GetTaskCountByProjectId(result.ProjectId);

                newModels.Add(result);
            }

            return newModels;
        } 

        // generate short name
        public string GenerateSortName(string projectFullName)
        {
            char[] projectName = projectFullName.ToCharArray();
            int count = 0;
            string shortName = null;

            for (int i = 0; i < projectName.Length; i++)
            {
                if (projectName[i] == ' ')
                {
                    count++;
                }
            }

            if (count > 1)
            {
                shortName = projectFullName.Substring(0, 1);

                for (int i = 0; i < projectName.Length; i++)
                {
                    if (projectName[i] == ' ' && i < (projectName.Length - 1))
                    {
                        shortName = shortName + projectName[i + 1];
                    }
                }
            }
            else if(count == 1)
            {
                shortName = projectFullName.Substring(0, 1);

                for (int i = 0; i < projectName.Length; i++)
                {
                    if (projectName[i] == ' ')
                    {
                        if ((projectName.Length - 1) >= 3)
                        {
                            shortName = shortName + projectName[i + 1] + projectName[i+2] +projectName[i+3];
                        }
                        else if ((projectName.Length - 1) == 2)
                        {
                            shortName = shortName + projectName[i + 1] + projectName[i + 2];
                        }
                        else if ((projectName.Length - 1) == 1)
                        {
                            shortName = shortName + projectName[i + 1];
                        }
                    }
                }
            }

            return shortName;
        }

        // get assigned user by project id for drop down
        public List<User> GetUserByProject(int projectId)
        {
            return projectGateway.GetAssignedUserByProjectId(projectId);
        } 

        // get assigned user by project id
        public List<UserViewModel> GetAssignedByProjectId(int projectId)
        {
            return projectGateway.GetAssignedByProjectId(projectId);
        }
    }
}