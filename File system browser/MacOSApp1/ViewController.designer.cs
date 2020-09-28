// WARNING
//
// This file has been generated automatically by Rider IDE
//   to store outlets and actions made in Xcode.
// If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace MacOSApp1
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSBrowser _browser { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (_browser != null) {
				_browser.Dispose ();
				_browser = null;
			}

		}
	}
}
