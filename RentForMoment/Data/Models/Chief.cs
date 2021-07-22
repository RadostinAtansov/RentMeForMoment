namespace RentForMoment.Data.Models
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.ChiefConstraint;

    public class Chief
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(MaxLengthName)]
        public string Name { get; set; }

        [Required]
        [MaxLength(MaxPhoneLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string SearchingFor { get; set; }

        public string UserId { get; set; }

        public IEnumerable<PersonProfile> PersonProfiles { get; set; } = new List<PersonProfile>();
    }
}
