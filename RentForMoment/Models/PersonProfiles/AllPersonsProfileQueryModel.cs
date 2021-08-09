namespace RentForMoment.Models.PersonProfiles
{
    using RentForMoment.Services.PersonProfiles;
    using RentForMoment.Services.PersonProfiles.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllPersonsProfileQueryModel
    {

        public const int ProfilesPerPage = 3;

        public string TypeOfWork { get; set ; }

        [Display(Name = "Search by text...")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public ProfileSorting Sorting { get; init; }

        public int TotalProfiles { get; set; }

        public IEnumerable<string> TypeOfWorks { get; set; }

        public IEnumerable<PersonProfilesServicesModel> Profiles { get; set; }
    }
}
