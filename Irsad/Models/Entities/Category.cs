using Irsad.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Irsad.Models.Entities
{
    [ModelMetadataType(typeof(AddCategoryVM))]
    public class Category : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Product>? Products { get; set; }

    }
}