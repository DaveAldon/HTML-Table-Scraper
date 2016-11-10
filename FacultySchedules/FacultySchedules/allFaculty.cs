using System;
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
		GiveData giveDB = new GiveData();
		SpecialNameFormatting dynamicNameFinder = new SpecialNameFormatting();
		List<string> everybodyName = new List<string>();
		string tempName;
		public List<string> getEveryonesName()
		{
			everybodyName.Clear();
			string connectionParam = Globals.connectionParam;
			MySqlConnection connection = null;
			MySqlDataReader dataReader = null;
			try
			{
				connection = new MySqlConnection(connectionParam);
				connection.Open();
				string stm = "SELECT Name FROM Names"; //String using query syntax
				MySqlCommand cmd = new MySqlCommand(stm, connection); //Query is placed alongside our MySQLConnection object to create a command object
				dataReader = cmd.ExecuteReader(); //Our data reader which was null is given our new command

				int count = dataReader.FieldCount; //This keeps track of how many items are in the query

				while (dataReader.Read())
				{
					for (int i = 0; i < count; i++)
					{
						everybodyName.Add(dataReader.GetString(i));
					}
				}
			}

			catch (MySqlException error) //If at any point there's a connection or query error, we want to know what exactly is going on
			{
				errorHandle(error);
			}

			finally //We need to close all of our connections once everything is retrieved
			{
				if (dataReader != null)
				{
					dataReader.Close();
				}

				if (connection != null)
				{
					connection.Close();
				}
			}
			return everybodyName;
		}

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
			oAlert.MessageText = "There's a problem with the query!";
			oAlert.InformativeText = error.ToString();
			oAlert.AlertStyle = NSAlertStyle.Informational;
		}
	}
}
