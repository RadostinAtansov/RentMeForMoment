namespace RentForMoment.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RentForMoment.Data.Models;

    public class RentForMomentDbContext : IdentityDbContext
    {

        public RentForMomentDbContext(DbContextOptions<RentForMomentDbContext> options)
            : base(options)
        {
        }

        public DbSet<PersonProfile> PersonProfiles { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Chief> Chiefs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder
                .Entity<PersonProfile>()
                .HasOne(c => c.Category)
                .WithMany(p => p.PersonProfiles)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Chief>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Chief>(c => c.UserId);

            base.OnModelCreating(builder);
        }

    }
}
