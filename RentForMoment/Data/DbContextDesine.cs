namespace RentForMoment.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public class DbContextDesine : IDesignTimeDbContextFactory<RentForMomentDbContext>
    {
        public RentForMomentDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RentForMomentDbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=RentForMoment;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new RentForMomentDbContext(optionsBuilder.Options);
        }
    }
}
