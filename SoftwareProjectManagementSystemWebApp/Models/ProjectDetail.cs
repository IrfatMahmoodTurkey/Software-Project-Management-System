using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoftwareProjectManagementSystemWebApp.Models
{
    public class ProjectDetail
    {
        public int Id { get; set; }
        public string ProjectCode { get; set; }
        [Required(ErrorMessage = "Project Name is required")]
        public string ProjectName { get; set; }
        [Required(ErrorMessage = "Code Name is required")]
        public string CodeName { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Start Date is required")]
        public string StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required")]
        public string EndDate { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }
        [ForeignKey("User")]
        public int ActionBy { get; set; }
        public User User { get; set; }
        public string ActionDone { get; set; }
        public string ActionTime { get; set; }

        public List<FileUrl> FileUrls { get; set; }
        public List<AssignUser> AssignUsers { get; set; }
        public List<Task> Tasks { get; set; }
        public List<Comment> Comments { get; set; }
    }
}