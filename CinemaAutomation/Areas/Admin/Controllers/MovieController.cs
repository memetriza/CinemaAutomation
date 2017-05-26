﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaAutomation.Areas.Admin.Controllers
{
    [Authorize(Roles ="admin")]
    public class MovieController : Controller
    {
        // GET: Admin/Movie
        public ActionResult Index()
        {
            return Content("Admin area movie controller index action");
        }
    }
}