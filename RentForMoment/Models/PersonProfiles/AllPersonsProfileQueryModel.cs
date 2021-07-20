namespace RentForMoment.Models.PersonProfiles
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllPersonsProfileQueryModel
    {

        public IEnumerable<string> TypeWork { get; init; }

        [Display(Name = "Search")]
        public string SearchTerm { get; init; }

        public IEnumerable<ProfileSorting> Sorting { get; init; }

        public IEnumerable<ListingProfilesViewModel> Profiles { get; init; }
    }
}
