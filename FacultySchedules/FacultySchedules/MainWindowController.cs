using System;
using Foundation;
using AppKit;
using System.Collections.Generic;

namespace FacultySchedules
{
	public partial class MainWindowController : NSWindowController
	{
		Run runInit = new Run(); //Instantiation of main engine
		allFaculty allFacultyInit = new allFaculty(); //Faculty finder
		SpecialNameFormatting specialFormatInit = new SpecialNameFormatting(); //Formats all of the crazy names into proper URLs
		GetData getDataInit = new GetData();
		List<string> names = new List<string>();

		public void ViewDidLoad()
		{
			base.AwakeFromNib();
		}

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();
		}

		public new MainWindow Window
		{
			get { return (MainWindow)base.Window; }
		}

		public MainWindowController(IntPtr handle) : base(handle) { }

		[Export("initWithCoder:")]
		public MainWindowController(NSCoder coder) : base(coder) { }

		public MainWindowController() : base("MainWindow") { }

		partial void findOut1(NSObject sender)
		{
			resultTextBox.StringValue = getDataInit.whenDoesXHaveY(facultyComboQuery.TitleOfSelectedItem, eventInput1.StringValue);
		}

		partial void findOut2(NSObject sender)
		{

		}

		partial void findOut3(NSObject sender)
		{

		}

		partial void findOut4(NSObject sender)
		{

		}

		partial void findOut5(NSObject sender)
		{

		}






		partial void clickedAddFacultyButton(NSObject sender) //Button click event
		{
			runInit.giveDB.createTable(firstNameInput.StringValue + " " + lastNameInput.StringValue);
		}

		partial void clickedRemoveFacultyButton(NSObject sender)
		{
			runInit.giveDB.dropTable(firstNameInput.StringValue + " " + lastNameInput.StringValue);
		}

		partial void clickedScheduleButton(Foundation.NSObject sender)
		{
			string facName = facultyListCombo.TitleOfSelectedItem; //Grabs the selected combo list item
			webViewSchedule.MainFrameUrl = specialFormatInit.splitNameGetURL(facName); //Sets browser view to the formatted URL
		}

		partial void clickedScrapeButton(Foundation.NSObject sender)
		{
			foreach (string eachName in allFacultyInit.getEveryonesName()) //Goes through each faculty name in the database table
			{
				facultyListCombo.AddItem(eachName); //Adds a new element to the combo list
				//facultyComboQuery.AddItem("asaf");
				names.Add(eachName);
				runInit.start(eachName); //Begins the main engine with the given name
			}
		}
	}
}
