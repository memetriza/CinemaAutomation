using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentMigrator;
using System.Data;

namespace CinemaAutomation.Migrations
{
    [Migration(3)]
    public class _003_Saloon_Seance_Ticket : Migration
    {
        public override void Down()
        {
            Delete.Table("ticket");
            Delete.Table("seance");
            Delete.Table("saloon");
        }

        public override void Up()
        {
            Create.Table("saloon")
                .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("saloonname").AsString()
                .WithColumn("salooncapacity").AsInt32();

            Create.Table("seance")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("seancetime").AsInt32();

            Create.Table("ticket")
                .WithColumn("saloon_id").AsInt32().ForeignKey("saloon", "id").OnDelete(Rule.Cascade)
                .WithColumn("seance_id").AsInt32().ForeignKey("seance", "id").OnDelete(Rule.Cascade);
        }
    }
}