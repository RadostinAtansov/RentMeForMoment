namespace RentForMoment.Services.PersonProfiles
{
    using RentForMoment.Models;
    using System.Collections.Generic;

    public interface IPersonProfilesService
    {
        PersonProfilesQueryServiceModel All(
            string typeOfWork,
            string searchTerm,
            ProfileSorting sorting,
            int currentPage,
            int profilesPerPage);

        IEnumerable<string> AllProfilesTypeOfWOrk();
    }
}
