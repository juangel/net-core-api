using AutoMapper;

namespace CoreApiTest.API.Profiles
{
    public class RegionsProfile:Profile
    {
        public RegionsProfile()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>()
                .ReverseMap();
            // .ForMember(dest => dest.Id, options => options.MapFrom(src=>src.Id)); // Specify the mapping from one property to another

            CreateMap<Models.DTO.Region, Models.Domain.Region>();
        }
    }
}
