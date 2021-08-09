namespace RentForMoment.Models.PersonProfiles
{
    using RentForMoment.Services.PersonProfiles;
    using RentForMoment.Services.PersonProfiles.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.PersonProfileConstraint;

    public class PersonProfileFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "nai-malko 5 bukvi - naj-mnogo 20")]
        public string Firstname { get; init; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Lastname { get; init; }

        [Range(MinYearsOld, MaxYearsOld)] //Database don`t make this validation
        public int Years { get; init; }

        [Required]
        [StringLength(MaximumCityLength, MinimumLength = MinimumCityLength)]
        public string City { get; init; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; init; }

        [Required]
        [StringLength(MaximumSkillsLength, MinimumLength = MinimumSkillsLength)]
        public string Skills { get; init; }

        [Url]
        [Required]
        [Display(Name = "Person Picture")]
        public string PersonImage { get; init; }

        [Display(Name = "What Work You Can Do")]
        public int CategoryId { get; init; }

        public string TypeOfWork { get; init; }

        public IEnumerable<PersonServiceCategoryModel> CategoriesPerson { get; set; }

    }
}
