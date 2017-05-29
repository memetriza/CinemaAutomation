using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentMigrator;

namespace CinemaAutomation.Migrations
{
    public class _003_AddSlugToMovie : Migration
    {
        public override void Down()
        {
            Delete.Column("slug").FromTable("movies");
        }

        public override void Up()
        {
            Create.Column("slug").OnTable("movies").AsString(128);
        }
    }
}