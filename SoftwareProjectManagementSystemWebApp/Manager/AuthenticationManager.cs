using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoftwareProjectManagementSystemWebApp.Gateaway;
using SoftwareProjectManagementSystemWebApp.Models;

namespace SoftwareProjectManagementSystemWebApp.Manager
{
    public class AuthenticationManager
    {
        private AuthenticationGateway authenticationGateway;
        private UserGateway userGateway;
        public AuthenticationManager()
        {
            authenticationGateway = new AuthenticationGateway();
            userGateway = new UserGateway();
        }

        //log in
        public bool LogIn(User user)
        {
            // check user exists
            if (authenticationGateway.LogIn(user))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // get userid by email and password
        public int GetUserByEmailAndPassword(User user)
        {
            return authenticationGateway.GetUserByEmailAndPassword(user).Id;
        }

        // check designation by user id
        public int CheckDesignation(int userId)
        {
            string designation = userGateway.GetDesignationByUserId(userId);

            if (designation.Equals(""))
            {
                return 5;
            }
            else
            {
                if (designation == "IT Admin")
                {
                    return 1;
                }
                else if (designation == "Project Manager")
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
        }
    }
}