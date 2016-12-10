//
//This class handles all creation of tables, deletion of tables, and insertion of data into the MySQL database.
//

using System;
using MySql.Data.MySqlClient;
using AppKit;

namespace FacultySchedules
{
	public class GiveData
	{
		string connectionParam = Globals.connectionParam;

		/// <summary>
		/// Seperates a given string of data into pieces and inserts it into the DB.
		/// </summary>
		/// <param name="weeksWorth">Weeks worth.</param>
		/// <param name="name">Name.</param>
		public void DBGather(string[,] weeksWorth, string name)
		{
			int inputDay;
			string inputHour, inputEvent;
			int inputRowSpan;
			createTable(name);

			//Seperates the string of data into pieces
			for (int h = 0; h < Globals.hourSlots; h++)
			{
				for (int d = 0; d < Globals.daySlots; d++)
				{
					if ((weeksWorth[h, d] != null) && (weeksWorth[h, d].Contains("nothing") == false)) //We want to make sure the data is in the correct format
					{
						string[] subStrings = weeksWorth[h, d].Split(new string[] { "$" }, StringSplitOptions.None);
						inputDay = int.Parse(subStrings[0]);
						inputHour = subStrings[1];
						inputEvent = subStrings[2];
						inputRowSpan = Convert.ToInt16(subStrings[3]);

						insertIntoTable(Globals.dayList[inputDay], inputHour, inputEvent, inputRowSpan, name); //Insert the data into the database
						Globals.uniqueClassInput.Add(inputEvent); //Place each unique event name into a global list
					}
					else continue;
				}
			}
		}

		/// <summary>
		/// Creates a DB table with the name provided.
		/// </summary>
		/// <param name="name">Name.</param>
		public void createTable(string name)
		{
			MySqlConnection connectionCreate = null;
			MySqlDataReader dataReaderCreate = null;
			try
			{
				connectionCreate = new MySqlConnection(connectionParam);
				connectionCreate.Open();
				string stm = "CREATE TABLE `" + name + "` (id int(50) NOT NULL AUTO_INCREMENT, " + Globals.DBFieldDay + " varchar(50), " + Globals.DBFieldHour + " varchar(50), " + Globals.DBFieldEvent + " varchar(50), " + Globals.DBFieldRowspan + " int (10), PRIMARY KEY (id))";
				MySqlCommand createCmd = new MySqlCommand(stm, connectionCreate);
				dataReaderCreate = createCmd.ExecuteReader();
			}

			catch (MySqlException error)
			{
				errorHandle(error);
			}

			finally //We need to close all of our connections once everything is retrieved
			{
				if (dataReaderCreate != null)
				{
					dataReaderCreate.Close();
				}

				if (connectionCreate != null)
				{
					connectionCreate.Close();
				}
			}
		}

		/// <summary>
		/// Inserts all passed data the into the given table.
		/// </summary>
		/// <param name="inputDay">Input day.</param>
		/// <param name="inputHour">Input hour.</param>
		/// <param name="inputEvent">Input event.</param>
		/// <param name="inputRowSpan">Input row span.</param>
		/// <param name="name">Name.</param>
		public void insertIntoTable(string inputDay, string inputHour, string inputEvent, int inputRowSpan, string name)
		{
			MySqlConnection addConnection = null;
			MySqlDataReader addDataReader = null;
			try
			{
				addConnection = new MySqlConnection(connectionParam);
				addConnection.Open();
				string stm = "INSERT INTO `" + name + "` (" + Globals.DBFieldDay + ", " + Globals.DBFieldHour + ", " + Globals.DBFieldEvent + ", " + Globals.DBFieldRowspan + ") VALUES(@day, @hour, @event, @rowspan)";
				MySqlCommand cmd = new MySqlCommand(stm, addConnection);

				//Just a different method of customizing parameters! Makes changing things easier.
				cmd.Parameters.AddWithValue("@day", inputDay);
				cmd.Parameters.AddWithValue("@hour", inputHour);
				cmd.Parameters.AddWithValue("@event", inputEvent);
				cmd.Parameters.AddWithValue("@rowspan", inputRowSpan);
				addDataReader = cmd.ExecuteReader();
			}

			catch (MySqlException error)
			{
				errorHandle(error);
			}

			finally
			{
				if (addDataReader != null)
				{
					addDataReader.Close();
				}

				if (addConnection != null)
				{
					addConnection.Close();
				}
			}
		}

		/// <summary>
		/// Drops the given table.
		/// </summary>
		/// <param name="name">Name.</param>
		public void dropTable(string name)
		{
			MySqlConnection connection = null;
			MySqlDataReader dataReader = null;
			try
			{
				connection = new MySqlConnection(connectionParam);
				connection.Open();
				string replaceStm = "DROP TABLE IF EXISTS `" + name + "`";
				MySqlCommand replaceCmd = new MySqlCommand(replaceStm, connection);
				dataReader = replaceCmd.ExecuteReader();
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