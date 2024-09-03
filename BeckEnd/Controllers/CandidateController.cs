using AutoMapper;
using BeckEnd.Core.Context;
using BeckEnd.Core.Dtos.Candidate;
using BeckEnd.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeckEnd.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CandidateController : ControllerBase
{
    private CoreContext _context;
    private IMapper _mapper;

    public CandidateController(CoreContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // CRUD Operations

    //Create Candidate

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateCandidate([FromForm] CandidateCreateDto dto,IFormFile pdfFile)
    {
        // File Upload to Server
        //Then Save the File Path to the Database
        var fileMegaByte = 5*1024*1024;
        var pdfMimeType = "application/pdf";

        if(pdfFile.Length > fileMegaByte || pdfFile.ContentType != pdfMimeType)
        {
            return BadRequest("File Size or File Type is not Supported");
        }

        var resumeUrl = Guid.NewGuid().ToString() + ".pdf";
        var filePath = Path.Combine(Directory.GetCurrentDirectory(),"documents","pdfs",resumeUrl);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await pdfFile.CopyToAsync(stream);
        }

        var newCandidate = _mapper.Map<Candidate>(dto);
        newCandidate.ResumeUrl = resumeUrl;
        await _context.Candidates.AddAsync(newCandidate);
        await _context.SaveChangesAsync();

        return Ok("Candidate Created Successfully");

    }

    //Read Operations
    [HttpGet]
    [Route("GetAll")]
    public async Task<ActionResult<IEnumerable<CandidateGetDto>>> GetCandidates()
    {
        var candidates = await _context.Candidates.Include(c=>c.Job).ToListAsync();
        var candidatesDto = _mapper.Map<IEnumerable<CandidateGetDto>>(candidates);

        return Ok(candidatesDto);
    }


    //Read (Download Pdf File)
    [HttpGet]
    [Route("Download/{url}")]

    public IActionResult DownloadPdfFile(string url)
    {
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),"documents","pdfs",url);
           if(!System.IO.File.Exists(filePath))
            {
               return NotFound("File Not Found");
           }

           var fileBytes = System.IO.File.ReadAllBytes(filePath);
           var file = File(fileBytes,"application/pdf",url);
           return file;
        }
    }
}
