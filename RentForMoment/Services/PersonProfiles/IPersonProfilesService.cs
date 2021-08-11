namespace RentForMoment.Services.PersonProfiles
{
    using RentForMoment.Models;
    using RentForMoment.Services.PersonProfiles.Models;
    using System.Collections.Generic;

    public interface IPersonProfilesService
    {
        PersonProfilesQueryServiceModel All(
            string typeOfWork,
            string searchTerm,
            ProfileSorting sorting,
            int currentPage,
            int profilesPerPage);

        IEnumerable<LatestPersonProfileServiceModel> Latest();

        PersonProfileDetailsServiceModel Details(int profileId);
            
        int Create(
                string firstname,
                string lastname,
                int years,
                string personImage,
                string skills,
                string city,
                string description,
                int categoryId,
                int chiefsId,
                string typeOfWork);

        bool Edit(
               int profileId,
               string firstname,
               string lastname,
               int years,
               string personImage,
               string skills,
               string city,
               string description,
               string typeOfWork);

        bool IsChiefs(int profileId, int chiefId);


        IEnumerable<PersonProfilesServicesModel> ByUser(string userId);

        IEnumerable<string> AllProfilesTypeOfWOrk();

        IEnumerable<PersonServiceCategoryModel> AllPersonProfilesCategory();

        bool CategoryExists(int categoryId);
    }
}
