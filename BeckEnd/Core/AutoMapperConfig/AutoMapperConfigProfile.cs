using AutoMapper;
using BeckEnd.Core.Dtos.Company;
using BeckEnd.Core.Dtos.Job;
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
        CreateMap<JobCreateDto, Job>();
        CreateMap<Job, JobGetDto>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name));


    }
}
