using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoftwareProjectManagementSystemWebApp.Models
{
    public class FileUrl
    {
        public int Id { get; set; }
        [ForeignKey("ProjectDetail")]
        public int ProjectId { get; set; }
        public ProjectDetail ProjectDetail { get; set; }
        public string FileName { get; set; }
        public string FileLocation { get; set; }
        [ForeignKey("User")]
        public int ActionBy { get; set; }
        public User User { get; set; }
        public string ActionDone { get; set; }
        public string ActionTime { get; set; }
    }
}