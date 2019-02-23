using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftwareProjectManagementSystemWebApp.Utility
{
    public static class ActionUtility
    {
        public static string ActionInsert { get; set; }
        public static string ActionUpdate { get; set; }

        static ActionUtility()
        {
            ActionInsert = "INSERTED";
            ActionUpdate = "UPDATED";
        }
    }
}