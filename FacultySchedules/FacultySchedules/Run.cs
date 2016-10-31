using System.Collections.Generic;
using System;
using HtmlAgilityPack;

namespace FacultySchedules
{
	class Run
	{
		public GiveData giveDB = new GiveData();
		public void start(string name)
		{
			List<string> days = new List<string>();
			List<string> classes = new List<string>();
			int index = 0;
			string value = "";
			string rowSpan = "rowspan=\"";
			string tempHTML = "";
			int span = 0;
			Scrape scraper = new Scrape();
			List<HtmlNode> x = scraper.BeginScrape(name);
			var time = "";
			DateTime firstTime;
			bool first = true;
			int breakIndex;
			string tempSubString = "";

			foreach (HtmlNode item in x)
			{
				if (first)
				{
					first = false;
					continue;
				}

				tempHTML = item.InnerHtml;
				string[] subStrings = tempHTML.Split(new string[] { "</td>" }, StringSplitOptions.None);

				for (int i = 0; i < subStrings.Length - 1; i++)
				{
					if (i == 0)
					{
						subStrings[i] = subStrings[i] + "</td>";
						time = subStrings[i].Substring(subStrings[i].IndexOf(">", StringComparison.Ordinal) + 1,
															  subStrings[i].IndexOf("</td>", StringComparison.Ordinal) -
															  subStrings[i].IndexOf(">", StringComparison.Ordinal) - 1);
						continue;
					}

					subStrings[i] = subStrings[i] + "</td>";

					var innerString = subStrings[i].Substring(subStrings[i].IndexOf(">", StringComparison.Ordinal) + 1,
														  subStrings[i].IndexOf("</td>", StringComparison.Ordinal) -
														  subStrings[i].IndexOf(">", StringComparison.Ordinal) - 1);
					
					if (innerString.Contains("<br>"))
					{
						breakIndex = innerString.IndexOf("<br>", StringComparison.Ordinal);
						if (breakIndex > 0)
						{
							innerString = innerString.Substring(0, breakIndex - 2);
						}
					}

					else if (innerString.Contains("Meeting "))
					{
						breakIndex = innerString.IndexOf("Meeting ", StringComparison.Ordinal);
						if (breakIndex > 0)
						{
							innerString = innerString.Substring(0, breakIndex - 2);
						}
					}

					tempSubString = subStrings[i];

					if (tempSubString.Contains(rowSpan))
					{
						if (tempSubString.LastIndexOf('2', 50) > 0)
						{
							span = 2;
						}
						else if (tempSubString.LastIndexOf('3', 50) > 0)
						{
							span = 3;
						}
						else if (tempSubString.LastIndexOf('4', 50) > 0)
						{
							span = 4;
						}
						else if (tempSubString.LastIndexOf('5', 50) > 0)
						{
							span = 5;
						}
						else if (tempSubString.LastIndexOf('6', 50) > 0)
						{
							span = 6;
						}

						value = innerString;
						firstTime = DateTime.Parse(time);
						days.Add(Globals.dayList[index] + "$" + String.Format("{0:t}", firstTime) + "$" + value + "$" + span);
						classes.Add(value);

						firstTime = DateTime.Parse(time);
						int tempSpan = span;

						while (tempSpan > 1)
						{
							firstTime = firstTime.AddMinutes(30);

							days.Add(Globals.dayList[index] + "$" + String.Format("{0:t}", firstTime) + "$" + value + "$" + span);
							tempSpan--;
						}
						index = index + 1;
					}

					else {
						span = 0;
						index = index + 1;
					}
				}
				index = 0;
			}
			giveDB.DBGather(days, name, classes);
		}
	}
}