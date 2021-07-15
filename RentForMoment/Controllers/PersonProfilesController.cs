using Microsoft.AspNetCore.Mvc;
using RentForMoment.Models.PersonProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentForMoment.Controllers
{
    public class PersonProfilesController : Controller
    {
        public IActionResult Add() => View();

        [HttpPost]

        public IActionResult Add(AddPersonProfile profile)
        {
            
        }
    }
}
