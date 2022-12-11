using System.ComponentModel.DataAnnotations;

namespace Irsad.Models.ViewModels
{
    public class AddCategoryVM
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}
