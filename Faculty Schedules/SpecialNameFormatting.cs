//
//This class formats a faculty name into its unique URL, returning the URL to the requester.
//

using System.Text.RegularExpressions;

namespace FacultySchedules
{
	public class SpecialNameFormatting
	{
		public string getNameForCombo(string firstName, string lastName)
		{
			string fullName = firstName + " " + lastName;
			return fullName;
		}

		public string getNameForTable(string name)
		{
			string[] subStrings = Regex.Split(name, ",");
			string fullName = subStrings[1] + " " + subStrings[0];
			return fullName;
		}

		public string getURL(string firstName, string lastName)
		{
			string URL = "http://www.cis.gvsu.edu/public/staffListing/index.php?page=staff&fname=" + firstName + "&lname=" + lastName;
			return URL;
		}

		public string splitNameGetURL(string input) //Splits the name into first and last names including their special characters
		{
			input = input.Trim();
			string[] nameToSplit = input.Split();
			string firstname, lastname;

			if (nameToSplit.Length == 3)
			{
				firstname = nameToSplit[0] + "+" + nameToSplit[1]; //URL needs "+" instead of " " to work
				lastname = nameToSplit[2];
			}
			else {
				firstname = nameToSplit[0];
				lastname = nameToSplit[1];
			}
			return getURL(firstname, lastname);
		}
	}
}
