using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace FacultySchedules
{
	public class Scrape
	{
		public List<HtmlNode> BeginScrape(string name)
		{
			SpecialNameFormatting specialFormatInit = new SpecialNameFormatting();
			WebClient webClient = new WebClient();

			string page = webClient.DownloadString(specialFormatInit.splitNameGetURL(name));

			HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
			doc.LoadHtml(page);

			List<HtmlNode> x = doc.GetElementbyId("ctl0_Main_tblSchedule").Elements("tr").ToList();

			/*
			HtmlDocument spanDoc = new HtmlDocument();
			spanDoc.LoadHtml(page);
			//List<List<Tuple<string,string>>> 
			HtmlNode table = spanDoc.DocumentNode.SelectSingleNode("//table[@border='3']")
					  .Descendants("tr")
					  .Skip(1)
					  .Where(tr => tr.Elements("td").Count() >= 4)
					  .Select(tr => tr.Elements("td")
						  .Select(td => new Tuple<string, string>(td.InnerText.Trim(), td.Attributes["rowspan"] != null ? td.Attributes["Colspan"].Value : "1").ToList())
					  .ToList();

*/

			return x;
		}
	}
}