namespace LibraryMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedBooks : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.Books", "UpdatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Books", "CreatedDate", c => c.DateTime(nullable: false));
        }
    }
}
