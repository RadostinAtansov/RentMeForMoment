namespace RentForMoment.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using RentForMoment.Data;
    using RentForMoment.Data.Models;
    using RentForMoment.Infrastructure;
    using RentForMoment.Models;
    using RentForMoment.Models.PersonProfiles;
    using RentForMoment.Services.PersonProfiles;
    using System.Collections.Generic;
    using System.Linq;


    public class PersonProfilesController : Controller
    {
        private readonly IPersonProfilesService profiles;
        private readonly RentForMomentDbContext data;

        public PersonProfilesController(
            IPersonProfilesService profiles,
            RentForMomentDbContext data)
        {
            this.profiles = profiles;
            this.data = data;
        }

        public IActionResult All([FromQuery] AllPersonsProfileQueryModel query)
        {



            var queryResult = this.profiles.All(
                query.TypeOfWork,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllPersonsProfileQueryModel.ProfilesPerPage);

            var profileTypeOfWork = this.profiles.AllProfilesTypeOfWOrk();

            query.TypeOfWorks = profileTypeOfWork;
            query.TotalProfiles = queryResult.TotalProfiles;
            query.Profiles = queryResult.Profiles;

            return View(query);
        }


        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsChief())
            {
                //this.TempData

                return RedirectToAction(nameof(ChiefsController.Create), "Chiefs");
            }

            return View(new AddPersonProfile
            {

                CategoriesPerson = this.GetCategories()

            });
        }

        [HttpPost]
        [Authorize]

        public IActionResult Add(AddPersonProfile profile, IFormFile image)
        {

            var chiefsId = this.data
                .Chiefs
                .Where(c => c.UserId == this.User.GetId())
                .Select(c => c.Id)
                .FirstOrDefault();

            if (chiefsId == 0)
            {
                if (!this.UserIsChief())
                {
                    return RedirectToAction(nameof(ChiefsController.Create), "Chiefs");
                }

                return View(new AddPersonProfile
                {

                    CategoriesPerson = this.GetCategories()

                });
            }

         

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
                ChiefId = chiefsId
            };

            this.data.PersonProfiles.Add(profileData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }


        private bool UserIsChief()
         => this.data
                .Chiefs
                .Any(c => c.UserId == this.User.GetId());

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
