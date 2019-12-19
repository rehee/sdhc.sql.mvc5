namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_e2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.E2", "MMM", c => c.Int(nullable: false));
            AddColumn("dbo.E2", "MMM2", c => c.Int(nullable: false));
            AddColumn("dbo.E2", "MMM3", c => c.Int(nullable: false));
            AddColumn("dbo.E2", "MMM4", c => c.Int(nullable: false));
            AddColumn("dbo.E2", "Range", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.E2", "Range");
            DropColumn("dbo.E2", "MMM4");
            DropColumn("dbo.E2", "MMM3");
            DropColumn("dbo.E2", "MMM2");
            DropColumn("dbo.E2", "MMM");
        }
    }
}
