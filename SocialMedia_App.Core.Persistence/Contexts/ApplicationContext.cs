using System;
using SocialMedia_App.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace SocialMedia_App.Infrastructure.Persistence.Contexts
{
	public class ApplicationContext : DbContext

    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Comment> Comments { get; set; }


        // constructor
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // fluent API
            base.OnModelCreating(modelBuilder);

            #region tables
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Post>().ToTable("Posts");
            modelBuilder.Entity<Friendship>().ToTable("Friendships");
            modelBuilder.Entity<Comment>().ToTable("Comments");
            #endregion

            #region primary-keys
            modelBuilder.Entity<User>().HasKey(user => user.Id);
            modelBuilder.Entity<Post>().HasKey(post => post.Id);
            modelBuilder.Entity<Friendship>().HasKey(friendship => friendship.Id);
            modelBuilder.Entity<Comment>().HasKey( comment => comment.Id);
            #endregion

            #region relationships
            modelBuilder.Entity<User>()
               .HasMany(u => u.Posts)
            #endregion

            #region property configurations

            #region doctor
            //modelBuilder.Entity<Doctor>(entity =>
            //    {
            //        entity.Property(d => d.FirstName).IsRequired();
            //        entity.Property(d => d.LastName).IsRequired();
            //        entity.Property(d => d.Email).IsRequired();
            //        entity.Property(d => d.PhoneNumber).IsRequired();
            //        entity.Property(d => d.IdentificationNumber).IsRequired();
            //        entity.Property(d => d.Photo).IsRequired();
            //    });
            #endregion

            #region appointment
            //modelBuilder.Entity<Appointment>(entity =>
            //{
            //    entity.Property(a => a.Date)
            //            .IsRequired()
            //            .HasColumnType("date")
            //            .HasConversion(
            //                d => d.ToDateTime(new TimeOnly()),
            //                d => DateOnly.FromDateTime(d)
            //            );
            //    entity.Property(a => a.Time)
            //            .IsRequired()
            //            .HasColumnType("time")
            //            .HasConversion(
            //                  t => t.ToTimeSpan(),
            //                  t => TimeOnly.FromTimeSpan(t)
            //              ); ;
            //    entity.Property(a => a.Reason).IsRequired();
            //});
            #endregion

            #region Patient
            //modelBuilder.Entity<Patient>(entity =>
            //{
            //    entity.Property(p => p.FirstName).IsRequired()
            //                                     .HasMaxLength(50);
            //    entity.Property(p => p.LastName).IsRequired();
            //    entity.Property(p => p.PhoneNumber).IsRequired();
            //    entity.Property(p => p.Address).IsRequired();
            //    entity.Property(p => p.IdentificationNumber).IsRequired();
            //    entity.Property(p => p.DateOfBirth)
            //            .IsRequired()
            //            .HasConversion(
            //                d => d.ToDateTime(new TimeOnly()),
            //                d => DateOnly.FromDateTime(d)
            //            );
            //    entity.Property(p => p.IsSmoker).IsRequired();
            //    entity.Property(p => p.HasAllergies).IsRequired();
            //    entity.Property(p => p.Photo).IsRequired();
            //});
            #endregion

            #region user
            //modelBuilder.Entity<User>(entity =>
            //{
            //    entity.Property(u => u.Username).IsRequired()
            //            .HasMaxLength(100);
            //    entity.Property(u => u.Password).IsRequired();
            //    entity.Property(u => u.Email).IsRequired();
            //    entity.Property(u => u.Name).IsRequired()
            //            .HasMaxLength(100);
            //    entity.Property(u => u.LastName).IsRequired();
            //    entity.Property(u => u.Role).IsRequired();
            //});
            #endregion

            #endregion
        }

    }
}

