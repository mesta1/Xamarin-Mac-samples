using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.ComponentModel;

namespace TreeView
{
    public partial class ItemView : ReactiveTableViewCell<ItemViewModel>
    {
        private CompositeDisposable _disposables;

        #region Constructors

        // Called when created from unmanaged code
        public ItemView(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public ItemView(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
        }

        #endregion

        public void UpdateUI()
        {
            _disposables?.Dispose();
            _disposables = new CompositeDisposable();

            _check.State = ViewModel.Checked ? NSCellStateValue.On : NSCellStateValue.Off;

            Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                h=> ViewModel.PropertyChanged +=h, h=>ViewModel.PropertyChanged-=h)
                .Subscribe(_ => _check.State = ViewModel.Checked ? NSCellStateValue.On : NSCellStateValue.Off)
                .DisposeWith(_disposables);

            //this.OneWayBind(ViewModel, vm => vm.Checked, v => v._check.State,
            //    vmv => vmv ? NSCellStateValue.On : NSCellStateValue.Off)
            //    .DisposeWith(_disposables);

            _check.ObservableActivated().Subscribe(_ => ViewModel.SetCheckedForChilds(_check.State == NSCellStateValue.On))
                .DisposeWith(_disposables);

            this.OneWayBind(ViewModel, vm => vm.Text, v => v._check.Title)
                .DisposeWith(_disposables);

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _disposables?.Dispose();
        }
    }
}
