namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContentIndexes", "ParentId", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContentIndexes", "ParentId", c => c.Guid(nullable: false));
        }
    }
}
