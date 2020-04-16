using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using AppKit;
using CoreGraphics;
using Foundation;

namespace ReactiveUI
{
    /// <summary>
    /// https://github.com/reactiveui/ReactiveUI/blob/196433808c46cf8404502350664d70ccd7e4ebec/src/ReactiveUI/Platforms/uikit-common/ReactiveTableViewCell.cs
    /// This is a UITableViewCell that is both an UITableViewCell and has ReactiveObject powers
    /// (i.e. you can call RaiseAndSetIfChanged).
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Classes with the same class names within.")]
    [SuppressMessage("Design", "CA1010: Implement generic IEnumerable", Justification = "UI Kit exposes IEnumerable")]
    public abstract class ReactiveCollectionViewItem : NSCollectionViewItem, IReactiveObject, ICanActivate
    {
        private Subject<Unit> _activated = new Subject<Unit>();
        private Subject<Unit> _deactivated = new Subject<Unit>();

       

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveTableViewCell"/> class.
        /// </summary>
        /// <param name="t">The object flag.</param>
        protected ReactiveCollectionViewItem(NSObjectFlag t)
            : base(t)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveTableViewCell"/> class.
        /// </summary>
        /// <param name="coder">The coder.</param>
        [SuppressMessage("Redundancy", "CA1801: Redundant parameter", Justification = "Legacy interface")]
        protected ReactiveCollectionViewItem(NSCoder coder)
            : base(NSObjectFlag.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveTableViewCell"/> class.
        /// </summary>
        protected ReactiveCollectionViewItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveTableViewCell"/> class.
        /// </summary>
        /// <param name="handle">The pointer.</param>
        protected ReactiveCollectionViewItem(IntPtr handle)
            : base(handle)
        {
        }

        protected ReactiveCollectionViewItem(string nibNameOrNull, NSBundle nibBundleOrNull)
            : base(NSObjectFlag.Empty)
        {

        }

        /// <inheritdoc/>
        public event PropertyChangingEventHandler PropertyChanging
        {
            add => WeakEventManager<INotifyPropertyChanging, PropertyChangingEventHandler, PropertyChangingEventArgs>.AddHandler(this, value);
            remove => WeakEventManager<INotifyPropertyChanging, PropertyChangingEventHandler, PropertyChangingEventArgs>.RemoveHandler(this, value);
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add => WeakEventManager<INotifyPropertyChanged, PropertyChangedEventHandler, PropertyChangedEventArgs>.AddHandler(this, value);
            remove => WeakEventManager<INotifyPropertyChanged, PropertyChangedEventHandler, PropertyChangedEventArgs>.RemoveHandler(this, value);
        }

        public override void ViewDidAppear()
        {
            base.ViewDidAppear();
            _activated.OnNext(Unit.Default);
        }

        public override void ViewDidDisappear()
        {
            base.ViewDidDisappear();
            _deactivated.OnNext(Unit.Default);                
        }


        /// <inheritdoc/>
        public IObservable<Unit> Activated => _activated.AsObservable();

        /// <inheritdoc/>
        public IObservable<Unit> Deactivated => _deactivated.AsObservable();        

        /// <inheritdoc/>
        void IReactiveObject.RaisePropertyChanging(PropertyChangingEventArgs args)
        {
            WeakEventManager<INotifyPropertyChanging, PropertyChangingEventHandler, PropertyChangingEventArgs>.DeliverEvent(this, args);
        }

        /// <inheritdoc/>
        void IReactiveObject.RaisePropertyChanged(PropertyChangedEventArgs args)
        {
            WeakEventManager<INotifyPropertyChanged, PropertyChangedEventHandler, PropertyChangedEventArgs>.DeliverEvent(this, args);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _activated?.Dispose();
                _deactivated?.Dispose();
            }

            base.Dispose(disposing);
        }
    }

    /// <summary>
    /// This is a UITableViewCell that is both an UITableViewCell and has ReactiveObject powers
    /// (i.e. you can call RaiseAndSetIfChanged).
    /// </summary>
    /// <typeparam name="TViewModel">The view model type.</typeparam>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Classes with the same class names within.")]
    [SuppressMessage("Design", "CA1010: Implement generic IEnumerable", Justification = "UI Kit exposes IEnumerable")]
    public abstract class ReactiveCollectionViewItem<TViewModel> : ReactiveCollectionViewItem, IViewFor<TViewModel>
        where TViewModel : class
    {
        private TViewModel _viewModel;      
        

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveTableViewCell{TViewModel}"/> class.
        /// </summary>
        /// <param name="t">The object flag.</param>
        protected ReactiveCollectionViewItem(NSObjectFlag t)
            : base(t)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveTableViewCell{TViewModel}"/> class.
        /// </summary>
        /// <param name="coder">The coder.</param>
        [SuppressMessage("Redundancy", "CA1801: Redundant parameter", Justification = "Legacy interface")]
        protected ReactiveCollectionViewItem(NSCoder coder)
            : base(NSObjectFlag.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveTableViewCell{TViewModel}"/> class.
        /// </summary>
        protected ReactiveCollectionViewItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveTableViewCell{TViewModel}"/> class.
        /// </summary>
        /// <param name="handle">The pointer.</param>
        protected ReactiveCollectionViewItem(IntPtr handle)
            : base(handle)
        {
        }

        protected ReactiveCollectionViewItem(string nibNameOrNull, NSBundle nibBundleOrNull)
           : base(NSObjectFlag.Empty)
        {

        }

        /// <inheritdoc/>
        public TViewModel ViewModel
        {
            get => _viewModel;
            set => this.RaiseAndSetIfChanged(ref _viewModel, value);
        }

        /// <inheritdoc/>
        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (TViewModel)value;
        }
    }
}
