using Microsoft.AspNetCore.Identity;
using w61905_PWO1.Models;

namespace w61905_PWO1.Data;

public class DbInitializer
{
    public static async Task Initialize(AnnouncementContext context, UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new User
            {
                UserName = "dominik",
                Email = "dominik@test.com"
            };

            await userManager.CreateAsync(user, "Pa$$word1");
            await userManager.AddToRoleAsync(user, "Member");
            
            var admin = new User
            {
                UserName = "admin",
                Email = "admin@test.com"
            };

            await userManager.CreateAsync(admin, "Pa$$word1");
            await userManager.AddToRolesAsync(admin, new[] {"Member", "Admin"});
        }

        if (context.Announcements.Any()) return;

        var announcements = new List<Announcement>
        {
            new Announcement
            {
                Title = "BMW X3",
                Description =
                    "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                Price = 60000,
                Location = "Rzeszów",
                Category = "auta osobowe",
                AnnouncementOwner = "Justyna Nowak",
                PhoneNumber = "725725726",
                PhotoUrl = "/images/people/BMWX3.png",
                OptionalEmail = "justynan33@gmail.com"
            },
            new Announcement
            {
                Title = "MacBook Air 13 2016",
                Description =
                    "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                Price = 2900,
                Location = "Warszawa",
                Category = "komputery",
                AnnouncementOwner = "Michał Kowalski",
                PhoneNumber = "987456123",
                PhotoUrl = "/images/people/MacAir.png",
                OptionalEmail = "michaelo@gmail.com"
            },
            new Announcement
            {
                Title = "Samsung Galaxy S10",
                Description =
                    "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                Price = 1200,
                Location = "Kraków",
                Category = "telefony",
                AnnouncementOwner = "Adam Kowalski",
                PhoneNumber = "445765221",
                PhotoUrl = "/images/people/samsung.png",
                OptionalEmail = "adasfast@gmail.com"
            },
            new Announcement
            {
                Title = "Felder Hammer A3 31",
                Description =
                    "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                Price = 16500,
                Location = "Lublin",
                Category = "maszyny",
                AnnouncementOwner = "Tomek Orzech",
                PhoneNumber = "987456123",
                PhotoUrl = "/images/people/felder.png",
                OptionalEmail = "tomekfelder@feldergroup.com"
            },
        };

        foreach (var announement in announcements)
        {
            context.Announcements.Add(announement);
        }

        context.SaveChanges();
    }
}