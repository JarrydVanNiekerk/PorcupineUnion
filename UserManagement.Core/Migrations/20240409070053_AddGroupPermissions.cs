using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermission_Groups_GroupId",
                table: "GroupPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermission_Permissions_PermissionId",
                table: "GroupPermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupPermission",
                table: "GroupPermission");

            migrationBuilder.RenameTable(
                name: "GroupPermission",
                newName: "GroupPermissions");

            migrationBuilder.RenameIndex(
                name: "IX_GroupPermission_PermissionId",
                table: "GroupPermissions",
                newName: "IX_GroupPermissions_PermissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupPermissions",
                table: "GroupPermissions",
                columns: new[] { "GroupId", "PermissionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermissions_Groups_GroupId",
                table: "GroupPermissions",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermissions_Permissions_PermissionId",
                table: "GroupPermissions",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "PermissionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermissions_Groups_GroupId",
                table: "GroupPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermissions_Permissions_PermissionId",
                table: "GroupPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupPermissions",
                table: "GroupPermissions");

            migrationBuilder.RenameTable(
                name: "GroupPermissions",
                newName: "GroupPermission");

            migrationBuilder.RenameIndex(
                name: "IX_GroupPermissions_PermissionId",
                table: "GroupPermission",
                newName: "IX_GroupPermission_PermissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupPermission",
                table: "GroupPermission",
                columns: new[] { "GroupId", "PermissionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermission_Groups_GroupId",
                table: "GroupPermission",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermission_Permissions_PermissionId",
                table: "GroupPermission",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "PermissionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
