using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
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
    public abstract class ReactiveTableViewCell : NSTableCellView, IReactiveObject, ICanActivate
    {
        private Subject<Unit> _activated = new Subject<Unit>();
        private Subject<Unit> _deactivated = new Subject<Unit>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveTableViewCell"/> class.
        /// </summary>
        /// <param name="frame">The frame.</param>
        protected ReactiveTableViewCell(CGRect frame)
            : base(frame)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveTableViewCell"/> class.
        /// </summary>
        /// <param name="t">The object flag.</param>
        protected ReactiveTableViewCell(NSObjectFlag t)
            : base(t)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveTableViewCell"/> class.
        /// </summary>
        /// <param name="coder">The coder.</param>
        [SuppressMessage("Redundancy", "CA1801: Redundant parameter", Justification = "Legacy interface")]
        protected ReactiveTableViewCell(NSCoder coder)
            : base(NSObjectFlag.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveTableViewCell"/> class.
        /// </summary>
        protected ReactiveTableViewCell()
        {
        } 

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveTableViewCell"/> class.
        /// </summary>
        /// <param name="handle">The pointer.</param>
        protected ReactiveTableViewCell(IntPtr handle)
            : base(handle)
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

        /// <inheritdoc />
   

        /// <inheritdoc/>
        public IObservable<Unit> Activated => _activated.AsObservable();

        /// <inheritdoc/>
        public IObservable<Unit> Deactivated => _deactivated.AsObservable();       

        /// <inheritdoc/>
        public override void ViewWillMoveToSuperview(NSView newSuperview)
        {
            base.ViewWillMoveToSuperview(newSuperview);
            (newSuperview != null ? _activated : _deactivated).OnNext(Unit.Default);

        }

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
    public abstract class ReactiveTableViewCell<TViewModel> : ReactiveTableViewCell, IViewFor<TViewModel>
        where TViewModel : class
    {
        private TViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveTableViewCell{TViewModel}"/> class.
        /// </summary>
        /// <param name="frame">The frame.</param>
        protected ReactiveTableViewCell(CGRect frame)
            : base(frame)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveTableViewCell{TViewModel}"/> class.
        /// </summary>
        /// <param name="t">The object flag.</param>
        protected ReactiveTableViewCell(NSObjectFlag t)
            : base(t)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveTableViewCell{TViewModel}"/> class.
        /// </summary>
        /// <param name="coder">The coder.</param>
        [SuppressMessage("Redundancy", "CA1801: Redundant parameter", Justification = "Legacy interface")]
        protected ReactiveTableViewCell(NSCoder coder)
            : base(NSObjectFlag.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveTableViewCell{TViewModel}"/> class.
        /// </summary>
        protected ReactiveTableViewCell()
        {
        }      

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveTableViewCell{TViewModel}"/> class.
        /// </summary>
        /// <param name="handle">The pointer.</param>
        protected ReactiveTableViewCell(IntPtr handle)
            : base(handle)
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