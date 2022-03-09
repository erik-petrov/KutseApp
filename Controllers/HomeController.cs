using KutseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace KutseApp.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Greeting = DateTime.Now.Hour < 12 ? "Tere hommikust" : $"Tere päevast";
			ViewBag.Message = "Ootan sind oma peole! Palun tule kindlasti";
			return View();
		}
		[HttpGet]
		public ViewResult Ankeet()
		{
			return View();
		}
		[HttpPost]
		public ViewResult Ankeet(Guest guest)
		{
			if (ModelState.IsValid)
			{
				db.Guests.Add(guest);
				db.SaveChanges();
				Thanks(guest.Email);
				return View("Thanks", guest);
			}
			else
				return View();
		}
		public ActionResult About()
		{
			ViewBag.Message = "Haha.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Hehe.";

			return View();
		}
		[HttpPost]
		public void Thanks(string email)
		{
			SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
			{
				Port = 587,
				Credentials = new NetworkCredential("programmeeriminetthk3@gmail.com", "tarpv20 op 228 1337"),
				EnableSsl = true,
			};
			MailMessage mailMessage = new MailMessage
			{
				From = new MailAddress("Cinema.Amogus@service.com"),
				Subject = "Kutse peole",
				Body = $"<h1>Ära unusta et sul on peole kutse!</h1>",
				IsBodyHtml = true,
			};

			mailMessage.To.Add(new MailAddress(email));
			smtpClient.Send(mailMessage);
		}
		
		GuestContext db = new GuestContext();
		[Authorize] //-только увидит залогиненный чел
		public ActionResult Guests()
		{
			IEnumerable<Guest> guests = db.Guests;
			return View(guests);
		}
		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Create(Guest guest)
		{
			db.Guests.Add(guest);
			db.SaveChanges();
			return RedirectToAction("Guests");
		}
		[HttpGet]
		public ActionResult Delete(int id)
		{
			Guest g = db.Guests.Find(id);
			if (g == null)
			{
				return HttpNotFound();
			}
			return View(g);
		}
		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			Guest g = db.Guests.Find(id);
			if(g == null)
			{
				return HttpNotFound();
			}
			db.Guests.Remove(g);
			db.SaveChanges();
			return RedirectToAction("Guests");
		}
	}
}