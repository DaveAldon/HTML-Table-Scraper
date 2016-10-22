using MySql.Data.MySqlClient;
using System.Data.Common;

namespace FacultySchedulesUserClient
{
	public class GiveData
	{

		public void DBpush()
		{
			string connectionParam = "server=127.0.0.1;uid=root;port=8889;pwd=root;database=Faculty;";
			MySqlConnection connection = null;
			MySqlDataReader dataReader = null;

			try
			{
				connection = new MySqlConnection(connectionParam);
				connection.Open();
				//string stm = "INSERT INTO trax (Entry, Name, PO, IDC) VALUES(@Entry, @Name, @PO, @IDC)";
				string stm = "INSERT INTO IraWoodring (day, hour, event) VALUES(@day, @hour, @event)";

				MySqlCommand cmd = new MySqlCommand(stm, connection);
				cmd.Parameters.AddWithValue("@day", DSSTstring);
				cmd.Parameters.AddWithValue("@hour", POstring);
				cmd.Parameters.AddWithValue("@event", IDCstring);
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
