using System;
using System.Diagnostics;
using ReactiveUI;

namespace ReactiveCollectionView
{
    public class ItemViewModel : ReactiveObject
    {
        private bool _checked;

        public string Name { get; set; }

        public bool Checked {
            get => _checked;
            set
            {
                _checked = value;
                Debug.WriteLine($"Checked {Checked}");
            }
        }

        public ItemViewModel()
        {
        }
    }
}
