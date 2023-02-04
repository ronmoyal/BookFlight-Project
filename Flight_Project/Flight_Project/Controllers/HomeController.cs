using Flight_Project.Dal;
using Flight_Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flight_Project.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        public ActionResult TImesUp() //after timer is over return to this action
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
            
            return RedirectToAction("Index");
        }


        public ActionResult Index()
        {
            

            return View();
        }


        /*       
                public ActionResult Contact()
                {
                    ViewBag.Message = "Your contact page.";

                    return View();
                }*/
    }
}