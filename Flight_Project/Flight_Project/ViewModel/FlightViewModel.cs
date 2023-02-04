using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Flight_Project.Models;

namespace Flight_Project.ViewModel
{
    public class FlightViewModel
    {
        public Flight flight { get; set; }

        public List<Flight> flightsList { get; set; }

        public List<Flight> backFlightsList { get; set; }

        public string OutFlightID{ get; set; }//מס טיסה חלוך
        public string InFlightID { get; set; }// מס טיסה חזור

        public int seatInput { get; set; }
        public int Totalprice { get; set; }

        public string originOUT { get; set; }

        public string destinationOUT { get; set; }

        public string originIN { get; set; }

        public string destinationIN { get; set; }

        public int Timer { get; set; }

        public DateTime dateOB { get; set; }

        public TimeSpan timeOB { get; set; }
        public DateTime dateReturn { get; set; }

        public TimeSpan timeReturn { get; set; }

        public Order order { get; set; }
    }
}