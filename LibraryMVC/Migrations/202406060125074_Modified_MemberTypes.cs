namespace LibraryMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modified_MemberTypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberTypes", "MaxBooks", c => c.Int(nullable: false));
            AddColumn("dbo.MemberTypes", "MaxDays", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MemberTypes", "MaxDays");
            DropColumn("dbo.MemberTypes", "MaxBooks");
        }
    }
}
