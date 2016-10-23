using System;
using Foundation;
using AppKit;
using System.Linq;

namespace FacultySchedules
{
	public partial class MainWindowController : NSWindowController
	{
		string facName;

		public void ViewDidLoad()
		{
			base.AwakeFromNib();
			//Set initial values here
		}

		partial void clickedScrapeButton(Foundation.NSObject sender)
		{
			Run runInit = new Run();

			facName = facultyListCombo.TitleOfSelectedItem;

			facName = RemoveWhitespace(facName);
			runInit.start(facName);
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

		public static string RemoveWhitespace(string input)
		{
			return new string(input.ToCharArray()
				.Where(c => !Char.IsWhiteSpace(c))
				.ToArray());
		}
	}
}
