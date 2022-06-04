namespace School_management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Services : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceId)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.SubjectId)
                .Index(t => t.TeacherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Services", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Services", "StudentId", "dbo.Students");
            DropIndex("dbo.Services", new[] { "TeacherId" });
            DropIndex("dbo.Services", new[] { "SubjectId" });
            DropIndex("dbo.Services", new[] { "StudentId" });
            DropTable("dbo.Services");
        }
    }
}
