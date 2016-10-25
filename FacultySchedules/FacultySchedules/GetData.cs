using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace FacultySchedules
{
	public class GetData
	{
		bool firstTime = true;

		public void multipleDBGather(List<string> data, string name)
		{

		}

		public void DBGather(List<string> data, string name)
		{
			string inputDay, inputHour, inputEvent;

			for (int i = 0; i < data.Count; i++)
			{
				string[] subStrings = data[i].Split(new string[] { "$" }, StringSplitOptions.None);
				inputDay = subStrings[0];
				inputHour = subStrings[1];
				inputEvent = subStrings[2];
				DBpush(inputDay, inputHour, inputEvent, name);
			}
		}
		//public void DBpush(int inputDay, int inputHour, int inputEvent)
		public void DBpush(string inputDay, string inputHour, string inputEvent, string name)
		{
			string connectionParam = "server=127.0.0.1;uid=test;port=8889;pwd=test;database=Faculty;";

			MySqlConnection addConnection = null;
			MySqlDataReader addDataReader = null;

			if (firstTime == true)
			{
				MySqlConnection connection = null;
				MySqlDataReader dataReader = null;
				try
				{
					connection = new MySqlConnection(connectionParam);
					connection.Open();
					string replaceStm = "truncate " + name;
					MySqlCommand replaceCmd = new MySqlCommand(replaceStm, connection);
					dataReader = replaceCmd.ExecuteReader();
				}
				catch (MySqlException error)
				{
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
				firstTime = false;
			}

			try
			{
				addConnection = new MySqlConnection(connectionParam);
				addConnection.Open();
				string stm = "INSERT INTO " + name + " (day, hour, event) VALUES(@day, @hour, @event)";
				MySqlCommand cmd = new MySqlCommand(stm, addConnection);
				cmd.Parameters.AddWithValue("@day", inputDay);
				cmd.Parameters.AddWithValue("@hour", inputHour);
				cmd.Parameters.AddWithValue("@event", inputEvent);
				addDataReader = cmd.ExecuteReader();
			}
			catch (MySqlException error)
			{
			}
			finally //We need to close all of our connections once everything is retrieved
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
	}
}