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
		AppKit.NSButton allFacultyCheckBox { get; set; }

		[Outlet]
		AppKit.NSPopUpButton facultyListCombo { get; set; }

		[Outlet]
		AppKit.NSTextField resultTextBox { get; set; }

		[Outlet]
		AppKit.NSButton scheduleButton { get; set; }

		[Outlet]
		AppKit.NSButton scrapeButton { get; set; }

		[Outlet]
		WebKit.WebView webViewSchedule { get; set; }

		[Action ("clickedScheduleButton:")]
		partial void clickedScheduleButton (Foundation.NSObject sender);

		[Action ("clickedScrapeButton:")]
		partial void clickedScrapeButton (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (allFacultyCheckBox != null) {
				allFacultyCheckBox.Dispose ();
				allFacultyCheckBox = null;
			}

			if (facultyListCombo != null) {
				facultyListCombo.Dispose ();
				facultyListCombo = null;
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

			if (webViewSchedule != null) {
				webViewSchedule.Dispose ();
				webViewSchedule = null;
			}
		}
	}
}
