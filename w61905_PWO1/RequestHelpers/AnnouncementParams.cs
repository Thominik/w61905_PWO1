namespace w61905_PWO1.RequestHelpers;

public class AnnouncementParams : PagionationParams
{
    public string OrderBy { get; set; }
    public string SearchTerm { get; set; }
    public string CityTerm { get; set; }
    public string CategoryTerm { get; set; }
}