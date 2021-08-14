namespace RentForMoment.Tests.Data
{

    using RentForMoment.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class PersonProfiles
    {
        public static IEnumerable<PersonProfile> TenPublicPersonProfiles()
           => Enumerable.Range(0, 10).Select(i => new PersonProfile
           {
               IsPublic = true
           });

    }
}
