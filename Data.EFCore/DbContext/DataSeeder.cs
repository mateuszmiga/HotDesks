using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EFCore.DbContext
{
    public class DataSeeder
    {
        public async Task SeedDataAsyncIfDbIsEmpty(Context context)
        {
            
            if (!context.Desks.Any())
            {
                await context.Owners.AddRangeAsync(
                    new Owner { Id = 1, Name = "John Smith" },
                    new Owner { Id = 2, Name = "Jane Doe" },
                    new Owner { Id = 3, Name = "Bob Johnson" },
                    new Owner { Id = 4, Name = "Samantha Williams" },
                    new Owner { Id = 5, Name = "Michael Brown" },
                    new Owner { Id = 6, Name = "Emily Davis" },
                    new Owner { Id = 7, Name = "Matthew Miller" },
                    new Owner { Id = 8, Name = "Madison Garcia" }
                );

                await context.Rooms.AddRangeAsync(
                    new Room { Id = 1, Description = "Room 1" },
                    new Room { Id = 2, Description = "Room 2" }
                    );

                await context.Desks.AddRangeAsync(
                    new Desk { Id = 1, Description = "Desk 1", RentingStart = new DateTime(2021, 1, 1), RentingEnd = new DateTime(2022, 12, 31), OwnerId = 1, RoomId = 1 },
                    new Desk { Id = 2, Description = "Desk 2", RentingStart = new DateTime(2022, 1, 1), RentingEnd = new DateTime(2022, 12, 31), OwnerId = 2, RoomId = 1 },
                    new Desk { Id = 3, Description = "Desk 3", RentingStart = new DateTime(2022, 1, 1), RentingEnd = new DateTime(2023, 12, 31), OwnerId = 3, RoomId = 2 },
                    new Desk { Id = 4, Description = "Desk 4", RentingStart = new DateTime(2022, 1, 1), RentingEnd = new DateTime(2023, 12, 31), OwnerId = 4, RoomId = 2 }
                    );

                await context.SaveChangesAsync();
            }

            
        }
    }
}
