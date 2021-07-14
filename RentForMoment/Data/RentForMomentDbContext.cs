namespace RentForMoment.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RentForMoment.Data.Models;

    public class RentForMomentDbContext : IdentityDbContext
    {

        public DbSet<PersonProfile> PersonsProfiles { get; init; }

        public RentForMomentDbContext(DbContextOptions<RentForMomentDbContext> options)
            : base(options)
        {
        }
    }
}
