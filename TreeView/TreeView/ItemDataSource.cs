using System;
using System.Collections.Generic;
using System.Linq;
using AppKit;
using Foundation;

namespace TreeView
{
    public class ItemDataSource : NSOutlineViewDataSource
    {
        public List<Item> Data = new List<Item>();

        public ItemDataSource()
        {
        }

        public override nint GetChildrenCount(NSOutlineView outlineView, NSObject item)
        {
            if (item == null)
            {
                return Data?.Count ?? 0;
            }
            else
            {
                return ((Item)item).Children.Count;
            }
        }

        public override NSObject GetChild(NSOutlineView outlineView, nint childIndex, NSObject item)
        {
            if (item == null)
            {
                return Data[(int)childIndex];
            }
            else
            {
                return ((Item)item).Children[(int)childIndex];
            }

        }

        public override bool ItemExpandable(NSOutlineView outlineView, NSObject item)
        {
            if (item == null)
            {
                return Data[0].Children.Any();
            }
            else
            {
                return ((Item)item).Children.Any();
            }
        }
    }

    public class ItemDelegate : NSOutlineViewDelegate
    {
        private readonly ItemDataSource _dataSource;

        public ItemDelegate(ItemDataSource dataSource, NSOutlineView outlineView)
        {
            _dataSource = dataSource;
            outlineView.RegisterNib(new NSNib(nameof(ItemView), NSBundle.MainBundle), nameof(ItemView));
        }

        public override NSView GetView(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item)
        {
            var view = (ItemView)outlineView.MakeView(nameof(ItemView), this);
            view.ViewModel = (Item)item;
            view.UpdateUI();
            return view;
        }

    }
}
