using System;
namespace FacultySchedules
{
	public class EventObject
	{
		public EventObject(string fn, string en, int h, int hh, int r)
		{
			string facultyName = fn;
			string eventName = en;
			int hour = h;
			int halfhour = hh;
			int rowspan = r;
		}
	}
}
