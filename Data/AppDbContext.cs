using API_tresure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Text;
using tresure_api.Data.Enum;
namespace tresure_api.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Role> AppRoles { get; set; }
        public DbSet<User> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole{Name = "User", NormalizedName = "USER"}
            );
            builder.Entity<Role>().HasData(
                new Role{Id = 1, Name = MemberRoles.Admin},
                new Role{Id = 2, Name = MemberRoles.Member},
                new Role{Id = 3, Name = MemberRoles.TaskMaster}
            );
        }

    }
}
