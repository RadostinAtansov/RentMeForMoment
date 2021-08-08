namespace RentForMoment.Services.PersonProfiles
{

    using RentForMoment.Data;
    using RentForMoment.Data.Models;
    using RentForMoment.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class PersonProfilesService : IPersonProfilesService
    {

        private readonly RentForMomentDbContext data;

        public PersonProfilesService(RentForMomentDbContext data)
        {
            this.data = data;
        }

        public PersonProfilesQueryServiceModel All(
            string typeOfWork, 
            string searchTerm,  
            ProfileSorting sorting,
            int currentPage,
            int profilesPerPage)
        {
            var profilesQuery = this.data.PersonProfiles.AsQueryable();

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

        public PersonProfileDetailsServiceModel Details(int id)
            => this.data
            .PersonProfiles
            .Where(p => p.Id == id)
            .Select(p => new PersonProfileDetailsServiceModel
            {
                Id = p.Id,
                Firstname = p.FirstName,
                Lastname = p.LastName,
                Age = p.Years,
                City = p.City,
                Skills = p.Skills,
                Image = p.PersonImage,
                Description = p.Description,
                TypeOfWorkName = p.TypeOfWork,
                Category = p.Category.Name,
                ChiefsId = p.ChiefId,
                ChiefsName = p.Chief.Name,
                UserId = p.Chief.UserId,
                CategoryId = p.CategoryId
            })
            .FirstOrDefault();

        public IEnumerable<PersonProfilesServicesModel> ByUser(string userId)
            => GetProfiles(this.data
                .PersonProfiles
                .Where(c => c.Chief.UserId == userId));

        public bool IsChiefs(int profileId, int chiefId)
            => this.data
                   .PersonProfiles
                   .Any(p => p.Id == profileId && p.ChiefId == chiefId);

        public IEnumerable<string> AllProfilesTypeOfWOrk()
             => this.data
                .PersonProfiles
                .Select(w => w.Category.Name)
                .Distinct()
                .ToList();

        public IEnumerable<PersonServiceCategoryModel> AllPersonProfilesCategory()
         => this.data
           .Categories
           .Select(p => new PersonServiceCategoryModel
           {
               Id = p.Id,
               Name = p.Name
           })
           .ToList();


     

        private static IEnumerable<PersonProfilesServicesModel> GetProfiles(IQueryable<PersonProfile> personProfileQuery)
            => personProfileQuery
            .Select(p => new PersonProfilesServicesModel
            {
                Id = p.Id,
                Firstname = p.FirstName,
                Lastname = p.LastName,
                Age = p.Years,
                City = p.City,
                Skills = p.Skills,
                Image = p.PersonImage,
                Description = p.Description,
                TypeOfWorkName = p.TypeOfWork,
                Category = p.Category.Name
            })
                .ToList();

        public int Create(string firstname, string lastname, int years, string personImage, string skills, string city, string description, int categoryId, int chiefsId, string typeOfWork)
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
                ChiefId = chiefsId,
                TypeOfWork = typeOfWork
            };

            this.data.PersonProfiles.Add(profileData);
            this.data.SaveChanges();

            return profileData.Id;
        }

         public bool CategoryExists(int categoryId)
            => this.data
            .Categories
            .Any(p => p.Id == categoryId);

        public bool Edit(int profileId, string firstname, string lastname, int years, string personImage, string skills, string city, string description, string typeOfWork)
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
            
            this.data.SaveChanges();

            return true;
        }


    }
}