using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flight_Project.Models
{
    public class Card
    {
        [Key]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^\d{9}$",
         ErrorMessage = "id not Valid")]
        public string IdNumber { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$",
        ErrorMessage = "Characters are not allowed")]
        public string FullName { get; set; }
        [Required]
        public string cardNum { get; set; }

        [Required]
        [RegularExpression(@"^\d{2}/\d{2}$",
        ErrorMessage = "Characters are not allowed")]
        public string expDate { get; set; }

        [Required]

        [RegularExpression(@"^\d{3}$",
             ErrorMessage = "CVV Not valid")]


        public string cvv { get; set; }

    }
}