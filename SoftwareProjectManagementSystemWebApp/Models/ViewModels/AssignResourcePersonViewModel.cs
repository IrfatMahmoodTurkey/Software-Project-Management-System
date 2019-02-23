using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftwareProjectManagementSystemWebApp.Models.ViewModels
{
    public class AssignResourcePersonViewModel
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ResourceName { get; set; }
        public string Designation { get; set; }
    }
}