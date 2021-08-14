namespace RentForMoment.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static AdminWebConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]

    public abstract class AdminController : Controller
    {
    }
}
