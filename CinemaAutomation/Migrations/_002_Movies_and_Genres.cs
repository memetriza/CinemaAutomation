using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CinemaAutomation.Migrations
{
    [Migration(2)]
    public class _002_Movies_and_Genres : Migration
    {
        public override void Down()
        {
            Delete.Table("movie_genre");
            Delete.Table("Genres");
            Delete.Table("Movies");
        }

        public override void Up()
        {
            Create.Table("Movies")
                .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("moviename").AsString()
                .WithColumn("moviedirector").AsString()
                .WithColumn("releasedate").AsDateTime()
                .WithColumn("summary").AsString()
                .WithColumn("created_at").AsDateTime()
                .WithColumn("updated_at").AsDateTime().Nullable()
                .WithColumn("deleted_at").AsDateTime().Nullable();

            Create.Table("Genres")
                .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("genrename").AsString(128);

            Create.Table("movie_genre")
                .WithColumn("movie_id").AsInt32().ForeignKey("Movies", "id").OnDelete(Rule.Cascade)
                .WithColumn("genre_id").AsInt32().ForeignKey("Genres", "id").OnDelete(Rule.Cascade);
        }
    }
}