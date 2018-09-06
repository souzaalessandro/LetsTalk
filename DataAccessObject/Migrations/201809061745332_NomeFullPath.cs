namespace DataAccessObject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NomeFullPath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Diretorios", "PathRelativo", c => c.String());
            AddColumn("dbo.Usuarios", "PathFotoPerfil", c => c.String());
            DropColumn("dbo.Diretorios", "FullPath");
            DropColumn("dbo.Usuarios", "FullPathFotoPerfil");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuarios", "FullPathFotoPerfil", c => c.String());
            AddColumn("dbo.Diretorios", "FullPath", c => c.String());
            DropColumn("dbo.Usuarios", "PathFotoPerfil");
            DropColumn("dbo.Diretorios", "PathRelativo");
        }
    }
}
