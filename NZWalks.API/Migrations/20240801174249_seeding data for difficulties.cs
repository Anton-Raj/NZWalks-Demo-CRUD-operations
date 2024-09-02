using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class seedingdatafordifficulties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0949c0b4-e8b4-414b-a301-2a1c8130fd51"), "Easy" },
                    { new Guid("5aa3a4af-7e09-482f-b8b5-d180b7a50826"), "Hard" },
                    { new Guid("b0ec6810-13e3-49de-83ca-aae3e9606549"), "Medium" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("0949c0b4-e8b4-414b-a301-2a1c8130fd51"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("5aa3a4af-7e09-482f-b8b5-d180b7a50826"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("b0ec6810-13e3-49de-83ca-aae3e9606549"));
        }
    }
}
