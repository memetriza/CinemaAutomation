using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaAutomation.Models
{
    public class Saloon
    {
        public virtual int Id { get; set; }
        public virtual string  SaloonName { get; set; }
        public virtual int SaloonCapacity { get; set; }
        public virtual IList<Seance> Seance { get; set; }
    }
    public class SaloonMap : ClassMapping<Saloon>
    {
        public SaloonMap()
        {
            Table("saloon");
            Id(x => x.Id, x => x.Generator(Generators.Identity));
            Property(x => x.SaloonName, x => x.NotNullable(true));
            Property(x => x.SaloonName, x => x.NotNullable(true));
            //Bag(x => x.Seance, x => {
            //    x.Table("ticket")
           // })
        }
    }
}