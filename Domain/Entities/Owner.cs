namespace Domain.Entities
{
    public class Owner : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Desk Desk { get; set; }
    }
}