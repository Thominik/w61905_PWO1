using AutoMapper;
using w61905_PWO1.DTOs;
using w61905_PWO1.Models;

namespace w61905_PWO1.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateAnnouncementDto, Announcement>();
        CreateMap<UpdateAnnouncementDto, Announcement>();
    }
}