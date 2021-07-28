using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TheGarageAPI.Entities
{
    public partial class TheGarageContext : DbContext
    {
        public TheGarageContext()
        {
        }

        public TheGarageContext(DbContextOptions<TheGarageContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DataUser> DataUsers { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceStatus> InvoiceStatuses { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<ReservationStatus> ReservationStatuses { get; set; }
        public virtual DbSet<Slot> Slots { get; set; }
        public virtual DbSet<SlotStatus> SlotStatuses { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:DataConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<DataUser>(entity =>
            {
                entity.ToTable("DATA_USER");

                entity.HasIndex(e => e.Email, "DATA_USER_EMAIL_UK")
                    .IsUnique();

                entity.HasIndex(e => e.Mobile, "DATA_USER_MOBILE_UK")
                    .IsUnique();

                entity.Property(e => e.DataUserId)
                    .HasMaxLength(10)
                    .HasColumnName("DATA_USER_ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("FIRST_NAME");

                entity.Property(e => e.FirstSurname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("FIRST_SURNAME");

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(14)
                    .HasColumnName("MOBILE");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(128)
                    .HasColumnName("PASSWORD_HASH");

                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(128)
                    .HasColumnName("PASSWORD_SALT");

                entity.Property(e => e.ProfilePicture)
                    .HasColumnType("image")
                    .HasColumnName("PROFILE_PICTURE");

                entity.Property(e => e.SecondName)
                    .HasMaxLength(50)
                    .HasColumnName("SECOND_NAME");

                entity.Property(e => e.SecondSurname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SECOND_SURNAME");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserTypeId).HasColumnName("USER_TYPE_ID");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.DataUsers)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DATA_USER_FK--USER_TYPE");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("INVOICE");

                entity.Property(e => e.InvoiceId)
                    .HasMaxLength(10)
                    .HasColumnName("INVOICE_ID");

                entity.Property(e => e.InvoiceStatusId).HasColumnName("INVOICE_STATUS_ID");

                entity.Property(e => e.PaidDate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("PAID_DATE");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(19, 4)")
                    .HasColumnName("PRICE");

                entity.Property(e => e.ReservationId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("RESERVATION_ID");

                entity.HasOne(d => d.InvoiceStatus)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.InvoiceStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("INVOICE_FK--INVOICE_STATUS");

                entity.HasOne(d => d.Reservation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.ReservationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("INVOICE_FK--RESERVATION");
            });

            modelBuilder.Entity<InvoiceStatus>(entity =>
            {
                entity.ToTable("INVOICE_STATUS");

                entity.HasIndex(e => e.Description, "INVOICE_STATUS_DESCRIPTION_UK")
                    .IsUnique();

                entity.Property(e => e.InvoiceStatusId).HasColumnName("INVOICE_STATUS_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("RESERVATION");

                entity.Property(e => e.ReservationId)
                    .HasMaxLength(10)
                    .HasColumnName("RESERVATION_ID");

                entity.Property(e => e.DataUserId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("DATA_USER_ID");

                entity.Property(e => e.End)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("END");

                entity.Property(e => e.ReservationStatusId).HasColumnName("RESERVATION_STATUS_ID");

                entity.Property(e => e.SlotId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("SLOT_ID");

                entity.Property(e => e.Start)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("START");

                entity.Property(e => e.VehiclePlate)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("VEHICLE_PLATE");

                entity.HasOne(d => d.DataUser)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.DataUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RESERVATION_FK--USER");

                entity.HasOne(d => d.ReservationStatus)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.ReservationStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RESERVATION_FK--RESERVATION_STATUS");

                entity.HasOne(d => d.Slot)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.SlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RESERVATION_FK--SLOT");

                entity.HasOne(d => d.VehiclePlateNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.VehiclePlate)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RESERVATION_FK--VEHICLE");
            });

            modelBuilder.Entity<ReservationStatus>(entity =>
            {
                entity.ToTable("RESERVATION_STATUS");

                entity.HasIndex(e => e.Description, "RESERVATION_STATUS_DESCRIPTION_UK")
                    .IsUnique();

                entity.Property(e => e.ReservationStatusId).HasColumnName("RESERVATION_STATUS_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Slot>(entity =>
            {
                entity.ToTable("SLOT");

                entity.Property(e => e.SlotId)
                    .HasMaxLength(10)
                    .HasColumnName("SLOT_ID");

                entity.Property(e => e.Floor)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("FLOOR");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("LOCATION");

                entity.Property(e => e.SlotStatusId).HasColumnName("SLOT_STATUS_ID");

                entity.HasOne(d => d.SlotStatus)
                    .WithMany(p => p.Slots)
                    .HasForeignKey(d => d.SlotStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SLOT_FK--SLOT_STATUS");
            });

            modelBuilder.Entity<SlotStatus>(entity =>
            {
                entity.ToTable("SLOT_STATUS");

                entity.HasIndex(e => e.Description, "SLOT_STATUS_DESCRIPTION_UK")
                    .IsUnique();

                entity.Property(e => e.SlotStatusId).HasColumnName("SLOT_STATUS_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("USER_TYPE");

                entity.HasIndex(e => e.Description, "USER_TYPE_DESCRIPTION_UK")
                    .IsUnique();

                entity.Property(e => e.UserTypeId).HasColumnName("USER_TYPE_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.VehiclePlate)
                    .HasName("VEHICLE_PK");

                entity.ToTable("VEHICLE");

                entity.Property(e => e.VehiclePlate)
                    .HasMaxLength(10)
                    .HasColumnName("VEHICLE_PLATE");

                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("BRAND");

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("COLOR");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Line)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("LINE");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(4)
                    .HasColumnName("MODEL");

                entity.Property(e => e.PlateCity)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("PLATE_CITY");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.VehicleTypeId).HasColumnName("VEHICLE_TYPE_ID");

                entity.HasOne(d => d.VehicleType)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.VehicleTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("VEHICLE_FK--VEHICLE_TYPE_ID");
            });

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.ToTable("VEHICLE_TYPE");

                entity.HasIndex(e => e.Description, "VEHICLE_TYPE_DESCRIPTION_UK")
                    .IsUnique();

                entity.Property(e => e.VehicleTypeId).HasColumnName("VEHICLE_TYPE_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
