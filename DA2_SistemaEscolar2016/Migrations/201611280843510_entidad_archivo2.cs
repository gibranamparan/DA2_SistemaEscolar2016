namespace DA2_SistemaEscolar2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entidad_archivo2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Archivoes", "formatoContenido", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Archivoes", "formatoContenido");
        }
    }
}
