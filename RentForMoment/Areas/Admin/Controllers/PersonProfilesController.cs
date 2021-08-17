namespace RentForMoment.Areas.Admin.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using RentForMoment.Services.PersonProfiles;

    //[Area("Ne6to si")]
    public class PersonProfilesController : AdminController
    {

        private readonly IPersonProfilesService personProfiles;

        public PersonProfilesController(IPersonProfilesService personProfiles) 
            => this.personProfiles = personProfiles;


        public IActionResult All() 
        {
           return View(this.personProfiles.All(publicOnly: false).Profiles);
        }

        public IActionResult Approvell(int id)
        {
            this.personProfiles.Approvell(id);

            return RedirectToAction(nameof(All));
        }

    }
}
