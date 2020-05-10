using Microsoft.EntityFrameworkCore.Migrations;

namespace EfCore.Data.Migrations
{
    public partial class AddingStoreProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE PROCEDURE dbo.SamuraisWhoSaidAWord
	                @text varchar(20)
	                AS
	                SELECT Samurais.Id, Samurais.Name, Samurais.ClanId
	                FROM Samurais
	                INNER JOIN Quotes ON Samurais.Id = Quotes.SamuraiId
	                WHERE (Quotes.Text LIKE '%'+@text+'%')");
            migrationBuilder.Sql(
                @"CREATE PROCEDURE dbo.DeleteQuotesForSamurai
	                @samuraiId int
	                AS
	                DELETE FROM Quotes
	                WHERE Quotes.SamuraiId = @samuraiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE dbo.SamuraisWhoSaidAWord");
            migrationBuilder.Sql("DROP PROCEDURE dbo.DeleteQuotesForSamurai");
        }
    }
}
