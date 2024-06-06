namespace LibraryMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Member_MemberType_BorrowDetail_Borrowing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BorrowDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BorrowId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Borrowings", t => t.BorrowId, cascadeDelete: true)
                .Index(t => t.BorrowId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Borrowings",
                c => new
                    {
                        BorrowId = c.Int(nullable: false, identity: true),
                        MemberId = c.String(nullable: false),
                        StaffId = c.String(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BorrowId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 80),
                        Birth = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false),
                        ExpireDate = c.DateTime(nullable: false),
                        MemberTypeId = c.Int(nullable: false),
                        Address = c.String(nullable: false, maxLength: 50),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MemberId);
            
            CreateTable(
                "dbo.MemberTypes",
                c => new
                    {
                        TypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Fee = c.Long(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.TypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BorrowDetails", "BorrowId", "dbo.Borrowings");
            DropForeignKey("dbo.BorrowDetails", "BookId", "dbo.Books");
            DropIndex("dbo.BorrowDetails", new[] { "BookId" });
            DropIndex("dbo.BorrowDetails", new[] { "BorrowId" });
            DropTable("dbo.MemberTypes");
            DropTable("dbo.Members");
            DropTable("dbo.Borrowings");
            DropTable("dbo.BorrowDetails");
        }
    }
}
