namespace School_management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Subject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectId = c.Int(nullable: false, identity: true),
                        SubjectName = c.String(),
                        SubjectFees = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubjectId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Subjects");
        }
    }
}
