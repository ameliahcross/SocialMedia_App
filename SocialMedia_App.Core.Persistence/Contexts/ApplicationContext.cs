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
            // User con Post
            modelBuilder.Entity<User>()
               .HasMany(u => u.Posts)
               .WithOne(p => p.User)
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            // User con Friendship
            modelBuilder.Entity<User>()
              .HasMany(u => u.Friendships)
              .WithOne(f => f.User)
              .HasForeignKey(f => f.UserId)
              .OnDelete(DeleteBehavior.Cascade);

            // Post con Comments
            modelBuilder.Entity<Post>()
               .HasMany(p => p.Comments)
               .WithOne(c => c.Post)
               .HasForeignKey(c => c.PostId)
               .OnDelete(DeleteBehavior.Cascade);

            // Friendship con User
            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.User)
                .WithMany(u => u.Friendships)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Friendship con User(Friend)
            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.Friend)
                .WithMany()
                .HasForeignKey(f => f.FriendId)
                .OnDelete(DeleteBehavior.Restrict);

            // Comment con User
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Comment con Comment(ParentComment)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ParentComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.ParentCommentId);
            #endregion

            #region property configurations
                #region User
                modelBuilder.Entity<User>(entity =>
                {
                    entity.Property(e => e.Name)
                            .IsRequired()
                            .HasMaxLength(100);

                    entity.Property(e => e.LastName)
                             .IsRequired()
                             .HasMaxLength(100);

                    entity.Property(e => e.Phone)
                            .IsRequired()
                            .HasMaxLength(15);

                    entity.Property(e => e.ImageUrl);

                    entity.Property(e => e.Email)
                            .IsRequired()
                            .HasMaxLength(100);

                    entity.Property(e => e.UserName)
                            .IsRequired()
                            .HasMaxLength(50);

                    entity.Property(e => e.Password)
                            .IsRequired();

                    entity.Property(e => e.IsActive)
                            .IsRequired();

                    entity.Property(e => e.ActivationToken);
                });
                 #endregion

                #region Post
            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.Content)
                        .IsRequired(false);

                entity.Property(e => e.CreationDate)
                        .IsRequired();

                entity.Property(e => e.ImageUrl)
                        .IsRequired(false);

                entity.Property(e => e.YouTubeLink)
                        .IsRequired(false);

            });
            #endregion

                #region Friendship

            #endregion

                #region Comment
                modelBuilder.Entity<Comment>(entity =>
                {
                    entity.Property(e => e.Content)
                            .IsRequired();

                    entity.Property(e => e.CreationDate)
                            .IsRequired();

                    entity.Property(e => e.ParentCommentId)
                            .IsRequired(false);
                });
                #endregion
            #endregion
        }

    }
}

