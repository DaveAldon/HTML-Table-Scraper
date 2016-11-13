//
//This class handles the beginning and end cycles of the application.
//

using AppKit;
using Foundation;

namespace FacultySchedules
{
	[Register("AppDelegate")]
	public class AppDelegate : NSApplicationDelegate
	{
		MainWindowController mainWindowController;

		public override void DidFinishLaunching(NSNotification notification)
		{
			mainWindowController = new MainWindowController();
			mainWindowController.Window.MakeKeyAndOrderFront(this);
		}

		public override void WillTerminate(NSNotification notification) {}
	}
}
