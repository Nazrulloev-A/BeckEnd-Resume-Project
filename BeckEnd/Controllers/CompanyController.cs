using AutoMapper;
using BeckEnd.Core.Context;
using BeckEnd.Core.Dtos.Company;
using BeckEnd.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeckEnd.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private CoreContext _context;
    private IMapper _mapper;

    public CompanyController(CoreContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    // CRUD Operations

    [HttpPost]
    [Route("Create")]

    public async Task<IActionResult> CreateCompany(CompanyCreateDto companyCreateDto)
    {
        Company newCompany = _mapper.Map<Company>(companyCreateDto);
        await _context.Companies.AddAsync(newCompany);
        await _context.SaveChangesAsync();

        return Ok("Company Created Successfully");

    }

    //Read Operations
    [HttpGet]
    [Route("GetAll")]
    public async Task<ActionResult<IEnumerable<CompanyGetDto>>> GetCompanies()
    {
        var companies = await _context.Companies.ToListAsync();
        var companiesDto = _mapper.Map<IEnumerable<CompanyGetDto>>(companies);

        return Ok(companiesDto);
    }


    



    
   
}
