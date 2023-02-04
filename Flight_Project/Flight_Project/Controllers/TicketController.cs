using Flight_Project.Dal;
using Flight_Project.Models;
using Flight_Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flight_Project.Controllers
{
    public class TicketController : Controller
    { 
        UserDal context = new UserDal();
        FlightViewModel fvm = new FlightViewModel();

        // GET: Ticket
        [HttpPost]

        public ActionResult Details()
        {
            FlightViewModel FVM = (FlightViewModel)Session["FlightViewModel"];


            FVM.order = new Order();
            FVM.order.Email = User.Identity.Name;
            FVM.order.OrderId = Guid.NewGuid().ToString();
            FVM.order.TicketQuantity = FVM.seatInput;
            FVM.order.Price = FVM.Totalprice;
            FVM.order.origin = FVM.originOUT;
            FVM.order.destination = FVM.destinationOUT;
            FVM.order.dateOB = FVM.dateOB;
            FVM.order.timeOB = FVM.timeOB;

            if (FVM.InFlightID != null) //two ways to flight
            {
                FVM.order.dateReturn = FVM.dateReturn;
                FVM.order.timeReturn = FVM.timeReturn;
            }


            using (var context = new UserDal())
            {

                context.Orders.Add(FVM.order);
                context.SaveChanges();
            }


            return View();
        }
    }
}