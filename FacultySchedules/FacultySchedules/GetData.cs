﻿//
//This class handles the sending of queries to the MySQL database, and returning their formated results to the requester.
//

using MySql.Data.MySqlClient;
using AppKit;
using System.Collections.Generic;
using System;

namespace FacultySchedules
{
	public class GetData
	{
		string connectionParam = Globals.connectionParam;
		/// <summary>
		/// Who is free from list query.
		/// </summary>
		/// <returns>Who is free from list.</returns>
		/// <param name="facultyTextNames">Faculty text names.</param>
		public string whoIsFreeFromList(string facultyTextNames)
		{
			string[] facultyNames = facultyTextNames.Split(new string[] { "\n" }, StringSplitOptions.None);
			int facultyCount = facultyNames.Length - 1;
			int existanceResult = 0;
			List<string> resultList = new List<string>();
			List<string> timeList = new List<string>();

			foreach (string eachDay in Globals.dayList)
			{
				resultList.Add("\n" + "~ " + eachDay + " ~" + "\n");
				foreach (string time in Globals.timeList)
				{
					for (int i = 0; i < facultyCount; i++)
					{
						MySqlConnection connection = null;
						MySqlDataReader dataReader = null;

						try
						{
							connection = new MySqlConnection(connectionParam);
							connection.Open();
							string stm = "SELECT 1 FROM `" + facultyNames[i] + "` WHERE " + Globals.DBFieldDay + " = '" + eachDay + "' AND " + Globals.DBFieldHour + " = '" + time + "'" + "LIMIT 1";
							MySqlCommand replaceCmd = new MySqlCommand(stm, connection);
							dataReader = replaceCmd.ExecuteReader();
							existanceResult = 0;

							while (dataReader.Read())
							{
								existanceResult = int.Parse(dataReader.GetString(0));
							}

							if (existanceResult == 0)
							{
								timeList.Add(time + "\n");
							}
						}

						catch (MySqlException error)
						{
							errorHandle(error);
						}

						finally
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
					}
					if (timeList.Count == facultyCount)
					{
						resultList.Add(time + "\n");
					}
					timeList.Clear();
				}
			}

			string finalResult = "";

			foreach (string eachResult in resultList)
			{
				finalResult += eachResult;
			}
			return finalResult;
		}

		/// <summary>
		/// Who is free at x.
		/// </summary>
		/// <returns>The free at x.</returns>
		/// <param name="time">Time.</param>
		public string whoIsFreeAtX(string time)
		{
			int existanceResult;
			string finalResult = "";
			List<string> resultList = new List<string>();
			List<string> everyName = new List<string>();
			everyName.AddRange(Globals.uniqueFacultyNames);
			int nameCount = everyName.Count;

			foreach (string eachDay in Globals.dayList)
			{
				resultList.Add("\n" + "~ " + eachDay + " ~" + "\n");
				for (int i = 0; i < nameCount; i++) {
					MySqlConnection connection = null;
					MySqlDataReader dataReader = null;

					try
					{
						connection = new MySqlConnection(connectionParam);
						connection.Open();
						string stm = "SELECT 1 FROM `" + everyName[i] + "` WHERE " + Globals.DBFieldDay + " = '" + eachDay + "' AND " + Globals.DBFieldHour + " = '" + time + "'" + "LIMIT 1";
						MySqlCommand replaceCmd = new MySqlCommand(stm, connection);
						dataReader = replaceCmd.ExecuteReader();
						existanceResult = 0;

						while (dataReader.Read())
						{
							existanceResult = int.Parse(dataReader.GetString(0));
						}

						if (existanceResult == 0)
						{
							resultList.Add(everyName[i] + "\n");
						}
					}

					catch (MySqlException error)
					{
						errorHandle(error);
					}

					finally
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
				}
			}

			foreach (string results in resultList)
			{
				finalResult += results;
			}

			everyName.Clear();
			return finalResult;
		}

		/// <summary>
		/// When is everyone available.
		/// </summary>
		/// <returns>The time everyone is available.</returns>
		public string whenIsEveryoneAvailable()
		{
			string finalResult = "";
			List<string> resultList = new List<string>();
			List<string> everyName = new List<string>();
			List<string> timeList = new List<string>();

			int existanceResult;

			foreach (string uniqueName in Globals.uniqueFacultyNames)
			{
				if (doesTableExist(uniqueName) == 1)
				{
					everyName.Add(uniqueName);
				}
			}

			int nameCount = everyName.Count;

			foreach (string eachDay in Globals.dayList)
			{
				resultList.Add("\n" + "~ " + eachDay + " ~" + "\n");
				foreach (string time in Globals.timeList)
				{
					for (int i = 0; i < nameCount; i++)
					{
						MySqlConnection connection = null;
						MySqlDataReader dataReader = null;

						try
						{
							connection = new MySqlConnection(connectionParam);
							connection.Open();
							string stm = "SELECT 1 FROM `" + everyName[i] + "` WHERE " + Globals.DBFieldDay + " = '" + eachDay + "' AND " + Globals.DBFieldHour + " = '" + time + "'" + "LIMIT 1";
							MySqlCommand replaceCmd = new MySqlCommand(stm, connection);
							dataReader = replaceCmd.ExecuteReader();
							existanceResult = 0;

							while (dataReader.Read())
							{
								existanceResult = int.Parse(dataReader.GetString(0));
							}

							if (existanceResult == 0)
							{
								timeList.Add(time + "\n");
							}
						}

						catch (MySqlException error)
						{
							errorHandle(error);
						}

						finally
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
					}
					if (timeList.Count == nameCount)
					{
						resultList.Add(time + "\n");
					}
					timeList.Clear();
				}
			}

			foreach (string results in resultList)
			{
				finalResult += results;
			}
			everyName.Clear();
			return finalResult;
		}

		/// <summary>
		/// Doeses the table exist.
		/// </summary>
		/// <returns>Whether the table exists in the form of an int.</returns>
		/// <param name="name">Name.</param>
		public int doesTableExist(string name)
		{
			int existanceResult = 0;
			MySqlConnection connection = null;
			MySqlDataReader dataReader = null;

			try
			{
				connection = new MySqlConnection(connectionParam);
				connection.Open();
				string stm = "SELECT 1 FROM `" + name + "`";
				MySqlCommand replaceCmd = new MySqlCommand(stm, connection);
				dataReader = replaceCmd.ExecuteReader();
				int count = dataReader.FieldCount;
				existanceResult = 0;

				while (dataReader.Read())
				{
					existanceResult = int.Parse(dataReader.GetString(0));
				}
			}

			catch (MySqlException error)
			{
				errorHandle(error);
			}

			finally
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
			return existanceResult;
		}

		/// <summary>
		/// Internal who teaches x. Used by "When is Everyone..." queries.
		/// </summary>
		/// <returns>Who teaches x.</returns>
		/// <param name="className">Class name.</param>
		public List<string> internalWhoTeachesX(string className)
		{
			int existanceResult;
			List<string> finalResult = new List<string>();

			foreach (string eachName in Globals.uniqueFacultyNames)
			{
				MySqlConnection connection = null;
				MySqlDataReader dataReader = null;

				try
				{
					connection = new MySqlConnection(connectionParam);
					connection.Open();
					string stm = "SELECT 1 FROM `" + eachName + "` WHERE " + Globals.DBFieldEvent + " = '" + className + "'" + "LIMIT 1";
					MySqlCommand replaceCmd = new MySqlCommand(stm, connection);
					dataReader = replaceCmd.ExecuteReader();
					int count = dataReader.FieldCount;
					existanceResult = 0;

					while (dataReader.Read())
					{
						existanceResult = int.Parse(dataReader.GetString(0));
					}

					if (existanceResult == 1)
					{
						finalResult.Add(eachName);
					}
				}

				catch (MySqlException error)
				{
					errorHandle(error);
				}

				finally
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
			}
			return finalResult;
		}

		/// <summary>
		/// Who teaches x.
		/// </summary>
		/// <returns>Who teaches x.</returns>
		/// <param name="className">Class name.</param>
		public string whoTeachesX(string className)
		{
			int existanceResult;
			string finalResult = "";

			foreach (string eachName in Globals.uniqueFacultyNames) {
				MySqlConnection connection = null;
				MySqlDataReader dataReader = null;

				try
				{
					connection = new MySqlConnection(connectionParam);
					connection.Open();
					string stm = "SELECT 1 FROM `" + eachName + "` WHERE " + Globals.DBFieldEvent + " = '" + className + "'" + "LIMIT 1";
					MySqlCommand replaceCmd = new MySqlCommand(stm, connection);
					dataReader = replaceCmd.ExecuteReader();
					int count = dataReader.FieldCount;
					existanceResult = 0;

					while (dataReader.Read())
					{
						existanceResult = int.Parse(dataReader.GetString(0));
					}

					if (existanceResult == 1)
					{
						finalResult += eachName + "\n";
					}
				}

				catch (MySqlException error)
				{
					errorHandle(error);
				}

				finally
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
			}
			return finalResult;
		}

		/// <summary>
		/// When does X have y.
		/// </summary>
		/// <returns>A large string of when X has y.</returns>
		/// <param name="name">Name.</param>
		/// <param name="className">Class name.</param>
		public string whenDoesXHaveY(string name, string className)
		{
			MySqlConnection connection = null;
			MySqlDataReader dataReader = null;

			try
			{
				connection = new MySqlConnection(connectionParam);
				connection.Open();

				string stm = "SELECT " + Globals.DBFieldDay + ", " + Globals.DBFieldHour + " FROM `" + name + "` " + "WHERE " + Globals.DBFieldEvent + " = '" + className + "'";
				string retrievedDay = "";
				string retrievedHour = "";
				string result = "";
				List<string> monday = new List<string>();
				List<string> tuesday = new List<string>();
				List<string> wednesday = new List<string>();
				List<string> thursday = new List<string>();
				List<string> friday = new List<string>();
				MySqlCommand replaceCmd = new MySqlCommand(stm, connection);
				dataReader = replaceCmd.ExecuteReader();
				int count = dataReader.FieldCount;

				while (dataReader.Read())
				{
					for (int i = 0; i < count-1; i++)
					{
						retrievedDay = dataReader.GetString(i);
						retrievedHour = dataReader.GetString(i + 1);

						if (retrievedDay.Contains(Globals.dayList[0])) {
							monday.Add(retrievedHour);
						} else
						if (retrievedDay.Contains(Globals.dayList[1])) {
							tuesday.Add(retrievedHour);
						}
						else
						if (retrievedDay.Contains(Globals.dayList[2])) {
							wednesday.Add(retrievedHour);
						}
						else
						if (retrievedDay.Contains(Globals.dayList[3])) {
							thursday.Add(retrievedHour);	
						}
						else
						if (retrievedDay.Contains(Globals.dayList[4])) {
							friday.Add(retrievedHour);
						}
					}
				}
				result += "~ Monday ~\n";
				foreach (string hour in monday)
				{
					result += hour + "\n";
				}
				result += "\n~ Tuesday ~\n";
				foreach (string hour in tuesday)
				{
					result += hour + "\n";
				}
				result += "\n~ Wednesday ~\n";
				foreach (string hour in wednesday)
				{
					result += hour + "\n";
				}
				result += "\n~ Thursday ~\n";
				foreach (string hour in thursday)
				{
					result += hour + "\n";
				}
				result += "\n~ Friday ~\n";
				foreach (string hour in friday)
				{
					result += hour + "\n";
				}
				return result;
			}

			catch (MySqlException error)
			{
				errorHandle(error);
				return null; //Return null in order to force a return on this code path
			}

			finally
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
		}

		/// <summary>
		/// Brings up a window with the given error.
		/// </summary>
		/// <param name="error">Error.</param>
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