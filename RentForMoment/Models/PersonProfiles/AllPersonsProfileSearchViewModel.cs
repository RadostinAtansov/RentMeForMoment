namespace RentForMoment.Models.PersonProfiles
{
    using System.Collections.Generic;

    public class AllPersonsProfileSearchViewModel
    {

        public IEnumerable<string> TypeWork { get; init; }

        public IEnumerable<string> SearchTerm { get; init; }

        public IEnumerable<ProfileSorting> Sorting { get; init; }

        public IEnumerable<ListingProfilesViewModel> Profiles { get; init; }
    }
}
