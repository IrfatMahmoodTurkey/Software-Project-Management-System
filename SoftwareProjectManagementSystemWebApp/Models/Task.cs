using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoftwareProjectManagementSystemWebApp.Models
{
    public class Task
    {
        public int Id { get; set; }
        [ForeignKey("ProjectDetail")]
        [Required(ErrorMessage = "Project is required")]
        public int ProjectId { get; set; }
        public ProjectDetail ProjectDetail { get; set; }
        
        [ForeignKey("AssignedTo")]
        [Required(ErrorMessage = "User is required")]
        public int AssignedToId { get; set; }
        public User AssignedTo { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Due Date is required")]
        public string DueDate { get; set; }
        [Required(ErrorMessage = "Priority is required")]
        public string Priority { get; set; }
        [ForeignKey("AssignedBy")]
        public int AssignedById { get; set; }
        public User AssignedBy { get; set; }
        public string ActionDone { get; set; }
        public string ActionTime { get; set; }
        public List<Comment> Comments { get; set; }
    }
}