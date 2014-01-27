namespace PrivateClinic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Patient_Document_model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PatientDocs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        UserId = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Diagnosis = c.String(),
                        Lab = c.String(),
                        Treatment = c.String(),
                        Result = c.String(),
                        Fee = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientDocs", "PatientId", "dbo.Patients");
            DropIndex("dbo.PatientDocs", new[] { "PatientId" });
            DropTable("dbo.PatientDocs");
        }
    }
}
