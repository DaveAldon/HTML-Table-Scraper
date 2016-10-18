using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HtmlAgilityPack;

namespace FacultySchedulesUserClient.Controllers
{
	public class HomeController : Controller
	{
		//List<List<string>> somelist = new List<List<string>>();
		public ActionResult Index()
		{
			Scrape scraper = new Scrape();
			List<HtmlNode> x = scraper.BeginScrape();

			foreach (HtmlNode node in x)
			{
				List<HtmlNode> s = node.Elements("td").ToList();
				foreach (HtmlNode item in s)
				{
					Console.WriteLine("TD Value: " + item.InnerText);
				}
			}
			Console.ReadLine();



			var mvcName = typeof(Controller).Assembly.GetName();
			var isMono = Type.GetType("Mono.Runtime") != null;

			ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
			ViewData["Runtime"] = isMono ? "Mono" : ".NET";

			return View();
		}
	}
}
