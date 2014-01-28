namespace PrivateClinic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Date_of_last_visit_column_to_Patient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "DOLV", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "DOLV");
        }
    }
}
