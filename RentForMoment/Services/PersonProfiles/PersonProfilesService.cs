using RentForMoment.Data;

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

            var profiles = profilesQuery
                .Skip((currentPage - 1) * profilesPerPage)
                .Take(profilesPerPage)
                .Select(p => new PersonProfilesServicesModel
                {
                    Firstname = p.FirstName,
                    Lastname = p.LastName,
                    Age = p.Years,
                    City = p.City,
                    Skills = p.Skills,
                    Image = p.PersonImage,
                    Description = p.Description,
                    TypeOfWork = p.TypeOfWork,
                    Category = p.Category.Name
                })
                .ToList();

            return new PersonProfilesQueryServiceModel
            {
                TotalProfiles = totalProfiles,
                CurrentPage = currentPage,
                
            };
        }

        public IEnumerable<string> AllProfilesTypeOfWOrk()
             => this.data
                .PersonProfiles
                .Select(w => w.Category.Name)
                .Distinct()
                .ToList();
    }
}
