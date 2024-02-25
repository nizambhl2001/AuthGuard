﻿using AuthGuad.Dto;
using AuthGuad.Migrations;
using AuthGuad.Models;
using AutoMapper;

namespace AuthGuad.AutoMapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Models.Customer, CustomerDto>().ForMember(item=>item.StatusName,opt=>opt.MapFrom(item=>item.IsActive? "Active": "InActive"));
        }
    }
}