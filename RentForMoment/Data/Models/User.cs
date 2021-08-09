namespace RentForMoment.Data.Models
{

    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.UserConstants;

    public class User : IdentityUser
    {

        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }

    }
}
