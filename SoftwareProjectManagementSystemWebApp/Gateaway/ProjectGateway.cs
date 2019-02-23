using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoftwareProjectManagementSystemWebApp.Models;
using SoftwareProjectManagementSystemWebApp.Models.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace SoftwareProjectManagementSystemWebApp.Gateaway
{
    public class ProjectGateway:BaseGateway
    {
        //save project
        public int SaveProject(ProjectDetail projectDetail)
        {
            Context.ProjectDetails.Add(projectDetail);
            return Context.SaveChanges();
        }

        // check if any same entry
        public bool IfAnyEntryExists(ProjectDetail projectDetail)
        {
            return
                Context.ProjectDetails.Any(
                    p =>
                        p.ProjectName == projectDetail.ProjectName &&
                        p.CodeName == projectDetail.CodeName &&
                        p.StartDate == projectDetail.StartDate && p.EndDate == projectDetail.EndDate &&
                        p.Status == projectDetail.Status && p.Id != projectDetail.Id);
        }

        // get project info by id
        public ProjectDetail GetInfoByProjectId(int projectId)
        {
            return Context.ProjectDetails.Where(p => p.Id == projectId).FirstOrDefault();
        }

        // get project info by guid
        public ProjectDetail GetProjectInfoByOtherInfo(ProjectDetail projectDetail)
        {
            return Context.ProjectDetails.Where(
                    p =>
                        p.ProjectName == projectDetail.ProjectName &&
                        p.CodeName == projectDetail.CodeName &&
                        p.StartDate == projectDetail.StartDate && p.EndDate == projectDetail.EndDate &&
                        p.Status == projectDetail.Status).FirstOrDefault();
        }

        // save project url on FileUrl
        public int UploadFileUrl(FileUrl fileUrl)
        {
            Context.FileUrls.Add(fileUrl);
            return Context.SaveChanges();
        }

        // get all project
        public List<ProjectDetailViewModel> GetAllProjects()
        {
            var query = Context.ProjectDetails.Include(p => p.User);
            List<ProjectDetailViewModel> viewModels = new List<ProjectDetailViewModel>();

            foreach (var result in query)
            {
                ProjectDetailViewModel model = new ProjectDetailViewModel();

                model.Id = result.Id;
                model.ProjectName = result.ProjectName;
                model.CodeName = result.CodeName;
                model.StartDate = result.StartDate;
                model.EndDate = result.EndDate;
                model.Status = result.Status;
                model.UploadedBy = result.User.Name;

                viewModels.Add(model);
            }

            return viewModels;
        }
 
        // get project files
        public List<FileUrl> GetProjectFiles(int projectId)
        {
            return Context.FileUrls.Where(f=>f.ProjectId == projectId).ToList();
        }

        // get project file by fileUrl id
        public FileUrl GetProjectFilesByFileUrlId(int id)
        {
            return Context.FileUrls.Where(f => f.Id == id).FirstOrDefault();
        }
 
        // check is project exists by Id
        public bool IsProjectExistsById(int projectId)
        {
            return Context.ProjectDetails.Any(p => p.Id == projectId);
        }

        // update project details
        public int UpdateProjectDetails(ProjectDetail projectDetail)
        {
            Context.ProjectDetails.AddOrUpdate(projectDetail);
            return Context.SaveChanges();
        }

        // update file url
        public int UpdateFileUrl(FileUrl fileUrl)
        {
            Context.FileUrls.AddOrUpdate(fileUrl);
            return Context.SaveChanges();
        }

        // assign user to project
        public int SaveAssignUser(AssignUser assignUser)
        {
            Context.AssignUsers.Add(assignUser);
            return Context.SaveChanges();
        }

        // check user already exists or not
        public bool IsUserAlreadyAssigned(AssignUser assignUser)
        {
            return Context.AssignUsers.Any(a => a.ProjectId == assignUser.ProjectId && a.UserId == assignUser.UserId);
        }

        // get assigned user
        public List<AssignResourcePersonViewModel> GetAssignedUser()
        {
            var query = Context.AssignUsers.Include(a => a.ProjectDetail).Include(a => a.AssignedUserTo).Select(s=> new
            {
                Id = s.Id,
                ProjectName = s.ProjectDetail.ProjectName,
                ResourceName = s.AssignedUserTo.Name,
                Designation = s.AssignedUserTo.Designation.Name
            });

            List<AssignResourcePersonViewModel> models = new List<AssignResourcePersonViewModel>();

            foreach (var result in query)
            {
                AssignResourcePersonViewModel model = new AssignResourcePersonViewModel();

                model.Id = result.Id;
                model.ProjectName = result.ProjectName;
                model.ResourceName = result.ResourceName;
                model.Designation = result.Designation;

                models.Add(model);
            }

            return models;
        }
 
        // get assigned project by user id
        public List<ProjectInvolveViewModel> GetAssignedProjectIdByUserId(int userId)
        {
            var query = Context.AssignUsers.Include(a => a.ProjectDetail).Where(a => a.UserId == userId);
            List<ProjectInvolveViewModel> models = new List<ProjectInvolveViewModel>();

            foreach (var result in query)
            {
                ProjectInvolveViewModel model = new ProjectInvolveViewModel();

                model.ProjectId = result.ProjectId;
                model.ProjectName = result.ProjectDetail.ProjectName;
                model.Status = result.ProjectDetail.Status;

                models.Add(model);
            }

            return models;
        }
 
        // get total no of members
        public int TotalMembersById(int projectId)
        {
            int totalNo = Context.AssignUsers.Where(a => a.ProjectId == projectId).Count();
            return totalNo;
        }

        // get assigned user by project id
        public List<User> GetAssignedUserByProjectId(int projectId)
        {
            var query = Context.AssignUsers.Include(a => a.AssignedUserTo).Where(a => a.ProjectId == projectId);

            List<User> users = new List<User>();

            foreach (var result in query)
            {
                User user = new User();

                user.Id = result.AssignedUserTo.Id;
                user.Name = result.AssignedUserTo.Name;

                users.Add(user);
            }

            return users;
        }
 
        // get assigned user by project id
        public List<UserViewModel> GetAssignedByProjectId(int projectId)
        {
            var query = Context.AssignUsers.Include(a => a.AssignedUserTo).Where(a => a.ProjectId == projectId).Select(x=>new
            {
                Id = x.AssignedUserTo.Id,
                Name = x.AssignedUserTo.Name,
                Designation = x.AssignedUserTo.Designation.Name
            });

            List<UserViewModel> users = new List<UserViewModel>();

            foreach (var result in query)
            {
                UserViewModel user = new UserViewModel();

                user.Id = result.Id;
                user.Name = result.Name;
                user.Designation = result.Designation;

                users.Add(user);
            }

            return users;
        }
    }
}