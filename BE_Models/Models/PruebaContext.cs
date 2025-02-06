using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BE_Models.Models;

public partial class PruebaContext : DbContext
{
    public PruebaContext()
    {
    }

    public PruebaContext(DbContextOptions<PruebaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-8TMMD59; Database=prueba; Trusted_Connection=True; trustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cliente__3213E83FB838C3C4");

            entity.ToTable("Cliente");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Cedula)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cedula");
            entity.Property(e => e.Direccion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NumCelular)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("num_celular");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Factura__3213E83FED50AA68");

            entity.ToTable("Factura");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Detalle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("detalle");
            entity.Property(e => e.FechaEmision)
                .HasColumnType("datetime")
                .HasColumnName("fechaEmision");
            entity.Property(e => e.FechaRetorno)
                .HasColumnType("datetime")
                .HasColumnName("fechaRetorno");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdVehiculo).HasColumnName("id_vehiculo");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("total");
            entity.Property(e => e.TotalHoras).HasColumnName("totalHoras");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Factura__id_clie__7F2BE32F");

            entity.HasOne(d => d.IdVehiculoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdVehiculo)
                .HasConstraintName("FK__Factura__id_vehi__7E37BEF6");
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vehiculo__3213E83F1F1221D7");

            entity.ToTable("Vehiculo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Modelo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("modelo");
            entity.Property(e => e.Placa)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("placa");
            entity.Property(e => e.TarifaBase)
                .HasColumnType("decimal(12, 6)")
                .HasColumnName("tarifaBase");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
