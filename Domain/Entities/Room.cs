namespace Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Desk> Desks { get; set; }
    }
}