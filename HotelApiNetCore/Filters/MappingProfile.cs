using AutoMapper;
using HotelApiNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApiNetCore.Infrastructure
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            CreateMap<RoomEntity, Room>().ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate / 100m));
        }

    }
}
