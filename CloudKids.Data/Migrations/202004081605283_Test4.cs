namespace CloudKids.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reclamations", "catreclamation", c => c.Int(nullable: false));
            AddColumn("dbo.Reclamations", "Test", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reclamations", "Test");
            DropColumn("dbo.Reclamations", "catreclamation");
        }
    }
}
