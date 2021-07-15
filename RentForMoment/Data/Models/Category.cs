namespace RentForMoment.Data.Models
{

    using System.Collections.Generic;

    public class Category
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public IEnumerable<PersonProfile> PersonProfiles { get; init; } = new List<PersonProfile>();
    }
}
