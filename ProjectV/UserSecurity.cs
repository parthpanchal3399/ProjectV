using ProjectV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectV
{
    public class UserSecurity
    {
        public static bool Login(string username, string password)
        {
            using(ProjectVEntities db = new ProjectVEntities())
            {
                User user = db.Users.Where(x => x.Username.Equals(username)).FirstOrDefault();
                if(user != null)
                {
                    if(user.Password == password)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}