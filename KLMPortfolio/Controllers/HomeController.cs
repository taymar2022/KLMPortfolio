using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using BlogPosts;
using Microsoft.AspNet.Identity;
using KLMPortfolio.Models;

namespace KLMPortfolio.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //POST:  BlogPosts/Contact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact([Bind(Include = "Id,FirstName,LastName,Email,Message,Phone")] Contact contact)
        {
            contact.Created = DateTime.Now;
            var newContact = contact.FirstName + " " + contact.LastName;

            var svc = new EmailService();
            var msg = new IdentityMessage();
            msg.Subject = "Contact From Portfolio Site";
            msg.Body = "\r\n You have recieved a request to contact from " + newContact + "(" + contact.Email + ")" + "\r\n"
                         + "\r\n With the following message: \r\n\t"
                         + contact.Message;


            await svc.SendAsync(msg);

            return View(contact);
        }


    public ActionResult Portfolio()
        {
            ViewBag.Message = "Your portfolio page.";

            return View();
        }

        public ActionResult JavaScriptExercises()
        {
            ViewBag.Message = "Your JS Exercises page.";

            return View();
        }

        public ActionResult Services()
        {
            ViewBag.Message = "Your services page.";

            return View();
        }
    }
}