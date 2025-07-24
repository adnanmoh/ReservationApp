using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Reservation_APIs.Models;

namespace Reservation_APIs.Data
{
    public partial class ReservationAppContext : DbContext
    {
        public ReservationAppContext()
        {
        }

        public ReservationAppContext(DbContextOptions<ReservationAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<AppUser> AppUsers { get; set; } = null!;
        public virtual DbSet<Chat> Chats { get; set; } = null!;
        public virtual DbSet<ChatsMessage> ChatsMessages { get; set; } = null!;
        public virtual DbSet<FinancialAccount> FinancialAccounts { get; set; } = null!;
        public virtual DbSet<Reserve> Reserves { get; set; } = null!;
        public virtual DbSet<Resort> Resorts { get; set; } = null!;
        public virtual DbSet<ResortService> ResortServices { get; set; } = null!;
        public virtual DbSet<ResortType> ResortTypes { get; set; } = null!;
        public virtual DbSet<ResortsPhoto> ResortsPhotos { get; set; } = null!;
        public virtual DbSet<TypesOfService> TypesOfServices { get; set; } = null!;
        public virtual DbSet<UserType> UserTypes { get; set; } = null!;
        public virtual DbSet<ResortAndService> ResortAndServices { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ResortAndService>(entity =>
            {
                entity.HasKey(e => new { e.ResortId, e.ServiceId })
                    .HasName("PK_ResortAndService");

                entity.HasOne(d => d.Resorts)
                    .WithMany(p => p.ResortAndServices)
                    .HasForeignKey(d => d.ResortId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResortAndService_Resort");

                entity.HasOne(d => d.Services)
                    .WithMany(p => p.ResortAndServices)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResortAndService_ResortService");
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__AppUser__1788CCAC7AE3045C");

                entity.Property(e => e.IsApproved).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsOnline).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.AppUsers)
                    .HasForeignKey(d => d.UserTypeId)
                    .HasConstraintName("FK__AppUser__UserTyp__33D4B598");
            });

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.Property(e => e.IsDeletedFromReceiver).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsDeletedFromSender).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.ChatReceivers)
                    .HasForeignKey(d => d.ReceiverId)
                    .HasConstraintName("FK__Chats__ReceiverI__4316F928");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.ChatSenders)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK__Chats__SenderID__440B1D61");
            });

            modelBuilder.Entity<ChatsMessage>(entity =>
            {
                entity.Property(e => e.IsReaded).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Chat)
                    .WithMany(p => p.ChatsMessages)
                    .HasForeignKey(d => d.ChatId)
                    .HasConstraintName("FK__ChatsMess__ChatI__534D60F1");
            });

            modelBuilder.Entity<FinancialAccount>(entity =>
            {
                entity.HasKey(e => e.AccountId)
                    .HasName("PK__Financia__349DA58652C33B27");
            });

            modelBuilder.Entity<Reserve>(entity =>
            {
                entity.Property(e => e.IsApproved).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsReady).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsRejected).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Reserves)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Reserve__Account__4E88ABD4");

                entity.HasOne(d => d.Resort)
                    .WithMany(p => p.Reserves)
                    .HasForeignKey(d => d.ResortId)
                    .HasConstraintName("FK__Reserve__ResortI__4D94879B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reserves)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Reserve__UserID__4F7CD00D");
            });

            modelBuilder.Entity<Resort>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsApprovedAdd).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsApprovedEdit).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ResortType)
                    .WithMany(p => p.Resorts)
                    .HasForeignKey(d => d.ResortTypeId)
                    .HasConstraintName("FK__Resorts__ResortT__398D8EEE");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Resorts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Resorts__UserID__3A81B327");

                entity.HasMany(d => d.Services)
                    .WithMany(p => p.Resorts)
                    .UsingEntity<Dictionary<string, object>>(
                        "ResortAndService",
                        l => l.HasOne<ResortService>().WithMany().HasForeignKey("ServiceId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__ResortAnd__Servi__47DBAE45"),
                        r => r.HasOne<Resort>().WithMany().HasForeignKey("ResortId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__ResortAnd__Resor__46E78A0C"),
                        j =>
                        {
                            j.HasKey("ResortId", "ServiceId").HasName("PK__ResortAn__C17CCF202D092CDF");

                            j.ToTable("ResortAndService");

                            j.IndexerProperty<int>("ResortId").HasColumnName("ResortID");

                            j.IndexerProperty<int>("ServiceId").HasColumnName("ServiceID");
                        });
            });

            modelBuilder.Entity<ResortService>(entity =>
            {
                entity.HasKey(e => e.ServiceId)
                    .HasName("PK__ResortSe__C51BB0EA1ECB6905");

                entity.HasOne(d => d.ServiceType)
                    .WithMany(p => p.ResortServices)
                    .HasForeignKey(d => d.ServiceTypeId)
                    .HasConstraintName("FK__ResortSer__Servi__2D27B809");
            });

            modelBuilder.Entity<ResortsPhoto>(entity =>
            {
                entity.HasKey(e => e.PhotoId)
                    .HasName("PK__ResortsP__21B7B582A74E394F");

                entity.Property(e => e.IsMain).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Resort)
                    .WithMany(p => p.ResortsPhotos)
                    .HasForeignKey(d => d.ResortId)
                    .HasConstraintName("FK__ResortsPh__Resor__3E52440B");
            });

            modelBuilder.Entity<TypesOfService>(entity =>
            {
                entity.HasKey(e => e.ServiceTypeId)
                    .HasName("PK__TypesOfS__8ADFAA0C710A5731");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
