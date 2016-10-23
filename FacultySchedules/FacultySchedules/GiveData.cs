using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace FacultySchedules
{
	public class GiveData
	{
		public string name;
		public void DBGather(List<string> data)
		{
			string inputDay, inputHour, inputEvent;

			for (int i = 0; i < data.Count; i++)
			{
				string[] subStrings = data[i].Split(new string[] { "$" }, StringSplitOptions.None);
				inputDay = subStrings[0];
				inputHour = subStrings[1];
				inputEvent = subStrings[2];
				DBpush(inputDay, inputHour, inputEvent);
			}
		}
		//public void DBpush(int inputDay, int inputHour, int inputEvent)
		public void DBpush(string inputDay, string inputHour, string inputEvent)
		{
			string connectionParam = "server=192.168.1.24;uid=test;port=8889;pwd=test;database=Faculty;";
			MySqlConnection connection = null;
			MySqlDataReader dataReader = null;

			try
			{
				connection = new MySqlConnection(connectionParam);
				connection.Open();
				//string stm = "INSERT INTO trax (Entry, Name, PO, IDC) VALUES(@Entry, @Name, @PO, @IDC)";
				string stm = "INSERT INTO IraWoodring (day, hour, event) VALUES(@day, @hour, @event)";

				MySqlCommand cmd = new MySqlCommand(stm, connection);
				cmd.Parameters.AddWithValue("@day", inputDay);
				cmd.Parameters.AddWithValue("@hour", inputHour);
				cmd.Parameters.AddWithValue("@event", inputEvent);
				dataReader = cmd.ExecuteReader();
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