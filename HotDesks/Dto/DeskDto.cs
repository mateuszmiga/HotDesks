using Domain.Entities;

namespace HotDesks.Api.Dto
{
    public class DeskDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? RentingStart { get; set; }
        public DateTime? RentingEnd { get; set; }

        public virtual Owner? Owner { get; set; }
        public virtual Room Room { get; set; }

    }
}
