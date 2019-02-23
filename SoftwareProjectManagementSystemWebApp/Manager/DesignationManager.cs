using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoftwareProjectManagementSystemWebApp.Gateaway;
using SoftwareProjectManagementSystemWebApp.Models;

namespace SoftwareProjectManagementSystemWebApp.Manager
{
    public class DesignationManager
    {
        private DesignationGateway designationGateway;
        private UserGateway userGateway;

        public DesignationManager()
        {
            designationGateway = new DesignationGateway();
            userGateway = new UserGateway();
        }

        // get designation
        public List<Designation> GetDesignations()
        {
            if (userGateway.IsAnyUsersExists())
            {
                return designationGateway.GetAllDesignationsWithoutAdmin();
            }
            else
            {
                return designationGateway.GetDesignations();
            }
        }
 
        // get designation for drop down
        public List<SelectListItem> GetDesignationForDropDown()
        {
            List<Designation> designations = GetDesignations();

            List<SelectListItem> selectListItems =
                designations.ConvertAll(d => new SelectListItem() {Text = d.Name, Value = d.Id.ToString()});

            List<SelectListItem> mainSelectListItems = new List<SelectListItem>();
            mainSelectListItems.Add(new SelectListItem()
            {
                Text = "-- Select Designation --",
                Value = ""
            });

            mainSelectListItems.AddRange(selectListItems);
            return mainSelectListItems;
        } 
    }
}