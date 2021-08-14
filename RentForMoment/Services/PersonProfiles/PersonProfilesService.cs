namespace RentForMoment.Services.PersonProfiles
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using RentForMoment.Data;
    using RentForMoment.Data.Models;
    using RentForMoment.Models;
    using RentForMoment.Services.PersonProfiles.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class PersonProfilesService : IPersonProfilesService
    {

        private readonly RentForMomentDbContext data;
        private readonly IConfigurationProvider mapper;

        public PersonProfilesService(
            RentForMomentDbContext data, 
            IMapper mapper)
        {
            this.mapper = mapper.ConfigurationProvider;
            this.data = data;
        }

        public PersonProfilesQueryServiceModel All(
            string typeOfWork = null, 
            string searchTerm = null,  
            ProfileSorting sorting = ProfileSorting.DateRegistered,
            int currentPage = 1,
            int profilesPerPage = int.MaxValue,
            bool publicOnly = true)
        {
            var profilesQuery = this.data
                .PersonProfiles
                .Where(p => publicOnly ? p.IsPublic : true);

            if (!string.IsNullOrWhiteSpace(typeOfWork))
            {
                profilesQuery = profilesQuery.Where(t => t.Category.Name == typeOfWork);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                profilesQuery = profilesQuery.Where(c =>
                c.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                c.Skills.ToLower().Contains(searchTerm.ToLower()) ||
                c.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            profilesQuery = sorting switch
            {
                ProfileSorting.Year => profilesQuery.OrderByDescending(y => y.Years),
                ProfileSorting.Skills => profilesQuery.OrderByDescending(s => s.Skills),
                ProfileSorting.DateRegistered or _ => profilesQuery.OrderByDescending(d => d.Id)
            };

            var totalProfiles = profilesQuery.Count();

            var profiles = GetProfiles(profilesQuery
                .Skip((currentPage - 1) * profilesPerPage)
                .Take(profilesPerPage));


            return new PersonProfilesQueryServiceModel
            {
                TotalProfiles = totalProfiles,
                CurrentPage = currentPage,
                Profiles = profiles
            };
        }

        public bool Delete(int id)
        {
            var profile = this.data.PersonProfiles.Find(id);

            data.Remove(profile);
            data.SaveChanges();

            return true;
        }

        public IEnumerable<LatestPersonProfileServiceModel> Latest()
              => data
                 .PersonProfiles
                 .Where(p => p.IsPublic)
                 .OrderByDescending(p => p.Id)
                 .ProjectTo<LatestPersonProfileServiceModel>(this.mapper)
                 .Take(3)
                 .ToList();

        public PersonProfileDetailsServiceModel Details(int id)
            => this.data
            .PersonProfiles
            .Where(p => p.Id == id)
            .ProjectTo<PersonProfileDetailsServiceModel>(this.mapper)
            .FirstOrDefault();

        public IEnumerable<PersonProfilesServicesModel> ByUser(string userId)
            => GetProfiles(this.data
                .PersonProfiles
                .Where(c => c.Chief.UserId == userId));

        public bool IsChiefs(int profileId, int chiefId)
            => this.data
                   .PersonProfiles
                   .Any(p => p.Id == profileId && p.ChiefId == chiefId);


        public void Approvell(int personProfileId)
        {
            var personProfile = this.data.PersonProfiles.Find(personProfileId);

            personProfile.IsPublic = !personProfile.IsPublic;

            this.data.SaveChanges();
        }

        public IEnumerable<string> AllProfilesTypeOfWOrk()
             => this.data
                .PersonProfiles
                .Select(w => w.Category.Name)
                .Distinct()
                .ToList();

        public IEnumerable<PersonServiceCategoryModel> AllPersonProfilesCategory()
         => this.data
           .Categories
           .ProjectTo<PersonServiceCategoryModel>(this.mapper)
           .ToList();


     

        private IEnumerable<PersonProfilesServicesModel> GetProfiles(IQueryable<PersonProfile> personProfileQuery)
            => personProfileQuery
            .ProjectTo<PersonProfilesServicesModel>(this.mapper)
                .ToList();

        public int Create(
            string firstname, 
            string lastname, 
            int years, 
            string personImage, 
            string skills, 
            string city, 
            string description, 
            int categoryId, 
            string typeOfWork, 
            int chiefsId)
        {
            var profileData = new PersonProfile
            {
                FirstName = firstname,
                LastName = lastname,
                Years = years,
                PersonImage = personImage,
                Skills = skills,
                City = city,
                Description = description,
                CategoryId = categoryId,
                TypeOfWork = typeOfWork,
                ChiefId = chiefsId,
                IsPublic = false
            };

            this.data.PersonProfiles.Add(profileData);
            this.data.SaveChanges();

            return profileData.Id;
        }

         public bool CategoryExists(int categoryId)
            => this.data
            .Categories
            .Any(p => p.Id == categoryId);

        public bool Edit(
            int profileId, 
            string firstname, 
            string lastname, 
            int years, 
            string personImage, 
            string skills, 
            string city, 
            string description, 
            string typeOfWork,
            bool isPublic)
        {

            var profileData = this.data.PersonProfiles.Find(profileId);

            if (profileData == null)
            {
                return false;
            }

            profileData.FirstName = firstname;
            profileData.LastName = lastname;
            profileData.Years = years;
            profileData.PersonImage = personImage;
            profileData.Skills = skills;
            profileData.City = city;
            profileData.Description = description;
            profileData.TypeOfWork = typeOfWork;
            profileData.IsPublic = isPublic;

            this.data.SaveChanges();

            return true;
        }
    }
}