using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemaAutomation.ViewModels
{
    public class Account
    {
     
            [Required]
            public string Username { get; set; }
            [Required, DataType(DataType.Password)]
            public string Password { get; set; }
            [Required, DataType(DataType.EmailAddress), MaxLength(256)]
            public string Email { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            public string Surname { get; set; }
            [Required]
            public string Tcno { get; set; }

    }
    public class AccountEdit
    {
        public string Username { get; set; }

        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Tcno { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }

    }
}