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


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Clients__E67E1A0463C11338");

            entity.HasIndex(e => e.ClientEmail, "UQ__Clients__AD48A6FF1AB37E7A").IsUnique();

            entity.Property(e => e.ClientId)
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
            entity.Property(e => e.TitleId).HasColumnName("TitleID");
            entity.Property(e => e.TypeId).HasColumnName("TypeID");

            entity.HasOne(d => d.Title).WithMany(p => p.Clients)
                .HasForeignKey(d => d.TitleId)
                .HasConstraintName("FK_Clients_Title");

            entity.HasOne(d => d.Type).WithMany(p => p.Clients)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_Clients_ClientTypes");
        });

        modelBuilder.Entity<ClientType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__Client_T__516F0395F214B4C1");

            entity.ToTable("Client_Types");

            entity.HasIndex(e => e.TypeName, "UQ__Client_T__D4E7DFA84B60AE67").IsUnique();

            entity.Property(e => e.TypeId).HasColumnName("TypeID");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ClientsLogin>(entity =>
        {
            entity.HasKey(e => e.LoginId).HasName("PK__Clients___4DDA283861131F08");

            entity.ToTable("Clients_Login");

            entity.HasIndex(e => e.LoginUsername, "UQ__Clients___9D11482E1C586698").IsUnique();

            entity.Property(e => e.LoginId).HasColumnName("LoginID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.LoginPassword)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.LoginUsername)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Client).WithMany(p => p.ClientsLogins)
                .HasForeignKey(d => d.ClientId)
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
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF1F42F58A2");

            entity.Property(e => e.EmployeeId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("EmployeeID");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeSurname)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TitleId).HasColumnName("TitleID");

            entity.HasOne(d => d.Title).WithMany(p => p.Employees)
                .HasForeignKey(d => d.TitleId)
                .HasConstraintName("FK_Employees_Title");
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.HasKey(e => e.TitleId).HasName("PK__Titles__757589E612E2AB62");

            entity.HasIndex(e => e.Title1, "UQ__Titles__2CB664DC5C012CD9").IsUnique();

            entity.Property(e => e.TitleId).HasColumnName("TitleID");
            entity.Property(e => e.Title1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
