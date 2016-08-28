using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EventTracker.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
                        .HasRequired(a => a.Event)
                        .WithMany()
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                        .HasMany(u => u.Followers)
                        .WithRequired(f => f.Followee)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followees)
                .WithRequired(f => f.Follower)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}