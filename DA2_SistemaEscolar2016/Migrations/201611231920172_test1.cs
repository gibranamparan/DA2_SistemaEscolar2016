namespace DA2_SistemaEscolar2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clases", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Clases", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Clases", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clases", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Clases", "ApplicationUser_Id");
            AddForeignKey("dbo.Clases", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
