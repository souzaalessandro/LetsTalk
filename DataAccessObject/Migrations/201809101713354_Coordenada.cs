namespace DataAccessObject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Coordenada : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Coordenadas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Usuarios", "UltimaCoordenada_ID", c => c.Int());
            CreateIndex("dbo.Usuarios", "UltimaCoordenada_ID");
            AddForeignKey("dbo.Usuarios", "UltimaCoordenada_ID", "dbo.Coordenadas", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuarios", "UltimaCoordenada_ID", "dbo.Coordenadas");
            DropIndex("dbo.Usuarios", new[] { "UltimaCoordenada_ID" });
            DropColumn("dbo.Usuarios", "UltimaCoordenada_ID");
            DropTable("dbo.Coordenadas");
        }
    }
}
