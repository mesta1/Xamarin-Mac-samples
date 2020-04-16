using System;
using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Disposables;
using Foundation;
using AppKit;
using System.Diagnostics;

namespace ReactiveCollectionView
{
    // The xib MUST have the same name of this class (ItemViewController.xib)
    public partial class ItemViewController : ReactiveCollectionViewItem<ItemViewModel>
    {
        private CompositeDisposable _disposables;  
        
        public ItemViewController(IntPtr handle) : base(handle)
        {
            Initialize();
        }
        
        [Export("initWithCoder:")]
        public ItemViewController(NSCoder coder) : base(coder)
        {
            Initialize();
        }
     
        public ItemViewController() : base("ItemView", NSBundle.MainBundle)
        {
            Initialize();
        }
       
        void Initialize()
        {
        }       

        //strongly typed view accessor
        public new ItemView View
        {
            get
            {
                return (ItemView)base.View;
            }
        }

        public void UpdateUI()
        {
            Debug.WriteLine($"Loaded {ViewModel.Name}");
            _disposables?.Dispose();
            _disposables = new CompositeDisposable();
            this.Bind(ViewModel, vm => vm.Checked, v => v._check.State, _check.ObservableActivated(),
                vmv => vmv ? NSCellStateValue.On : NSCellStateValue.Off, vvm => vvm == NSCellStateValue.On)
                .DisposeWith(_disposables);

            this.OneWayBind(ViewModel, vm => vm.Name, v => v._check.Title)
              .DisposeWith(_disposables);

            Deactivated
               .Take(1)
               .Subscribe(_ =>
               {
                   _disposables.Dispose();
                   Debug.WriteLine($"Unloaded {ViewModel.Name}");
               });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _disposables?.Dispose();
        }
    }

    // DO NOT COPY, IS ALREADY THERE

    public static class SignalHelpers
    {
        public static IObservable<Unit> ObservableActivated(this NSButton button)
        {
            return Observable
                .FromEventPattern<EventHandler, EventArgs>(
                    h => button.Activated += h, h => button.Activated -= h)
                .Select(_ => Unit.Default);
        }       
    }
}
