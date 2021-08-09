namespace RentForMoment.Models.Chiefs
{

    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.ChiefConstraint;

    public class BecomeChiefViewModel
    {


        [Required]
        [StringLength(MaxLengthName, MinimumLength = MinLengthName)]
        public string Name { get; set; }

        [Required]
        [StringLength(MaxPhoneLength, MinimumLength = MinLengthName)]
        [Display(Name = "Phone Number")]

        public string PhoneNumber { get; set; }


    }
}
