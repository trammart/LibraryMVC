namespace LibraryMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modified_Phone_Mem : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Members", new[] { "Phone" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Members", "Phone", unique: true);
        }
    }
}
