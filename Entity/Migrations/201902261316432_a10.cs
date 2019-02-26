namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.B1",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.B2",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.B2");
            DropTable("dbo.B1");
        }
    }
}
