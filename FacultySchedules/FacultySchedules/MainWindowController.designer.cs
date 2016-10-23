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
		AppKit.NSButton scrapeButton { get; set; }

		[Action ("clickedScrapeButton:")]
		partial void clickedScrapeButton (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (scrapeButton != null) {
				scrapeButton.Dispose ();
				scrapeButton = null;
			}

			if (resultTextBox != null) {
				resultTextBox.Dispose ();
				resultTextBox = null;
			}

			if (facultyListCombo != null) {
				facultyListCombo.Dispose ();
				facultyListCombo = null;
			}

			if (allFacultyCheckBox != null) {
				allFacultyCheckBox.Dispose ();
				allFacultyCheckBox = null;
			}
		}
	}
}
