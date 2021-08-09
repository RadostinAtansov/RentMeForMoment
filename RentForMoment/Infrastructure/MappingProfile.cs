namespace RentForMoment.Infrastructure
{

    using AutoMapper;
    using RentForMoment.Data.Models;
    using RentForMoment.Models.Home;
    using RentForMoment.Models.PersonProfiles;
    using RentForMoment.Services.PersonProfiles.Models;

    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            this.CreateMap<PersonProfile, ProfileIndexViewModel>();
            this.CreateMap<PersonProfileDetailsServiceModel, PersonProfileFormModel>();

            this.CreateMap<PersonProfile, PersonProfileDetailsServiceModel>()
                .ForMember(c => c.UserId, cfg => cfg.MapFrom(c => c.Chief.UserId));
        }

    }
}
