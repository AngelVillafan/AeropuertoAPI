using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VuelosAPI.Models
{
    public partial class sistem21_aeromexicoContext : DbContext
    {
        public sistem21_aeromexicoContext()
        {
        }

        public sistem21_aeromexicoContext(DbContextOptions<sistem21_aeromexicoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Vuelo> Vuelo { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Vuelo>(entity =>
            {
                entity.ToTable("vuelo");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Aerolinea).HasMaxLength(45);

                entity.Property(e => e.Destino).HasMaxLength(65);

                entity.Property(e => e.Estado).HasColumnType("int(11)");

                entity.Property(e => e.HorarioLlegada).HasColumnType("datetime");

                entity.Property(e => e.HorarioSalida).HasColumnType("datetime");

                entity.Property(e => e.Origen).HasMaxLength(65);

                entity.Property(e => e.Puerta).HasColumnType("int(11)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
