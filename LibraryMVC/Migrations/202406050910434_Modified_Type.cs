namespace LibraryMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modified_Type : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Borrowings", "MemberId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Borrowings", "MemberId", c => c.String(nullable: false));
        }
    }
}
