using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace FacultySchedulesUserClient
{
	public class Scrape
	{
		public List<HtmlNode> BeginScrape()
		{
			WebClient webClient = new WebClient();
			string page = webClient.DownloadString("http://www.cis.gvsu.edu/public/staffListing/index.php?page=staff&fname=Ira&lname=Woodring");

			HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
			doc.LoadHtml(page);

			List<HtmlNode> x = doc.GetElementbyId("ctl0_Main_tblSchedule").Elements("tr").ToList();

			return x;
		}
	}
}