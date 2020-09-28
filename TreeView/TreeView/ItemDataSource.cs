using System;
using System.Collections.Generic;
using System.Linq;
using AppKit;
using Foundation;

namespace TreeView
{
    public class ItemDataSource : NSOutlineViewDataSource
    {
        public List<ItemViewModel> Data = new List<ItemViewModel>();

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
                return ((ItemViewModel)item).Children.Count;
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
                return ((ItemViewModel)item).Children[(int)childIndex];
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
                return ((ItemViewModel)item).Children.Any();
            }
        }
    }

    public class ItemDelegate : NSOutlineViewDelegate
    {
        public ItemDelegate(NSOutlineView outlineView)
        {           
            outlineView.RegisterNib(new NSNib(nameof(ItemView), NSBundle.MainBundle), nameof(ItemView));
        }

        public override NSView GetView(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item)
        {
            var view = (ItemView)outlineView.MakeView(nameof(ItemView), this);
            view.ViewModel = (ItemViewModel)item;
            view.UpdateUI();
            return view;
        }

    }
}
