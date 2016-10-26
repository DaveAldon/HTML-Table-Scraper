using System;
namespace FacultySchedules
{
	public class SpecialNameFormatting
	{
		public string getNameForCombo(string firstName, string lastName)
		{
			string fullName = firstName + " " + lastName;
			return fullName;
		}

		public string getURL(string firstName, string lastName)
		{
			string URL;
			//string finalFirstName = firstName.Replace(" ", "+");

			URL = "http://www.cis.gvsu.edu/public/staffListing/index.php?page=staff&fname=" + firstName + "&lname=" + lastName;
			return URL;
		}

		public string splitNameGetURL(string input)
		{
			string[] nameToSplit = input.Split();
			string firstname, lastname;

			if (nameToSplit.Length == 3)
			{
				firstname = nameToSplit[0] + "+" + nameToSplit[1];
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
