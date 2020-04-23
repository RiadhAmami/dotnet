namespace CloudKids.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategrieReclamation : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Reclamations", "Test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reclamations", "Test", c => c.String());
        }
    }
}
