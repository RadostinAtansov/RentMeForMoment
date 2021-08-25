namespace RentForMoment.Infrastructure
{

    using AutoMapper;
    using RentForMoment.Data.Models;
    using RentForMoment.Models;
    using RentForMoment.Models.Home;
    using RentForMoment.Models.PersonProfiles;
    using RentForMoment.Services.PersonProfiles;
    using RentForMoment.Services.PersonProfiles.Models;

    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            this.CreateMap<Category, PersonServiceCategoryModel>();

            this.CreateMap<PersonProfile, PersonProfilesServicesModel>()
                .ForMember(p => p.CategoryName, cfg => cfg.MapFrom(p => p.Category.Name));

            this.CreateMap<PersonProfile, LatestPersonProfileServiceModel>();
            this.CreateMap<PersonProfileDetailsServiceModel, PersonProfileFormModel>();

            this.CreateMap<PersonProfile, PersonProfileDetailsServiceModel>()
                .ForMember(c => c.UserId, cfg => cfg.MapFrom(c => c.Chief.UserId));
        }

    }
}
