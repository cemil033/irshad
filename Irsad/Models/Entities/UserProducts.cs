namespace Irsad.Models.Entities
{
    public class UserProduct:Entity
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual AppUser User { get; set; }
    }
}
