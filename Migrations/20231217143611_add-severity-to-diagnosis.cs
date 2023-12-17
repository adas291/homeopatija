using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace homeopatija.Migrations
{
    /// <inheritdoc />
    public partial class addseveritytodiagnosis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>("Severity", "DiagnosisSymptoms", defaultValueSql: "0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("Severity", "DiagnosisSymptoms");
        }
    }
}
