// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace FacultySchedules
{
	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		AppKit.NSView addFacultyButton { get; set; }

		[Outlet]
		AppKit.NSButton allFacultyCheckBox { get; set; }

		[Outlet]
		AppKit.NSPopUpButton classCombo { get; set; }

		[Outlet]
		AppKit.NSPopUpButton classCombo2 { get; set; }

		[Outlet]
		AppKit.NSPopUpButton classCombo3 { get; set; }

		[Outlet]
		AppKit.NSTextField eventInput1 { get; set; }

		[Outlet]
		AppKit.NSTextField eventInput2 { get; set; }

		[Outlet]
		AppKit.NSTextField eventInput3 { get; set; }

		[Outlet]
		AppKit.NSTextField eventInput5 { get; set; }

		[Outlet]
		AppKit.NSPopUpButton facultyListCombo { get; set; }

		[Outlet]
		AppKit.NSTextField firstNameInput { get; set; }

		[Outlet]
		AppKit.NSPopUpButton htmlScheduleCombo { get; set; }

		[Outlet]
		AppKit.NSTextField lastNameInput { get; set; }

		[Outlet]
		AppKit.NSTextField middleInitialInput { get; set; }

		[Outlet]
		AppKit.NSView removeFacultyButton { get; set; }

		[Outlet]
		AppKit.NSTextField resultTextBox { get; set; }

		[Outlet]
		AppKit.NSButton scheduleButton { get; set; }

		[Outlet]
		AppKit.NSButton scrapeButton { get; set; }

		[Outlet]
		AppKit.NSPopUpButton timeCombo { get; set; }

		[Outlet]
		AppKit.NSPopUpButton timeCombo2 { get; set; }

		[Outlet]
		WebKit.WebView webViewSchedule { get; set; }

		[Action ("clickedAddFacultyButton:")]
		partial void clickedAddFacultyButton (Foundation.NSObject sender);

		[Action ("clickedRemoveFacultyButton:")]
		partial void clickedRemoveFacultyButton (Foundation.NSObject sender);

		[Action ("clickedScheduleButton:")]
		partial void clickedScheduleButton (Foundation.NSObject sender);

		[Action ("clickedScrapeButton:")]
		partial void clickedScrapeButton (Foundation.NSObject sender);

		[Action ("findOut1:")]
		partial void findOut1 (Foundation.NSObject sender);

		[Action ("findOut2:")]
		partial void findOut2 (Foundation.NSObject sender);

		[Action ("findOut3:")]
		partial void findOut3 (Foundation.NSObject sender);

		[Action ("findOut4:")]
		partial void findOut4 (Foundation.NSObject sender);

		[Action ("findOut5:")]
		partial void findOut5 (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (timeCombo2 != null) {
				timeCombo2.Dispose ();
				timeCombo2 = null;
			}

			if (addFacultyButton != null) {
				addFacultyButton.Dispose ();
				addFacultyButton = null;
			}

			if (allFacultyCheckBox != null) {
				allFacultyCheckBox.Dispose ();
				allFacultyCheckBox = null;
			}

			if (classCombo != null) {
				classCombo.Dispose ();
				classCombo = null;
			}

			if (classCombo2 != null) {
				classCombo2.Dispose ();
				classCombo2 = null;
			}

			if (classCombo3 != null) {
				classCombo3.Dispose ();
				classCombo3 = null;
			}

			if (eventInput1 != null) {
				eventInput1.Dispose ();
				eventInput1 = null;
			}

			if (eventInput2 != null) {
				eventInput2.Dispose ();
				eventInput2 = null;
			}

			if (eventInput3 != null) {
				eventInput3.Dispose ();
				eventInput3 = null;
			}

			if (eventInput5 != null) {
				eventInput5.Dispose ();
				eventInput5 = null;
			}

			if (facultyListCombo != null) {
				facultyListCombo.Dispose ();
				facultyListCombo = null;
			}

			if (firstNameInput != null) {
				firstNameInput.Dispose ();
				firstNameInput = null;
			}

			if (htmlScheduleCombo != null) {
				htmlScheduleCombo.Dispose ();
				htmlScheduleCombo = null;
			}

			if (lastNameInput != null) {
				lastNameInput.Dispose ();
				lastNameInput = null;
			}

			if (middleInitialInput != null) {
				middleInitialInput.Dispose ();
				middleInitialInput = null;
			}

			if (removeFacultyButton != null) {
				removeFacultyButton.Dispose ();
				removeFacultyButton = null;
			}

			if (resultTextBox != null) {
				resultTextBox.Dispose ();
				resultTextBox = null;
			}

			if (scheduleButton != null) {
				scheduleButton.Dispose ();
				scheduleButton = null;
			}

			if (scrapeButton != null) {
				scrapeButton.Dispose ();
				scrapeButton = null;
			}

			if (timeCombo != null) {
				timeCombo.Dispose ();
				timeCombo = null;
			}

			if (webViewSchedule != null) {
				webViewSchedule.Dispose ();
				webViewSchedule = null;
			}
		}
	}
}
