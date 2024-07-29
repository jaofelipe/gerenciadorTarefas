using GerenciadorTarefas.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefas.Data.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            var adminRoleId = Guid.Parse("3f9e6ef3-7a3d-4375-8a3a-5f76b741b7a3");
            // Tabela
            builder.ToTable("Role");

            // Chave Primária
            builder.HasKey(x => x.Id);

            // Propriedades
            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Slug)
                .IsRequired()
                .HasColumnName("Slug")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            // Dados Iniciais
            builder.HasData(
                new Role
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    Slug = "admin"
                }, 
                new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Usuario",
                    Slug = "usuario"
                }
            );
        }
    }

}
