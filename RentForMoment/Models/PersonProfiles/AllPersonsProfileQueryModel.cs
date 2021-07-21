namespace RentForMoment.Models.PersonProfiles
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllPersonsProfileQueryModel
    {

        public const int ProfilesPerPage = 4;

        public string TypeOfWork { get; set ; }

        public IEnumerable<string> TypeOfWorks { get; set; }

        [Display(Name = "Search by text...")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public ProfileSorting Sorting { get; init; }

        public int TotalProfiles { get; set; }

        public IEnumerable<ListingProfilesViewModel> Profiles { get; set; }
    }
}
