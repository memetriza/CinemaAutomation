using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaAutomation.Models
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Email { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string Name { get; set; }
        public virtual string SurName { get; set; }
        public virtual string TcNo { get; set; }
        public virtual IList<Role> Roles { get; set; }
        public User()
        {
            Roles = new List<Role>();
        }
    }
    public class UserMap: ClassMapping<User>
    {
        public UserMap()
        {
            Table("users");

            Id(x => x.Id, x => x.Generator(Generators.Identity));
            Property(x => x.Username, x => x.NotNullable(true));
            Property(x => x.Email, x => x.NotNullable(true));
            Property(x => x.PasswordHash, x =>
             {
                 x.Column("password_hash");
                 x.NotNullable(true);
 
             });
            Property(x => x.Name, x => x.NotNullable(true));
            Property(x => x.SurName, x => x.NotNullable(true));
            Property(x => x.TcNo, x => x.NotNullable(true));
            Bag(x => x.Roles, x => {
                x.Table("role_users");
                x.Key(k => k.Column("user_id"));

            }, x => x.ManyToMany(k => k.Column("role_id")));
        }
    }
}