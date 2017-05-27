using CinemaAutomation.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaAutomation.Areas.Admin.ViewModels
{
    public class RoleCheckBox
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }

    }
    public class UsersIndex
    {
        public IEnumerable<User> Users { get; set; }
    }

    public class UsersNew
    {
        public IList<RoleCheckBox> Roles { get; set; }

        [Required]
        public string Username { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        [Required,DataType(DataType.EmailAddress),MaxLength(256)]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Tcno { get; set; }

    }

    public class UsersEdit
    {
        public IList<RoleCheckBox> Roles { get; set; }
        [Required]
        public string Username { get; set; }

        [Required, DataType(DataType.EmailAddress), MaxLength(256)]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Tcno { get; set; }

    }
    public class UsersResetPassword
    {

        public string Username { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

    }
}