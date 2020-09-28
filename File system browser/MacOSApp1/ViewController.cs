using System;
using System.IO;
using System.Linq;
using AppKit;
using Foundation;
using WpfApp4;

namespace MacOSApp1
{
    public partial class ViewController : NSViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            var browserDelegate = new FolderBrowserDelegate(_browser, Environment.GetLogicalDrives().Select(s=>new NSFolderViewModel(FolderViewModel.CreateDirectory(s))).ToList());
            _browser.Delegate = browserDelegate;
            _browser.MaxVisibleColumns = 3;
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }
    }
}
