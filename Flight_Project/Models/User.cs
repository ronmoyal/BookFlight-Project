using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flight_Project.Models
{
    public class User
    {
        [Key]
        [Required]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z]{1,40}$",
         ErrorMessage = "Characters are not allowed.")]
        public string Fname { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z]{1,40}$",
         ErrorMessage = "Characters are not allowed.")]
        public string Lname { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]{8,15}$",
         ErrorMessage = "Password must contain 8-15 character")]
        [DataType(DataType.Password)]
        public string Pass { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]{8,15}$",
         ErrorMessage = "Please enter the confirm password")]
        [DataType(DataType.Password)]
        [Compare("Pass")]
        public string ConfirmPass { get; set; }

    }
}