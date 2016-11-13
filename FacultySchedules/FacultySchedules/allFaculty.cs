using System.Collections.Generic;
using System.Text.RegularExpressions;
using AppKit;
using HtmlAgilityPack;
using MySql.Data.MySqlClient;

namespace FacultySchedules
{
	public class allFaculty
	{
		Scrape scraper = new Scrape();
		SpecialNameFormatting dynamicNameFinder = new SpecialNameFormatting();
		string tempName;

		public void findAndInsertAllNames()
		{
			var facultyNames = scraper.ScrapeFaculty();
			var facultyNamesEven = scraper.ScrapeFacultyEven();
			List<string> allNames = new List<string>();

			foreach (HtmlNode eachName in facultyNamesEven)
			{
				facultyNames.Add(eachName);
			}

			foreach (HtmlNode eachName in facultyNames)
			{
				tempName = eachName.InnerText;
				string[] subStrings = Regex.Split(tempName, "\\n");

				for (int i = 1; i < subStrings.Length - 1; i += 3)
				{
					allNames.Add(dynamicNameFinder.getNameForTable(subStrings[i]));
				}
				foreach (string name in allNames)
				{
					Globals.uniqueFacultyNames.Add(name);
				}
			}
		}

		void errorHandle(MySqlException error)
		{
			NSAlert oAlert = new NSAlert();
			// Set the buttons
			oAlert.InvokeOnMainThread(delegate
			{
				oAlert.AddButton("Ok");
			});
			// Show the message box and capture
			oAlert.MessageText = "There's a problem with the faculty names!";
			oAlert.InformativeText = error.ToString();
			oAlert.AlertStyle = NSAlertStyle.Informational;
		}
	}
}
