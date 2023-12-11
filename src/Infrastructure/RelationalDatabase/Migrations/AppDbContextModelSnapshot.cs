﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RelationalDatabase.Database;

#nullable disable

namespace RelationalDatabase.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RelationalDatabase.DTO.Deputado.Comissao", b =>
                {
                    b.Property<int>("IdOrgaoLegislativoCD")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdOrgaoLegislativoCD"));

                    b.Property<string>("CondicaoMembro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataEntrada")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataSaida")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeputadoId")
                        .HasColumnType("int");

                    b.Property<string>("NomeComissao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SiglaComissao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdOrgaoLegislativoCD");

                    b.HasIndex("DeputadoId");

                    b.ToTable("Comissao", "congresso");
                });

            modelBuilder.Entity("RelationalDatabase.DTO.Deputado.Deputado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CondicaoEleitoral")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Data")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataFalecimento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Escolaridade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GabineteId")
                        .HasColumnType("int");

                    b.Property<int>("Legislatura")
                        .HasColumnType("int");

                    b.Property<string>("MunicipioNascimento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeCivil")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeEleitoral")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeParlamentarAtual")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PartidoAtualId")
                        .HasColumnType("int");

                    b.Property<string>("RedeSocial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SiglaPartido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SiglaUf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Situacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SituacaoNaLegislaturaAtual")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UfNascimento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UfRepresentacaoAtual")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Uri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UriPartido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlFoto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlWebsite")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GabineteId");

                    b.HasIndex("PartidoAtualId");

                    b.ToTable("Deputado", "congresso");
                });

            modelBuilder.Entity("RelationalDatabase.DTO.Deputado.DeputyExpenses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Ano")
                        .HasColumnType("int");

                    b.Property<string>("CnpjCpfFornecedor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CodDocumento")
                        .HasColumnType("int");

                    b.Property<int>("CodLote")
                        .HasColumnType("int");

                    b.Property<int>("CodTipoDocumento")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataDocumento")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeputadoId")
                        .HasColumnType("int");

                    b.Property<bool>("HasData")
                        .HasColumnType("bit");

                    b.Property<int>("IdDeputy")
                        .HasColumnType("int");

                    b.Property<int>("Mes")
                        .HasColumnType("int");

                    b.Property<string>("NomeFornecedor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumDocumento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumRessarcimento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Parcela")
                        .HasColumnType("int");

                    b.Property<string>("TipoDespesa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoDocumento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlDocumento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ValorDocumento")
                        .HasColumnType("float");

                    b.Property<double>("ValorGlosa")
                        .HasColumnType("float");

                    b.Property<double>("ValorLiquido")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DeputadoId");

                    b.ToTable("DeputyExpenses", "congresso");
                });

            modelBuilder.Entity("RelationalDatabase.DTO.Deputado.DeputyWorkPresence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Ano")
                        .HasColumnType("int");

                    b.Property<int>("CarteiraParlamentar")
                        .HasColumnType("int");

                    b.Property<int>("DeputadoId")
                        .HasColumnType("int");

                    b.Property<int>("Legislatura")
                        .HasColumnType("int");

                    b.Property<int>("Mes")
                        .HasColumnType("int");

                    b.Property<string>("NomeParlamentar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SiglaPartido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SiglaUF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DeputadoId");

                    b.ToTable("DeputyWorkPresences", "congresso");
                });

            modelBuilder.Entity("RelationalDatabase.DTO.Deputado.DiaSessao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeputadoId")
                        .HasColumnType("int");

                    b.Property<int?>("DeputyWorkPresenceId")
                        .HasColumnType("int");

                    b.Property<string>("FrequenciaNoDia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Justificativa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QtdeSessoes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeputadoId");

                    b.HasIndex("DeputyWorkPresenceId");

                    b.ToTable("DiaSessao", "congresso");
                });

            modelBuilder.Entity("RelationalDatabase.DTO.Deputado.Gabinete", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Andar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Predio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sala")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Gabinete", "congresso");
                });

            modelBuilder.Entity("RelationalDatabase.DTO.Deputado.PartidoAtual", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PartidoAtual", "congresso");
                });

            modelBuilder.Entity("RelationalDatabase.DTO.Deputado.Comissao", b =>
                {
                    b.HasOne("RelationalDatabase.DTO.Deputado.Deputado", null)
                        .WithMany("Comissoes")
                        .HasForeignKey("DeputadoId");
                });

            modelBuilder.Entity("RelationalDatabase.DTO.Deputado.Deputado", b =>
                {
                    b.HasOne("RelationalDatabase.DTO.Deputado.Gabinete", "Gabinete")
                        .WithMany()
                        .HasForeignKey("GabineteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RelationalDatabase.DTO.Deputado.PartidoAtual", "PartidoAtual")
                        .WithMany()
                        .HasForeignKey("PartidoAtualId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gabinete");

                    b.Navigation("PartidoAtual");
                });

            modelBuilder.Entity("RelationalDatabase.DTO.Deputado.DeputyExpenses", b =>
                {
                    b.HasOne("RelationalDatabase.DTO.Deputado.Deputado", "Deputado")
                        .WithMany("Expenses")
                        .HasForeignKey("DeputadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deputado");
                });

            modelBuilder.Entity("RelationalDatabase.DTO.Deputado.DeputyWorkPresence", b =>
                {
                    b.HasOne("RelationalDatabase.DTO.Deputado.Deputado", "Deputado")
                        .WithMany("WorkPresences")
                        .HasForeignKey("DeputadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deputado");
                });

            modelBuilder.Entity("RelationalDatabase.DTO.Deputado.DiaSessao", b =>
                {
                    b.HasOne("RelationalDatabase.DTO.Deputado.Deputado", "Deputado")
                        .WithMany()
                        .HasForeignKey("DeputadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RelationalDatabase.DTO.Deputado.DeputyWorkPresence", null)
                        .WithMany("DiasDeSessoes")
                        .HasForeignKey("DeputyWorkPresenceId");

                    b.Navigation("Deputado");
                });

            modelBuilder.Entity("RelationalDatabase.DTO.Deputado.Deputado", b =>
                {
                    b.Navigation("Comissoes");

                    b.Navigation("Expenses");

                    b.Navigation("WorkPresences");
                });

            modelBuilder.Entity("RelationalDatabase.DTO.Deputado.DeputyWorkPresence", b =>
                {
                    b.Navigation("DiasDeSessoes");
                });
#pragma warning restore 612, 618
        }
    }
}
