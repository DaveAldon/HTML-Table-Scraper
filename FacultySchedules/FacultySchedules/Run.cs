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
			Scrape scraper = new Scrape();
			List<HtmlNode> x = scraper.BeginScrape(name);
			int index = 0;
			int span = 0;
			int skipHorizontal = 0;
			int weeksWorthCounter = 0;
			int breakIndex;
			var time = "";
			DateTime firstTime;
			bool first = true;
			string value = "";
			string rowSpan = "rowspan=\"";
			string tempHTML = "";
			string tempSubString = "";
			string[,] weeksWorth = new string[32, 5];
			string[,] sabbaticalWeek = new string[0, 0];

			foreach (HtmlNode item in x)
			{
				if (first)
				{
					if (item.InnerText.Contains("Sabbatical"))
					{
						sabbaticalWeek[0,0] = (0 + "$" + "All Semester" + "$" + "Sabbatical" + "$" + 0);
						giveDB.DBGather(sabbaticalWeek, name);
					}
					break;
				}
				first = false;
					
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
						for (int spanFinder = 2; spanFinder < 32; spanFinder++)
						{
							if (tempSubString.LastIndexOf(spanFinder.ToString(), 50, StringComparison.Ordinal) > 0)
							{
								span = spanFinder;
								break;
							}
						}

						value = innerString;
						firstTime = DateTime.Parse(time);
						int tempSpan = span;

						for (int d = 0; d < 5; d++)
						{
							if (weeksWorth[weeksWorthCounter, d] == null)
							{
								skipHorizontal = d;
								break;
							}
						}

						int tempCount = weeksWorthCounter;

						while (tempSpan > 0)
						{
							weeksWorth[tempCount, skipHorizontal] = (skipHorizontal + "$" + String.Format("{0:t}", firstTime) + "$" + value + "$" + span);
							firstTime = firstTime.AddMinutes(30);
							tempCount++;
							tempSpan--;
						}
						index = index + 1;
					}
					else {
						for (int d = 0; d < 5; d++)
						{
							if (weeksWorth[weeksWorthCounter, d] == null)
							{
								skipHorizontal = d;
								break;
							}
						}
						weeksWorth[weeksWorthCounter, skipHorizontal] = "nothing";
						span = 0;
						index += 1;
					}
				}
				weeksWorthCounter++;
				index = 0;
				skipHorizontal = 0;
			}
			giveDB.DBGather(weeksWorth, name);
		}
	}
}