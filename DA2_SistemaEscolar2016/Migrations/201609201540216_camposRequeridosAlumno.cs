namespace DA2_SistemaEscolar2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class camposRequeridosAlumno : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Alumnoes", "nombre", c => c.String(nullable: false));
            AlterColumn("dbo.Alumnoes", "apellidoP", c => c.String(nullable: false));
            AlterColumn("dbo.Alumnoes", "apellidoM", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Alumnoes", "apellidoM", c => c.String());
            AlterColumn("dbo.Alumnoes", "apellidoP", c => c.String());
            AlterColumn("dbo.Alumnoes", "nombre", c => c.String());
        }
    }
}
