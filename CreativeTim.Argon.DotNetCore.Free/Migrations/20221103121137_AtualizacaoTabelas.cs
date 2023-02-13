using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoraWeb.Migrations
{
    public partial class AtualizacaoTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FK_PESSOA_CADASTRO",
                table: "TUSUARIO");

            migrationBuilder.DropColumn(
                name: "OB_USUARIO",
                table: "TUSUARIO");

            migrationBuilder.DropColumn(
                name: "QT_ERRO_SENHA",
                table: "TUSUARIO");

            migrationBuilder.DropColumn(
                name: "fk_pessoa_atualizacao",
                table: "TUSUARIO");

            migrationBuilder.DropColumn(
                name: "DC_CPFCNPJ",
                table: "TPESSOA");

            migrationBuilder.DropColumn(
                name: "DS_BAIRRO",
                table: "TPESSOA");

            migrationBuilder.DropColumn(
                name: "DS_CIDADE",
                table: "TPESSOA");

            migrationBuilder.DropColumn(
                name: "DS_COMPLEMENTO",
                table: "TPESSOA");

            migrationBuilder.DropColumn(
                name: "DS_EMAIL",
                table: "TPESSOA");

            migrationBuilder.DropColumn(
                name: "DS_ENDERECO",
                table: "TPESSOA");

            migrationBuilder.DropColumn(
                name: "DS_PAIS",
                table: "TPESSOA");

            migrationBuilder.DropColumn(
                name: "DS_SITE",
                table: "TPESSOA");

            migrationBuilder.DropColumn(
                name: "DS_UF",
                table: "TPESSOA");

            migrationBuilder.DropColumn(
                name: "DT_NASCIMENTO",
                table: "TPESSOA");

            migrationBuilder.DropColumn(
                name: "NM_DDD",
                table: "TPESSOA");

            migrationBuilder.DropColumn(
                name: "NM_ENDERECO",
                table: "TPESSOA");

            migrationBuilder.DropColumn(
                name: "NM_FONE",
                table: "TPESSOA");

            migrationBuilder.DropColumn(
                name: "OB_ATUALIZACAO",
                table: "TPESSOA");

            migrationBuilder.DropColumn(
                name: "OB_PESSOA",
                table: "TPESSOA");

            migrationBuilder.AddColumn<byte>(
                name: "FL_ATIVO",
                table: "TUSUARIO",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FL_ATIVO",
                table: "TUSUARIO");

            migrationBuilder.AddColumn<int>(
                name: "FK_PESSOA_CADASTRO",
                table: "TUSUARIO",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OB_USUARIO",
                table: "TUSUARIO",
                type: "character varying(500)",
                unicode: false,
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "QT_ERRO_SENHA",
                table: "TUSUARIO",
                type: "smallint",
                nullable: true,
                defaultValueSql: "((0))");

            migrationBuilder.AddColumn<int>(
                name: "fk_pessoa_atualizacao",
                table: "TUSUARIO",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DC_CPFCNPJ",
                table: "TPESSOA",
                type: "character varying(14)",
                unicode: false,
                maxLength: 14,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DS_BAIRRO",
                table: "TPESSOA",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DS_CIDADE",
                table: "TPESSOA",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DS_COMPLEMENTO",
                table: "TPESSOA",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DS_EMAIL",
                table: "TPESSOA",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DS_ENDERECO",
                table: "TPESSOA",
                type: "character varying(70)",
                maxLength: 70,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DS_PAIS",
                table: "TPESSOA",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DS_SITE",
                table: "TPESSOA",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DS_UF",
                table: "TPESSOA",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DT_NASCIMENTO",
                table: "TPESSOA",
                type: "timestamp",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NM_DDD",
                table: "TPESSOA",
                type: "character varying(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NM_ENDERECO",
                table: "TPESSOA",
                type: "character varying(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NM_FONE",
                table: "TPESSOA",
                type: "character varying(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OB_ATUALIZACAO",
                table: "TPESSOA",
                type: "character varying(500)",
                unicode: false,
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OB_PESSOA",
                table: "TPESSOA",
                type: "character varying(500)",
                unicode: false,
                maxLength: 500,
                nullable: true);
        }
    }
}
