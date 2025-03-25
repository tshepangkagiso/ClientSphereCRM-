using System;
using System.Collections.Generic;
using CRM_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM_API.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientType> ClientTypes { get; set; }

    public virtual DbSet<ClientsLogin> ClientsLogins { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Title> Titles { get; set; }
    public virtual DbSet<EmployeeLogin> EmployeeLogins { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientID).HasName("PK__Clients__E67E1A0463C11338");

            entity.HasIndex(e => e.ClientEmail, "UQ__Clients__AD48A6FF1AB37E7A").IsUnique();

            entity.Property(e => e.ClientID)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ClientID");
            entity.Property(e => e.ClientAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ClientContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ClientEmail).HasMaxLength(150);
            entity.Property(e => e.ClientName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ClientSurname)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TitleID).HasColumnName("TitleID");
            entity.Property(e => e.TypeID).HasColumnName("TypeID");

            entity.HasOne(d => d.Title).WithMany(p => p.Clients)
                .HasForeignKey(d => d.TitleID)
                .HasConstraintName("FK_Clients_Title");

            entity.HasOne(d => d.Type).WithMany(p => p.Clients)
                .HasForeignKey(d => d.TypeID)
                .HasConstraintName("FK_Clients_ClientTypes");
        });

        modelBuilder.Entity<ClientType>(entity =>
        {
            entity.HasKey(e => e.TypeID).HasName("PK__Client_T__516F0395F214B4C1");

            entity.ToTable("Client_Types");

            entity.HasIndex(e => e.TypeName, "UQ__Client_T__D4E7DFA84B60AE67").IsUnique();

            entity.Property(e => e.TypeID).HasColumnName("TypeID");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ClientsLogin>(entity =>
        {
            entity.HasKey(e => e.LoginID).HasName("PK__Clients___4DDA283861131F08");

            entity.ToTable("Clients_Login");

            entity.HasIndex(e => e.LoginUsername, "UQ__Clients___9D11482E1C586698").IsUnique();

            entity.Property(e => e.LoginID).HasColumnName("LoginID");
            entity.Property(e => e.ClientID).HasColumnName("ClientID");
            entity.Property(e => e.LoginPassword)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.LoginUsername)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Client).WithMany(p => p.ClientsLogins)
                .HasForeignKey(d => d.ClientID)
                .HasConstraintName("FK_ClientsLogin_Clients");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__C3B4DFCA013F32BE");

            entity.Property(e => e.CommentDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CommentMessage)
                .HasMaxLength(355)
                .IsUnicode(false);

            entity.HasOne(d => d.CommentSentByNavigation).WithMany(p => p.CommentCommentSentByNavigations)
                .HasForeignKey(d => d.CommentSentBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_SentBy");

            entity.HasOne(d => d.CommentSentToNavigation).WithMany(p => p.CommentCommentSentToNavigations)
                .HasForeignKey(d => d.CommentSentTo)
                .HasConstraintName("FK_Comments_SentTo_Client");

            entity.HasOne(d => d.CommentSentTo1).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CommentSentTo)
                .HasConstraintName("FK_Comments_SentTo_Employee");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeID).HasName("PK__Employee__7AD04FF1F42F58A2");

            entity.Property(e => e.EmployeeID)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("EmployeeID");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeSurname)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TitleID).HasColumnName("TitleID");

            entity.HasOne(d => d.Title).WithMany(p => p.Employees)
                .HasForeignKey(d => d.TitleID)
                .HasConstraintName("FK_Employees_Title");
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.HasKey(e => e.TitleID).HasName("PK__Titles__757589E612E2AB62");

            entity.HasIndex(e => e.TitleName, "UQ__Titles__2CB664DC5C012CD9").IsUnique();

            entity.Property(e => e.TitleID).HasColumnName("TitleID");
            entity.Property(e => e.TitleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Title");
        });

        modelBuilder.Entity<EmployeeLogin>(entity =>
        {
            entity.HasKey(e => e.LoginID).HasName("PK__Employee__4DDA28381EC6BDC0");

            entity.ToTable("Employee_Login");

            entity.HasIndex(e => e.LoginUsername, "UQ__Employee__9D11482E4B634D7D").IsUnique();

            entity.Property(e => e.LoginID).HasColumnName("LoginID");
            entity.Property(e => e.EmployeeID).HasColumnName("EmployeeID");
            entity.Property(e => e.LoginPassword)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.LoginUsername)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
