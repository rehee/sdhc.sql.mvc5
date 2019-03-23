namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BaseSelects", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BaseSelects", "Discriminator");
        }
    }
}
