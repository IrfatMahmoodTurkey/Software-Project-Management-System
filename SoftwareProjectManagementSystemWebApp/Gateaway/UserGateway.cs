using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoftwareProjectManagementSystemWebApp.Models;
using SoftwareProjectManagementSystemWebApp.Models.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace SoftwareProjectManagementSystemWebApp.Gateaway
{
    public class UserGateway:BaseGateway
    {
        // save user
        public int SaveUser(User user)
        {
            Context.Users.Add(user);
            return Context.SaveChanges();
        }

        // check any user exists
        public bool IsAnyUsersExists()
        {
            bool isUserExists = Context.Users.Any();
            return isUserExists;
        }

        // check is email exists
        public bool IsUserEmailExists(User user)
        {
            bool isExists = Context.Users.Any(u => u.Email == user.Email);
            return isExists;
        }

        // check is email exists
        public bool IsUserEmailExistsForAnotherUser(User user)
        {
            bool isExists = Context.Users.Any(u => u.Email == user.Email && u.Id != user.Id);
            return isExists;
        }

        // get all user
        public List<UserViewModel> GetAllUser()
        {
            var query = Context.Users.Include(u => u.Designation).Where(u=>u.Designation.Name != "IT Admin");
            List<UserViewModel> userViewModels = new List<UserViewModel>();

            foreach (var result in query)
            {
                UserViewModel model = new UserViewModel();

                model.Id = result.Id;
                model.Name = result.Name;
                model.Email = result.Email;
                model.Designation = result.Designation.Name;
                model.Status = result.Status;

                userViewModels.Add(model);
            }

            return userViewModels;
        }
 
        // update existing user
        public int UpdateUser(User user)
        {
            Context.Users.AddOrUpdate(user);
            return Context.SaveChanges();
        }

        // get user by id
        public User GetUserById(int id)
        {
            return Context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        // check is id exists
        public bool IsUserIdExists(int id)
        {
            return Context.Users.Any(u => u.Id == id);
        }

        
        // get designation by user id
        public string GetDesignationByUserId(int userId)
        {
            var query = Context.Users.Include(u => u.Designation).Where(u => u.Id == userId).FirstOrDefault();

            if (query == null)
            {
                return "";
            }
            else
            {
                return query.Designation.Name;
            }
        }
    }
}