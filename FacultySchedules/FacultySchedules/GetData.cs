using MySql.Data.MySqlClient;
using AppKit;
using System.Collections.Generic;
using System;

namespace FacultySchedules
{
	public class GetData
	{
		//allFaculty allFacultyInit = new allFaculty();
		string connectionParam = Globals.connectionParam;
		public string whoIsFreeAtXFromList(string time, string facultyTextNames)
		{
			string[] facultyNames = facultyTextNames.Split(new string[] { "\n" }, StringSplitOptions.None);
			int facultyCount = facultyNames.Length;
			int existanceResult = 0;
			List<string> resultList = new List<string>();

			foreach (string eachDay in Globals.dayList)
			{
				resultList.Add("\n" + "~ " + eachDay + " ~" + "\n");
				for (int i = 0; i < facultyCount; i++)
				{
					MySqlConnection connection = null;
					MySqlDataReader dataReader = null;

					try
					{
						connection = new MySqlConnection(connectionParam);
						connection.Open();
						string stm = "SELECT 1 FROM `" + facultyNames[i] + "` WHERE day = '" + eachDay + "' AND hour = '" + time + "'" + "LIMIT 1";
						MySqlCommand replaceCmd = new MySqlCommand(stm, connection);
						dataReader = replaceCmd.ExecuteReader();
						existanceResult = 0;

						while (dataReader.Read())
						{
							existanceResult = int.Parse(dataReader.GetString(0));
						}

						if (existanceResult == 0)
						{
							resultList.Add(facultyNames[i] + "\n");
						}
					}

					catch (MySqlException error)
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
				}
			}

			string finalResult = "";

			foreach (string eachResult in resultList)
			{
				finalResult += eachResult + "\n";
			}
		return finalResult;
		}

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
						string stm = "SELECT 1 FROM `" + everyName[i] + "` WHERE day = '" + eachDay + "' AND hour = '" + time + "'" + "LIMIT 1";
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

		public string whenIsEveryoneAvailable()
		{
			string finalResult = "";
			List<string> resultList = new List<string>();
			List<string> everyName = new List<string>();
			List<string> timeList = new List<string>();

			everyName.AddRange(Globals.uniqueFacultyNames);

			int existanceResult;
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
							string stm = "SELECT 1 FROM `" + Globals.uniqueFacultyNames[i] + "` WHERE day = '" + eachDay + "' AND hour = '" + time + "'" + "LIMIT 1";
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
					string stm = "SELECT 1 FROM `" + eachName + "` WHERE event = '" + className + "'" + "LIMIT 1";
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
					string stm = "SELECT 1 FROM `" + eachName + "` WHERE event = '" + className + "'" + "LIMIT 1";
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

		public string whenDoesXHaveY(string name, string className)
		{
			MySqlConnection connection = null;
			MySqlDataReader dataReader = null;

			try
			{
				connection = new MySqlConnection(connectionParam);
				connection.Open();
				int count = dataReader.FieldCount;
				string stm = "SELECT day, hour FROM `" + name + "` " + "WHERE event = '" + className + "'";
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

				while (dataReader.Read())
				{
					for (int i = 0; i < count-1; i++)
					{
						retrievedDay = dataReader.GetString(i);
						retrievedHour = dataReader.GetString(i + 1);

						if (retrievedDay.Contains("Monday")) {
							monday.Add(retrievedHour);
						} else
						if (retrievedDay.Contains("Tuesday")) {
							tuesday.Add(retrievedHour);
						}
						else
						if (retrievedDay.Contains("Wednesday")) {
							wednesday.Add(retrievedHour);
						}
						else
						if (retrievedDay.Contains("Thursday")) {
							thursday.Add(retrievedHour);	
						}
						else
						if (retrievedDay.Contains("Friday")) {
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