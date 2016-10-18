using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HtmlAgilityPack;

namespace FacultySchedulesUserClient.Controllers
{
	public class HomeController : Controller
	{
		List<string> timeList = new List<string>();
		List<string> monList = new List<string>();
		List<string> tueList = new List<string>();
		List<string> wedList = new List<string>();
		List<string> thuList = new List<string>();
		List<string> friList = new List<string>();
		//List<List<string>> somelist = new List<List<string>>();
		public ActionResult Index()
		{
			Scrape scraper = new Scrape();
			List<HtmlNode> x = scraper.BeginScrape();

			foreach (HtmlNode node in x)
			{
				
				int dayCount = 0;
				List<HtmlNode> s = node.Elements("td").ToList();
				foreach (HtmlNode item in s)
				{
					if (dayCount == 0)
					{
						timeList.Add(item.InnerText);
					}
					else if (dayCount == 1)
					{
						if (item.InnerText == "&nbsp;")
						{
							monList.Add("Empty");
						}
						else
						monList.Add(item.InnerText);
					}
					else if (dayCount == 2)
					{
						if (item.InnerText == "&nbsp;")
						{
							tueList.Add("Empty");
						}
						else
						tueList.Add(item.InnerText);
					}
					else if (dayCount == 3)
					{
						if (item.InnerText == "&nbsp;")
						{
							wedList.Add("Empty");
						}
						else
						wedList.Add(item.InnerText);
					}
					else if (dayCount == 4)
					{
						if (item.InnerText == "&nbsp;")
						{
							thuList.Add("Empty");
						}
						else
						thuList.Add(item.InnerText);
					}
					else if (dayCount == 5)
					{
						if (item.InnerText == "&nbsp;")
						{
							friList.Add("Empty");
						}
						else
						friList.Add(item.InnerText);
					}
					dayCount++;
				}
				dayCount = 0;
			}


			var mvcName = typeof(Controller).Assembly.GetName();
			var isMono = Type.GetType("Mono.Runtime") != null;

			ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor + thuList[0];
			ViewData["Runtime"] = isMono ? "Mono" : ".NET";

			return View();
		}
	}
}
