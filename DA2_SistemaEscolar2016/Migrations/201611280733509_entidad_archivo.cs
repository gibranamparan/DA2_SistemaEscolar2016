namespace DA2_SistemaEscolar2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entidad_archivo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Archivoes",
                c => new
                    {
                        archivoID = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                        tipo = c.String(),
                        contenido = c.Binary(),
                        noMatricula = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.archivoID)
                .ForeignKey("dbo.Alumnoes", t => t.noMatricula, cascadeDelete: true)
                .Index(t => t.noMatricula);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Archivoes", "noMatricula", "dbo.Alumnoes");
            DropIndex("dbo.Archivoes", new[] { "noMatricula" });
            DropTable("dbo.Archivoes");
        }
    }
}
