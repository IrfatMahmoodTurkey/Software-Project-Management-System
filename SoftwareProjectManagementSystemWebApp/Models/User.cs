using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoftwareProjectManagementSystemWebApp.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name must be in 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [StringLength(100, ErrorMessage = "Email must be in 100 characters")]
        [EmailAddress(ErrorMessage = "Email must be in valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password must be in 100 characters")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Status is required")]
        [StringLength(10, ErrorMessage = "Status must be in 10 characters")]
        public string Status { get; set; }
        [ForeignKey("Designation")]
        [Required(ErrorMessage = "Designation is required")]
        public int DesignationId { get; set; }
        public Designation Designation { get; set; }
        public string ActionBy { get; set; }
        public string ActionDone { get; set; }
        public string ActionTime { get; set; }
        public List<ProjectDetail> ProjectDetails { get; set; }
        public List<FileUrl> FileUrls { get; set; }
        [InverseProperty("AssignedUserTo")]
        public List<AssignUser> AssignTo { get; set; }
        [InverseProperty("AssignedUserBy")]
        public List<AssignUser> AssignBy { get; set; }
        [InverseProperty("AssignedTo")]
        public List<Task> TaskUser { get; set; }
        [InverseProperty("AssignedBy")]
        public List<Task> TaskAction { get; set; }
        public List<Comment> Comments { get; set; }
    }
}