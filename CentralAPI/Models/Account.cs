﻿using System.ComponentModel.DataAnnotations;

namespace CentralAPI.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
