using Flight_Project.Dal;
using Flight_Project.Models;
using Flight_Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using System.Security.Cryptography;


namespace Flight_Project.Controllers
{
    public class TicketController : Controller
    {
        UserDal context = new UserDal();
        FlightViewModel fvm = new FlightViewModel();

        // GET: Ticket


        public ActionResult Details(Card card, bool save)
        {
            if (card == null)
            {
                card = context.Cards.FirstOrDefault(x => x.Email.Equals(User.Identity.Name));
                //insert the card to db


            }
            card.Email = User.Identity.Name;
            FlightViewModel FVM = (FlightViewModel)Session["FlightViewModel"];


            FVM.order = new Order();
            FVM.order.Email = User.Identity.Name;
            FVM.order.OrderId = Guid.NewGuid().ToString().Substring(0, 8); ;
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

            if (save)
            {
                var delete = context.Cards.FirstOrDefault(x => x.Email.Equals(User.Identity.Name));
                if (delete != null)
                {
                    context.Cards.Remove(delete);
                    context.SaveChanges();
                }

                card.cardNum = Encrypt(card.cardNum);
                //insert the card to db
                context.Cards.Add(card);
                context.SaveChanges();
            }

            context.Orders.Add(FVM.order);
            context.SaveChanges();



            return View();
        }
        private string Encrypt(string clearText)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }

            return clearText;
        }

        private string Decrypt(string cipherText)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return cipherText;
        }
    }
}
