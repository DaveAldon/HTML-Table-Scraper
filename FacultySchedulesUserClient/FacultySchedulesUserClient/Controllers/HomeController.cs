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

		List<string> dayList = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday"};

		List<string> days = new List<string>();
		int index = 0;
		string time = "";
		string value = "";
		string poo = "";

		//List<List<string>> somelist = new List<List<string>>();
		public ActionResult Index()
		{
			Scrape scraper = new Scrape();
			List<HtmlNode> x = scraper.BeginScrape();

			bool first = true;
			foreach (HtmlNode item in x)
			{
				if (first)
				{
					first = false;
				}

				List<HtmlNode> cell = item.Elements("td").ToList();
				foreach (HtmlNode events in cell)
				{
					value = events.InnerText;
					if (index == 0)
					{
						index = index + 1;
						time = value;
						continue;
					}

					days.Add(dayList[index - 1] + " " + time + " " + value);
					index = index + 1;
				}
				index = 0;
			}

			/*
			foreach (HtmlNode node in x)
			{
				List<HtmlNode> s = node.Elements("tr").ToList();
				foreach (HtmlNode item in s)
				{
					int dayCount = 0;

					List<HtmlNode> r = node.Elements("td").ToList();
					foreach (HtmlNode cell in r)
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
				}
				}
				*/

			for (int i = 0; i < days.Count; i++)
			{
				poo = poo + days[i] + "\r\n";
			}

		var mvcName = typeof(Controller).Assembly.GetName();
		var isMono = Type.GetType("Mono.Runtime") != null;

		ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor + poo;
		ViewData["Runtime"] = isMono? "Mono" : ".NET";

		return View();
		}
	}
}