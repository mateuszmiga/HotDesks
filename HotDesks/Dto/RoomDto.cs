using Domain.Entities;

namespace HotDesks.Api.Dto
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Desk> Desks { get; set; }
    }
}
