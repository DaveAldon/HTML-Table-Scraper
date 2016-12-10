//
// This class contains easy-to-change settings in case small elements of your HTML table change with
// regards to layout style, URL, or day/time changes.
// Additionally, all database connection parameters can be changed from here if you change the credentials of your MySQL setup.
//

using System.Collections.Generic;

namespace FacultySchedules
{
	public static class Globals
	{
		//Make sure that these values match your current database configuration
		public static string ip = "YOUR_IP_ADDRESS";
		public static string username = "YOUR_DB_USERNAME";
		public static string port = "YOUR_PORT";
		public static string password = "YOUR_DB_PASSWORD";
		public static string database = "YOUR_DB_NAME";
		public static string extraSecurityParameters = ""; //Add any extra security settings here, with each followed by a semi-colon ';'

		public static int hourSlots = 32; //Amount of hour vertical slots
		public static int daySlots = 5; //Amount of day horizontal slots
		public static int minEventPeriod = 2; //The minimum rowspan for events
		public static int maxCharLength = 7; //The maximum char length for events we want stored and visible. The larger, the more cluttered
		public static int cellIncrement = 30; //The amount of minutes between each cell

		//The values of these variables are examples for a HTML table you may want to scan through
		public static string tableClass = "ctl0_Main_tblSchedule"; //Class name of table to scan
		public static string tableClassElement = "tr"; //Tag seperator

		//Name of your database fields. These are used by the queries
		public static string DBFieldEvent = "event";
		public static string DBFieldDay = "day";
		public static string DBFieldHour = "hour";
		public static string DBFieldRowspan = "rowspan";

		//Optionals to check for at the top of a table
		public static string AnomalyToCheck = "Sabbatical";
		public static string AnomalyForUserToSee = "On Sabbatical";
		public static string MissingCaseAnomaly = "Missing Information";

		public static string dynamicNameURL = "http://www.cis.gvsu.edu/public/staffListing/"; //URL of the faculty listing
		public static string dynamicNameTableClass = "ctl0_Main_grdListing"; //Class name of table to scan
		public static string dynamicNameTableClassEven = "ctl0_Main_grdListingEven"; //Class name of table to scan
		public static string dynamicNameTableClassElement = "tbody"; //Tag seperator

		//If these values are altered, the formulas used to scan through the tables will need to be changed as well.
		public static List<string> dayList = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
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

		//Do not change anything below unless you are performing an overhaul. These are important presets and variable types.
		public static HashSet<string> uniqueClassInput = new HashSet<string>();
		public static HashSet<string> uniqueFacultyNames = new HashSet<string>();
		public static HashSet<string> eventAnomolies = new HashSet<string>();
		public static bool isMissing = false;
		public static string connectionParam = "server=" + ip + ";uid=" + username + ";port=" + port + ";pwd=" + password + ";database=" + database + ";" + extraSecurityParameters;
	}
}
