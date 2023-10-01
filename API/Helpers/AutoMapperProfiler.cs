using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiler : Profile
    {
        public AutoMapperProfiler() //
        {
            CreateMap<AppUser, MemberDto>()
            .ForMember(destinationMember => destinationMember.PhotoUrl, options => options.MapFrom(source => source.Photos.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

            CreateMap<Photo, PhotoDto>();

            CreateMap<MemberUpdateDto, AppUser>();
        }
    }
}