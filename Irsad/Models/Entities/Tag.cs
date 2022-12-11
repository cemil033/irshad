using Irsad.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Irsad.Models.Entities
{
    [ModelMetadataType(typeof(AddTagVM))]
    public class Tag : Entity
    {
        public string Name { get; set; }
        public virtual IEnumerable<ProductTag>? ProductTags { get; set; }
    }
}
