﻿using System.ComponentModel.DataAnnotations;

namespace Irsad.Models.ViewModels
{
    public class RegisterationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
       
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
