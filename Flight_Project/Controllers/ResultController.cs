using Flight_Project.Dal;
using Flight_Project.Models;
using Flight_Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Flight_Project.Controllers
{
    [Authorize]
    public class ResultController : Controller
    {
        UserDal context = new UserDal();
        // GET: Result
        public ActionResult showSearchPage(Find find)
        {
            List<Flight> objFlight = context.Flights.ToList<Flight>();
            FlightViewModel fvm = new FlightViewModel();
            fvm.flight = new Flight();
            fvm.flightsList = new List<Flight>();
            fvm.backFlightsList = new List<Flight>();
            foreach (Flight x in objFlight)
            {
                TimeSpan currentTime = DateTime.Now.TimeOfDay;
                DateTime currentDate = DateTime.Now.Date;

                if (x.origin.Trim(' ').Equals(find.Origin) && x.destination.Trim(' ').Equals(find.Destination) && x.date == find.Date && x.seat != 0 && ((currentTime < x.time && currentDate == x.date) || (currentDate < x.date)))
                {
                    fvm.flightsList.Add(x);
                }

                if (x.origin.Trim(' ').Equals(find.Destination) && x.destination.Trim(' ').Equals(find.Origin) && x.date == find.ReturnDate && x.seat != 0 && ((currentTime < x.time && currentDate == x.date) || (currentDate < x.date)))
                {
                    fvm.backFlightsList.Add(x);
                }
            }



            if (fvm.flightsList.ToList().Count() == 0)
            {
                TempData["Message"] = "Sorry, there are no flights available for the selected origin, destination, and date.";
            }

            Session["FlightViewModel"] = fvm;

            return View();
        }


        public ActionResult showReturnFlight(FlightViewModel FVM, string subButton)
        {
            string submitButtonValue = subButton;
            List<Flight> objFlight = context.Flights.ToList<Flight>();
            FlightViewModel fvm = new FlightViewModel();
            fvm.flight = new Flight();
            fvm.backFlightsList = new List<Flight>();
            fvm.OutFlightID = FVM.OutFlightID;

            if (submitButtonValue == "Choose Return Flight")
            {

                // If the user has selected an outbound flight, find the corresponding return flights
                if (fvm.OutFlightID != null)
                {
                    // Find the outbound flight that was selected by the user
                    Flight outboundFlight = objFlight.FirstOrDefault(f => f.flightID == fvm.OutFlightID);
                    // Find return flights that have the same destination as the outbound flight's origin and the same origin as the outbound flight's destination

                    foreach (Flight x in objFlight)
                    {
                        if (x.origin == outboundFlight.destination && x.destination == outboundFlight.origin && x.seat != 0 && ((x.time > outboundFlight.time && x.date == outboundFlight.date) || (x.date > outboundFlight.date)))
                        {
                            fvm.backFlightsList.Add(x);
                        }
                    }
                }
                if (fvm.backFlightsList.ToList().Count() == 0 && fvm.OutFlightID != null)//נבחרה טיסה הלוך
                {
                    TempData["Message"] = "No return flights available";
                    return RedirectToAction("showAllFlights", "Result");
                }


                Session["FlightViewModel"] = fvm;

                return View("showAllFlights");
            }
            else
            {

                Session["FlightViewModel"] = fvm;
                return RedirectToAction("BookSeat");
            }
        }

        public ActionResult SortTable(string sortOrder)
        {
            // Get the list of all flights from the database
            List<Flight> objFlight = context.Flights.ToList<Flight>();

            // Sort the list of flights by the selected option
            switch (sortOrder)
            {
                case "price_asc":
                    objFlight = objFlight.OrderBy(f => f.price).ToList();
                    break;
                case "price_desc":
                    objFlight = objFlight.OrderByDescending(f => f.price).ToList();
                    break;
                case "popularity_asc":
                    objFlight = objFlight.OrderBy(f => f.seat).ToList();
                    break;
                case "country_asc":
                    objFlight = objFlight.OrderBy(f => f.origin).ToList();
                    break;
                default:
                    objFlight = objFlight.OrderBy(f => f.price).ToList();
                    break;
            }

            // Initialize the FlightViewModel object
            FlightViewModel fvm = new FlightViewModel();
            fvm.flight = new Flight();
            fvm.flightsList = new List<Flight>();
            fvm.backFlightsList = new List<Flight>();

            // Iterate through all flights in the sorted list
            foreach (Flight x in objFlight)
            {
                TimeSpan currentTime = DateTime.Now.TimeOfDay;
                DateTime currentDate = DateTime.Now.Date;

                // Add the flight to the outbound flights list if it has seats available and its departure time has not passed
                if (x.seat != 0 && ((currentTime < x.time && currentDate == x.date) || (currentDate < x.date)))
                {
                    fvm.flightsList.Add(x);
                }
            }

            // Pass the sorted list of flights to the view
            Session["FlightViewModel"] = fvm;

            return View("showAllFlights");
        }

        public ActionResult Existcard()
        {
            Card cr = new Card();
            bool save = false;
            return RedirectToAction("Details", "Ticket", new { card = cr, save = save });
        }

        public ActionResult showAllFlights()
        {
            // Get the list of all flights from the database
            List<Flight> objFlight = context.Flights.ToList<Flight>();

            // Initialize the FlightViewModel object
            FlightViewModel fvm = new FlightViewModel();
            fvm.flight = new Flight();
            fvm.flightsList = new List<Flight>();
            fvm.backFlightsList = new List<Flight>();

            // Iterate through all flights in the database
            foreach (Flight x in objFlight)
            {
                TimeSpan currentTime = DateTime.Now.TimeOfDay;
                DateTime currentDate = DateTime.Now.Date;

                // Add the flight to the outbound flights list if it has seats available and its departure time has not passed
                if (x.seat != 0 && ((currentTime < x.time && currentDate == x.date) || (currentDate < x.date)))
                {
                    fvm.flightsList.Add(x);
                }
            }

            Session["FlightViewModel"] = fvm;

            return View();
        }

        public ActionResult ChangeSeats(FlightViewModel fvm)
        {
            FlightViewModel FVM = (FlightViewModel)Session["FlightViewModel"];
            //נחזיר את נספר הכסאות לדאטה - לא שולם
            UserDal context = new UserDal();

            // Calculate the new number of seats available on the outbound flight after booking
            var NumSeatOUT = (from x in context.Flights where x.flightID.Equals(FVM.OutFlightID) select x.seat).FirstOrDefault() + FVM.seatInput;
            // Update the number of seats available for the outbound flight in the database
            var flightToUpdateOUT = context.Flights.FirstOrDefault(f => f.flightID == FVM.OutFlightID);
            flightToUpdateOUT.seat = NumSeatOUT;

            if (FVM.InFlightID != null)
            {
                var NumSeatIN = (from x in context.Flights where x.flightID.Equals(FVM.InFlightID) select x.seat).FirstOrDefault() + FVM.seatInput;
                var flightToUpdateIN = context.Flights.FirstOrDefault(f => f.flightID == FVM.InFlightID);
                flightToUpdateIN.seat = NumSeatIN;
            }
            context.SaveChanges();

            return RedirectToAction("BookSeat");

        }

        public ActionResult BookSeat(FlightViewModel fvm)
        {

            FlightViewModel FVM = (FlightViewModel)Session["FlightViewModel"];

            if (fvm.OutFlightID == null)// if we come from all flight table
            {
                fvm.OutFlightID = FVM.OutFlightID;
            }


            if (fvm.OutFlightID != null)
            {
                FVM.OutFlightID = fvm.OutFlightID;
                var origin = from x in context.Flights where x.flightID.Equals(FVM.OutFlightID) select x.origin; //enter origin to FVM
                FVM.originOUT = origin.FirstOrDefault();

                var destination = from x in context.Flights where x.flightID.Equals(FVM.OutFlightID) select x.destination; //enter origin to FVM
                FVM.destinationOUT = destination.FirstOrDefault();

                var date = from x in context.Flights where x.flightID.Equals(FVM.OutFlightID) select x.date;//date- out to fvm
                FVM.dateOB = date.FirstOrDefault();
                var time = from x in context.Flights where x.flightID.Equals(FVM.OutFlightID) select x.time;//time- out to fvm
                FVM.timeOB = time.FirstOrDefault();

            }


            if (fvm.InFlightID != null)
            {
                FVM.InFlightID = fvm.InFlightID;
                var origin = from x in context.Flights where x.flightID.Equals(FVM.InFlightID) select x.origin; //if have return change destinationt 
                FVM.originIN = origin.FirstOrDefault();
                var destination = from x in context.Flights where x.flightID.Equals(FVM.InFlightID) select x.destination; //if have return change destinationt 
                FVM.destinationIN = destination.FirstOrDefault();

                var date = from x in context.Flights where x.flightID.Equals(FVM.InFlightID) select x.date;//date- in to fvm
                FVM.dateReturn = date.FirstOrDefault();
                var time = from x in context.Flights where x.flightID.Equals(FVM.InFlightID) select x.time;//time- in to fvm
                FVM.timeReturn = time.FirstOrDefault();
            }


            return View();
        }

        public ActionResult Payment(FlightViewModel fvm)
        {
            FlightViewModel FVM = (FlightViewModel)Session["FlightViewModel"];

            FVM.Timer = 120;


            if (fvm.seatInput > (from x in context.Flights where x.flightID.Equals(FVM.OutFlightID) select x.seat).FirstOrDefault())
            {
                // Display an error message if there are not enough seats available on the selected outbound flight
                TempData["Message"] = "Sorry, there are not enough seats available on the selected outbound flight.";
                return RedirectToAction("BookSeat", "Result");

            }
            else
            {
                var outPrice = from x in context.Flights where x.flightID.Equals(FVM.OutFlightID) select x.price;
                FVM.Totalprice = outPrice.Sum() * fvm.seatInput;
            }
            if (FVM.InFlightID != null)
            {
                if (fvm.seatInput > (from x in context.Flights where x.flightID.Equals(FVM.InFlightID) select x.seat).FirstOrDefault())
                {
                    // Display an error message if there are not enough seats available on the selected inbound flight
                    TempData["Message"] = "Sorry, there are not enough seats available on the selected inbound flight.";
                    return RedirectToAction("BookSeat", "Result");
                }

                else
                {
                    var InPrice = from x in context.Flights where x.flightID.Equals(FVM.InFlightID) select x.price;
                    FVM.Totalprice += InPrice.Sum() * fvm.seatInput;
                }
            }

            FVM.seatInput = fvm.seatInput;


            // Calculate the new number of seats available on the outbound flight after booking
            var newNumSeatOUT = (from x in context.Flights where x.flightID.Equals(FVM.OutFlightID) select x.seat).FirstOrDefault() - FVM.seatInput;
            // Update the number of seats available for the outbound flight in the database
            var flightToUpdateOUT = context.Flights.FirstOrDefault(f => f.flightID == FVM.OutFlightID);
            flightToUpdateOUT.seat = newNumSeatOUT;

            if (FVM.InFlightID != null)
            {
                var newNumSeatIN = (from x in context.Flights where x.flightID.Equals(FVM.InFlightID) select x.seat).FirstOrDefault() - FVM.seatInput;
                var flightToUpdateIN = context.Flights.FirstOrDefault(f => f.flightID == FVM.InFlightID);
                flightToUpdateIN.seat = newNumSeatIN;
            }
            context.SaveChanges();
            var card = context.Cards.Any(x => x.Email.Equals(User.Identity.Name));
            if (card)
            {
                ViewBag.creditExist = "Hi! we have your credit card details, if you wanna use it tap here  ->  ";
            }

            return View();
        }
    }


}