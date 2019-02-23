using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoftwareProjectManagementSystemWebApp.Gateaway;
using SoftwareProjectManagementSystemWebApp.Models;
using SoftwareProjectManagementSystemWebApp.Models.ViewModels;

namespace SoftwareProjectManagementSystemWebApp.Manager
{
    public class UserManager
    {
        private UserGateway userGateway;

        public UserManager()
        {
            userGateway = new UserGateway();
        }

        // save user
        public int SaveUser(User user)
        {
            if (userGateway.IsUserEmailExists(user))
            {
                return 2;
            }
            else
            {
                int rowsAffected = userGateway.SaveUser(user);

                if (rowsAffected > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        // get all user
        public List<UserViewModel> GetAllUsers()
        {
            return userGateway.GetAllUser();
        }

        // get all user for dropdown
        public List<SelectListItem> GetAllUsersFromDropDown()
        {
            List<UserViewModel> models = GetAllUsers();

            List<SelectListItem> selectListItems =
                models.ConvertAll(c => new SelectListItem() {Text = c.Name, Value = c.Id.ToString()});

            List<SelectListItem> mainSelectListItems = new List<SelectListItem>();
            mainSelectListItems.Add(new SelectListItem(){Text = " -- Select Resource Person --", Value = ""});

            mainSelectListItems.AddRange(selectListItems);
            return mainSelectListItems;
        } 
 
        // reset password
        public int ResetPassword(int id)
        {
            User user = userGateway.GetUserById(id);

            user.Password = user.Email + "123";

            int rowsAffected = userGateway.UpdateUser(user);

            if (rowsAffected > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        // check user exists
        public bool IsUserExists(int id)
        {
            return userGateway.IsUserIdExists(id);
        }

        // get user by id
        public User GetUserById(int id)
        {
            return userGateway.GetUserById(id);
        }

        // save user
        public int UpdateUser(User user)
        {
            if (userGateway.IsUserEmailExistsForAnotherUser(user))
            {
                return 2;
            }
            else
            {
                int rowsAffected = userGateway.UpdateUser(user);

                if (rowsAffected > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        // get designation by user id
        public string GetDesignationByUserId(int userId)
        {
            return userGateway.GetDesignationByUserId(userId);
        }
    }
}