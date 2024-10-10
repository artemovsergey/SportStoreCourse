using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SportStore.Application.Dto;
using SportStore.Domain;

namespace SportStore.Application.Mappers;

    public class UserMapping : Profile
    {
        public UserMapping(){
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
