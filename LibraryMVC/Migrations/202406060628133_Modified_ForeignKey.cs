namespace LibraryMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modified_ForeignKey : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Borrowings", "StaffId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Borrowings", "MemberId");
            CreateIndex("dbo.Borrowings", "StaffId");
            AddForeignKey("dbo.Borrowings", "MemberId", "dbo.Members", "MemberId", cascadeDelete: true);
            AddForeignKey("dbo.Borrowings", "StaffId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Borrowings", "StaffId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Borrowings", "MemberId", "dbo.Members");
            DropIndex("dbo.Borrowings", new[] { "StaffId" });
            DropIndex("dbo.Borrowings", new[] { "MemberId" });
            AlterColumn("dbo.Borrowings", "StaffId", c => c.String(nullable: false));
        }
    }
}
