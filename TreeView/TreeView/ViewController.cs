using System;
using System.Collections.Generic;
using AppKit;
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

            var items = new List<Item> {
                new Item()
                {
                    Text = "item1", Checked = false,
                    Children = new List<Item>{ new Item{Text = "subItem1"}, new Item { Text = "subItem2", Checked = true} }
                },
                new Item { Text = "item2", Checked = true } };
            var dataSource = new ItemDataSource();
            dataSource.Data = items;

            _outletView.DataSource = dataSource;
            _outletView.Delegate = new ItemDelegate(dataSource, _outletView);
            // Do any additional setup after loading the view.
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
