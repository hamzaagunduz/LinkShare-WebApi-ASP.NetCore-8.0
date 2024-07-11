using Link.Domain.Entities;
using Link.Persistence.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Link.Persistence.Context
{
    public class LinkContext : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-O82KITD;initial Catalog=LinkDb;integrated Security=true; TrustServerCertificate=True");

        }
        public DbSet<Test> Tests { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Linke> Linkes { get; set; }
        public DbSet<ProfileComment> ProfileComments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerConfiguration());
            modelBuilder.ApplyConfiguration(new FollowerConfiguration());
            modelBuilder.ApplyConfiguration(new FollowingConfiguration());
            modelBuilder.ApplyConfiguration(new LinkeConfiguration());
            modelBuilder.ApplyConfiguration(new ProfileCommentConfiguration());

        }
    }
}