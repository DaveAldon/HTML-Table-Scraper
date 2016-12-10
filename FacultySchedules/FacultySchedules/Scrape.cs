//
//This class seperates an HTML page into pieces by a given class and tag, returning a list of HTML nodes to the requester.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AppKit;
using HtmlAgilityPack;

namespace FacultySchedules
{
	public class Scrape
	{
		/// <summary>
		/// Begins the scrape. Returns an HTML Node from a URL.
		/// </summary>
		/// <returns>The scrape.</returns>
		/// <param name="name">Name.</param>
		public List<HtmlNode> BeginScrape(string name)
		{
			SpecialNameFormatting specialFormatInit = new SpecialNameFormatting();
			WebClient webClient = new WebClient();

			string page = webClient.DownloadString(specialFormatInit.splitNameGetURL(name));

			HtmlDocument doc = new HtmlDocument();
			doc.LoadHtml(page);

			try
			{
				List<HtmlNode> x = doc.GetElementbyId(Globals.tableClass).Elements(Globals.tableClassElement).ToList();
				Globals.isMissing = false;
				return x;
			}

			catch (NullReferenceException error)
			{
				errorHandle(error);
				Globals.isMissing = true;
				return null;
			}
		}

		/// <summary>
		/// Scrapes an HTML table of names. Returns an HTML Node.
		/// </summary>
		/// <returns>The faculty.</returns>
		public List<HtmlNode> ScrapeFaculty()
		{
			WebClient webClient = new WebClient();

			string page = webClient.DownloadString(Globals.dynamicNameURL);

			HtmlDocument doc = new HtmlDocument();
			doc.LoadHtml(page);

			List<HtmlNode> x = doc.GetElementbyId(Globals.dynamicNameTableClass).Elements(Globals.dynamicNameTableClassElement).ToList();
			return x;
		}

		/// <summary>
		/// Scrapes an HTML table of names in the even that there's a second column. Returns an HTML Node.
		/// </summary>
		/// <returns>The faculty even.</returns>
		public List<HtmlNode> ScrapeFacultyEven()
		{
			WebClient webClient = new WebClient();

			string page = webClient.DownloadString(Globals.dynamicNameURL);

			HtmlDocument doc = new HtmlDocument();
			doc.LoadHtml(page);

			List<HtmlNode> x = doc.GetElementbyId(Globals.dynamicNameTableClassEven).Elements(Globals.dynamicNameTableClassElement).ToList();
			return x;
		}

		/// <summary>
		/// Brings up a window with the given error.
		/// </summary>
		/// <param name="error">Error.</param>
		void errorHandle(NullReferenceException error)
		{
			NSAlert oAlert = new NSAlert();
			// Set the buttons
			oAlert.InvokeOnMainThread(delegate
			{
				oAlert.AddButton("Ok");
			});
			// Show the message box and capture
			oAlert.MessageText = "There's a problem with the website or your internet connection!";
			oAlert.InformativeText = error.ToString();
			oAlert.AlertStyle = NSAlertStyle.Informational;
		}
	}
}