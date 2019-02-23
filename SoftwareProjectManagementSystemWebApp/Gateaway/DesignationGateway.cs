using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoftwareProjectManagementSystemWebApp.Models;

namespace SoftwareProjectManagementSystemWebApp.Gateaway
{
    public class DesignationGateway:BaseGateway
    {
        // get all designations
        public List<Designation> GetDesignations()
        {
            return Context.Designations.Where(d=>d.Name == "IT Admin").ToList();
        } 

        // get all designations without ADMIN
        public List<Designation> GetAllDesignationsWithoutAdmin()
        {
            return Context.Designations.Where(d => d.Name != "IT Admin").ToList();
        }
    }
}