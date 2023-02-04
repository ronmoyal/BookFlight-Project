using Flight_Project.Dal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flight_Project.Models
{
    public class Order
    {
        [Key]
        public string OrderId { get; set; } 
        public int TicketQuantity { get; set; } 
        public int Price { get; set; }
        public string Email { get; set; }

        public string origin { get; set; }

        public string destination { get; set; }

        public DateTime dateOB { get; set; }

        public TimeSpan timeOB { get; set; }

        public DateTime dateReturn { get; set; }
        public TimeSpan timeReturn { get; set; }

    }

}





















/*        public bool IsPaid { get; set; } // האם שולם
        public DateTimeOffset OrderPlacedTime { get; set; } // כמה זמן המנה*/

/*        public void Save()
        {
            using (var context = new UserDal())
            {
                context.Orders.Add(this);
                context.SaveChanges();
            }
        }*/
/*        public static void DeleteExpiredUnpaidOrders()
        {
            using (var context = new UserDal())
            {
                // Find all unpaid orders that were placed more than 2 minutes ago
                var expiredOrders = context.Orders
                    .Where(o => !o.IsPaid && o.OrderPlacedTime < DateTimeOffset.Now.AddMinutes(-2))
                    .ToList();

                // Delete the expired orders
                context.Orders.RemoveRange(expiredOrders);
                context.SaveChanges();
            }
        }
*/