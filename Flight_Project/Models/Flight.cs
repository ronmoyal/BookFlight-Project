using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flight_Project.Models
{
    public class Flight
    {
        [Key]
        [Required]
        public string flightID { get; set; }
        [Required]

        public string origin { get; set; }
        [Required]

        public string destination { get; set; }
        [Required]
        public DateTime date { get; set; }

        [Required]
        public TimeSpan time { get; set; }
       
        [Required]
        public int price { get; set; }

        public int seat { get; set; }

    }
}