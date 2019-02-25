namespace Entity.Migrations
{
  using System;
  using System.Data.Entity.Migrations;

  public partial class a4 : DbMigration
  {
    public override void Up()
    {
      RenameTable(name: "dbo.E1", newName: "E1_2");
      Sql("UPDATE dbo.E3 SET PageUrl= '123'");
    }

    public override void Down()
    {
      RenameTable(name: "dbo.E1_2", newName: "E1");
    }
    
  }
}
