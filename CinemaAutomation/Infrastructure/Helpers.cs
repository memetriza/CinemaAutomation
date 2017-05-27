using CinemaAutomation.Models;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaAutomation.Infrastructure
{
    public class Helpers
    {
        public static int GetUsersId(string username)
        {
            var user = Database.Session.Query<User>().FirstOrDefault(u => u.Username == username);
            return user.Id;
        }
    }
}