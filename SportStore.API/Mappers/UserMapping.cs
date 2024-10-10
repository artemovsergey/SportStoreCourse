using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SportStore.API.Dto;
using SportStore.API.Entities;

namespace SportStore.Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserRecordDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Password)))
            .ForMember(dest => dest.PasswordSalt, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Password)));
        
        CreateMap<User, UserRecordDto>();
    }
}
