using AutoMapper;
using Entities.Models;
using MeetupWebAPI.Entities.DataTransferObjects;

namespace MeetupWebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Meetup, MeetupDto>();
            CreateMap<User, UserDto>();
            CreateMap<Tag, TagDto>();
            CreateMap<MeetupForCreationDto, Meetup>();
            CreateMap<MeetupForUpdateDto, Meetup>();
        }
    }
}
