using System;
using Foundation;
using AppKit;
using System.Linq;

namespace FacultySchedules
{
	public partial class MainWindowController : NSWindowController
	{
		string facName, firstName, lastName;

		public void ViewDidLoad()
		{
			base.AwakeFromNib();
			//Set initial values here
		}

		partial void clickedScrapeButton(Foundation.NSObject sender)
		{
			Run runInit = new Run();

			if (allFacultyCheckBox.State == NSCellStateValue.On)
			{

			}
			else
			{
				facName = facultyListCombo.TitleOfSelectedItem;
				firstName = splitNameGetFirst(facName);
				lastName = splitNameGetLast(facName);
				facName = RemoveWhitespace(facName);
				runInit.start(facName, firstName, lastName);
			}
		}

		partial void clickedScheduleButton(Foundation.NSObject sender)
		{
			webViewSchedule.MainFrameUrl = "https://www.gvsu.edu/";
		}

		public MainWindowController(IntPtr handle) : base(handle) { }

		[Export("initWithCoder:")]
		public MainWindowController(NSCoder coder) : base(coder) { }

		public MainWindowController() : base("MainWindow") { }

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();
		}

		public new MainWindow Window
		{
			get { return (MainWindow)base.Window; }
		}

		public static string splitNameGetFirst(string input)
		{
			string[] names = input.Split();
			string first = names[0];
			return first;
		}

		public string splitNameGetLast(string input)
		{
			string[] names = input.Split();
			string last = names[1];
			return last;
		}

		public static string RemoveWhitespace(string input)
		{
			return new string(input.ToCharArray()
				.Where(c => !Char.IsWhiteSpace(c))
				.ToArray());
		}
	}
}
