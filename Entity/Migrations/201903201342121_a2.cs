namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BaseSelects",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.S2", "Gender", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.S2", "Gender");
            DropTable("dbo.BaseSelects");
        }
    }
}
