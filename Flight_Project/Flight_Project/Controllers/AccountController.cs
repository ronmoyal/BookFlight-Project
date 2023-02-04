using Flight_Project.Dal;
using Flight_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Flight_Project.Controllers
{
    public class AccountController : Controller
    {
        UserDal dal = new UserDal();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User usr  )
        {
            bool userExits = dal.Users.Any(x=>x.Email==usr.Email&&x.Pass==usr.Pass);
            User u = dal.Users.FirstOrDefault(x => x.Email == usr.Email && x.Pass == usr.Pass);
           
            if (userExits)
            {
                FormsAuthentication.SetAuthCookie(u.Email, false);
                return RedirectToAction("Index", "Home"); 
            }

            ModelState.AddModelError("", "Email or Password is wrong");
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User usr)
        {
            dal.Users.Add(usr);
            dal.SaveChanges();

            return RedirectToAction("Login");
        }

        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}