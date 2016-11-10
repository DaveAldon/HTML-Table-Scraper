using System;
using Foundation;
using AppKit;

namespace FacultySchedules
{
	public partial class MainWindowController : NSWindowController
	{
		Run runInit = new Run(); //Instantiation of main engine
		allFaculty allFacultyInit = new allFaculty(); //Faculty finder
		SpecialNameFormatting specialFormatInit = new SpecialNameFormatting(); //Formats all of the different faculty names into proper URLs
		GetData getDataInit = new GetData(); //Class of query builders
		GiveData clearInit = new GiveData(); //Class of queries, only needed to reset tables in this class

		//
		//UI dependancies and instantiation
		//

		public void ViewDidLoad()
		{
			base.AwakeFromNib();
		}

		public new MainWindow Window //Instantiates a new NSView
		{
			get { return (MainWindow)base.Window; }
		}

		public MainWindowController(IntPtr handle) : base(handle) { }

		[Export("initWithCoder:")]
		public MainWindowController(NSCoder coder) : base(coder) { }

		public MainWindowController() : base("MainWindow") { }

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();
		}

		//
		//Event Handling
		//

		partial void findOut1(NSObject sender)
		{
			resultTextBox.StringValue = getDataInit.whenDoesXHaveY(facultyListCombo.TitleOfSelectedItem, classCombo.TitleOfSelectedItem);
		}

		partial void findOut2(NSObject sender)
		{
			resultTextBox.StringValue = getDataInit.whoTeachesX(classCombo2.TitleOfSelectedItem);
		}

		partial void findOut3(NSObject sender)
		{
			resultTextBox.StringValue = getDataInit.whoIsFreeAtXFromList(timeCombo2.TitleOfSelectedItem, listOfChosenText.StringValue);
		}

		partial void findOut4(NSObject sender) //All faculty availability query
		{
			resultTextBox.StringValue = getDataInit.whenIsEveryoneAvailable();
		}

		partial void findOut5(NSObject sender)
		{
			resultTextBox.StringValue = getDataInit.whoIsFreeAtX(timeCombo.TitleOfSelectedItem);
		}

		partial void pushAddName(NSObject sender)
		{
			listOfChosenText.StringValue += classCombo3.TitleOfSelectedItem + "\n";
		}

		partial void pushRemoveName(NSObject sender)
		{
			if (listOfChosenText.StringValue.Contains(classCombo3.TitleOfSelectedItem))
			{
				listOfChosenText.StringValue = listOfChosenText.StringValue.Replace(classCombo3.TitleOfSelectedItem + "\n", string.Empty);
			}
		}

		partial void clickedAddFacultyButton(NSObject sender)
		{
			runInit.giveDB.createTable(firstNameInput.StringValue + " " + lastNameInput.StringValue);
		}

		partial void clickedRemoveFacultyButton(NSObject sender)
		{
			runInit.giveDB.dropTable(firstNameInput.StringValue + " " + lastNameInput.StringValue);
		}

		partial void clickedScheduleButton(NSObject sender)
		{
			string facName = htmlScheduleCombo.TitleOfSelectedItem; //Grabs the selected combo list item
			webViewSchedule.MainFrameUrl = specialFormatInit.splitNameGetURL(facName); //Sets browser view to the formatted URL
		}

		partial void clickedScrapeButton(NSObject sender)
		{
			allFacultyInit.findAndInsertAllNames();
			foreach (string eachName in Globals.uniqueFacultyNames) //Goes through each faculty name in the database table
			{
				clearInit.dropTable(eachName); //Drop all of the faculty name tables to refresh the data
				facultyListCombo.AddItem(eachName); //Populate the faculty names combo lists
				htmlScheduleCombo.AddItem(eachName);
				classCombo3.AddItem(eachName);
				runInit.start(eachName); //Begins the main engine with the given name
			}

			foreach (string eachClassName in Globals.uniqueClassInput) //Populate the class combo lists
			{
				classCombo.AddItem(eachClassName);
				classCombo2.AddItem(eachClassName);
			}

			foreach (string eachTime in Globals.timeList) //Populate the time combo lists
			{
				timeCombo.AddItem(eachTime);
				timeCombo2.AddItem(eachTime);
			}
		}
	}
}
