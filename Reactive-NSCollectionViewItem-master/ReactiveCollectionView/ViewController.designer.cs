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
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSCollectionView _collectionView { get; set; }		
		
		void ReleaseDesignerOutlets ()
		{
			if (_collectionView != null) {
				_collectionView.Dispose ();
				_collectionView = null;
			}
		}
	}
}
