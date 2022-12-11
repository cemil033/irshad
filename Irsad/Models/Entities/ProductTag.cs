namespace Irsad.Models.Entities
{
    public class ProductTag:Entity
    {
        public int ProductId { get; set; }
        public int TagId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Tag Tag { get; set; }

    }
}
