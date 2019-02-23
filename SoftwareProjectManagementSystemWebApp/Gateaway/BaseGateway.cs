using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoftwareProjectManagementSystemWebApp.Models.Context;

namespace SoftwareProjectManagementSystemWebApp.Gateaway
{
    public class BaseGateway
    {
        public ProjectManagementSystemDbContext Context { get; set; }

        public BaseGateway()
        {
            Context = new ProjectManagementSystemDbContext();
        }
    }
}