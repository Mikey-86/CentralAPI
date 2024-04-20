using System;
using System.Linq;
using CentralAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CentralAPI.Data
{
    public class Seed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new dbContext(
                serviceProvider.GetRequiredService<DbContextOptions<dbContext>>()))
            {
                // is database is created?
                context.Database.EnsureCreated();

                // If there are any data, return.
                if (context.Complaints.Any() || context.Players.Any() || context.Teams.Any())
                {
                    return;  
                }

                context.Complaints.AddRange(
                    new Complaint
                    {
                        FirstName = "Michael",
                        LastName = "Francke",
                        Email = "Michaelfrancke0@gmail.com",
                        MobileNumber = "0514776525",
                        ComplaintDetails = "Other player tripped me while running",
                        IPAddress = "10.130.256.44",
                        CreatedDate = "18-04-2024"
                    },
                    new Complaint
                    {
                        FirstName = "Tristan",
                        LastName = "Francke",
                        Email = "TristanAP0@gmail.com",
                        MobileNumber = "0896542789",
                        ComplaintDetails = "Player 17 kicked ball in my face",
                        IPAddress = "10.120.10.47",
                        CreatedDate = "1-04-2024"
                    }
                );

                context.Players.AddRange(
                        new Player
                        {
                            FirstName = "Tristan",
                            LastName = "Francke",
                            Age = 25,
                            IDNumber = "9955665479036",
                            Address = "23 Powell Drive",
                            DesiredTeam = "3"
                        },
                        new Player
                        {
                            FirstName = "Michael",
                            LastName = "Francke",
                            Age = 28,
                            IDNumber = "96563565479036",
                            Address = "1 Sainz Road",
                            DesiredTeam = "4"
                        }
                    );

                context.Teams.AddRange(
                        new Team
                        {
                            Name = "Arsenal",
                            DateCreated = "18-12-2009",
                            IsActive = true
                        },
                        new Team
                        {
                            Name = "Liverpool",
                            DateCreated = "24-10-2006",
                            IsActive = true
                        }
                    );
                context.SaveChanges();
            }
        }
    }
}
