using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FRESHY.Authentication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class V11_INIT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1297941-f38a-43da-9b00-4926cf24b3e0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e9173ac1-c9a3-49a1-9395-5e10fa078ad1", "AQAAAAEAACcQAAAAEIoZZekkukGrTjNZWIqXmSU4bygOAMZJ14rVUXzuEajRN3+tmrF7Qxr+wZKs3zbeDg==", "68efdeea-2631-4bfc-91a7-bd7dbdcec120" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "41f0043e-5240-4607-9fe4-a891f58dbd43", 0, "556df75d-05ff-473d-bc27-c55c7cc290a6", "default@freshy.com", false, false, null, "DEFAULT@FRESHY.COM", "DEFAULTCUSTOMER", "AQAAAAEAACcQAAAAED4RTXkoyg2j/uICQrb9ZThi0P9u0BnkbsyVdr4M+WMtKWVVHxeOJ74KN2UuqnMmxg==", null, false, "33681717-2769-4f2e-82b8-743dfe2f2356", false, "DefaultCustomer" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b7d5bf05-030c-42ea-b1e2-c6df46ed74d4", "41f0043e-5240-4607-9fe4-a891f58dbd43" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b7d5bf05-030c-42ea-b1e2-c6df46ed74d4", "41f0043e-5240-4607-9fe4-a891f58dbd43" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41f0043e-5240-4607-9fe4-a891f58dbd43");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1297941-f38a-43da-9b00-4926cf24b3e0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3daa4fe5-4034-4707-b258-419ddf210ae4", "AQAAAAEAACcQAAAAEKcvmnT9TDFO8YTLiHdCQcRUOD2n2VNh6WDprOqN/tF+NqrPvnGxaXIfiY1/F1O/+g==", "4509f186-443a-4049-8ae8-4b807b1a0203" });
        }
    }
}