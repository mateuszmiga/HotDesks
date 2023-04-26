using Domain.Entities;

namespace HotDesks.Api.Dto
{
    public class CreateDeskDto
    {
        public string Description { get; set; }
        public int RoomId { get; set; }

    }
    
    public class DeskDto : CreateDeskDto
    {
        public DateTime? RentingStart { get; set; }
        public DateTime? RentingEnd { get; set; }
        public int Id { get; set; }

        public RoomDto Room { get; set; }
        public OwnerDto? Owner { get; set; }

    }
}
