namespace MvcProjesi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Etikets",
                c => new
                    {
                        Etiket_Id = c.Int(nullable: false, identity: true),
                        Icerik = c.String(nullable: false, maxLength: 50),
                        Guncelleme_Id = c.Int(nullable: false),
                        GuncellemeTarih = c.DateTime(nullable: false),
                        OlusturmaTarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Etiket_Id);
            
            CreateTable(
                "dbo.Makales",
                c => new
                    {
                        Makale_Id = c.Int(nullable: false, identity: true),
                        Baslik = c.String(maxLength: 50),
                        Icerik = c.String(nullable: false, maxLength: 500),
                        Guncelleme_Id = c.Int(nullable: false),
                        GuncellemeTarih = c.DateTime(nullable: false),
                        OlusturmaTarih = c.DateTime(nullable: false),
                        Uye_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Makale_Id)
                .ForeignKey("dbo.Uyes", t => t.Uye_Id)
                .Index(t => t.Uye_Id);
            
            CreateTable(
                "dbo.Uyes",
                c => new
                    {
                        Uye_Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(nullable: false, maxLength: 50),
                        Soyad = c.String(nullable: false, maxLength: 50),
                        EPosta = c.String(nullable: false),
                        WebSite = c.String(),
                        ResimYol = c.String(),
                        Sifre = c.String(nullable: false),
                        Guncelleme_Id = c.Int(nullable: false),
                        GuncellemeTarih = c.DateTime(nullable: false),
                        OlusturmaTarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Uye_Id);
            
            CreateTable(
                "dbo.Yorums",
                c => new
                    {
                        Yorum_Id = c.Int(nullable: false, identity: true),
                        Icerik = c.String(nullable: false, maxLength: 150),
                        Guncelleme_Id = c.Int(nullable: false),
                        GuncellemeTarih = c.DateTime(nullable: false),
                        OlusturmaTarih = c.DateTime(nullable: false),
                        Makale_Id = c.Int(),
                        Uye_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Yorum_Id)
                .ForeignKey("dbo.Makales", t => t.Makale_Id)
                .ForeignKey("dbo.Uyes", t => t.Uye_Id)
                .Index(t => t.Makale_Id)
                .Index(t => t.Uye_Id);
            
            CreateTable(
                "dbo.MakaleEtikets",
                c => new
                    {
                        Makale_Id = c.Int(nullable: false),
                        Etiket_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Makale_Id, t.Etiket_Id })
                .ForeignKey("dbo.Makales", t => t.Makale_Id, cascadeDelete: true)
                .ForeignKey("dbo.Etikets", t => t.Etiket_Id, cascadeDelete: true)
                .Index(t => t.Makale_Id)
                .Index(t => t.Etiket_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Yorums", "Uye_Id", "dbo.Uyes");
            DropForeignKey("dbo.Yorums", "Makale_Id", "dbo.Makales");
            DropForeignKey("dbo.Makales", "Uye_Id", "dbo.Uyes");
            DropForeignKey("dbo.MakaleEtikets", "Etiket_Id", "dbo.Etikets");
            DropForeignKey("dbo.MakaleEtikets", "Makale_Id", "dbo.Makales");
            DropIndex("dbo.MakaleEtikets", new[] { "Etiket_Id" });
            DropIndex("dbo.MakaleEtikets", new[] { "Makale_Id" });
            DropIndex("dbo.Yorums", new[] { "Uye_Id" });
            DropIndex("dbo.Yorums", new[] { "Makale_Id" });
            DropIndex("dbo.Makales", new[] { "Uye_Id" });
            DropTable("dbo.MakaleEtikets");
            DropTable("dbo.Yorums");
            DropTable("dbo.Uyes");
            DropTable("dbo.Makales");
            DropTable("dbo.Etikets");
        }
    }
}
