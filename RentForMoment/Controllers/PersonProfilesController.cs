namespace RentForMoment.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RentForMoment.Infrastructure.Extensions;
    using RentForMoment.Models.PersonProfiles;
    using RentForMoment.Services.Chiefs;
    using RentForMoment.Services.PersonProfiles;

    using static WebConstants;

    public class PersonProfilesController : Controller
    {
        private readonly IPersonProfilesService profiles;
        private readonly IChiefsService chiefs;
        private readonly IMapper mapper;


        public PersonProfilesController(
            IPersonProfilesService profiles,
            IChiefsService chiefs,
            IMapper mapper)
        {
            this.profiles = profiles;
            this.mapper = mapper;
            this.chiefs = chiefs;
        }

        public IActionResult Delete(int id)
        {
            this.profiles.Delete(id);

            TempData[GlobalMessageKey] = "Successful Delete Profile";

            return RedirectToAction(nameof(All));
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

        public IActionResult Details(int id)
        {
            var profile = this.profiles.Details(id);

            return View(profile);
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

            return View(new PersonProfileFormModel
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

            var personProfileId = this.profiles.Create(
                profile.Firstname,
                profile.Lastname,
                profile.Years,
                profile.PersonImage,
                profile.Skills,
                profile.City,
                profile.Description,
                profile.CategoryId,
                profile.TypeOfWork,
                chiefsId);

            TempData[GlobalMessageKey] = "Successful Add Profile";

            return RedirectToAction(nameof(Mine));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.chiefs.IsChief(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(ChiefsController.Create), "Chiefs");
            }

            var profiles = this.profiles.Details(id);

            if (profiles.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var profileForm = this.mapper.Map<PersonProfileFormModel>(profiles);

            profileForm.CategoriesPerson = this.profiles.AllPersonProfilesCategory(); 

            return View(profileForm);

        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, PersonProfileFormModel profile)
        {
            var chiefsId = this.chiefs.GetIdByUser(this.User.GetId());

            if (chiefsId == 0 && !User.IsAdmin())
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

            if (!this.profiles.IsChiefs(id, chiefsId) && !User.IsAdmin())
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
                profile.TypeOfWork,
                this.User.IsAdmin());

            TempData[GlobalMessageKey] = "Successful Edit Form";

            return RedirectToAction(nameof(All));

        }

    }
}
