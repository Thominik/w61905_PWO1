using w61905_PWO1.Models;

namespace w61905_PWO1.Extensions;

public static class AnnouncementExtensions
{
    public static IQueryable<Announcement> Sort(this IQueryable<Announcement> query, string orderBy)
    {

        if (string.IsNullOrWhiteSpace(orderBy)) return query.OrderBy(p => p.Title);

        query = orderBy switch
        {
            "price" => query.OrderBy(p => p.Price),
            "priceDesc" => query.OrderByDescending(p => p.Price),
            _ => query.OrderBy(p => p.Title)
        };

        return query;
    }

    public static IQueryable<Announcement> Search(this IQueryable<Announcement> query, string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm)) return query;

        var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

        return query.Where(p => p.Title.ToLower().Contains(lowerCaseSearchTerm));
    }

    public static IQueryable<Announcement> CityFilter(this IQueryable<Announcement> query,
        string city)
    {
        if (string.IsNullOrEmpty(city)) return query;

        var lowerCaseCityFilterTerm = city.Trim().ToLower();

        return query.Where(c => c.Location.ToLower().Contains(lowerCaseCityFilterTerm));
    }

    public static IQueryable<Announcement> SubjectFilter(this IQueryable<Announcement> query,
        string subjectLesson)
    {
        if (string.IsNullOrEmpty(subjectLesson)) return query;

        var lowerCaseSubjectLessonFilterTerm = subjectLesson.Trim().ToLower();

        return query.Where(c => c.Category.ToLower().Contains(lowerCaseSubjectLessonFilterTerm));
    }
}