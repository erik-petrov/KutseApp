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
				if(guest.Attend == true)
				{
					SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
					{
						Port = 587,
						Credentials = new NetworkCredential("programmeeriminetthk2@gmail.com", "2.kuursus tarpv20"),
						EnableSsl = true,
					};
					MailMessage mailMessage = new MailMessage
					{
						From = new MailAddress("Cinema.Amogus@service.com"),
						Subject = "Kutse peole",
						Body = $"<h1>Ära unusta et sul on peole kutse!</h1>",
						IsBodyHtml = true,
					};

					mailMessage.To.Add(new MailAddress(guest.Email));
					smtpClient.Send(mailMessage);
				}
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
	}
}