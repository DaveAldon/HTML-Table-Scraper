//
//This class runs the window instantiation and event handling for inputs. All scraping and query requests originate from here.
//

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

		partial void findOut1(NSObject sender) //When does a faculty teach a specified class
		{
			try
			{
				resultTextBox.StringValue = getDataInit.whenDoesXHaveY(facultyListCombo.TitleOfSelectedItem, classCombo.TitleOfSelectedItem);
			}

			#pragma warning disable CS0168 // Variable is declared but never used
			catch (ArgumentNullException error)
			#pragma warning restore CS0168
			{
				errorMessage();
			}
		}

		partial void findOut2(NSObject sender) //Who teaches a specific class query
		{
			try
			{
				resultTextBox.StringValue = getDataInit.whoTeachesX(classCombo2.TitleOfSelectedItem);
			}

			#pragma warning disable CS0168
			catch (NullReferenceException error)
			#pragma warning restore CS0168
			{
				errorMessage();
			}
		}

		partial void findOut3(NSObject sender) //Customized availability query
		{
			try
			{
				resultTextBox.StringValue = getDataInit.whoIsFreeFromList(listOfChosenText.StringValue);
			}

			#pragma warning disable CS0168
			catch (NullReferenceException error)
			#pragma warning restore CS0168
			{
				errorMessage();
			}
		}

		partial void findOut4(NSObject sender) //All faculty availability query
		{
			try
			{
				resultTextBox.StringValue = getDataInit.whenIsEveryoneAvailable();
			}

			#pragma warning disable CS0168
			catch (NullReferenceException error)
			#pragma warning restore CS0168
			{
				errorMessage();
			}
		}

		partial void findOut5(NSObject sender) //Who is free at a specified time query
		{
			try
			{
				resultTextBox.StringValue = getDataInit.whoIsFreeAtX(timeCombo.TitleOfSelectedItem);
			}

			#pragma warning disable CS0168
			catch (NullReferenceException error)
			#pragma warning restore CS0168
			{
				errorMessage();
			}
		}

		partial void pushAddName(NSObject sender) //Adds a name to the customized list
		{
			if (!listOfChosenText.StringValue.Contains(classCombo3.TitleOfSelectedItem)) //Checks for duplicates
			{
				listOfChosenText.StringValue += classCombo3.TitleOfSelectedItem + "\n";
			}
		}

		partial void pushRemoveName(NSObject sender) //Removes a name from the customized list
		{
			if (listOfChosenText.StringValue.Contains(classCombo3.TitleOfSelectedItem))
			{
				listOfChosenText.StringValue = listOfChosenText.StringValue.Replace(classCombo3.TitleOfSelectedItem + "\n", string.Empty);
			}
		}

		partial void clickedScheduleButton(NSObject sender) //Creates and grabs the appropriate URL for the selected faculty
		{
			try
			{
				string facName = htmlScheduleCombo.TitleOfSelectedItem; //Grabs the selected combo list item
				webViewSchedule.MainFrameUrl = specialFormatInit.splitNameGetURL(facName); //Sets browser view to the formatted URL
			}

			#pragma warning disable CS0168
			catch (NullReferenceException error)
			#pragma warning restore CS0168
			{
				errorMessage();
			}
		}

		partial void clickedScrapeButton(NSObject sender) //Gets all of the names of the faculty and scrapes through each of their schedules, placing everything into their appropriate database tables
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
			}
		}

		void errorMessage()
		{
			resultTextBox.StringValue = "Please press the \"Scrape\" button first.";
		}
	}
}
