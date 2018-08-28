namespace DataAccessObject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Primeira : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Diretorios",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FullPath = c.String(),
                        Usuario_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_ID)
                .Index(t => t.Usuario_ID);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Sobrenome = c.String(),
                        DataNascimento = c.DateTime(nullable: false),
                        Genero = c.Int(nullable: false),
                        Email = c.String(),
                        Hash = c.Binary(),
                        Salt = c.Binary(),
                        FraseApresentacao = c.String(),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TagUsuarios",
                c => new
                    {
                        Tag_ID = c.Int(nullable: false),
                        Usuario_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_ID, t.Usuario_ID })
                .ForeignKey("dbo.Tags", t => t.Tag_ID, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_ID, cascadeDelete: true)
                .Index(t => t.Tag_ID)
                .Index(t => t.Usuario_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagUsuarios", "Usuario_ID", "dbo.Usuarios");
            DropForeignKey("dbo.TagUsuarios", "Tag_ID", "dbo.Tags");
            DropForeignKey("dbo.Diretorios", "Usuario_ID", "dbo.Usuarios");
            DropIndex("dbo.TagUsuarios", new[] { "Usuario_ID" });
            DropIndex("dbo.TagUsuarios", new[] { "Tag_ID" });
            DropIndex("dbo.Diretorios", new[] { "Usuario_ID" });
            DropTable("dbo.TagUsuarios");
            DropTable("dbo.Tags");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Diretorios");
        }
    }
}
