using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ComprasISO810.Models;

public partial class ComprasIso810Context : DbContext
{
    public ComprasIso810Context()
    {
    }

    public ComprasIso810Context(DbContextOptions<ComprasIso810Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Articulo> Articulos { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<OrdenDeCompra> OrdenDeCompras { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<SolicitudDeArticulo> SolicitudDeArticulos { get; set; }

    public virtual DbSet<UnidadesDeMedidum> UnidadesDeMedida { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=localhost; database=ComprasISO810; integrated security=true; TrustServerCertificate=Yes");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Articulo__3213E83F9CEABB01");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Existencia).HasColumnName("existencia");
            entity.Property(e => e.Marca).HasColumnName("marca");
            entity.Property(e => e.UnidadDeMedida).HasColumnName("unidad_de_medida");

            entity.HasOne(d => d.MarcaNavigation).WithMany(p => p.Articulos)
                .HasForeignKey(d => d.Marca)
                .HasConstraintName("FK__Articulos__marca__440B1D61");

            entity.HasOne(d => d.UnidadDeMedidaNavigation).WithMany(p => p.Articulos)
                .HasForeignKey(d => d.UnidadDeMedida)
                .HasConstraintName("FK__Articulos__unida__44FF419A");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departam__3213E83F45A0D10C");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empleado__3213E83F62DEE18E");

            entity.HasIndex(e => e.Cedula, "UQ__Empleado__415B7BE5087BBEB1").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cedula)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("cedula");
            entity.Property(e => e.Departamento).HasColumnName("departamento");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.DepartamentoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.Departamento)
                .HasConstraintName("FK__Empleados__depar__3A81B327");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Marcas__3213E83F0390F9C9");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("estado");
        });

        modelBuilder.Entity<OrdenDeCompra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orden_de__3213E83F3245C075");

            entity.ToTable("Orden_de_compra");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Articulo).HasColumnName("articulo");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FechaOrden).HasColumnName("fecha_orden");
            entity.Property(e => e.IdSolicitud).HasColumnName("idSolicitud");
            entity.Property(e => e.Marca).HasColumnName("marca");
            entity.Property(e => e.UnidadDeMedida).HasColumnName("unidad_de_medida");

            entity.HasOne(d => d.ArticuloNavigation).WithMany(p => p.OrdenDeCompras)
                .HasForeignKey(d => d.Articulo)
                .HasConstraintName("FK__Orden_de___artic__4D94879B");

            entity.HasOne(d => d.IdSolicitudNavigation).WithMany(p => p.OrdenDeCompras)
                .HasForeignKey(d => d.IdSolicitud)
                .HasConstraintName("FK__Orden_de___idSol__4CA06362");

            entity.HasOne(d => d.MarcaNavigation).WithMany(p => p.OrdenDeCompras)
                .HasForeignKey(d => d.Marca)
                .HasConstraintName("FK__Orden_de___marca__4F7CD00D");

            entity.HasOne(d => d.UnidadDeMedidaNavigation).WithMany(p => p.OrdenDeCompras)
                .HasForeignKey(d => d.UnidadDeMedida)
                .HasConstraintName("FK__Orden_de___unida__4E88ABD4");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Proveedo__3213E83FB3A89EB0");

            entity.HasIndex(e => e.Cedula, "UQ__Proveedo__415B7BE5FBFC39A8").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cedula)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("cedula");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.NombreComercial)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_comercial");
        });

        modelBuilder.Entity<SolicitudDeArticulo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Solicitu__3213E83FFBFF24DE");

            entity.ToTable("Solicitud_de_articulos", tb =>
                {
                    tb.HasTrigger("copy_solicitud_data");
                    tb.HasTrigger("insertar_orden_compra");
                    tb.HasTrigger("update_existencia");
                });

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Articulo).HasColumnName("articulo");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.EmpleadoSolicitante).HasColumnName("empleado_solicitante");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FechaSolicitud).HasColumnName("fecha_solicitud");
            entity.Property(e => e.UnidadesDeMedida).HasColumnName("unidades_de_medida");

            entity.HasOne(d => d.ArticuloNavigation).WithMany(p => p.SolicitudDeArticulos)
                .HasForeignKey(d => d.Articulo)
                .HasConstraintName("FK__Solicitud__artic__48CFD27E");

            entity.HasOne(d => d.EmpleadoSolicitanteNavigation).WithMany(p => p.SolicitudDeArticulos)
                .HasForeignKey(d => d.EmpleadoSolicitante)
                .HasConstraintName("FK__Solicitud__emple__47DBAE45");

            entity.HasOne(d => d.UnidadesDeMedidaNavigation).WithMany(p => p.SolicitudDeArticulos)
                .HasForeignKey(d => d.UnidadesDeMedida)
                .HasConstraintName("FK__Solicitud__unida__49C3F6B7");
        });

        modelBuilder.Entity<UnidadesDeMedidum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Unidades__3213E83F6977705F");

            entity.ToTable("Unidades_de_medida");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("estado");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
