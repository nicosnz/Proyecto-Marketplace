using System;
using System.Collections.Generic;
using marketplace_backend.dtos;
using Microsoft.EntityFrameworkCore;

namespace marketplace_backend.Models;

public partial class MarketplaceDbContext : DbContext
{
    public MarketplaceDbContext()
    {
    }

    public MarketplaceDbContext(DbContextOptions<MarketplaceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<DetalleOrden> DetalleOrdens { get; set; }

    public virtual DbSet<Ordene> Ordenes { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<VwProductosCatalogo> VwProductosCatalogos { get; set; }

    public virtual DbSet<VwProductosMasVendido> VwProductosMasVendidos { get; set; }

    public virtual DbSet<VwResumenVentasVendedor> VwResumenVentasVendedors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-7SL9R98;Database=MarketplaceDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DetalleComprasUsuario>().HasNoKey();
        modelBuilder.Entity<ProductosVendidosUsuario>().HasNoKey();
        modelBuilder.Entity<UsuarioInfodto>().HasNoKey();
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK__Categori__F353C1C5CCBE0510");

            entity.HasIndex(e => e.Activo, "IX_Categorias_Activo");

            entity.HasIndex(e => e.Nombre, "UQ__Categori__75E3EFCFAAD02714").IsUnique();

            entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<DetalleOrden>(entity =>
        {
            entity.HasKey(e => e.DetalleId).HasName("PK__DetalleO__6E19D6FA689F7552");

            entity.ToTable("DetalleOrden");

            entity.HasIndex(e => e.OrdenId, "IX_DetalleOrden_OrdenID");

            entity.HasIndex(e => e.ProductoId, "IX_DetalleOrden_ProductoID");

            entity.Property(e => e.DetalleId).HasColumnName("DetalleID");
            entity.Property(e => e.OrdenId).HasColumnName("OrdenID");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");

            entity.HasOne(d => d.Orden).WithMany(p => p.DetalleOrdens)
                .HasForeignKey(d => d.OrdenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleOrden_Ordenes");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetalleOrdens)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleOrden_Productos");
        });

        modelBuilder.Entity<Ordene>(entity =>
        {
            entity.HasKey(e => e.OrdenId).HasName("PK__Ordenes__C088A4E4419C6BCD");

            entity.ToTable(tb => tb.HasTrigger("trg_ReponerStock_CancelacionOrden"));

            entity.HasIndex(e => e.CompradorId, "IX_Ordenes_CompradorID");

            entity.HasIndex(e => e.Estado, "IX_Ordenes_Estado");

            entity.Property(e => e.OrdenId).HasColumnName("OrdenID");
            entity.Property(e => e.Ciudad).HasMaxLength(100);
            entity.Property(e => e.CompradorId).HasColumnName("CompradorID");
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Estado).HasMaxLength(20);
            entity.Property(e => e.FechaOrden)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Pais).HasMaxLength(100);
            entity.Property(e => e.Total).HasColumnType("decimal(12, 2)");

            entity.HasOne(d => d.Comprador).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.CompradorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ordenes_Personas");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.PersonaId).HasName("PK__Personas__7C34D323DEE03863");

            entity.HasIndex(e => e.Email, "UQ__Personas__A9D105341E21531B").IsUnique();

            entity.Property(e => e.PersonaId).HasColumnName("PersonaID");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PK__Producto__A430AE83FE21B52F");

            entity.HasIndex(e => e.Activo, "IX_Productos_Activo");

            entity.HasIndex(e => e.CategoriaId, "IX_Productos_CategoriaID");

            entity.HasIndex(e => e.VendedorId, "IX_Productos_VendedorID");

            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(150);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.VendedorId).HasColumnName("VendedorID");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("FK_Productos_Categorias");

            entity.HasOne(d => d.Vendedor).WithMany(p => p.Productos)
                .HasForeignKey(d => d.VendedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Personas");
        });

        
        

        modelBuilder.Entity<VwProductosCatalogo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ProductosCatalogo");

            entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");
            entity.Property(e => e.CategoriaNombre).HasMaxLength(100);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.ProductoNombre).HasMaxLength(150);
            entity.Property(e => e.VendedorId).HasColumnName("VendedorID");
            entity.Property(e => e.VendedorNombre).HasMaxLength(100);
        });

        modelBuilder.Entity<VwProductosMasVendido>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ProductosMasVendidos");

            entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");
            entity.Property(e => e.CategoriaNombre).HasMaxLength(100);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.ProductoNombre).HasMaxLength(150);
            entity.Property(e => e.TotalVentas).HasColumnType("decimal(38, 2)");
            entity.Property(e => e.VendedorId).HasColumnName("VendedorID");
            entity.Property(e => e.VendedorNombre).HasMaxLength(100);
        });

        modelBuilder.Entity<VwResumenVentasVendedor>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ResumenVentasVendedor");

            entity.Property(e => e.TotalVentas).HasColumnType("decimal(38, 2)");
            entity.Property(e => e.VendedorId).HasColumnName("VendedorID");
            entity.Property(e => e.VendedorNombre).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
