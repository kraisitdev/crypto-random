using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using crypto_random.DataAccesses.CryptoRandomDbContext;

namespace crypto_random.DataAccesses.CryptoRandomDbContext.Context
{
    public partial class AppDb : DbContext
    {
        public AppDb()
        {
        }

        public AppDb(DbContextOptions<AppDb> options)
            : base(options)
        {
        }

        public virtual DbSet<TblPlayer> TblPlayers { get; set; } = null!;
        public virtual DbSet<TblTranaction> TblTranactions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=localhost;Port=54301;Database=CryptoRandomDb;User Id=pgadmin;Password=P@ssw0rd;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblPlayer>(entity =>
            {
                entity.HasKey(e => e.PlayerName)
                    .HasName("tbl_player_pkey");

                entity.ToTable("tbl_player");

                entity.HasIndex(e => e.IsActive, "idx_tbl_player");

                entity.Property(e => e.PlayerName)
                    .HasMaxLength(50)
                    .HasColumnName("player_name");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Modifydate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("modifydate");
            });

            modelBuilder.Entity<TblTranaction>(entity =>
            {
                entity.ToTable("tbl_tranaction");

                entity.HasIndex(e => e.PlayerName, "idx_tbl_tranaction");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CoinCode)
                    .HasMaxLength(50)
                    .HasColumnName("coin_code");

                entity.Property(e => e.CoinName)
                    .HasMaxLength(255)
                    .HasColumnName("coin_name");

                entity.Property(e => e.CoinUnit).HasColumnName("coin_unit");

                entity.Property(e => e.Modifydate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("modifydate");

                entity.Property(e => e.PlayerName)
                    .HasMaxLength(50)
                    .HasColumnName("player_name");

                entity.HasOne(d => d.PlayerNameNavigation)
                    .WithMany(p => p.TblTranactions)
                    .HasForeignKey(d => d.PlayerName)
                    .HasConstraintName("tbl_tranaction_player_name_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
