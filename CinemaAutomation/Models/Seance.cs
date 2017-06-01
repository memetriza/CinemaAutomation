using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaAutomation.Models
{
    public class Seance
    {
        public virtual int Id { get; set; }
        public virtual int SeanceTime { get; set; }
    }
    public class SeanceMap : ClassMapping<Seance>
    {
        public SeanceMap()
        {
            Table("seance");
            Id(x => x.Id, x => x.Generator(Generators.Identity));
            Property(x => x.SeanceTime, x => x.NotNullable(true));
        }
    }
}