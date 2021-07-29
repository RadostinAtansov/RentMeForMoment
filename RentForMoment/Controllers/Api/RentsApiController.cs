namespace RentForMoment.Controllers.Api
{

    using Microsoft.AspNetCore.Mvc;
    using RentForMoment.Data;
    using RentForMoment.Data.Models;
    using System.Collections;
    using System.Linq;

    [ApiController]
    [Route("api/rents")]
    public class RentsApiController : ControllerBase
    {

        private readonly RentForMomentDbContext data;

        public RentsApiController(RentForMomentDbContext data)
            => this.data = data;
        

        [HttpGet]

        public IEnumerable GetRent()
        {
            return this.data.PersonProfiles.ToList();
        }


        public IActionResult SaveProfile(PersonProfile profile)
        {
            return Ok();
        }

        
    }


}
