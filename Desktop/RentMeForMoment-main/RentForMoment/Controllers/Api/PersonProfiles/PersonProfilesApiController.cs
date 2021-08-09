namespace RentForMoment.Controllers.Api.PersonProfile
{

    using Microsoft.AspNetCore.Mvc;
    using RentForMoment.Data;
    using System.Linq;
    using System.Collections;
    using RentForMoment.Models.Api.PersonProfiles;
    using RentForMoment.Models;
    using RentForMoment.Services.PersonProfiles;

    //using RentForMoment.Models.PersonProfiles;

    [ApiController]
    [Route("api/rents")]

    public class PersonProfilesApiController : ControllerBase
    {

        private readonly IPersonProfilesService personProfles;

        public PersonProfilesApiController(IPersonProfilesService personProfles)
            => this.personProfles = personProfles;

        [HttpGet]

        public PersonProfilesQueryServiceModel All([FromQuery] AllPersonProfilesApiRequestModel query)
           =>  this.personProfles.All(
                query.TypeOfWork,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.ProfilesPerPage);

    }


}
