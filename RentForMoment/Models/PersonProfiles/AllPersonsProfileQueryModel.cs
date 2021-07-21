namespace RentForMoment.Models.PersonProfiles
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllPersonsProfileQueryModel
    {

        public string TypeOfWork { get; set ; }

        public IEnumerable<string> TypeOfWorks { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; init; }

        public ProfileSorting Sorting { get; init; }

        public IEnumerable<ListingProfilesViewModel> Profiles { get; set; }
    }
}
