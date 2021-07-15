using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentForMoment.Models.PersonProfiles
{
    public class AddPersonProfile
    {

        public string FirstName { get; init; }

        public string LastName { get; init; }

        //[Range(MinYearsOld, MaxYearsOld)] Database don`t make this validation
        public int Years { get; init; }

        public string City { get; init; }

        public string Description { get; init; }

        public string Skills { get; init; }

        public string PersonImage { get; init; }

        public string FieldOfWork { get; set; }

        public int CategoryId { get; init; }

    }
}
