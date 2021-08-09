namespace RentForMoment.Services.PersonProfiles.Models
{
    public class PersonProfileDetailsServiceModel : PersonProfilesServicesModel
    {
        public int ChiefsId { get; init; }

        public string ChiefsName { get; init; }

        public string UserId { get; init; }

        public int CategoryId { get; init; }
    }
}
