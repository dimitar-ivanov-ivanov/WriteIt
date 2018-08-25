namespace WriteIt.Data
{
    using System;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using WriteIt.Models;

    public class WriteItContext : IdentityDbContext<User>
    {
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Thread> Threads { get; set; }

        public DbSet<ThreadInstance> ThreadInstances { get; set; }

        public DbSet<UserThreadInstance> UserThreadInstances { get; set; }

        public WriteItContext(DbContextOptions<WriteItContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ConfigureUserThreadInstance(builder);
            //ConfigureThreadInstance(builder);
            //ConfigurePosts(builder);

            builder.
                Entity<Thread>()
               .HasIndex(t => t.Name)
               .IsUnique(true);

            builder.
             Entity<ThreadInstance>()
            .HasIndex(t => t.Name)
            .IsUnique(true);

            base.OnModelCreating(builder);
        }

        //private void ConfigurePosts(ModelBuilder builder)
        //{
        //    builder
        //        .Entity<Post>()
        //        .HasOne(p => p.ThreadInstance)
        //        .WithMany(ti => ti.Posts)
        //        .HasForeignKey(p => p.ThreadInstanceId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //}

        //private void ConfigureThreadInstance(ModelBuilder builder)
        //{
        //    builder.
        //             Entity<ThreadInstance>()
        //            .HasMany(t => t.Posts)
        //            .WithOne(p => p.ThreadInstance)
        //            .HasForeignKey(p => p.ThreadInstanceId)
        //            .OnDelete(DeleteBehavior.Cascade);
        //}

        private void ConfigureUserThreadInstance(ModelBuilder builder)
        {
            builder
                .Entity<UserThreadInstance>()
                .HasKey(uti => new { uti.SubscriberId, uti.ThreadInstanceId });

            builder
                .Entity<User>()
                .HasMany(u => u.SubscribedToThreads)
                .WithOne(uti => uti.Subscriber)
                .HasForeignKey(uti => uti.SubscriberId);

            builder
              .Entity<ThreadInstance>()
              .HasMany(u => u.Subscribers)
              .WithOne(uti => uti.ThreadInstance)
              .HasForeignKey(uti => uti.ThreadInstanceId);
        }
    }
}
