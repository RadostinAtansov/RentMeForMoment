namespace RentForMoment.Services.PersonProfiles.Models
{

    using System.Collections.Generic;

    public class PersonProfilesQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int TotalProfiles { get; init; }

        public int PersonProfiles { get; init; }

        public int ProfilePerPage { get; init; }

        public string TypeOfWork { get; set; }

        public IEnumerable<PersonProfilesServicesModel> Profiles { get; init; }
    }
}
