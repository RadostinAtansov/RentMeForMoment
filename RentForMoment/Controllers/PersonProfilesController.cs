namespace RentForMoment.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using RentForMoment.Data;
    using RentForMoment.Data.Models;
    using RentForMoment.Models.PersonProfiles;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    public class PersonProfilesController : Controller
    {

        private readonly RentForMomentDbContext data;

        public PersonProfilesController(RentForMomentDbContext data) => this.data = data;

        public IActionResult Add() => View(new AddPersonProfile
        {
            CategoriesPerson = this.GetCategories()
        });


        public IActionResult All([FromQuery]AllPersonsProfileQueryModel query)
        {

            var profilesQuery = this.data.PersonProfiles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.TypeOfWork))
            {
                profilesQuery = profilesQuery.Where(t => t.Category.Name == query.TypeOfWork);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                profilesQuery = profilesQuery.Where(c =>
                c.FirstName.ToLower().Contains(query.SearchTerm.ToLower()) ||
                c.Skills.ToLower().Contains(query.SearchTerm.ToLower()) ||
                c.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            profilesQuery = query.Sorting switch
            {
                ProfileSorting.DateRegistered => profilesQuery.OrderByDescending(c => c.Id),
                ProfileSorting.Year => profilesQuery.OrderByDescending(y => y.Years),
                ProfileSorting.Skills => profilesQuery.OrderByDescending(s => s.Skills),
                _ => profilesQuery.OrderByDescending(d => d.Id)
            };

            var profiles = profilesQuery
                .Select(p => new ListingProfilesViewModel
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

            var profileTypeOfWork = this.data
                .PersonProfiles
                .Select(w => w.Category.Name)
                .Distinct()
                .ToList();

            query.TypeOfWorks = profileTypeOfWork  ;
            query.Profiles = profiles;

            return View(query);
        }

        [HttpPost]

        public IActionResult Add(AddPersonProfile profile, IFormFile image)
        {

            if (!this.data.Categories.Any(c => c.Id == profile.CategoryId))
            {
                this.ModelState.AddModelError(nameof(profile.CategoryId), "Category does not exist");
            }

            if (!ModelState.IsValid)
            {

                profile.CategoriesPerson = this.GetCategories();

                return View(profile);
            }

            var profileData = new PersonProfile
            {
                FirstName = profile.Firstname,
                LastName = profile.Lastname,
                Years = profile.Years,
                PersonImage = profile.PersonImage,
                Skills = profile.Skills,
                City = profile.City,
                Description = profile.Description,
                CategoryId = profile.CategoryId,
                
            };

            this.data.PersonProfiles.Add(profileData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private IEnumerable<PersonCategory> GetCategories()
            => this.data
            .Categories
            .Select(p => new PersonCategory
            {
                Id = p.Id,
                Name = p.Name
            })
            .ToList();

    }
}
