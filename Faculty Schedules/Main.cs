//
//This class runs the instantiation of the entire project.
//

using AppKit;

namespace FacultySchedules
{
	static class MainClass
	{
		static void Main(string[] args) //Main window is fed into this class
		{
			NSApplication.Init();
			NSApplication.Main(args);
		}
	}
}