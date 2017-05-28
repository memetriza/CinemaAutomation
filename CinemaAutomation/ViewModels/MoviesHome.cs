using CinemaAutomation.Infrastructure;
using CinemaAutomation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaAutomation.ViewModels
{
    public class MoviesIndex
    {
        public PagedData<Movie> Movies { get; set; }
    }
}