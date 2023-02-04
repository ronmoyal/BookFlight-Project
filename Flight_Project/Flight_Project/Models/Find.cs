using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flight_Project.Models
{
    public class Find
    {
        [Required]
        [RegularExpression(@"^[A-Za-z]{1,40}$",ErrorMessage = "Characters are not allowed.")]
        public string Origin { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z]{1,40}$", ErrorMessage = "Characters are not allowed.")]
        public string Destination { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }
    }
}