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

			URL = "http://www.cis.gvsu.edu/public/staffListing/index.php?page=staff&fname=" + firstName + "&lname=" + lastName;
			return URL;
		}

		public string splitNameGetURL(string input) //Splits the name into first and last names including their special characters
		{
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
