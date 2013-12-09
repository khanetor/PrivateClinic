namespace PrivateClinic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_userid_to_Patient_object : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "UserId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "UserId");
        }
    }
}
