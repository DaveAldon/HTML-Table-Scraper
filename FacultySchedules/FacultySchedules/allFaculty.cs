//
//This class finds the names of all faculty and stores them in the Globals class so that any class can access it for future use.
//

using System.Collections.Generic;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace FacultySchedules
{
	public class allFaculty
	{
		Scrape scraper = new Scrape();
		SpecialNameFormatting dynamicNameFinder = new SpecialNameFormatting();
		string tempName;

		/// <summary>
		/// Finds and inserts all names into the Global class to be accessed by everything.
		/// </summary>
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

				for (int i = 1; i < subStrings.Length - 1; i += 3) //We skip 3 in order to skip over the faculty's title
				{
					allNames.Add(dynamicNameFinder.getNameForTable(subStrings[i]));
				}
				foreach (string name in allNames)
				{
					Globals.uniqueFacultyNames.Add(name); //Add their name to the HashSet for any class to access
				}
			}
		}
	}
}
