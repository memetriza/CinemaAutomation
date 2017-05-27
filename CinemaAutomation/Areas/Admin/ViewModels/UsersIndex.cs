using CinemaAutomation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaAutomation.Areas.Admin.ViewModels
{
    public class UsersIndex
    {
        public IEnumerable<User> Users { get; set; }
    }
}