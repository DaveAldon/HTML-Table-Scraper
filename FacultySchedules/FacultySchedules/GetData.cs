using MySql.Data.MySqlClient;
using AppKit;

namespace FacultySchedules
{
	public class GetData
	{
		public string whoTeachesX(string className)
		{
			allFaculty allFacultyInit = new allFaculty();
			int existanceResult;
			string finalResult = "";

			foreach (string eachName in allFacultyInit.getEveryonesName()) {
				string connectionParam = Globals.connectionParam;
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
						finalResult = finalResult + eachName + "\n";
					}
				}

				catch (MySqlException error)
				{
					errorHandle(error);
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
			return finalResult;
		}

		public string whenDoesXHaveY(string name, string className)
		{
			string connectionParam = Globals.connectionParam;
			MySqlConnection connection = null;
			MySqlDataReader dataReader = null;

			try
			{
				connection = new MySqlConnection(connectionParam);
				connection.Open();
				string stm = "SELECT day, hour FROM `" + name + "` " + "WHERE event = '" + className + "'";
				MySqlCommand replaceCmd = new MySqlCommand(stm, connection);
				dataReader = replaceCmd.ExecuteReader();

				int count = dataReader.FieldCount;
				string retrievedDay = "";
				string retrievedHour = "";
				string result = "";

				while (dataReader.Read())
				{
					for (int i = 0; i < count-1; i++)
					{
						retrievedDay = dataReader.GetString(i);
						retrievedHour = dataReader.GetString(i + 1);
						result = result + retrievedDay + " " + retrievedHour + "\n";
					}
				}
				return result;
			}

			catch (MySqlException error)
			{
				errorHandle(error);
				return null;
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
			oAlert.MessageText = "Oops!";
			oAlert.InformativeText = error.ToString();
			oAlert.AlertStyle = NSAlertStyle.Informational;
		}
	}
}