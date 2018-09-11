namespace DataAccessObject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CoordenadaNoUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Usuarios", "UltimaCoordenada_ID", "dbo.Coordenadas");
            DropIndex("dbo.Usuarios", new[] { "UltimaCoordenada_ID" });
            AddColumn("dbo.Usuarios", "Latitude", c => c.Double());
            AddColumn("dbo.Usuarios", "Longitude", c => c.Double());
            DropColumn("dbo.Usuarios", "UltimaCoordenada_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuarios", "UltimaCoordenada_ID", c => c.Int());
            DropColumn("dbo.Usuarios", "Longitude");
            DropColumn("dbo.Usuarios", "Latitude");
            CreateIndex("dbo.Usuarios", "UltimaCoordenada_ID");
            AddForeignKey("dbo.Usuarios", "UltimaCoordenada_ID", "dbo.Coordenadas", "ID");
        }
    }
}
