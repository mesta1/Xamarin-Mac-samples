using System;
using AppKit;
using CoreGraphics;
using Foundation;

namespace ReactiveCollectionView
{
    public partial class ViewController : NSViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            _collectionView.RegisterClassForItem(typeof(ItemViewController), "ItemViewCell");
           
            var flowLayout = new NSCollectionViewFlowLayout()
            {
                ItemSize = new CGSize(150, 150),
                SectionInset = new NSEdgeInsets(10, 10, 10, 20),
                MinimumInteritemSpacing = 10,
                MinimumLineSpacing = 10
            };
            _collectionView.WantsLayer = true;

            // Setup collection view
            _collectionView.CollectionViewLayout = flowLayout;
            _collectionView.Delegate = new CollectionViewDelegate();

            var datasource = new ItemsDataSource(_collectionView);
            datasource.Data = new System.Collections.Generic.List<ItemViewModel> {
                new ItemViewModel { Name = "Test1", Checked=true },
                new ItemViewModel { Name = "Test2" , Checked =false},
                new ItemViewModel { Name = "Test3" , Checked = true},
                new ItemViewModel { Name = "Test4" , Checked = true},
                new ItemViewModel { Name = "Test5" , Checked = true},
                new ItemViewModel { Name = "Test6" , Checked = true},
                new ItemViewModel { Name = "Test7" , Checked = true}};
            _collectionView.ReloadData();
            // Do any additional setup after loading the view.
        }

        public override NSObject RepresentedObject
        {
            get { return base.RepresentedObject; }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }
    }
}