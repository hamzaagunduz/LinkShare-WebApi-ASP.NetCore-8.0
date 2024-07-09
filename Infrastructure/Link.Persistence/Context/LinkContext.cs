using Link.Domain.Entities;
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
        public DbSet<Profile> Profiles { get; set; }
    }
}