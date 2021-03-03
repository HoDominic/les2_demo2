using System;

using AutoMapper;
using les2_demo2.Models;

namespace les2_demo2.DTO
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<VaccinationRegistration, VaccinationRegistrationDTO>();
        }
    }
}
