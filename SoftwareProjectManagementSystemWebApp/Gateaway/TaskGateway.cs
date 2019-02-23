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
    public class TaskGateway:BaseGateway
    {
        // add task
        public int AddTask(Task task)
        {
            Context.Tasks.Add(task);
            int rowsAffected = Context.SaveChanges();
            return rowsAffected;
        }
 
        // get tasklist by project
        public List<TaskListViewModel> GetTasksByProjectId(int projectId)
        {
            var query = Context.Tasks.Include(t => t.AssignedBy).Include(t => t.AssignedTo).Where(t=>t.ProjectId == projectId);
            List<TaskListViewModel> models = new List<TaskListViewModel>();

            foreach (var result in query)
            {
                TaskListViewModel model = new TaskListViewModel();

                model.Id = result.Id;
                model.Description = result.Description;
                model.AssignedTo = result.AssignedTo.Name;
                model.AssignedBy = result.AssignedBy.Name;
                model.Priority = result.Priority;
                model.DueDate = result.DueDate;

                models.Add(model);
            }

            return models;
        }
 
        // get task by task id
        public TaskListViewModel GetTasksByTaskId(int taskId)
        {
            var query = Context.Tasks.Include(t=>t.ProjectDetail).Include(t => t.AssignedBy).Include(t => t.AssignedTo).Where(t=>t.Id == taskId);
            TaskListViewModel model = new TaskListViewModel();

            foreach (var result in query)
            {
                model.Id = result.Id;
                model.ProjectId = result.ProjectId;
                model.ProjectName = result.ProjectDetail.ProjectName;
                model.AssignedToId = result.AssignedToId;
                model.Description = result.Description;
                model.AssignedTo = result.AssignedTo.Name;
                model.AssignedBy = result.AssignedBy.Name;
                model.Priority = result.Priority;
                model.DueDate = result.DueDate;
            }

            return model;
        }

        // check task exists by Id
        public bool IsTaskExists(int taskId)
        {
            return Context.Tasks.Any(t => t.Id == taskId);
        }

        // update task
        public int UpdateTask(Task task)
        {
            Context.Tasks.AddOrUpdate(task);
            return Context.SaveChanges();
        }

        // get project name by task id
        public string GetProjectNameByTaskId(int taskId)
        {
            var query = Context.Tasks.Include(t => t.ProjectDetail).Where(t => t.Id == taskId).FirstOrDefault();

            return query.ProjectDetail.ProjectName;
        }

        // get task by task id
        public string GetTaskNameByTaskId(int taskId)
        {
            var query = Context.Tasks.Where(t => t.Id == taskId).FirstOrDefault();
            return query.Description;
        }

        // get count task by project id
        public int GetTaskCountByProjectId(int projectId)
        {
            return Context.Tasks.Where(t => t.ProjectId == projectId).Count();
        }
    }
}