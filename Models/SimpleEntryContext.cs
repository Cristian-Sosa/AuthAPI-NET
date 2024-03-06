using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SimpleEntry.Models;

public partial class SimpleEntryContext : DbContext
{
    public SimpleEntryContext()
    {
    }

    public SimpleEntryContext(DbContextOptions<SimpleEntryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RegistroAcciones> RegistroAcciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RegistroAcciones>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__REGISTRO__3214EC2740983C56");

            entity.ToTable("REGISTRO_ACCIONES");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Accion).IsUnicode(false);
            entity.Property(e => e.FechaHora)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Hora");
            entity.Property(e => e.UsuarioId).HasColumnName("Usuario_ID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.OAcciones)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__REGISTRO___Usuar__4CA06362");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_USUARIO");

            entity.ToTable("USUARIOS");

            entity.Property(e => e.Apellido).HasMaxLength(50);
            entity.Property(e => e.FechaHora)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_hora");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Pass).HasMaxLength(1000);
            entity.Property(e => e.Intentos_Login)
            .HasColumnType("int")
            .HasColumnName("Intentos_Login")
            .HasDefaultValue(0);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
