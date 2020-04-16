// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace TreeView
{
	[Register ("ItemView")]
	partial class ItemView
	{
		[Outlet]
		AppKit.NSButton _check { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_check != null) {
				_check.Dispose ();
				_check = null;
			}
		}
	}
}
