using AutoMapper;
using BeckEnd.Core.Context;
using BeckEnd.Core.Dtos.Job;
using BeckEnd.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeckEnd.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobController : ControllerBase
{

    private CoreContext _context;
    private IMapper _mapper;

    public JobController(CoreContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // CRUD Operations

    //Create Job

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateJob([FromBody]JobCreateDto dto)
    {
        Job newJob = _mapper.Map<Job>(dto);
        await _context.Jobs.AddAsync(newJob);
        await _context.SaveChangesAsync();

        return Ok("Job Created Successfully");

    }

    //Read Operations
    [HttpGet]
    [Route("GetAll")]
    public async Task<ActionResult<IEnumerable<JobGetDto>>> GetJobs()
    {
        var jobs = await _context.Jobs.Include(job=>job.Company).ToListAsync();
        var convertdJobs = _mapper.Map<IEnumerable<JobGetDto>>(jobs);

        return Ok(convertdJobs);
    }


}
