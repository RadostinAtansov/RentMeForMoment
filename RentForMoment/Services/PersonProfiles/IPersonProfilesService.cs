namespace RentForMoment.Services.PersonProfiles
{
    using RentForMoment.Models;
    using RentForMoment.Services.PersonProfiles.Models;
    using System.Collections.Generic;

    public interface IPersonProfilesService
    {
        PersonProfilesQueryServiceModel All(
            string typeOfWork = null,
            string searchTerm = null,
            ProfileSorting sorting = ProfileSorting.DateRegistered,
            int currentPage = 1,
            int profilesPerPage = int.MaxValue,
            bool publicOnly = true);

        IEnumerable<LatestPersonProfileServiceModel> Latest();

        PersonProfileDetailsServiceModel Details(int profileId);

        bool Delete(int id);

        void Approvell(int personProfileId);

        int Create(
                string firstname,
                string lastname,
                int years,
                string personImage,
                string skills,
                string city,
                string description,
                int categoryId,
                string typeOfWork,
                int chiefsId);

        bool Edit(
               int profileId,
               string firstname,
               string lastname,
               int years,
               string personImage,
               string skills,
               string city,
               string description,
               string typeOfWork,
               bool isPublic);

        bool IsChiefs(int profileId, int chiefId);


        IEnumerable<PersonProfilesServicesModel> ByUser(string userId);

        IEnumerable<string> AllProfilesTypeOfWOrk();

        IEnumerable<PersonServiceCategoryModel> AllPersonProfilesCategory();

        bool CategoryExists(int categoryId);
    }
}
