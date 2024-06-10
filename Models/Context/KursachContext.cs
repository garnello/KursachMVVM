using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KursachWPF.Models;

public partial class KursachContext : DbContext
{
    public KursachContext()
    {
    }

    public KursachContext(DbContextOptions<KursachContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<HistoryOfIssuance> HistoryOfIssuances { get; set; }

    public virtual DbSet<HistoryOfReturn> HistoryOfReturns { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Workwear> Workwears { get; set; }

    public virtual DbSet<WorkwearReceiptHistory> WorkwearReceiptHistories { get; set; }

    public virtual DbSet<WriteOffHistory> WriteOffHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Kursach;Username=postgres;Password=Zhjckfd2100");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("account_pkey");

            entity.ToTable("account");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Avatar).HasColumnName("avatar");
            entity.Property(e => e.Email)
                .HasMaxLength(75)
                .HasColumnName("email");
            entity.Property(e => e.Login)
                .HasMaxLength(30)
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("password");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("surname");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employee_pkey");

            entity.ToTable("employee");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateOfBirthday).HasColumnName("date_of_birthday");
            entity.Property(e => e.DateOfEmployment).HasColumnName("date_of_employment");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Number)
                .HasMaxLength(11)
                .HasColumnName("number");
            entity.Property(e => e.Post)
                .HasMaxLength(50)
                .HasColumnName("post");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .HasColumnName("surname");
        });

        modelBuilder.Entity<HistoryOfIssuance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("history_of_issuance_pkey");

            entity.ToTable("history_of_issuance");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateOfIssuance).HasColumnName("date_of_issuance");
            entity.Property(e => e.IdEmployee).HasColumnName("id_employee");
            entity.Property(e => e.IdWorkwear).HasColumnName("id_workwear");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.HistoryOfIssuances)
                .HasForeignKey(d => d.IdEmployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("history_of_issuance_id_employee_fkey");

            entity.HasOne(d => d.IdWorkwearNavigation).WithMany(p => p.HistoryOfIssuances)
                .HasForeignKey(d => d.IdWorkwear)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("history_of_issuance_id_workwear_fkey");
        });

        modelBuilder.Entity<HistoryOfReturn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("history_of_return_pkey");

            entity.ToTable("history_of_return");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateOfReturn).HasColumnName("date_of_return");
            entity.Property(e => e.IdEmployee).HasColumnName("id_employee");
            entity.Property(e => e.IdWorkwear).HasColumnName("id_workwear");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.HistoryOfReturns)
                .HasForeignKey(d => d.IdEmployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("history_of_return_id_employee_fkey");

            entity.HasOne(d => d.IdWorkwearNavigation).WithMany(p => p.HistoryOfReturns)
                .HasForeignKey(d => d.IdWorkwear)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("history_of_return_id_workwear_fkey");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("issuance_report_pkey");

            entity.ToTable("Report");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('issuance_report_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.DateOfReport).HasColumnName("date_of_report");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.IdEmployee).HasColumnName("id_employee");
            entity.Property(e => e.StatusOfReport)
                .HasMaxLength(1)
                .HasColumnName("status_of_report");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.Reports)
                .HasForeignKey(d => d.IdEmployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("issuance_report_id_employee_fkey");
        });

        modelBuilder.Entity<Workwear>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workwear_pkey");

            entity.ToTable("workwear");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DegreeOfWear).HasColumnName("degree_of_wear");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.Type)
                .HasMaxLength(75)
                .HasColumnName("type");
        });

        modelBuilder.Entity<WorkwearReceiptHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workwear_order_history_pkey");

            entity.ToTable("workwear_receipt_history");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('workwear_order_history_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.DateOfOrder).HasColumnName("date_of_order");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TypeWorkwear)
                .HasMaxLength(75)
                .HasColumnName("type_workwear");
        });

        modelBuilder.Entity<WriteOffHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("replacement_history_pkey");

            entity.ToTable("write-off_history");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('replacement_history_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.DateOfWriteOff).HasColumnName("date_of_write-off");
            entity.Property(e => e.IdWorkwear).HasColumnName("id_workwear");
            entity.Property(e => e.ReasonForWirteOff)
                .HasMaxLength(100)
                .HasColumnName("reason_for_wirte-off");

            entity.HasOne(d => d.IdWorkwearNavigation).WithMany(p => p.WriteOffHistories)
                .HasForeignKey(d => d.IdWorkwear)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("replacement_history_id_workwear_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
