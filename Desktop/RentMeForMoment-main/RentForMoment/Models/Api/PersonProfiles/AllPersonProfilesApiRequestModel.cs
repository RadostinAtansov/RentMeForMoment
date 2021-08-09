namespace RentForMoment.Models.Api.PersonProfiles
{

    using System.Collections.Generic;

    public class AllPersonProfilesApiRequestModel
    {

        public int ProfilesPerPage = 10;

        public string TypeOfWork { get; set; }

        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public ProfileSorting Sorting { get; init; }

        public int TotalProfiles { get; set; }

    }
}
