using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using AppKit;
using Foundation;
using ReactiveUI;
using Splat;

namespace TreeView
{
    //public class Item
    //{
    //    public string Text { get; set; }
    //   
    //    public bool Checked { get; set; }
    //
    //    public List<Item> Children { get; set; }
    //
    //    public Item()
    //    {
    //        Children = new List<Item>();
    //    }
    //}

    public class ItemViewModel : ReactiveNSObject
    {
        private bool _checked;

        public string Text { get; set; }

        public bool Checked
        {
            get => _checked;
            set { RaiseAndSetIfChanged(ref _checked, value); }
        }

        public List<ItemViewModel> Children { get; set; }

        public ItemViewModel()
        {
            Children = new List<ItemViewModel>();
        }

        public void SetCheckedForChilds(bool @checked)
        {
            this.Checked = @checked;
            foreach (var child in Children)
                child.SetCheckedForChilds(@checked);
        }
    }


    /// <summary>
    /// https://github.com/Nethereum/Nethereum.UI.Wallet.Sample/blob/a52f3fb8589f23f9e9d9fb9ea582a65e08ce8d92/Nethereum.UI/Nethereum.UI.Core/Infractructure/MvxReactiveViewModel.cs
    /// </summary>
    public class ReactiveNSObject : NSObject, IReactiveNotifyPropertyChanged<IReactiveObject>, IReactiveObject, INotifyPropertyChanged, ReactiveUI.INotifyPropertyChanging, IEnableLogger
    {
        ProxyReactiveObject reactiveObj;

        public ReactiveNSObject()
        {
            reactiveObj = new ProxyReactiveObject();
        }
     
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { this.reactiveObj.PropertyChanged += value; }
            remove { this.reactiveObj.PropertyChanged -= value; }
        }

        public void RaisePropertyChanging(ReactiveUI.PropertyChangingEventArgs args)
        {
            reactiveObj.RaisePropertyChanging(args.PropertyName);
        }

        public event ReactiveUI.PropertyChangingEventHandler PropertyChanging
        {
            add { this.reactiveObj.PropertyChanging += value; }
            remove { this.reactiveObj.PropertyChanging -= value; }
        }

        public IObservable<IReactivePropertyChangedEventArgs<IReactiveObject>> Changing => this.reactiveObj.Changing;

        public IObservable<IReactivePropertyChangedEventArgs<IReactiveObject>> Changed => this.reactiveObj.Changed;  

        public IDisposable SuppressChangeNotifications()
        {
            throw new NotImplementedException();
        }

        public void RaisePropertyChanged(PropertyChangedEventArgs args)
        {
            reactiveObj.RaisePropertyChanged(args.PropertyName);
        }

       
        public TRet RaiseAndSetIfChanged<TRet>(ref TRet backingField, TRet newValue, [CallerMemberName] string propertyName = null)
        {
            return reactiveObj.RaiseAndSetIfChanged(ref backingField, newValue, propertyName);
        }

        class ProxyReactiveObject : ReactiveObject
        {
        }        
    }

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

