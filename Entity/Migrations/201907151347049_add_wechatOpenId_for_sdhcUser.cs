namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_wechatOpenId_for_sdhcUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "WeChatOpenId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "WeChatOpenId");
        }
    }
}
