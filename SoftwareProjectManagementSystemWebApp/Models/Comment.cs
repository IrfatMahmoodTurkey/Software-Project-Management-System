using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoftwareProjectManagementSystemWebApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [ForeignKey("ProjectDetail")]
        [Required(ErrorMessage = "Project is required")]
        public int ProjectId { get; set; }
        public ProjectDetail ProjectDetail { get; set; }
        [ForeignKey("Task")]
        [Required(ErrorMessage = "Task is required")]
        public int TaskId { get; set; }
        public Task Task { get; set; }
        [Required(ErrorMessage = "Comment is required")]
        public string CommentDescription { get; set; }
        [ForeignKey("User")]
        [Required(ErrorMessage = "User is required")]
        public int ActionById { get; set; }
        public User User { get; set; }
        public string ActionTime { get; set; }
        public string ActionDone { get; set; }
    }
}