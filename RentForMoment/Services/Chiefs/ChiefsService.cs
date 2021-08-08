namespace RentForMoment.Services.Chiefs
{
    using System.Linq;
    using RentForMoment.Data;

    public class ChiefsService : IChiefsService
    {

        private readonly RentForMomentDbContext data;

        public ChiefsService(RentForMomentDbContext data)
            => this.data = data;

        public int GetIdByUser(string userId)
        =>this.data
             .Chiefs
             .Where(c => c.UserId == userId)
             .Select(c => c.Id)
             .FirstOrDefault();

            public bool IsChief(string userId)
        => this.data
            .Chiefs
            .Any(c => c.UserId == userId);
    }
}
