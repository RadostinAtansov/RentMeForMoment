namespace RentForMoment.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RentForMoment.Data;
    using RentForMoment.Infrastructure;
    using RentForMoment.Models.PersonProfiles;
    using RentForMoment.Services.Chiefs;
    using RentForMoment.Services.PersonProfiles;
    using RentForMoment.Data.Models;

    public class PersonProfilesController : Controller
    {
        private readonly IPersonProfilesService profiles;
        private readonly IChiefsService chiefs;
        private readonly RentForMomentDbContext data;

        public PersonProfilesController(
            IPersonProfilesService profiles,
            IChiefsService chiefs,
            RentForMomentDbContext data)
        {
            this.profiles = profiles;
            this.data = data;
            this.chiefs = chiefs;
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
        public IActionResult Mine()
        {
            var myProfile = this.profiles.ByUser(this.User.GetId());

            return View(myProfile);
        }


        [Authorize]
        public IActionResult Add()
        {
            if (!this.chiefs.IsChief(this.User.GetId()))
            {

                return RedirectToAction(nameof(ChiefsController.Create), "Chiefs");
            }

            return base.View(new Models.PersonProfiles.PersonProfileFormModel
            {

                CategoriesPerson = this.profiles.AllPersonProfilesCategory()

            });
        }

        [HttpPost]
        [Authorize]

        public IActionResult Add(PersonProfileFormModel profile)
        {

            var chiefsId = this.chiefs.GetIdByUser(this.User.GetId());

            if (chiefsId == 0)
            {

                return RedirectToAction(nameof(ChiefsController.Create), "Chiefs");

            }

            if (!this.profiles.CategoryExists(profile.CategoryId))
            {
                this.ModelState.AddModelError(nameof(profile.CategoryId), "Category does not exist");
            }

            if (!ModelState.IsValid)
            {

                profile.CategoriesPerson = this.profiles.AllPersonProfilesCategory();

                return View(profile);
            }

            this.profiles.Create(profile.Firstname,
                profile.Lastname,
                profile.Years,
                profile.PersonImage,
                profile.Skills,
                profile.City,
                profile.Description,
                profile.CategoryId,
                profile.CategoryId,
                profile.TypeOfWork);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.chiefs.IsChief(userId))
            {
                return RedirectToAction(nameof(ChiefsController.Create), "Chiefs");
            }

            var profiles = this.profiles.Details(id);

            if (profiles.UserId != userId)
            {
                return Unauthorized();
            }


            return View(new PersonProfileFormModel
            {
                Firstname = profiles.Firstname,
                Lastname = profiles.Lastname,
                Years = profiles.Age,
                City = profiles.City,
                Description = profiles.Description,
                Skills = profiles.Skills,
                PersonImage = profiles.Image,
                TypeOfWork = profiles.TypeOfWorkName,
                CategoryId = profiles.CategoryId,
                 CategoriesPerson = this.profiles.AllPersonProfilesCategory()
            }) ;

        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, PersonProfileFormModel profile)
        {
            var chiefsId = this.chiefs.GetIdByUser(this.User.GetId());

            if (chiefsId == 0)
            {

                return RedirectToAction(nameof(ChiefsController.Create), "Chiefs");

            }

            if (!this.profiles.CategoryExists(profile.CategoryId))
            {
                this.ModelState.AddModelError(nameof(profile.CategoryId), "Category does not exist");
            }

            if (!ModelState.IsValid)
            {

                profile.CategoriesPerson = this.profiles.AllPersonProfilesCategory();

                return View(profile);
            }

            if (!this.profiles.IsChiefs(id, chiefsId))
            {
                return BadRequest();
            }

           this.profiles.Edit(
                id,
                profile.Firstname,
                profile.Lastname,
                profile.Years,
                profile.PersonImage,
                profile.Skills,
                profile.City,
                profile.Description,
                profile.TypeOfWork);

            return RedirectToAction(nameof(All));

        }

    }
}
