namespace DataAccessObject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropriedadeFotoClasseUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "FullPathFotoPerfil", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuarios", "FullPathFotoPerfil");
        }
    }
}
