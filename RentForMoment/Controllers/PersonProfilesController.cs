namespace RentForMoment.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using RentForMoment.Data;
    using RentForMoment.Data.Models;
    using RentForMoment.Models.PersonProfiles;
    using System.Collections.Generic;
    using System.Linq;

    public class PersonProfilesController : Controller
    {

        private readonly RentForMomentDbContext data;

        public PersonProfilesController(RentForMomentDbContext data) => this.data = data;

        public IActionResult Add() => View(new AddPersonProfile
        {
            CategoriesPerson = this.GetCategories()
        });

        [HttpPost]

        public IActionResult Add(AddPersonProfile profile, IFormFile image)
        {

            if (!this.data.Categories.Any(c => c.Id ==  profile.CategoryId))
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

            return RedirectToAction("Index", "Home");
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
