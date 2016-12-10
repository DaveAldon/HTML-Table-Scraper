//
//This class formats a faculty name into its unique URL, returning the URL to the requester.
//

using System.Text.RegularExpressions;

namespace FacultySchedules
{
	public class SpecialNameFormatting
	{

		/// <summary>
		/// Turns two names into one.
		/// </summary>
		/// <returns>Full name.</returns>
		/// <param name="firstName">First name.</param>
		/// <param name="lastName">Last name.</param>
		public string getNameForCombo(string firstName, string lastName)
		{
			string fullName = firstName + " " + lastName;
			return fullName;
		}

		/// <summary>
		/// Gets the name for a table.
		/// </summary>
		/// <returns>The name for table.</returns>
		/// <param name="name">Name.</param>
		public string getNameForTable(string name)
		{
			string[] subStrings = Regex.Split(name, ",");
			string fullName = subStrings[1] + " " + subStrings[0];
			return fullName;
		}

		/// <summary>
		/// Gets the URL. You can change this URL to whatever is necessary.
		/// </summary>
		/// <returns>The URL.</returns>
		/// <param name="firstName">First name.</param>
		/// <param name="lastName">Last name.</param>
		public string getURL(string firstName, string lastName)
		{
			string URL = "http://www.cis.gvsu.edu/public/staffListing/index.php?page=staff&fname=" + firstName + "&lname=" + lastName;
			return URL;
		}

		/// <summary>
		/// Splits the name up into first/last and returns the URL.
		/// </summary>
		/// <returns>The URL.</returns>
		/// <param name="input">Input.</param>
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
