using CinemaAutomation.Infrastructure;
using CinemaAutomation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemaAutomation.Areas.Admin.ViewModels
{
    public class GenreCheckBox
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }

    }
    public class MoviesIndex
    {
        public PagedData<Movie> Movies { get; set; }
    }
    public class MoviesForm
    {
        public bool IsNew { get; set; }
        public int? MovieId { get; set; }

        [Required, MaxLength(128)]
        public string MovieName { get; set; }

        [Required, MaxLength(128)]
        public string MovieDirector { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime ReleaseDate { get; set; }
        [Required,DataType(DataType.Url)]
        public string LinkText { get; set; }
        [Required, DataType(DataType.MultilineText)]
        public string Summary { get; set; }
    }
}