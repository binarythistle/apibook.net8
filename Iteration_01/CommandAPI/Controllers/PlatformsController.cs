using CommandAPI.Data;
using CommandAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CommandAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PlatformsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Platform>>> GetAllPlatforms()
    {
        var platforms = await _context.platforms.ToListAsync();

        return Ok(platforms);
    }

    [HttpGet("{id}", Name = "GetPlatformById")]
    public async Task<ActionResult<Platform>> GetPlatformById(int id)
    {
        var platform = await _context.platforms.FirstOrDefaultAsync(p => p.Id == id);
        if(platform == null)
            return NotFound();

        return Ok(platform);
    }

    [HttpPost]
    public async Task<ActionResult<Platform>> CreatePlatform(Platform platform)
    {
        if(platform == null)
        {
            return BadRequest();
        }
        
        await _context.platforms.AddAsync(platform);
        await _context.SaveChangesAsync();

        return CreatedAtRoute(nameof(GetPlatformById), new { Id = platform.Id}, platform);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePlatform(int id, Platform platform)
    {
        //Validations for non conformat model are built in

        var platformFromContext = await _context.platforms.FirstOrDefaultAsync(p => p.Id == id);
        if(platformFromContext == null)
        {
            return NotFound();
        }

        //Manual Mapping
        platformFromContext.PlatformName = platform.PlatformName;
        
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Do not require PATCH for this controller - do that in the commands controller

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePlatform(int id)
    {
        var platformFromContext = await _context.platforms.FirstOrDefaultAsync(p => p.Id == id);
        if(platformFromContext == null)
        {
            return NotFound();
        }

        _context.platforms.Remove(platformFromContext);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}