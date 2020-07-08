using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SetProducts(FormCollection collection)
        {
            UserImplement imp = new UserImplement();
            User usr = new User { 
                id_User = 0,
                name_User = collection ["Username"],
                password_User = collection["Password"]
            };
          
            if (imp.LogIn(usr))
            {
                return RedirectToAction("ShowProducts", "Product");
            }
            else { return RedirectToAction("Index"); }
        }
        [HttpPost]
        public ActionResult CreateUser(FormCollection collection)
        {
            UserImplement imp = new UserImplement();
            User usr = new User
            {
                name_User = collection["Username1"],
                password_User = collection["Password1"]
            };
            imp.Create(usr);
            return RedirectToAction("Index");
        }
    }
}
