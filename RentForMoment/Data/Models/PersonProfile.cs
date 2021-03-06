namespace RentForMoment.Data.Models
{

    using System.ComponentModel.DataAnnotations;

    using static DataConstants.PersonProfileConstraint;

    public class PersonProfile
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        //[Range(MinYearsOld, MaxYearsOld)] Database don`t make this validation
        public int Years { get; set; }

        [Required]
        [StringLength(MaximumSkillsLength, MinimumLength = 88)]
        public string Skills { get; set; }

        public string PersonImage { get; set; }

        public bool IsPublic { get; set; }

        public bool IsOnline { get; set; }

        [Required]
        public string City { get; set; }

        public string TypeOfWork { get; set; }

        public int CategoryId { get; set; }
        
        public Category Category { get; init; }

        public int ChiefId { get; set; }

        public Chief Chief { get; init; }
    }
}
