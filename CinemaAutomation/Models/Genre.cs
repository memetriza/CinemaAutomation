using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaAutomation.Models
{
    public class Genre
    {
        public virtual int Id { get; set; }
        public virtual string GenreName { get; set; }
        /*public virtual IList<Movie> Movies { get; set; }
        public Genre()
        {
            Movies = new List<Movie>();
        }*/
    }
    public class GenreMap : ClassMapping<Genre>
    {
        public GenreMap()
        {
            Table("genres");
            Id(x => x.Id, x => x.Generator(Generators.Identity));
            Property(x => x.GenreName, x => x.NotNullable(true));

            /*Bag(x => x.Movies, x =>{
                x.Table("movie_genre");
                x.Key(y => y.Column("genre_id"));
                
            }, x => x.ManyToMany(y => y.Column("movie_id")));*/

        }
    }
}