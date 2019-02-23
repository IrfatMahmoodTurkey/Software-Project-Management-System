using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftwareProjectManagementSystemWebApp.Models.ViewModels
{
    public class ProjectDetailViewModel
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string CodeName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
        public string UploadedBy { get; set; }
    }
}