    namespace RentForMoment.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RentForMoment.Data;
    using RentForMoment.Data.Models;
    using RentForMoment.Infrastructure.Extensions;
    using RentForMoment.Models.Chiefs;
    using System.Linq;

    using static WebConstants;

    public class ChiefsController : Controller
    {
        private readonly RentForMomentDbContext data;

        public ChiefsController(RentForMomentDbContext data)
           => this.data = data;

        [Authorize]

        public IActionResult Create() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Create(BecomeChiefViewModel chief)
        {

            var userId = this.User.GetId();

            var userIdAlredyChief = this.data
                .Chiefs
                .Any(d => d.UserId == userId);

            if (userIdAlredyChief)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(chief);
            }

            var chiefData = new Chief
            {
                Name = chief.Name,
                PhoneNumber = chief.PhoneNumber,
                UserId = userId,
                
            };

            this.data.Chiefs.Add(chiefData);
            this.data.SaveChanges();

            TempData[GlobalMessageKey] = "You are the BIG BOSS";

            return RedirectToAction("All", "PersonProfiles");

        }

    }
}
