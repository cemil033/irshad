using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Irsad.Models.ViewModels
{
    public class AddProductVM
    {
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Price is required")]        
        public double Price { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        public int[] TagIds { get; set; }

        [Range(0,999)]
        public int Count { get; set; }

        public IFormFile ImageUrl { get; set; }
    }
}
