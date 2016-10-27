using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace FacultySchedules
{
	class Run
	{
		public GiveData giveDB = new GiveData();

		public void start(string name)
		{
			List<string> dayList = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };

			List<string> days = new List<string>();
			int index = 0;
			string time = "";
			string value = "";
			string rowSpan = "rowspan=";
			string rowSpanLength = "";
			string tempHTML = "";
			char rowSpanChar;

			Scrape scraper = new Scrape();
			List<HtmlNode> x = scraper.BeginScrape(name);

			bool first = true;

			foreach (HtmlNode item in x)
			{
				if (first)
				{
					first = false;
				}

				tempHTML = item.InnerHtml;
				if (tempHTML.Contains(rowSpan))
				{
					rowSpanChar = tempHTML[tempHTML.IndexOf(rowSpan, System.StringComparison.Ordinal) + 9];
					rowSpanLength = rowSpanChar.ToString();
				}
				else {
					rowSpanLength = "0";
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
					days.Add(dayList[index - 1] + "$" + time + "$" + value + "$" + rowSpanLength);
					index = index + 1;
				}
				index = 0;
			}
			giveDB.DBGather(days, name);
		}
	}
}