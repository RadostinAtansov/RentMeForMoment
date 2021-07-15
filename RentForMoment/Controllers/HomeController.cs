namespace RentForMoment.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using RentForMoment.Models;
    using System.Diagnostics;

    public class HomeController : Controller
    {

        public IActionResult Index() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        
    }
}
