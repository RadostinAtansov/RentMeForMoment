namespace RentForMoment.Models.Home
{
  
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int TotalProfiles { get; init; }

        public int TotalUsers { get; init; }

        public int TotalRentForWork { get; init; }

        public List<ProfileIndexViewModel> Profiles { get; init; }
    }
}
