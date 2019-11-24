namespace TrainingWebAPI.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initdata : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 30, unicode: false),
                        LastName = c.String(maxLength: 30, unicode: false),
                        Gender = c.String(maxLength: 1, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cast",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActorId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                        Role = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movie", t => t.MovieId)
                .ForeignKey("dbo.Actor", t => t.ActorId)
                .Index(t => t.ActorId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.Movie",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(maxLength: 150, unicode: false),
                        Year = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Director",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FirstName = c.String(maxLength: 30, unicode: false),
                        LastName = c.String(maxLength: 30, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rating",
                c => new
                    {
                        MovieId = c.Int(nullable: false),
                        ReviewerId = c.Int(nullable: false),
                        Score = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieId, t.ReviewerId })
                .ForeignKey("dbo.Reviewer", t => t.ReviewerId)
                .ForeignKey("dbo.Movie", t => t.MovieId)
                .Index(t => t.MovieId)
                .Index(t => t.ReviewerId);
            
            CreateTable(
                "dbo.Reviewer",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FullName = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
            CreateTable(
                "dbo.MovieDirector",
                c => new
                    {
                        DirectorId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DirectorId, t.MovieId })
                .ForeignKey("dbo.Director", t => t.DirectorId, cascadeDelete: true)
                .ForeignKey("dbo.Movie", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.DirectorId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.MovieGenre",
                c => new
                    {
                        GenreId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GenreId, t.MovieId })
                .ForeignKey("dbo.Genre", t => t.GenreId, cascadeDelete: true)
                .ForeignKey("dbo.Movie", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.GenreId)
                .Index(t => t.MovieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cast", "ActorId", "dbo.Actor");
            DropForeignKey("dbo.Rating", "MovieId", "dbo.Movie");
            DropForeignKey("dbo.Rating", "ReviewerId", "dbo.Reviewer");
            DropForeignKey("dbo.MovieGenre", "MovieId", "dbo.Movie");
            DropForeignKey("dbo.MovieGenre", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.MovieDirector", "MovieId", "dbo.Movie");
            DropForeignKey("dbo.MovieDirector", "DirectorId", "dbo.Director");
            DropForeignKey("dbo.Cast", "MovieId", "dbo.Movie");
            DropIndex("dbo.MovieGenre", new[] { "MovieId" });
            DropIndex("dbo.MovieGenre", new[] { "GenreId" });
            DropIndex("dbo.MovieDirector", new[] { "MovieId" });
            DropIndex("dbo.MovieDirector", new[] { "DirectorId" });
            DropIndex("dbo.Rating", new[] { "ReviewerId" });
            DropIndex("dbo.Rating", new[] { "MovieId" });
            DropIndex("dbo.Cast", new[] { "MovieId" });
            DropIndex("dbo.Cast", new[] { "ActorId" });
            DropTable("dbo.MovieGenre");
            DropTable("dbo.MovieDirector");
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Reviewer");
            DropTable("dbo.Rating");
            DropTable("dbo.Genre");
            DropTable("dbo.Director");
            DropTable("dbo.Movie");
            DropTable("dbo.Cast");
            DropTable("dbo.Actor");
        }
    }
}
