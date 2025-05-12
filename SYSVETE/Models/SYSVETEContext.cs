using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SYSVETE.Models
{
    public partial class SYSVETEContext : DbContext
    {
        public SYSVETEContext()
        {
        }

        public SYSVETEContext(DbContextOptions<SYSVETEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Modulo> Modulos { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Paciente> Pacientes { get; set; } = null!;
        public virtual DbSet<Procedimiento> Procedimientos { get; set; } = null!;
        public virtual DbSet<Rol> Roles { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Permiso> Permisos { get; set; } = null!;
        public virtual DbSet<TipoInsumo> TipoInsumos { get; set; } = null!;
        public virtual DbSet<Insumo> Insumos { get; set; } = null!;
        public virtual DbSet<UnidadMedida> UnidadMedidas { get; set; } = null!;
        public virtual DbSet<Presentacion> Presentaciones { get; set; } = null!;
        public virtual DbSet<Impuesto> Impuestos { get; set; } = null!;
        public virtual DbSet<Especie> Especies { get; set; } = null!;
        public virtual DbSet<Raza> Razas { get; set; } = null!;
        public virtual DbSet<Patologia> Patologias { get; set; } = null!;
        public virtual DbSet<Vacuna> Vacunas { get; set; } = null!;
        public virtual DbSet<Lote> Lotes { get; set; } = null!;

        
        public virtual DbSet<Tratamiento> Tratamientos { get; set; } = null!;
        public virtual DbSet<Proveedor> Proveedores { get; set; } = null!;
        public virtual DbSet<Compra> Compras { get; set; } = null!;
        public virtual DbSet<CompraDetalle> CompraDetalles { get; set; } = null!;
        public virtual DbSet<HistorialClinico> HistorialClinicoS { get; set; } = null!;
        public virtual DbSet<StockInsumo> StockInsumos { get; set; } = null!;
        public virtual DbSet<PagoVenta> PagoVentas { get; set; } = null!;

        public virtual DbSet<Venta> Ventas { get; set; } = null!;
        public virtual DbSet<VentaDetalle> VentaDetalles { get; set; } = null!;
        public virtual DbSet<DeudaProveedor> DeudaProveedores { get; set; } = null!;
        public virtual DbSet<HistorialMovimiento> HistorialMovimientos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Modulo>(entity =>
            {
                entity.HasKey(e => e.IdModulo);

                entity.ToTable("Modulo");
                entity.Property(e => e.Nombre).HasColumnName("Modulo");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona);

                entity.ToTable("Persona");

                entity.Property(e => e.IdPersona).HasColumnName("idPersona");

                entity.Property(e => e.Cedula)
                    .HasMaxLength(10)
                    .HasColumnName("Cedula");
                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaNacimiento");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("Nombre");
                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .HasColumnName("Apellido");

                entity.Property(e => e.Borrado)
                .HasColumnName("borrado")
                .HasDefaultValue(false);
                entity.Property(e => e.FechaInsertado)
                   .HasColumnType("datetime")
                   .HasColumnName("FechaInsertado")
                   .HasDefaultValueSql("getdate()");
                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");
                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");
                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");

                entity.HasQueryFilter(e => !e.Borrado);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.ToTable("Cliente");

                entity.HasKey(e => e.IdCliente);
                entity.Property(e => e.RUC).HasColumnName("RUC");
                entity.Property(e => e.Telefono).HasColumnName("Telefono");
                entity.Property(e => e.Email).HasColumnName("Email");
                entity.Property(e => e.Activo).HasColumnName("Activo");
                entity.Property(e => e.FechaInsertado)
                 .HasColumnType("datetime")
                 .HasColumnName("FechaInsertado")
                 .HasDefaultValueSql("getdate()");

                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");

                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");

                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");
                entity.HasOne(d => d.IdPersonaNavigation)
                                                 .WithMany(p => p.Clientes)
                                                 .HasForeignKey(d => d.IdPersona)
                                                 .OnDelete(DeleteBehavior.ClientSetNull)
                                                 .HasConstraintName("FK_Cliente_Persona");
                entity.HasQueryFilter(e => !e.Borrado);


            });
            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.HasKey(e => e.IdPaciente);

                entity.ToTable("Paciente");

                entity.HasKey(e => e.IdPaciente);

                entity.Property(e => e.Nombre).HasColumnName("Nombre");
                entity.Property(e => e.Edad).HasColumnName("Edad");
                entity.Property(e => e.Sexo).HasColumnName("Sexo");
                entity.Property(e => e.Peso).HasColumnName("Peso");
                entity.Property(e => e.Activo).HasColumnName("Activo");
                entity.Property(e => e.Borrado).HasColumnName("Borrado");
                entity.Property(e => e.FechaInsertado)
                 .HasColumnType("datetime")
                 .HasColumnName("FechaInsertado")
                 .HasDefaultValueSql("getdate()");

                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");

                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");

                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");
                entity.HasOne(d => d.IdClienteNavigation)
                                                 .WithMany(p => p.Pacientes)
                                                 .HasForeignKey(d => d.IdCliente)
                                                 .OnDelete(DeleteBehavior.ClientSetNull)
                                                 .HasConstraintName("FK_Paciente_Cliente");
                entity.HasOne(d => d.IdRazaNavigation)
                                                .WithMany(p => p.Pacientes)
                                                .HasForeignKey(d => d.IdRaza)
                                                .OnDelete(DeleteBehavior.ClientSetNull)
                                                .HasConstraintName("FK_Paciente_Raza");
                entity.HasQueryFilter(e => !e.Borrado);

            });
            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.HasKey(e => e.IdPermiso);

                entity.ToTable("Permiso");

                entity.HasKey(e => e.IdPermiso);

                entity.Property(e => e.Agregar).HasColumnName("agregar");
                entity.Property(e => e.Consultar).HasColumnName("consultar");
                entity.Property(e => e.Borrar).HasColumnName("borrar");
                entity.Property(e => e.Borrado).HasColumnName("borrado");
                entity.Property(e => e.Editar).HasColumnName("editar");
                
                entity.Property(e => e.Borrado)
                .HasColumnName("borrado")
                .HasDefaultValue(false);
                entity.Property(e => e.FechaInsertado)
                   .HasColumnType("datetime")
                   .HasColumnName("FechaInsertado")
                   .HasDefaultValueSql("getdate()");

                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");

                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");

                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");
                entity.HasOne(d => d.IdRolNavigation)
                                                 .WithMany(p => p.Permisos)
                                                 .HasForeignKey(d => d.IdRol)
                                                 .OnDelete(DeleteBehavior.ClientSetNull)
                                                 .HasConstraintName("FK_Privilegio_Rol");
                entity.HasOne(d => d.IdModuloNavigation)
                                                 .WithMany(p => p.Permisos)
                                                 .HasForeignKey(d => d.IdModulo)
                                                 .OnDelete(DeleteBehavior.ClientSetNull)
                                                 .HasConstraintName("[FK_Permiso_Modulo]");
                entity.HasQueryFilter(e => !e.Borrado);

            });


            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol);

                entity.ToTable("Rol");

                entity.HasIndex(e => new { e.Descripcion, e.Activo }, "Unique_Rol")
                    .IsUnique()
                    .HasFilter("([activo]=(1))");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Borrado)
                .HasColumnName("borrado")
                .HasDefaultValue(false);
                entity.Property(e => e.FechaInsertado)
                   .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()")
                   .HasColumnName("FechaInsertado");

                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");

                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");

                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");

                entity.HasQueryFilter(e => !e.Borrado);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("Usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Alias)
                    .HasMaxLength(50)
                    .HasColumnName("alias");

                entity.Property(e => e.Contrasena).HasColumnName("contrasena");

                entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("Nombre");

                entity.HasOne(d => d.IdRolNavigation)
                                    .WithMany(p => p.Usuarios)
                                    .HasForeignKey(d => d.IdRol)
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName("FK_Privilegio_Rol");
                entity.Property(e => e.Borrado)
                .HasColumnName("borrado")
                .HasDefaultValue(false);

                entity.HasQueryFilter(e => !e.Borrado);

            });
            modelBuilder.Entity<TipoInsumo>(entity =>   
            {
                entity.HasKey(e => e.IdTipoInsumo);

                entity.ToTable("TipoInsumo");
                entity.Property(e => e.FechaInsertado)
                 .HasColumnType("datetime")
                  .HasDefaultValueSql("getdate()")
                 .HasColumnName("FechaInsertado");

                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");

                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");

                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");

                entity.Property(e => e.Descripcion).HasColumnName("Descricion");
                entity.Property(e => e.Nombre).HasColumnName("Nombre");
                entity.HasQueryFilter(e => !e.Borrado);

            });

            modelBuilder.Entity<Insumo>(entity =>
            {
                entity.HasKey(e => e.IdInsumo);

                entity.ToTable("Insumo");

                entity.HasKey(e => e.IdInsumo);

                entity.Property(e => e.IdPresentacion).HasColumnName("IdPresentacion");
                entity.Property(e => e.IdImpuesto).HasColumnName("IdImpuesto");
                entity.Property(e => e.codigo).HasColumnName("codigo");
                entity.Property(e => e.descripcion).HasColumnName("descripcion");
                entity.Property(e => e.Activo).HasColumnName("Activo");
                entity.Property(e => e.Borrado).HasColumnName("Borrado");
                entity.Property(e => e.FechaInsertado)
                 .HasColumnType("datetime")
                 .HasColumnName("FechaInsertado")
                 .HasDefaultValueSql("getdate()");

                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");

                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");

                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");
                entity.HasOne(d => d.IdTipoInsumoNavigation)
                                                 .WithMany(p => p.Insumos)
                                                 .HasForeignKey(d => d.IdTipoInsumo)
                                                 .OnDelete(DeleteBehavior.ClientSetNull)
                                                 .HasConstraintName("FK_Permisp_Tipo");
                entity.HasOne(d => d.IdImpuestoNavigation)
                                                .WithMany(p => p.Insumos)
                                                .HasForeignKey(d => d.IdImpuesto)
                                                .OnDelete(DeleteBehavior.ClientSetNull)
                                                .HasConstraintName("FK_Insumo_Impuesto");
                entity.HasQueryFilter(e => !e.Borrado);

            });

            modelBuilder.Entity<UnidadMedida>(entity =>
            {
                entity.HasKey(e => e.IdUnidad);

                entity.ToTable("UnidadMedida");

                entity.Property(e => e.Borrado).HasColumnName("Borrado");

                entity.Property(e => e.Descripcion).HasColumnName("Descripcion");
                entity.Property(e => e.Abreviatura).HasColumnName("Abreviatura");
                entity.Property(e => e.FechaInsertado)
                 .HasColumnType("datetime")
                 .HasColumnName("FechaInsertado")
                 .HasDefaultValueSql("getdate()");

                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");

                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");

                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");
                entity.HasQueryFilter(e => !e.Borrado);

            });
            modelBuilder.Entity<Presentacion>(entity =>
            {
                entity.HasKey(e => e.IdPresentacion);

                entity.ToTable("Presentacion");

                entity.HasKey(e => e.IdPresentacion);

                entity.Property(e => e.IdUnidad).HasColumnName("IdUnidad");
                entity.Property(e => e.CantidadPresentacion).HasColumnName("CantidadPresentacion");
                entity.Property(e => e.Descripcion).HasColumnName("descripcion");
                entity.Property(e => e.Activo).HasColumnName("Activo");
                entity.Property(e => e.Borrado).HasColumnName("Borrado");
                entity.Property(e => e.FechaInsertado)
                 .HasColumnType("datetime")
                 .HasColumnName("FechaInsertado")
                 .HasDefaultValueSql("getdate()");

                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");

                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");

                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");
                entity.HasOne(d => d.IdUnidadNavigation)
                                                 .WithMany(p => p.Presentaciones)
                                                 .HasForeignKey(d => d.IdUnidad)
                                                 .OnDelete(DeleteBehavior.ClientSetNull)
                                                 .HasConstraintName("FK_Presentacion_UnidadMedida");
                entity.HasQueryFilter(e => !e.Borrado);

            });

            modelBuilder.Entity<Impuesto>(entity =>
            {
                entity.HasKey(e => e.idImpuesto);
                entity.ToTable("Impuesto");
                entity.Property(e => e.Descripcion).HasColumnName("Descripcion");
                entity.Property(e => e.Valor).HasColumnName("Valor");
                entity.Property(e => e.FechaInsertado)
                  .HasColumnType("datetime")
                  .HasColumnName("FechaInsertado")
                  .HasDefaultValueSql("getdate()");

                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");

                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");

                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");
                entity.HasQueryFilter(e => !e.Borrado);

            });

            modelBuilder.Entity<Especie>(entity =>
            {
                entity.HasKey(e => e.IdEspecie);

                entity.ToTable("Especie");
                entity.Property(e => e.Nombre).HasColumnName("Nombre");
                entity.Property(e => e.Activo).HasColumnName("Activo");
                entity.Property(e => e.FechaInsertado)
                  .HasColumnType("datetime")
                  .HasDefaultValueSql("getdate()")
                  .HasColumnName("FechaInsertado");

                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");

                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");

                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");
                entity.HasQueryFilter(e => !e.Borrado);

            });
            modelBuilder.Entity<Raza>(entity =>
            {
                entity.HasKey(e => e.IdRaza);

                entity.ToTable("Raza");


                entity.Property(e => e.IdEspecie).HasColumnName("IdEspecie");
                entity.Property(e => e.Nombre).HasColumnName("Nombre");
                entity.Property(e => e.Activo).HasColumnName("Activo");
                entity.Property(e => e.FechaInsertado)
                 .HasColumnType("datetime")
                 .HasColumnName("FechaInsertado")
                 .HasDefaultValueSql("getdate()");

                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");

                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");

                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");
                entity.HasOne(d => d.IdEspecieNavigation)
                                                 .WithMany(p => p.Razas)
                                                 .HasForeignKey(d => d.IdEspecie)
                                                 .OnDelete(DeleteBehavior.ClientSetNull)
                                                 .HasConstraintName("FK_Raza_Especie");
                entity.HasQueryFilter(e => !e.Borrado);

            });
            modelBuilder.Entity<Tratamiento>(entity =>
            {
                entity.HasKey(e => e.IdTratamiento);

                entity.ToTable("Tratamiento");

                entity.Property(e => e.Nombre).HasColumnName("Nombre");
                entity.Property(e => e.Borrado).HasColumnName("Borrado");
                entity.Property(e => e.Costo).HasColumnName("Costo");

                entity.Property(e => e.FechaInsertado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()")
                    .HasColumnName("FechaInsertado");

                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");

                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");

                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");
                entity.HasQueryFilter(e => !e.Borrado);


            });
            modelBuilder.Entity<Patologia>(entity =>
            {
                entity.HasKey(e => e.IdPatologia);

                entity.ToTable("Patologia");

                entity.Property(e => e.Nombre).HasColumnName("Nombre");
                entity.Property(e => e.Borrado).HasColumnName("Borrado");

                entity.Property(e => e.FechaInsertado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()")
                    .HasColumnName("FechaInsertado");

                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");

                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");

                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");
                entity.HasQueryFilter(e => !e.Borrado);


            });

            modelBuilder.Entity<Procedimiento>(entity =>
            {
                entity.HasKey(e => e.IdProcedimiento);

                entity.ToTable("Procedimiento");

                entity.Property(e => e.Descripcion).HasColumnName("Descripcion");
                entity.Property(e => e.Costo).HasColumnName("Costo");

                entity.Property(e => e.Borrado).HasColumnName("Borrado");

                entity.Property(e => e.FechaInsertado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()")
                    .HasColumnName("FechaInsertado");

                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");

                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");

                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");
                entity.HasQueryFilter(e => !e.Borrado);


            });

            modelBuilder.Entity<Vacuna>(entity =>
            {
                entity.HasKey(e => e.IdVacuna);

                entity.ToTable("Vacuna");

                entity.Property(e => e.Nombre).HasColumnName("Nombre");
                entity.Property(e => e.Costo).HasColumnName("Costo");
                entity.Property(e => e.Borrado).HasColumnName("Borrado");

                entity.Property(e => e.FechaInsertado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()")
                    .HasColumnName("FechaInsertado");

                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");

                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");

                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");
                entity.HasQueryFilter(e => !e.Borrado);


            });
            modelBuilder.Entity<Lote>(entity =>
            {
                entity.HasKey(e => e.IdLote);

                entity.ToTable("Lote");

                entity.Property(e => e.CodigoLote).HasColumnName("CodigoLote");
                entity.Property(e => e.Borrado).HasColumnName("Borrado");
                entity.Property(e => e.FechaFabricacion)
                   .HasColumnType("datetime")
                   .HasColumnName("FechaFabricacion");
                entity.Property(e => e.FechaVencimiento)
                   .HasColumnType("datetime")
                   .HasColumnName("FechaVencimiento");
                entity.Property(e => e.FechaInsertado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()")
                    .HasColumnName("FechaInsertado");

                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");

                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");

                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");
                entity.HasQueryFilter(e => !e.Borrado);


            });
            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.IdProveedor);

                entity.ToTable("Proveedor");

                entity.Property(e => e.IdPersona)
                    .HasColumnName("IdPersona");
                entity.Property(e => e.Ruc)
                    .HasColumnName("Ruc");
                entity.Property(e => e.Telefono)
                    .HasColumnName("Telefono")
                    .IsRequired(false);
                entity.Property(e => e.Email)
                    .HasColumnName("Email")
                    .IsRequired(false);

                entity.Property(e => e.Borrado)
                    .HasColumnName("borrado")
                    .HasDefaultValue(false);
                entity.Property(e => e.FechaInsertado)
                 .HasColumnType("datetime")
                 .HasColumnName("FechaInsertado")
                 .HasDefaultValueSql("getdate()");
                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");
                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");
                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");

                entity.HasOne(e => e.IdPersonaNavigation)
                    .WithMany()
                    .HasForeignKey(e => e.IdPersona)
                    .HasConstraintName("FK_Proveedores_Persona");

                entity.HasQueryFilter(e => !e.Borrado);
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(e => e.IdCompra);

                entity.ToTable("Compra");

                entity.Property(e => e.IdProveedor)
                    .HasColumnName("IdProveedor");
                entity.Property(e => e.Finalizado).HasColumnName("Finalizado");

                entity.Property(e => e.NroBoleta)
                    .HasColumnName("NroBoleta");
                entity.Property(e => e.Facturado).HasColumnName("Facturado");
                entity.Property(e => e.TipoCompra).HasColumnName("TipoCompra");

                entity.Property(e => e.FechaCompra)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaCompra")
                    .HasDefaultValueSql("getdate()");
                entity.Property(e => e.Facturado).HasColumnName("Facturado");

                entity.Property(e => e.Borrado)
                    .HasColumnName("borrado")
                    .HasDefaultValue(false);
                entity.Property(e => e.FechaInsertado)
                 .HasColumnType("datetime")
                 .HasColumnName("FechaInsertado")
                 .HasDefaultValueSql("getdate()");
                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");
                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");
                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");

                entity.HasOne(e => e.Proveedor)
                    .WithMany()
                    .HasForeignKey(e => e.IdProveedor)
                    .HasConstraintName("FK_Compras_Proveedores");

                entity.HasQueryFilter(e => !e.Borrado);
            });

            modelBuilder.Entity<CompraDetalle>(entity =>
            {
                entity.HasKey(e => e.IdCompraDetalle);

                entity.ToTable("CompraDetalle");

                entity.Property(e => e.IdCompra)
                    .HasColumnName("IdCompra");
                entity.Property(e => e.IdInsumo)
                    .HasColumnName("IdInsumo");
                entity.Property(e => e.Descripcion)
                    .HasColumnName("Descripcion");
                entity.Property(e => e.Precio)
                    .HasColumnName("Precio")
                    .HasColumnType("numeric(18,2)");
                entity.Property(e => e.Cantidad)
                    .HasColumnName("Cantidad")
                    .HasColumnType("numeric(18,5)");

                entity.Property(e => e.Borrado)
                    .HasColumnName("borrado")
                    .HasDefaultValue(false);
                entity.Property(e => e.FechaInsertado)
                 .HasColumnType("datetime")
                 .HasColumnName("FechaInsertado")
                 .HasDefaultValueSql("getdate()");
                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");
                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");
                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");

                entity.HasOne(e => e.Compra)
                    .WithMany()
                    .HasForeignKey(e => e.IdCompra)
                    .HasConstraintName("FK_ComprasDetalle_Compras");
                
                entity.HasOne(e => e.Insumo)
                    .WithMany()
                    .HasForeignKey(e => e.IdInsumo)
                    .HasConstraintName("FK_ComprasDetalle_Insumo");

                entity.HasQueryFilter(e => !e.Borrado);
            });

            modelBuilder.Entity<HistorialClinico>(entity =>
            {
                entity.HasKey(e => e.IdHistorial);

                entity.ToTable("HistorialClinico");

                entity.Property(e => e.IdPaciente)
                    .HasColumnName("IdPaciente");
                entity.Property(e => e.IdTratamiento)
                    .HasColumnName("IdTratamiento")
                     .IsRequired(false);
                entity.Property(e => e.IdPatologia)
                    .HasColumnName("IdPatologia")
                    .IsRequired(false);
                entity.Property(e => e.IdVacuna)
                    .HasColumnName("IdVacuna")
                    .IsRequired(false);
                entity.Property(e => e.IdProcedimiento)
                    .HasColumnName("IdProcedimiento")
                    .IsRequired(false);
                entity.Property(e => e.Descripcion)
                    .HasColumnName("Descripcion")
                    .IsRequired(false);
                entity.Property(e => e.Borrado)
                    .HasColumnName("borrado")
                    .HasDefaultValue(false);
                entity.Property(e => e.Facturado)
                    .HasColumnName("Facturado");
                entity.Property(e => e.FechaInsertado)
                 .HasColumnType("datetime")
                 .HasColumnName("FechaInsertado")
                 .HasDefaultValueSql("getdate()");
                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");
                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");
                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");
                entity.HasOne(d => d.IdTratamientoNavigation)
                                                .WithMany(p => p.HistorialClinicos)
                                                .HasForeignKey(d => d.IdTratamiento)
                                                .OnDelete(DeleteBehavior.ClientSetNull)
                                                .HasConstraintName("FK_HistorialClinico_Tratamiento");
                entity.HasOne(d => d.IdPacienteNavigation)
                                                .WithMany(p => p.HistorialClinicos)
                                                .HasForeignKey(d => d.IdPaciente)
                                                .OnDelete(DeleteBehavior.ClientSetNull)
                                                .HasConstraintName("FK_HistorialClinico_Paciente");
                entity.HasOne(d => d.IdPatologiaNavigation)
                                                               .WithMany(p => p.HistorialClinicos)
                                                               .HasForeignKey(d => d.IdPatologia)
                                                               .OnDelete(DeleteBehavior.ClientSetNull)
                                                               .HasConstraintName("FK_HistorialClinico_Patologia");
                entity.HasOne(d => d.IdPatologiaNavigation)
                                                               .WithMany(p => p.HistorialClinicos)
                                                               .HasForeignKey(d => d.IdPatologia)
                                                               .OnDelete(DeleteBehavior.ClientSetNull)
                                                               .HasConstraintName("FK_HistorialClinico_Patologia");
                entity.HasOne(d => d.IdVacunaNavigation)
                                                               .WithMany(p => p.HistorialClinicos)
                                                               .HasForeignKey(d => d.IdVacuna)
                                                               .OnDelete(DeleteBehavior.ClientSetNull)
                                                               .HasConstraintName("FK_HistorialClinico_Vacuna");
                entity.HasOne(d => d.IdProcedimientoNavigation)
                                                                 .WithMany(p => p.HistorialClinicos)
                                                                 .HasForeignKey(d => d.IdProcedimiento)
                                                                 .OnDelete(DeleteBehavior.ClientSetNull)
                                                                 .HasConstraintName("FK_Compras_Proveedores");

                entity.HasQueryFilter(e => !e.Borrado);
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.IdVenta);

                entity.ToTable("Venta");

                entity.HasKey(e => e.IdVenta);

                entity.Property(e => e.NroBoleta).HasColumnName("NroBoleta");
                entity.Property(e => e.IdCliente).HasColumnName("IdCliente");
                entity.Property(e => e.FechaVenta).HasColumnName("FechaVenta");
                entity.Property(e => e.Facturado).HasColumnName("Facturado");
                entity.Property(e => e.Finalizado).HasColumnName("Finalizado");
                entity.Property(e => e.FechaVenta)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaVenta");
                entity.Property(e => e.Borrado).HasColumnName("Borrado");
                entity.Property(e => e.FechaInsertado)
                 .HasColumnType("datetime")
                 .HasColumnName("FechaInsertado")
                 .HasDefaultValueSql("getdate()");

                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");

                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");

                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");
                entity.HasOne(d => d.IdClienteNavigation)
                                                 .WithMany(p => p.Ventas)
                                                 .HasForeignKey(d => d.IdCliente)
                                                 .OnDelete(DeleteBehavior.ClientSetNull)
                                                 .HasConstraintName("FK_Venta_Cliente");

                entity.HasQueryFilter(e => !e.Borrado);
                
                });

            modelBuilder.Entity<VentaDetalle>(entity =>
            {
                entity.HasKey(e => e.IdVentaDetalle);

                entity.ToTable("VentaDetalle");

                entity.HasKey(e => e.IdVentaDetalle);

                entity.Property(e => e.IdVenta).HasColumnName("IdVenta");
                entity.Property(e => e.IdInsumo).HasColumnName("IdInsumo");
                entity.Property(e => e.Cantidad).HasColumnName("Cantidad");
                entity.Property(e => e.Precio).HasColumnName("Precio");
                entity.Property(e => e.Descripcion).HasColumnName("Descripcion");
                entity.Property(e => e.Borrado).HasColumnName("Borrado");
                entity.Property(e => e.FechaInsertado)
                 .HasColumnType("datetime")
                 .HasColumnName("FechaInsertado")
                 .HasDefaultValueSql("getdate()");

                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");

                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");

                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");
                entity.HasOne(d => d.IdInsumoNavigation)
                                                 .WithMany(p => p.VentaDetalles)
                                                 .HasForeignKey(d => d.IdInsumo)
                                                 .OnDelete(DeleteBehavior.ClientSetNull)
                                                 .HasConstraintName("FK_VentaDetalle_Insumo");
                entity.HasOne(d => d.IdVentaNavigation)
                                                .WithMany(p => p.VentaDetalles)
                                                .HasForeignKey(d => d.IdVenta)
                                                .OnDelete(DeleteBehavior.ClientSetNull)
                                                .HasConstraintName("FK_VentaDetalle_Venta");

                entity.HasOne(d => d.IdHistorialNavigation)
                                                 .WithMany(p => p.VentaDetalles)
                                                 .HasForeignKey(d => d.IdHistorial)
                                                 .OnDelete(DeleteBehavior.ClientSetNull)
                                                 .HasConstraintName("FK_VentaDetalle_Cliente");
                entity.HasQueryFilter(e => !e.Borrado);
            });
            modelBuilder.Entity<StockInsumo>(entity =>
            {
                entity.HasKey(e => e.IdStock);

                entity.ToTable("StockInsumos");

                entity.HasKey(e => e.IdStock);

                entity.Property(e => e.IdInsumo).HasColumnName("IdInsumo");
                entity.Property(e => e.IdLote).HasColumnName("IdLote");
                entity.Property(e => e.CantidadActual).HasColumnName("CantidadActual");
                entity.Property(e => e.Borrado).HasColumnName("Borrado");
                entity.Property(e => e.FechaInsertado)
                 .HasColumnType("datetime")
                 .HasColumnName("FechaInsertado")
                 .HasDefaultValueSql("getdate()");

                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");

                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");

                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");
                entity.HasOne(d => d.IdInsumoNavigation)
                                                 .WithMany(p => p.StockInsumos)
                                                 .HasForeignKey(d => d.IdInsumo)
                                                 .OnDelete(DeleteBehavior.ClientSetNull)
                                                 .HasConstraintName("FK_StockInsumo_Insumo");
                entity.HasOne(d => d.IdLoteNavigation)
                                                 .WithMany(p => p.StockInsumos)
                                                 .HasForeignKey(d => d.IdLote)
                                                 .OnDelete(DeleteBehavior.ClientSetNull)
                                                 .HasConstraintName("FK_StockInsumoLote");
                entity.HasQueryFilter(e => !e.Borrado);
            });
            modelBuilder.Entity<DeudaProveedor>(entity =>
            {
                entity.HasKey(e => e.IdDeuda);

                entity.ToTable("DeudaProveedor");

                entity.HasKey(e => e.IdDeuda);

                entity.Property(e => e.IdCompra).HasColumnName("IdCompra");
                entity.Property(e => e.FechaPago).HasColumnName("FechaPago");
                entity.Property(e => e.MontoPagado).HasColumnName("MontoPagado");
                entity.Property(e => e.MetodoPago).HasColumnName("MetodoPago");
                entity.Property(e => e.Borrado).HasColumnName("Borrado");
                entity.Property(e => e.FechaInsertado)
                 .HasColumnType("datetime")
                 .HasColumnName("FechaInsertado")
                 .HasDefaultValueSql("getdate()");

                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");

                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");

                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");
                entity.HasOne(d => d.IdCompraNavigation)
                                                 .WithMany(p => p.DeudaProveedores)
                                                 .HasForeignKey(d => d.IdCompra)
                                                 .OnDelete(DeleteBehavior.ClientSetNull)
                                                 .HasConstraintName("FK_DeudaProveedor_Compra");
                entity.HasQueryFilter(e => !e.Borrado);
            });
            modelBuilder.Entity<PagoVenta>(entity =>
            {
                entity.HasKey(e => e.IdPago);

                entity.ToTable("PagoVenta");
                entity.Property(e => e.IdVenta).HasColumnName("IdVenta");
                entity.Property(e => e.FechaPago).HasColumnName("FechaPago");
                entity.Property(e => e.MontoPagado).HasColumnName("MontoPagado");
                entity.Property(e => e.MetodoPago).HasColumnName("MetodoPago");
                entity.Property(e => e.Borrado).HasColumnName("Borrado");
                entity.Property(e => e.FechaInsertado)
                 .HasColumnType("datetime")
                 .HasColumnName("FechaInsertado")
                 .HasDefaultValueSql("getdate()");

                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");

                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");

                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");
                entity.HasOne(d => d.IdVentaNavigation)
                                                 .WithMany(p => p.PagoVentas)
                                                 .HasForeignKey(d => d.IdVenta )
                                                 .OnDelete(DeleteBehavior.ClientSetNull)
                                                 .HasConstraintName("FK_PagoVenta_Venta");
                entity.HasQueryFilter(e => !e.Borrado);
            });
            OnModelCreatingPartial(modelBuilder);
            modelBuilder.Entity<HistorialMovimiento>(entity =>
            {
                entity.HasKey(e => e.IdHistorial);

                entity.ToTable("HistorialMovimiento");
                entity.Property(e => e.Fecha).HasColumnName("Fecha");
                entity.Property(e => e.Cantidad).HasColumnName("Cantidad");
                entity.Property(e => e.Precio).HasColumnName("Precio");
                entity.Property(e => e.IdCompraDetalle).HasColumnName("IdCompraDetalle");
                entity.Property(e => e.IdVentaDetalle).HasColumnName("IdVentaDetalle");
                entity.Property(e => e.Borrado).HasColumnName("Borrado");
                entity.Property(e => e.FechaInsertado)
                 .HasColumnType("datetime")
                 .HasColumnName("FechaInsertado")
                 .HasDefaultValueSql("getdate()");

                entity.Property(e => e.FechaModificado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaModificado");

                entity.Property(e => e.FechaBorrado)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaBorrado");
                entity.Property(e => e.IdUsuarioInserto).HasColumnName("IdUsuarioInserto");

                entity.Property(e => e.IdUsuarioModifico).HasColumnName("IdUsuarioModifico");
                entity.HasOne(d => d.IdCompraDetalleNavigation)
                                                 .WithMany(p => p.HistorialMovimientos)
                                                 .HasForeignKey(d => d.IdCompraDetalle)
                                                 .OnDelete(DeleteBehavior.ClientSetNull)
                                                 .HasConstraintName("FK_HistorialMovimiento_CompraDetalle");
                entity.HasOne(d => d.IdVentaDetalleNavigation)
                                                 .WithMany(p => p.HistorialMovimientos)
                                                 .HasForeignKey(d => d.IdVentaDetalle)
                                                 .OnDelete(DeleteBehavior.ClientSetNull)
                                                 .HasConstraintName("FK_HistorialMovimiento_VentaDetalle");
                entity.HasQueryFilter(e => !e.Borrado);
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
