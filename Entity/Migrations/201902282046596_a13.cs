namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.E4", "Lll", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.E4", "Lll");
        }
    }
}
