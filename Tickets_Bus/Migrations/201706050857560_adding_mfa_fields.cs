namespace Tickets_Bus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adding_mfa_fields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CountryCode", c => c.String());
            AddColumn("dbo.AspNetUsers", "Phone", c => c.String());
            AddColumn("dbo.AspNetUsers", "PIN", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PIN");
            DropColumn("dbo.AspNetUsers", "Phone");
            DropColumn("dbo.AspNetUsers", "CountryCode");
        }
    }
}
