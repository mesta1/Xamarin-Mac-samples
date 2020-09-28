using System;
using System.Collections.Generic;
using System.Linq;
using AppKit;
using CoreGraphics;
using Foundation;

namespace TreeView
{
    public partial class ViewController : NSViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var items = new List<ItemViewModel> {
                new ItemViewModel()
                {
                    Text = "item1item1item1item1item1item1item1item1item1item1item1item1item1item1item1item1",
                    Checked = false,
                    Children = new List<ItemViewModel>{ new ItemViewModel { Text = "subItem1"}, new ItemViewModel { Text = "subItem2", Checked = true} }
                },
                new ItemViewModel { Text = "item2", Checked = true } };
            var dataSource = new ItemDataSource();
            dataSource.Data = items;

            _outletView.DataSource = dataSource;
            _outletView.Delegate = new ItemDelegate(_outletView);

            // Do any additional setup after loading the view.
        }

        public void SizeToFit()
        {
            var view = (NSTableCellView) _outletView.GetView(0, 0, true);
            if (view != null)
            {
                var width = view.FittingSize.Width;
                _outletView.SetFrameSize(new CGSize(_outletView.Frame.Height, width));
            }
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
