using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommandeAppWeb.Migrations
{
    /// <inheritdoc />
    public partial class AjoutRoleUtilisateur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Utilisateurs",
                type: "text",
                nullable: false,
                defaultValue: "Utilisateur"); //valeur par défaut pour les lignes existantes
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Utilisateurs");
        }
    }
}
