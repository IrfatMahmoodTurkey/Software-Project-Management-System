using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoftwareProjectManagementSystemWebApp.Models
{
    public class AssignUser
    {
        public int Id { get; set; }
        [ForeignKey("ProjectDetail")]
        [Required(ErrorMessage = "Project is required")]
        public int ProjectId { get; set; }
        public ProjectDetail ProjectDetail { get; set; }
        [ForeignKey("AssignedUserTo")]
        [Required(ErrorMessage = "User is required")]
        public int UserId { get; set; }
        public User AssignedUserTo { get; set; }
        [ForeignKey("AssignedUserBy")]
        [Required(ErrorMessage = "User is required")]
        public int ActionBy { get; set; }
        public User AssignedUserBy { get; set; }
        public string ActionDone { get; set; }
        public string ActionTime { get; set; }
    }
}