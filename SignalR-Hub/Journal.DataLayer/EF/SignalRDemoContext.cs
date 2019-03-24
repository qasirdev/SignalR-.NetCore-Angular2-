using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Journal.DataLayer.EF
{
    public partial class SignalRDemoContext : DbContext
    {
        public virtual DbSet<DummyAlertNotification> DummyAlertNotification { get; set; }
        public virtual DbSet<JournalItem> JournalItem { get; set; }
        public virtual DbSet<PlayerDetail> PlayerDetail { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=YourServer;Database=YourDatabase;user id=YourUser;password=YourPassword;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DummyAlertNotification>(entity =>
            {
                entity.HasKey(e => e.NotificationId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.DummyAlertNotification)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DummyAlertNotification_PlayerDetail");
            });

            modelBuilder.Entity<JournalItem>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.SoldDate).HasColumnType("datetime");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.JournalItem)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JournalItem_PlayerDetail");
            });

            modelBuilder.Entity<PlayerDetail>(entity =>
            {
                entity.HasKey(e => e.PlayerId);

                entity.Property(e => e.BadgePhotoUrl)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentPricePc).HasColumnName("CurrentPricePC");

                entity.Property(e => e.CurrentPricePs4).HasColumnName("CurrentPricePS4");

                entity.Property(e => e.CurrentPriceXbox).HasColumnName("CurrentPriceXBox");

                entity.Property(e => e.FacePhotoUrl)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.IsTradable).HasDefaultValueSql("((1))");

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.NationPhotoUrl)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.PlayerDburl)
                    .HasColumnName("PlayerDBURL")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.PlayerExternalId)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.PlayerFullName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PlayerDummybinId)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.PlayerDummyheadId)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.PlayerDummywizId)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.PlayerShortName).HasMaxLength(80);

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(6);
            });
        }
    }
}
