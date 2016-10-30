﻿using System.Collections.Generic;

namespace FacultySchedules
{
	public static class Globals
	{
		public const string ip = "127.0.0.1";
		public const string username = "test";
		public const string port = "8889";
		public const string password = "test";
		public const string database = "Faculty";

		public const string connectionParam = "server=" + ip + ";uid=" + username + ";port=" + port + ";pwd=" + password + ";database=" + database + ";";

		public static List<string> timeList = new List<string>{ 
		"7:00 AM",
		"7:30 AM",
		"8:00 AM",
		"8:30 AM",
		"9:00 AM",
		"9:30 AM",
		"10:00 AM",
		"10:30 AM",
		"11:00 AM",
		"11:30 AM",
		"12:00 PM",
		"12:30 PM",
		"1:00 PM",
		"1:30 PM",
		"2:00 PM",
		"2:30 PM",
		"3:00 PM",
		"3:30 PM",
		"4:00 PM",
		"4:30 PM",
		"5:00 PM",
		"5:30 PM",
		"6:00 PM",
		"6:30 PM",
		"7:00 PM",
		"7:30 PM",
		"8:00 PM",
		"8:30 PM",
		"9:00 PM",
		"9:30 PM",
		"10:00 PM",
		};
	}
}
