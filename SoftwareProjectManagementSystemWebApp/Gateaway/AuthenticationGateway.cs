using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using SoftwareProjectManagementSystemWebApp.Models;

namespace SoftwareProjectManagementSystemWebApp.Gateaway
{
    public class AuthenticationGateway:BaseGateway
    {
        //log in
        public bool LogIn(User user)
        {
            return Context.Users.Any(u => u.Email == user.Email && u.Password == user.Password);
        }

        // get user by email and password
        public User GetUserByEmailAndPassword(User user)
        {
            return Context.Users.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();
        }
    }
}