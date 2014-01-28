namespace PrivateClinic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Symptom_column_for_PatientDoc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientDocs", "Symptom", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientDocs", "Symptom");
        }
    }
}
