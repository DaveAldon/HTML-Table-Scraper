using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace FacultySchedules
{
	public class allFaculty
	{
		List<string> everybodyName = new List<string>();
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
	}
}
