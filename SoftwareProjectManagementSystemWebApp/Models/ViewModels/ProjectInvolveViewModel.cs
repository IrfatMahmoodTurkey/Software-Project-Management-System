using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftwareProjectManagementSystemWebApp.Models.ViewModels
{
    public class ProjectInvolveViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectShortName { get; set; }
        public string Status { get; set; }
        public int NoOfMembers { get; set; }
        public int NoOfTasks { get; set; }
    }
}