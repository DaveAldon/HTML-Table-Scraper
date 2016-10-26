using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace FacultySchedules
{
	public class GiveData
	{
		string connectionParam = "server=127.0.0.1;uid=test;port=8889;pwd=test;database=Faculty;";

		public void multipleDBGather(List<string> data, string name)
		{

		}

		public void DBGather(List<string> data, string name)
		{
			string inputDay, inputHour, inputEvent;
			bool firstTime = true;
			if (firstTime == true)
			{
				dropTable(name);
			}
			for (int i = 0; i < data.Count; i++)
			{
				string[] subStrings = data[i].Split(new string[] { "$" }, StringSplitOptions.None);
				inputDay = subStrings[0];
				inputHour = subStrings[1];
				inputEvent = subStrings[2];
				DBpush(inputDay, inputHour, inputEvent, name);
			}
			firstTime = false;
		}
		public void DBpush(string inputDay, string inputHour, string inputEvent, string name)
		{
			createTable(name);
			insertIntoTable(inputDay, inputHour, inputEvent, name);
		}

		public void createTable(string name)
		{
			MySqlConnection connectionCreate = null;
			MySqlDataReader dataReaderCreate = null;
			try
			{
				connectionCreate = new MySqlConnection(connectionParam);
				connectionCreate.Open();
				string stm = "CREATE TABLE `" + name + "` (id int(50) NOT NULL AUTO_INCREMENT, day varchar(50), hour varchar(50), event varchar(50), PRIMARY KEY (id))";
				MySqlCommand createCmd = new MySqlCommand(stm, connectionCreate);
				dataReaderCreate = createCmd.ExecuteReader();
			}
			catch (MySqlException error)
			{
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

		public void insertIntoTable(string inputDay, string inputHour, string inputEvent, string name)
		{
			MySqlConnection addConnection = null;
			MySqlDataReader addDataReader = null;
			try
			{
				addConnection = new MySqlConnection(connectionParam);
				addConnection.Open();
				string stm = "INSERT INTO `" + name + "` (day, hour, event) VALUES(@day, @hour, @event)";
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
}