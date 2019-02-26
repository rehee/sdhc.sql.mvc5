namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.E3", "TList", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.E3", "TList");
        }
    }
}
