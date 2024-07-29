using GerenciadorTarefas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecureIdentity.Password;

namespace GerenciadorTarefas.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // IDs fixos para dados iniciais
            var adminUserId = Guid.Parse("d2f1f799-09b6-44b0-91a4-13d5cd3640b1");
            var adminRoleId = Guid.Parse("3f9e6ef3-7a3d-4375-8a3a-5f76b741b7a3");

            // Tabela
            builder.ToTable("User");

            // Chave Primária
            builder.HasKey(x => x.Id);


            // Propriedades
            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Bio)
                .IsRequired(false);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("VARCHAR")
                .HasMaxLength(160);

            builder.Property(x => x.Image)
                .IsRequired(false);

            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasColumnName("PasswordHash")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(x => x.Slug)
                .IsRequired()
                .HasColumnName("Slug")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            // Índices
            builder
                .HasIndex(x => x.Slug, "IX_User_Slug")
                .IsUnique();



            // Relacionamentos
            builder
            .HasMany(x => x.Roles)
            .WithMany(x => x.Users)
            .UsingEntity<UserRole>(
                role => role
                    .HasOne(x => x.Role)
                    .WithMany()
                    .HasForeignKey(x => x.RoleId)
                    .HasConstraintName("FK_UserRole_RoleId")
                    .OnDelete(DeleteBehavior.Cascade),
                user => user
                    .HasOne(x => x.User)
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .HasConstraintName("FK_UserRole_UserId")
                    .OnDelete(DeleteBehavior.Cascade),
            userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasData(
                    new UserRole
                    {
                        UserId = adminUserId,
                        RoleId = adminRoleId
                    }
                );

                builder.HasData(
                    new User
                    {
                        Id = adminUserId,
                        Name = "admin",
                        Email = "admin@gmail.com",
                        Slug = "admin-gmail-com",
                        PasswordHash = PasswordHasher.Hash("Net@123"),
                    }
                );

            });
        }
    }
}