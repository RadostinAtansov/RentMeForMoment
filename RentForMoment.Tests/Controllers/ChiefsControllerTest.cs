namespace RentForMoment.Tests.Controllers
{

    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using RentForMoment.Controllers;
    using RentForMoment.Models.Chiefs;
    using RentForMoment.Data.Models;
    using System.Linq;
    using static WebConstants;
    using RentForMoment.Models.PersonProfiles;

    public class ChiefsControllerTest
    {
        [Fact]
        public void CreateShouldBEForAuthorizedUsers()
            => MyController<ChiefsController>
            .Instance()
            .Calling(c => c.Create())
            .ShouldHave()
            .ActionAttributes(attribute => attribute
            .RestrictingForAuthorizedRequests());

        [Fact]
        public void CreateShoudReturnView()
            => MyController<ChiefsController>
            .Instance()
            .Calling(c => c.Create())
            .ShouldReturn()
            .View();

        [Theory]
        [InlineData("Chief", "+359123456789")]
        public void PostCreateShouldBeForAuthorizedUsersAndReturnRedirectWithValidView(
            string chiefsName,
            string phoneNumber)
            => MyController<ChiefsController>
            .Instance(controller => controller
            .WithUser())
            .Calling(c => c.Create(new BecomeChiefViewModel
            {
                Name = chiefsName,
                PhoneNumber = phoneNumber
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .ValidModelState()
                .Data(data => data
                    .WithSet<Chief>(chiefs => chiefs
                    .Any(d =>
                    d.Name == chiefsName &&
                    d.PhoneNumber == phoneNumber &&
                    d.UserId == TestUser.Identifier)))
            .TempData(tempData => tempData
            .ContainingEntryWithKey(GlobalMessageKey))
            .AndAlso()
            .ShouldReturn()
            .Redirect(redirect => redirect
            .To<PersonProfilesController>(c => c.All(With.Any<AllPersonsProfileQueryModel>())));
    }
}
