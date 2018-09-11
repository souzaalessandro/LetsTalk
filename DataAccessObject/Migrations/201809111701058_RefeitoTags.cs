namespace DataAccessObject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefeitoTags : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TagUsuarios", "Tag_ID", "dbo.Tags");
            DropForeignKey("dbo.TagUsuarios", "Usuario_ID", "dbo.Usuarios");
            DropIndex("dbo.TagUsuarios", new[] { "Tag_ID" });
            DropIndex("dbo.TagUsuarios", new[] { "Usuario_ID" });
            AddColumn("dbo.Usuarios", "Tags", c => c.String());
            DropTable("dbo.Tags");
            DropTable("dbo.TagUsuarios");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TagUsuarios",
                c => new
                    {
                        Tag_ID = c.Int(nullable: false),
                        Usuario_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_ID, t.Usuario_ID });
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.Usuarios", "Tags");
            CreateIndex("dbo.TagUsuarios", "Usuario_ID");
            CreateIndex("dbo.TagUsuarios", "Tag_ID");
            AddForeignKey("dbo.TagUsuarios", "Usuario_ID", "dbo.Usuarios", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TagUsuarios", "Tag_ID", "dbo.Tags", "ID", cascadeDelete: true);
        }
    }
}
