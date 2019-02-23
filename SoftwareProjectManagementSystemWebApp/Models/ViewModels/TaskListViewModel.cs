using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftwareProjectManagementSystemWebApp.Models.ViewModels
{
    public class TaskListViewModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int AssignedToId { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public string Priority { get; set; }
        public string AssignedBy { get; set; }
        public string DueDate { get; set; }
    }
}