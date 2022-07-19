using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ETF_API.Models.Database.EntityFramework
{
    public partial class ETFContext : DbContext
    {
        public ETFContext()
        {
        }

        public ETFContext(DbContextOptions<ETFContext> options)
            : base(options)
        {
        }

        public virtual DbSet<T_ETF> T_ETF { get; set; }
        public virtual DbSet<T_ETF_DATA> T_ETF_DATA { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=127.0.0.1;Database=ETF;Username=postgres;Password=password");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T_ETF>(entity =>
            {
                entity.HasIndex(e => e.Name, "T_ETF_Name_key")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<T_ETF_DATA>(entity =>
            {
                entity.Property(e => e.AccuralDate).HasMaxLength(100);

                entity.Property(e => e.AssetClass).HasMaxLength(100);

                entity.Property(e => e.CUSIP).HasMaxLength(100);

                entity.Property(e => e.Currency).HasMaxLength(5);

                entity.Property(e => e.Exchange).HasMaxLength(100);

                entity.Property(e => e.FxRate).HasPrecision(5, 2);

                entity.Property(e => e.ISIN).HasMaxLength(100);

                entity.Property(e => e.Location).HasMaxLength(100);

                entity.Property(e => e.MarketCurrency).HasMaxLength(5);

                entity.Property(e => e.MarketValue).HasPrecision(14, 2);

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.NotionalValue).HasPrecision(14, 2);

                entity.Property(e => e.Price).HasPrecision(9, 2);

                entity.Property(e => e.SEDOL).HasMaxLength(100);

                entity.Property(e => e.Sector).HasMaxLength(100);

                entity.Property(e => e.Shares).HasPrecision(14, 2);

                entity.Property(e => e.Ticker).HasMaxLength(10);

                entity.Property(e => e.Weight).HasPrecision(5, 2);

                entity.HasOne(d => d.EtfNavigation)
                    .WithMany(p => p.T_ETF_DATA)
                    .HasForeignKey(d => d.Etf)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("T_ETF_DATA_Etf_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
