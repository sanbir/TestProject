using System.Data.Entity.Migrations;

namespace ContosoUniversity.Migrations
{
    public partial class MaxLengthOnNames : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Student", "LastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Student", "FirstMidName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Student", "FirstMidName", c => c.String());
            AlterColumn("dbo.Student", "LastName", c => c.String());
        }
    }
}
