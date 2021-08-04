namespace RentForMoment.Services.Chiefs
{
    using RentForMoment.Data;
    using System.Linq;

    public class ChiefsService : IChiefsService
    {

        private readonly RentForMomentDbContext data;

        public ChiefsService(RentForMomentDbContext data)
            => this.data = data;

        public bool IsChief(string userId)
        => this.data
            .Chiefs
            .Any(c => c.UserId == userId);
    }
}
