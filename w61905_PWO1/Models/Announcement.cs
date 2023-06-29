namespace w61905_PWO1.Models;

public class Announcement
{
    public int Id { get; set; }
    public string Title { get; set; } 
    public string Category { get; set; }
    public string Description { get; set; }
    public long Price { get; set; }
    public string Location { get; set; }
    public string PublicId { get; set; }
    public string PhotoUrl { get; set; }
    public string PhoneNumber { get; set; }
    public string AnnouncementOwner { get; set; }
    
}