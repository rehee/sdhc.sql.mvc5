namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContentIndexes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ModelId = c.Long(nullable: false),
                        ParentId = c.Guid(nullable: false),
                        PageURL = c.String(),
                        FullType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ContentIndexes");
        }
    }
}
