namespace LibraryMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modified_Member : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Total");
        }
    }
}
