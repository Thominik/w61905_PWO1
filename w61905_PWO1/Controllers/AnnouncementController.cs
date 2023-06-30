using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using w61905_PWO1.Data;
using w61905_PWO1.DTOs;
using w61905_PWO1.Extensions;
using w61905_PWO1.Models;
using w61905_PWO1.RequestHelpers;
using w61905_PWO1.Services;

namespace w61905_PWO1.Controllers;

public class AnnouncementController : BaseApiController
{
    private readonly AnnouncementContext _context;
    private readonly IMapper _mapper;
    private readonly ImageService _imageService;

    public AnnouncementController(AnnouncementContext context, IMapper mapper, ImageService imageService)
    {
        _context = context;
        _mapper = mapper;
        _imageService = imageService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<Announcement>>> GetAllAnnouncements()
    {
        var announcements = await _context.Announcements.ToListAsync();

        return Ok(announcements);
    }
    
    [HttpGet("GetAllWithFilters")]
    public async Task<ActionResult<PagedList<Announcement>>> 
        GetAnnouncements([FromQuery]AnnouncementParams announcementParams)
    {
        var query = _context.Announcements
            .Sort(announcementParams.OrderBy)
            .Search(announcementParams.SearchTerm)
            .CityFilter(announcementParams.CityTerm)
            .SubjectFilter(announcementParams.CategoryTerm)
            .AsQueryable();

        var announcements = await PagedList<Announcement>.ToPagedList(query,
            announcementParams.PageNumber, announcementParams.PageSize);
        
        Response.AddPaginationHeader(announcements.MetaData);

        return announcements;
    }

    
    [HttpGet("GetAnnouncementsByUsername")]
    public async Task<ActionResult<PagedList<Announcement>>> 
        GetAnnouncementsByUsername([FromQuery]AnnouncementParams announcementParams)
    {
        var currentUserName = User.FindFirstValue(ClaimTypes.Name);
        
        var query = _context.Announcements
            .Where(x => x.AnnouncementOwner == currentUserName)
            .Sort(announcementParams.OrderBy)
            .Search(announcementParams.SearchTerm)
            .CityFilter(announcementParams.CityTerm)
            .SubjectFilter(announcementParams.CategoryTerm)
            .AsQueryable();

        var announcements = await PagedList<Announcement>.ToPagedList(query,
            announcementParams.PageNumber, announcementParams.PageSize);
        
        Response.AddPaginationHeader(announcements.MetaData);

        return announcements;
    }

    [HttpGet("{id}", Name = "GetAnnouncement")]
    public async Task<ActionResult<Announcement>> GetAnnouncement(int id)
    {
        var announcement = await _context.Announcements.FindAsync(id);

        if (announcement == null) return NotFound();

        return announcement;
    }
    
    [Authorize(Roles = "Member")]
    [HttpPost]
    public async Task<ActionResult<Announcement>> 
        CreateAnnouncement([FromForm]CreateAnnouncementDto announcementDto)
    {
        var announcement = _mapper.Map<Announcement>(announcementDto);
        
        if (announcementDto != null)
        {
            var imageResult = await _imageService.AddImageAsync(announcementDto.File);
            if (imageResult.Error != null) 
                return BadRequest(new ProblemDetails{Title = "Błąd podczas dodawania zdjęcia"});
            
            announcement.PhotoUrl = imageResult.SecureUrl.AbsoluteUri;
            announcement.PublicId = imageResult.PublicId;
        }

        var currentUserName = User.Identity.Name;
        announcement.AnnouncementOwner = currentUserName;
        _context.Announcements.Add(announcement);
        
        var currentEmail = User.FindFirstValue(ClaimTypes.Email);
        announcement.OptionalEmail = currentEmail;
        _context.Announcements.Add(announcement);

        var result = await _context.SaveChangesAsync() > 0;
        if (result) return CreatedAtRoute("GetAnnouncement", new {Id = announcement.Id}, announcement);

        return BadRequest(new ProblemDetails {Title = "Bład podczas dodawania ogłoszenia"});
    }

    [Authorize(Roles = "Member")]
    [HttpPut]
    public async Task<ActionResult<Announcement>> UpdateAnnouncement([FromForm]UpdateAnnouncementDto announcementDto)
    {
        var announcement = await _context.Announcements.FindAsync(announcementDto.Id);
        
        if (announcement == null) return NotFound();
        
        _mapper.Map(announcementDto, announcement);

        if (announcementDto.File != null)
        {
            var imageResult = await _imageService.AddImageAsync(announcementDto.File);
            
            if (imageResult.Error != null) 
                return BadRequest(new ProblemDetails{Title = "Błąd podczas aktualizacji zdjęcia"});

            if (!string.IsNullOrEmpty(announcement.PublicId))
                await _imageService.DeleteImageAsync(announcement.PublicId);
            
            announcement.PhotoUrl = imageResult.SecureUrl.AbsoluteUri;
            announcement.PublicId = imageResult.PublicId;
        }
        
        var result = await _context.SaveChangesAsync() > 0;
        
        if (result) return Ok(announcement);
        
        return BadRequest(new ProblemDetails {Title = "Bład podczas aktualizacji ogłoszenia"});
    }
    
    [Authorize(Roles = "Member")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAnnouncement(int id)
    {
        var announcement = await _context.Announcements.FindAsync(id);
        
        if (announcement == null) return NotFound();
        
        if (!string.IsNullOrEmpty(announcement.PublicId))
            await _imageService.DeleteImageAsync(announcement.PublicId);
        
        _context.Announcements.Remove(announcement);
        
        var result = await _context.SaveChangesAsync() > 0;
        
        if (result) return Ok();
        
        return BadRequest(new ProblemDetails {Title = "Bład podczas usuwania ogłoszenia"});
    }
}