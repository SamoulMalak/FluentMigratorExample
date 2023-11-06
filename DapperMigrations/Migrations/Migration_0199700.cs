using FluentMigrator;
using FluentMigrator.SqlServer;

namespace DapperMigrations.Migrations
{
    [Migration(0199700)]
    public class Migration_0199700 : Migration
    {
        public override void Down()
        {
            Delete.Table("Authors");
            Delete.Index("IX_Authors_Id");
            Delete.Table("Books");
            Delete.Index("IX_Books_Id");
        }

        public override void Up()
        {
            Create.Table("Authors")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                 .WithColumn("Name").AsString().NotNullable();

            Create.Index("IX_Authors_Id").OnTable("Authors").OnColumn("Id");


            Create.Table("Books")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                 .WithColumn("Name").AsString().Nullable()
                 .WithColumn("Description").AsString().Nullable()
                 .WithColumn("Rate").AsInt16().Nullable()
                 .WithColumn("AuthorId").AsInt32().ForeignKey("Authors", "Id");

            Create.Index("IX_Books_Id").OnTable("Books").OnColumn("Id");
        }
    }
}
