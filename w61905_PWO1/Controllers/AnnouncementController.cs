using Microsoft.AspNetCore.Mvc;
using w61905_PWO1.Data;
using w61905_PWO1.Extensions;
using w61905_PWO1.Models;
using w61905_PWO1.RequestHelpers;

namespace w61905_PWO1.Controllers;

public class AnnouncementController : BaseApiController
{
    private readonly AnnouncementContext _context;

    public AnnouncementController(AnnouncementContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<Announcement>>> GetAnnouncements(
        [FromQuery] AnnouncementParams announcementParams)
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

    [HttpGet("{id}")]
    public async Task<ActionResult<Announcement>> GetAnnouncement(int id)
    {
        var announcement = await _context.Announcements.FindAsync(id);

        if (announcement == null) return NotFound();

        return announcement;
    }
}