namespace RentForMoment.Data.Models
{

    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class PersonProfile
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(NameLength)]
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

        public bool IsOnline { get; set; }

        [Required]
        public string City { get; set; }

        public int CategoryId { get; set; }
        
        public Category Category { get; set; }
    }
}
