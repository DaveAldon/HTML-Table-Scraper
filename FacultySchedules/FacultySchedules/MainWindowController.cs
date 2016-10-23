using System;
using Foundation;
using AppKit;
using MySql.Data.MySqlClient;

namespace FacultySchedules
{
	public partial class MainWindowController : NSWindowController
	{
		//int numberOfTimesClicked = 0;

		public void ViewDidLoad()
		{
			base.AwakeFromNib();

			// Set the initial value for the label
			//resultTextBox.StringValue = "Button has not been clicked yet.";
		}

		partial void clickedScrapeButton(Foundation.NSObject sender)
		{
			string facName = "IraWoodring";
			Run runInit = new Run();
			runInit.start();
			/*facName = facultyListCombo.SelectedItem.ToString();

			facName.Replace(" ", string.Empty); */
			runInit.name = facName; 
			//resultTextBox.StringValue = runInit.start().ToString();
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
	}
}
