using AutoMapper;
using BeckEnd.Core.Dtos.Company;
using BeckEnd.Core.Entities;

namespace BeckEnd.Core.AutoMapperConfig;

public class AutoMapperConfigProfile : Profile
{
    public AutoMapperConfigProfile()
    {
        // Company
        CreateMap<CompanyCreateDto, Company>();
        CreateMap<Company, CompanyGetDto>();

        // Job
    }
}
