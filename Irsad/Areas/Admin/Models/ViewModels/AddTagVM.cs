using System.ComponentModel.DataAnnotations;

namespace Irsad.Models.ViewModels
{   

    public class AddTagVM
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}
