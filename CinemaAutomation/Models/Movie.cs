using CinemaAutomation.Models;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaAutomation.Models
{
    public class Movie
    {
        public virtual int Id { get; set; }
        public virtual string MovieName { get; set; }
        //public virtual string Slug { get; set; }
        public virtual string MovieDirector { get; set; }
        public virtual DateTime ReleaseDate { get; set; }
        public virtual string Summary { get; set; }
        public virtual string LinkText { get; set; }

        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
        public virtual DateTime? DeletedAt { get; set; }

        public virtual bool IsDeleted { get { return DeletedAt != null; }}
        public virtual IList<Genre> Genres { get; set; }
        public Movie()
        {
            Genres = new List<Genre>();
        }
    }
    public class MovieMap : ClassMapping<Movie>
    {
        public MovieMap()
        {
            Table("movies");
            Id(x => x.Id, x => x.Generator(Generators.Identity));
            Property(x => x.MovieName, x => x.NotNullable(true));
            //Property(x => x.Slug, x => x.NotNullable(true));
            Property(x => x.MovieDirector, x => x.NotNullable(true));
            Property(x => x.ReleaseDate, x => x.NotNullable(true));
            Property(x => x.Summary, x => x.NotNullable(true));
            Property(x => x.LinkText, x => x.NotNullable(true));
            Property(x => x.CreatedAt, x =>
            {
                x.Column("created_at");
                x.NotNullable(true);

            });
            Property(x => x.UpdatedAt, x => x.Column("updated_at"));
            Property(x => x.DeletedAt, x => x.Column("deleted_at"));
            Bag(x => x.Genres, x =>{
                x.Table("movie_genre");
                x.Key(y => y.Column("movie_id"));                
            }, x => x.ManyToMany(y => y.Column("genre_id")));
        }
    }
}