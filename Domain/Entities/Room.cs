namespace Domain.Entities
{
    public class Room : BaseEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Desk> Desks { get; set; }
    }
}