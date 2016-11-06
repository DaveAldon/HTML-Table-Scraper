using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace FacultySchedules
{
	public class GiveData
	{
		string connectionParam = Globals.connectionParam;
		bool firstTime = true;
		bool skip = false;
		int rowSkip = 0;

		public void DBGather(List<string> data, string name, List<string> classes)
		{
			int inputDay;
			string inputHour, inputEvent;
			int inputRowSpan;

			if (firstTime == true)
			{
				//dropTable(name);
				dropTable("Classes");
				createClassesTable();
				firstTime = false;
			}

			createTable(name);

			for (int i = 0; i < data.Count; i++)
			{
				if (skip)
				{
					skip = false;
					if (i + rowSkip + 1 >= data.Count) { }
					else {
						i += rowSkip + 1;
					/*
							string[] subStringsSkip = data[i - 2].Split(new string[] { "$" }, StringSplitOptions.None); //Seperates the string of data into three pieces
							inputDay = int.Parse(subStringsSkip[0]) + 1;
							inputHour = subStringsSkip[1];
							inputEvent = subStringsSkip[2];
							inputRowSpan = Convert.ToInt16(subStringsSkip[3]);
							fixOverlapThenPush(inputDay, inputHour, inputEvent, inputRowSpan, name);

						string[] subStringsSkipp = data[i - 1].Split(new string[] { "$" }, StringSplitOptions.None); //Seperates the string of data into three pieces
						//inputDay = int.Parse(subStringsSkipp[0]) + 1;
						inputHour = subStringsSkipp[1];
						inputEvent = subStringsSkipp[2];
						inputRowSpan = Convert.ToInt16(subStringsSkipp[3]);
						fixOverlapThenPush(inputDay, inputHour, inputEvent, inputRowSpan, name);//Sends the data out into the final filter
						*/
					}
				}
				string[] subStrings = data[i].Split(new string[] { "$" }, StringSplitOptions.None); //Seperates the string of data into three pieces
				inputDay = int.Parse(subStrings[0]);
				inputHour = subStrings[1];
				inputEvent = subStrings[2];
				inputRowSpan = Convert.ToInt16(subStrings[3]);
				fixOverlapThenPush(inputDay, inputHour, inputEvent, inputRowSpan, name); //Sends the data out into the final filter
			}

			foreach (string className in classes) 
			{
				insertIntoClasses(className);
			}
		}

		//Filter that fixes the overlap that happens with the cells that have rowspans higher than 2, then pushes it to the database
		public void fixOverlapThenPush(int inputDay, string inputHour, string inputEvent, int inputRowSpan, string name)
		{
			if (inputDay == 4)
			{
				insertIntoTable(Globals.dayList[inputDay], inputHour, inputEvent, inputRowSpan, name);
			}
			else {
				if (checkOverlap(name, Globals.dayList[inputDay], inputHour) == 1)
				{
					insertIntoTable(Globals.dayList[inputDay + 1], inputHour, inputEvent, inputRowSpan, name);
					for (int i = inputRowSpan - 1; i > 0; i--)
					{
						insertIntoTable(Globals.dayList[inputDay + 1], String.Format("{0:t}", DateTime.Parse(inputHour).AddMinutes(30 * i)), inputEvent, inputRowSpan, name);
					}
					skip = true;
					rowSkip = inputRowSpan;
				}
				else {
					insertIntoTable(Globals.dayList[inputDay], inputHour, inputEvent, inputRowSpan, name);
				}
			}
		}

		public void justPush(int inputDay, string inputHour, string inputEvent, int inputRowSpan, string name)
		{
			insertIntoTable(Globals.dayList[inputDay], inputHour, inputEvent, inputRowSpan, name);
		}


		public int checkOverlap(string name, string day, string time)
		{
			int existanceResult = 0;
			MySqlConnection connection = null;
			MySqlDataReader dataReader = null;

			try
			{
				connection = new MySqlConnection(connectionParam);
				connection.Open();
				string stm = "SELECT 1 FROM `" + name + "` WHERE day = '" + day + "' AND hour = '" + time + "'" + "AND rowspan > '2'"+ "LIMIT 1";
				MySqlCommand replaceCmd = new MySqlCommand(stm, connection);
				dataReader = replaceCmd.ExecuteReader();

				while (dataReader.Read())
				{
					existanceResult = int.Parse(dataReader.GetString(0));
				}
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
			return existanceResult;
		}

		public void createClassesTable()
		{
			MySqlConnection connectionCreate = null;
			MySqlDataReader dataReaderCreate = null;
			try
			{
				connectionCreate = new MySqlConnection(connectionParam);
				connectionCreate.Open();
				string stm = "CREATE TABLE `Classes` (id int(50) NOT NULL AUTO_INCREMENT, Name varchar(50), PRIMARY KEY (id))";
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

		public void createTable(string name)
		{
			MySqlConnection connectionCreate = null;
			MySqlDataReader dataReaderCreate = null;
			try
			{
				connectionCreate = new MySqlConnection(connectionParam);
				connectionCreate.Open();
				string stm = "CREATE TABLE `" + name + "` (id int(50) NOT NULL AUTO_INCREMENT, day varchar(50), hour varchar(50), event varchar(50), rowspan int (10), PRIMARY KEY (id))";
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

		public void insertIntoTable(string inputDay, string inputHour, string inputEvent, int inputRowSpan, string name)
		{
			MySqlConnection addConnection = null;
			MySqlDataReader addDataReader = null;
			try
			{
				addConnection = new MySqlConnection(connectionParam);
				addConnection.Open();
				string stm = "INSERT INTO `" + name + "` (day, hour, event, rowspan) VALUES(@day, @hour, @event, @rowspan)";
				MySqlCommand cmd = new MySqlCommand(stm, addConnection);
				cmd.Parameters.AddWithValue("@day", inputDay);
				cmd.Parameters.AddWithValue("@hour", inputHour);
				cmd.Parameters.AddWithValue("@event", inputEvent);
				cmd.Parameters.AddWithValue("@rowspan", inputRowSpan);
				addDataReader = cmd.ExecuteReader();
			}

			catch (MySqlException error)
			{
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

		public void insertIntoClasses(string className)
		{
			MySqlConnection addConnection = null;
			MySqlDataReader addDataReader = null;
			try
			{
				addConnection = new MySqlConnection(connectionParam);
				addConnection.Open();
				string stm = "INSERT INTO Classes (name) VALUES(@name)";
				MySqlCommand cmd = new MySqlCommand(stm, addConnection);
				cmd.Parameters.AddWithValue("@name", className);
				addDataReader = cmd.ExecuteReader();
			}

			catch (MySqlException error)
			{
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
}