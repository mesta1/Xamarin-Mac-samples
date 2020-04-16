using System;
using System.Collections.Generic;
using AppKit;
using Foundation;

namespace ReactiveCollectionView
{
    public class ItemsDataSource : NSCollectionViewDataSource
    {
        public List<ItemViewModel> Data { get; set; } = new List<ItemViewModel>();

        public NSCollectionView ParentCollectionView { get; }

        public ItemsDataSource(NSCollectionView parent)
        {
            // Initialize
            ParentCollectionView = parent;

            // Attach to collection view
            parent.DataSource = this;
        }

        public override nint GetNumberOfSections(NSCollectionView collectionView) => 1;
        
        public override NSCollectionViewItem GetItem(NSCollectionView collectionView, NSIndexPath indexPath)
        {
            var item = collectionView.MakeItem("ItemViewCell", indexPath) as ItemViewController;
            item.ViewModel = Data[(int)indexPath.Item];
            item.UpdateUI();
            return item;
        }

        public override nint GetNumberofItems(NSCollectionView collectionView, nint section)
        {
            return Data?.Count ?? 0;
        }
    }
}
