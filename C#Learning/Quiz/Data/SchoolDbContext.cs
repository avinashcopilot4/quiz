//using System;
//using System.Collections.Generic;
//using Entity.Models;
//using Microsoft.EntityFrameworkCore;

//namespace Entity.Data;

//public partial class SchoolDbContext : DbContext
//{
//    public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
//        : base(options)
//    {
//    }

//    public virtual DbSet<AttemptDetail> AttemptDetails { get; set; }

//    public virtual DbSet<AuditLog> AuditLogs { get; set; }

//    public virtual DbSet<Question> Questions { get; set; }

//    public virtual DbSet<Entity.Models.Quiz> Quizzes { get; set; }

//    public virtual DbSet<QuizAttempt> QuizAttempts { get; set; }

//    public virtual DbSet<User> Users { get; set; }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<AttemptDetail>(entity =>
//        {
//            entity.HasKey(e => e.DetailId).HasName("PK__AttemptD__135C314D5BEC8AE0");

//            entity.Property(e => e.DetailId).HasColumnName("DetailID");
//            entity.Property(e => e.AttemptId).HasColumnName("AttemptID");
//            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

//            entity.HasOne(d => d.Attempt).WithMany(p => p.AttemptDetails)
//                .HasForeignKey(d => d.AttemptId)
//                .HasConstraintName("FK__AttemptDe__Attem__787EE5A0");

//            entity.HasOne(d => d.Question).WithMany(p => p.AttemptDetails)
//                .HasForeignKey(d => d.QuestionId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__AttemptDe__Quest__797309D9");
//        });

//        modelBuilder.Entity<AuditLog>(entity =>
//        {
//            entity.HasKey(e => e.LogId).HasName("PK__AuditLog__5E5499A89A35A342");

//            entity.ToTable("AuditLog");

//            entity.Property(e => e.LogId).HasColumnName("LogID");
//            entity.Property(e => e.ActionDate)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.ActionType).HasMaxLength(50);
//            entity.Property(e => e.EntityId).HasColumnName("EntityID");
//            entity.Property(e => e.EntityType).HasMaxLength(50);
//        });

//        modelBuilder.Entity<Question>(entity =>
//        {
//            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06F8CF818DD68");

//            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
//            entity.Property(e => e.CreatedDate)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.DisplayOrder).HasDefaultValue(1);
//            entity.Property(e => e.IsActive).HasDefaultValue(true);
//            entity.Property(e => e.Option1).HasMaxLength(500);
//            entity.Property(e => e.Option2).HasMaxLength(500);
//            entity.Property(e => e.Option3).HasMaxLength(500);
//            entity.Property(e => e.Option4).HasMaxLength(500);
//            entity.Property(e => e.QuestionText).HasMaxLength(1000);
//            entity.Property(e => e.QuizId).HasColumnName("QuizID");

//            entity.HasOne(d => d.Quiz).WithMany(p => p.Questions)
//                .HasForeignKey(d => d.QuizId)
//                .HasConstraintName("FK__Questions__QuizI__5629CD9C");
//        });

//        modelBuilder.Entity<Entity.Models.Quiz>(entity =>
//        {
//            entity.HasKey(e => e.QuizId).HasName("PK__Quizzes__8B42AE6E78DB454C");

//            entity.Property(e => e.QuizId).HasColumnName("QuizID");
//            entity.Property(e => e.CreatedDate)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.Description).HasMaxLength(500);
//            entity.Property(e => e.Status)
//                .HasMaxLength(50)
//                .HasDefaultValue("Draft");
//            entity.Property(e => e.Title).HasMaxLength(200);
//            entity.Property(e => e.UpdatedDate)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//        });

//        modelBuilder.Entity<QuizAttempt>(entity =>
//        {
//            entity.HasKey(e => e.AttemptId).HasName("PK__QuizAtte__891A6886FBB4C87B");

//            entity.Property(e => e.AttemptId).HasColumnName("AttemptID");
//            entity.Property(e => e.CompletedDate)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.PercentageScore).HasColumnType("decimal(5, 2)");
//            entity.Property(e => e.QuizId).HasColumnName("QuizID");
//            entity.Property(e => e.UserId).HasColumnName("UserID");

//            entity.HasOne(d => d.Quiz).WithMany(p => p.QuizAttempts)
//                .HasForeignKey(d => d.QuizId)
//                .HasConstraintName("FK__QuizAttem__QuizI__6754599E");

//            entity.HasOne(d => d.User).WithMany(p => p.QuizAttempts)
//                .HasForeignKey(d => d.UserId)
//                .HasConstraintName("FK__QuizAttem__UserI__66603565");
//        });

//        modelBuilder.Entity<User>(entity =>
//        {
//            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC6B24CB7C");

//            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534BBC0893D").IsUnique();

//            entity.Property(e => e.UserId).HasColumnName("UserID");
//            entity.Property(e => e.CreatedDate)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.Email)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.IsActive).HasDefaultValue(true);
//            entity.Property(e => e.Password).HasMaxLength(255);
//            entity.Property(e => e.Gender).HasMaxLength(10);
//            entity.Property(e => e.Role)
//                .HasMaxLength(50)
//                .HasDefaultValue("Employee");
//            entity.Property(e => e.UpdatedDate)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.UserName).HasMaxLength(100);
//        });

//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//}
