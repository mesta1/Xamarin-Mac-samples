// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ReactiveCollectionView
{
	[Register ("ItemViewController")]
	partial class ItemViewController
	{
		[Outlet]
		AppKit.NSButton _check { get; set; }

		[Outlet]
		AppKit.NSTextField _label { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_check != null) {
				_check.Dispose ();
				_check = null;
			}

			if (_label != null) {
				_label.Dispose ();
				_label = null;
			}
		}
	}
}
