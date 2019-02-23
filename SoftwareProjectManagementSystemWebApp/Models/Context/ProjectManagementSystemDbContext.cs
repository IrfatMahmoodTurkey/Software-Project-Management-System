using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SoftwareProjectManagementSystemWebApp.Models.Context
{
    public class ProjectManagementSystemDbContext:DbContext
    {
        public ProjectManagementSystemDbContext()
            : base("ProjectManagementSystemDbConString")
        {
        }

        public DbSet<Designation> Designations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProjectDetail> ProjectDetails { get; set; }
        public DbSet<FileUrl> FileUrls { get; set; }
        public DbSet<AssignUser> AssignUsers { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}