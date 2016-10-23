using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace FacultySchedules
{
	public class Scrape
	{
		public List<HtmlNode> BeginScrape(string firstName, string lastName)
		{
			WebClient webClient = new WebClient();
			string page = webClient.DownloadString("http://www.cis.gvsu.edu/public/staffListing/index.php?page=staff&fname=" + firstName + "&lname=" + lastName);

			HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
			doc.LoadHtml(page);

			List<HtmlNode> x = doc.GetElementbyId("ctl0_Main_tblSchedule").Elements("tr").ToList();

			return x;
		}
	}
}