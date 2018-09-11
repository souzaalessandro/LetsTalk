namespace DataAccessObject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrecisaoSeisDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuarios", "Latitude", c => c.Decimal(precision: 18, scale: 6));
            AlterColumn("dbo.Usuarios", "Longitude", c => c.Decimal(precision: 18, scale: 6));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuarios", "Longitude", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Usuarios", "Latitude", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
