using Irsad.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Irsad.Models.Entities
{
    [ModelMetadataType(typeof(AddProductVM))]
    public class Product : Entity 
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int Count { get; set; }
        public virtual Category Category { get; set; }
        public virtual IEnumerable<ProductTag>? ProductTags { get; set; }
        public virtual IEnumerable<UserProduct>? UserProducts { get; set; }
    }
}
