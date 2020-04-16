using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using ReactiveUI;
using System.Reactive.Disposables;

namespace TreeView
{
    public partial class ItemView : ReactiveView<Item>
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

            this.Bind(ViewModel, vm => vm.Checked, v => v._check.State, _check.ObservableActivated(),
                vmv => vmv ? NSCellStateValue.On : NSCellStateValue.Off, vvm => vvm == NSCellStateValue.On)
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
