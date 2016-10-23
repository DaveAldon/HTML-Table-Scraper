using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace FacultyApp
{
	public class GiveData
	{
        public string name;
        public bool DBGather(List<string>data)
        {
            bool didSucceed = false;
            List<string> threeData = new List<string>();
            char delimiter = '$';
            int inputDay, inputHour, inputEvent;
            for (int i = 0; i > data.Count; i++)
            {
                String[] subStrings = data[i].Split(delimiter);
                inputDay = Convert.ToInt16(subStrings[0]);
                inputHour = Convert.ToInt16(subStrings[1]);
                inputEvent = Convert.ToInt16(subStrings[2]);
                didSucceed = DBpush(inputDay, inputHour, inputEvent);
            }
            return didSucceed;
        }

		public bool DBpush(int inputDay, int inputHour, int inputEvent)
		{
            bool success = false;
			string connectionParam = "server=127.0.0.1;uid=test;port=8889;pwd=test;database=Faculty;";
			MySqlConnection connection = null;
			MySqlDataReader dataReader = null;

			try
			{
				connection = new MySqlConnection(connectionParam);
				connection.Open();
				//string stm = "INSERT INTO trax (Entry, Name, PO, IDC) VALUES(@Entry, @Name, @PO, @IDC)";
				string stm = "INSERT INTO " + name + " (day, hour, event) VALUES(@day, @hour, @event)";

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
                success = true;
			}
            return success;
		}
	}
}
