namespace RentForMoment.Services.PersonProfiles.Models
{
    public class PersonProfilesServicesModel
    {

        public int Id { get; init; }

        public string PersonImage { get; init; }

        public string Description { get; init; }

        public string Firstname { get; init; }

        public string Lastname { get; init; }

        public int Years { get; init; }

        public string City { get; init; }

        public string Skills { get; init; }

        public string CategoryName { get; init; }

        public string TypeOfWorkName { get; init; }

        public bool IsPublic { get; init; }

    }
}
