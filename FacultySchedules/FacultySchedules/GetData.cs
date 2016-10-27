using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace FacultySchedules
{
	public class GetData
	{
		public string whenDoesXHaveY(string name, string className)
		{
			string connectionParam = Globals.connectionParam;
			MySqlConnection connection = null;
			MySqlDataReader dataReader = null;

			try
			{
				connection = new MySqlConnection(connectionParam);
				connection.Open();
				string stm = "SELECT day, hour FROM `" + name + "` " + "WHERE event = 'Office Hour'";
				MySqlCommand replaceCmd = new MySqlCommand(stm, connection);
				dataReader = replaceCmd.ExecuteReader();

				int count = dataReader.FieldCount; //This keeps track of how many items are in the query
				string retrievedDay = "";
				string retrievedHour = "";
				string result = "";

				while (dataReader.Read())
				{
					for (int i = 0; i < count-1; i++) //Increment by four in order to seperate all of the values from Entry
					{
						retrievedDay = dataReader.GetString(i);
						retrievedHour = dataReader.GetString(i + 1);
						result = result + retrievedDay + " " + retrievedHour + "\n";
					}
				}
				//name + " is available at the following: \n"
				return result;
			}
			catch (MySqlException error)
			{
				return error.ToString();
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