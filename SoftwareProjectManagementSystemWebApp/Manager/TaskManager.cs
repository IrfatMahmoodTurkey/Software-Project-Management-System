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
    public class TaskManager
    {
        private TaskGateway taskGateway;

        public TaskManager()
        {
            taskGateway = new TaskGateway();
        }

        // add task
        public string AddTask(Task task)
        {
            int rowsAffected = taskGateway.AddTask(task);

            if (rowsAffected > 0)
            {
                return "Assigned Task Successfully";
            }
            else
            {
                return "Assigned Task Failed";
            }
        }

        // get tasklist by project
        public List<TaskListViewModel> GetTasksByProjectId(int projectId)
        {
            return taskGateway.GetTasksByProjectId(projectId);
        }

        // get task by task id
        public TaskListViewModel GetTasksByTaskId(int taskId)
        {
            return taskGateway.GetTasksByTaskId(taskId);
        }

        // check task exists by Id
        public bool IsTaskExists(int taskId)
        {
            return taskGateway.IsTaskExists(taskId);
        }

        // update task
        public string UpdateTask(Task task)
        {
            int rowsAffected = taskGateway.UpdateTask(task);

            if (rowsAffected > 0)
            {
                return "Successfully Updated";
            }
            else
            {
                return "Update Failed";
            }
        }

        // get task by project id for drop down
        public List<SelectListItem> GetTaskByProjectIdForDropDown(int projectId)
        {
            List<TaskListViewModel> taskViewModels = GetTasksByProjectId(projectId);

            List<SelectListItem> selectListItems =
                taskViewModels.ConvertAll(
                    x => new SelectListItem() {Text = x.ProjectName, Value = x.ProjectId.ToString()});
            List<SelectListItem> mainSelectListItems = new List<SelectListItem>();
            mainSelectListItems.Add(new SelectListItem(){Text = "-- Select Project --", Value = ""});

            mainSelectListItems.AddRange(selectListItems);

            return mainSelectListItems;
        }
 
        // get project name by task id
        public string GetProjectNameByTaskId(int taskId)
        {
            return taskGateway.GetProjectNameByTaskId(taskId);
        }

        // get task by task id
        public string GetTaskNameByTaskId(int taskId)
        {
            return taskGateway.GetTaskNameByTaskId(taskId);
        }
    }
}