namespace AccountingForTouristTrips.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserAndRoleTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ClientId = c.Int(nullable: false),
                        Login = c.String(),
                        PasswordHash = c.String(),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClientId)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Users", "ClientId", "dbo.Clients");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Users", new[] { "ClientId" });
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
        }
    }
}
