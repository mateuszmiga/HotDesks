using Domain.Entities;

namespace HotDesks.Api.Dto
{
    public class CreateDeskDto
    {
        public string Description { get; set; }
        public int RoomId { get; set; }

    }
    
    public class DeskDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? RentingStart { get; set; }
        public DateTime? RentingEnd { get; set; }        

        public RoomDto Room { get; set; }
        public OwnerDto? Owner { get; set; }
    }

    public class UpdateDeskDto 
    {
        public string Description { get; set; }
        public DateTime? RentingStart { get; set; }
        public DateTime? RentingEnd { get; set; }
        public int? OwnerId { get; set; }
        public int RoomId { get; set; }
    }
}
