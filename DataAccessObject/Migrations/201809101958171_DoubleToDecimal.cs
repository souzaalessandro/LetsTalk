namespace DataAccessObject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoubleToDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuarios", "Latitude", c => c.Decimal(precision: 18, scale: 6));
            AlterColumn("dbo.Usuarios", "Longitude", c => c.Decimal(precision: 18, scale: 6));
            DropTable("dbo.Coordenadas");
        }
        
        public override void Down()
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
            
            AlterColumn("dbo.Usuarios", "Longitude", c => c.Double());
            AlterColumn("dbo.Usuarios", "Latitude", c => c.Double());
        }
    }
}
