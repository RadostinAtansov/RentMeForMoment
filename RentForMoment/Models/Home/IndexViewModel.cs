namespace RentForMoment.Models.Home
{
    using RentForMoment.Services.PersonProfiles.Models;
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int TotalProfiles { get; init; }

        public int TotalUsers { get; init; }

        public int TotalRentForWork { get; init; }

        public IList<LatestPersonProfileServiceModel> Profiles { get; set; }
    }
}
