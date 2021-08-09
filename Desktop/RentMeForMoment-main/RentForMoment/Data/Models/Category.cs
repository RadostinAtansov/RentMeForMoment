namespace RentForMoment.Data.Models
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.CategoryConstraint;

    public class Category
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        public IEnumerable<PersonProfile> PersonProfiles { get; init; } = new List<PersonProfile>();
    }
}
