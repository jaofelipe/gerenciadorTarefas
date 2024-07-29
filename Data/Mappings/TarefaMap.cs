using GerenciadorTarefas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorTarefas.Data.Mappings
{
    public class TarefaMap : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            // Tabela
            builder.ToTable("Tarefa");

            // Chave Primária
            builder.HasKey(x => x.Id);

            

            // Propriedades
            builder.Property(x => x.Titulo)
                .IsRequired()
                .HasColumnName("Titulo")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasColumnName("Descricao")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.DataVencimento)
               .IsRequired()
               .HasColumnName("DataVencimento")
               .HasColumnType("DateTime");

            builder.Property(x => x.StatusTarefa)
               .IsRequired()
               .HasColumnName("StatusTarefa")
               .HasColumnType("int");

            builder.Property(x => x.Responsavel)
                .IsRequired()
                .HasColumnName("Responsavel")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            // Índices
           
        }
    }
}