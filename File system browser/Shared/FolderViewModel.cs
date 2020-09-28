using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WpfApp4
{
    public class FolderViewModel : INotifyPropertyChanged, IExpandable
    {
        private List<FolderViewModel> _subItems;

        public FolderViewModel(string path)
        {
            ShortPath = System.IO.Path.GetFileName(path);
            if (string.IsNullOrEmpty(ShortPath))
                ShortPath = path;
            Path = path;
            SubItems = new List<FolderViewModel>();
        }

        public string Path { get; }

        public string ShortPath { get; }

        public List<FolderViewModel> SubItems
        {
            get => _subItems;
            set
            {
                _subItems = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void LoadChildren()
        {
            SubItems = Directory.GetDirectories(Path).Select(CreateDirectory).ToList();
        }

        public static FolderViewModel CreateDirectory(string path)
        {
            bool hasChildren;
            try
            {
                hasChildren = Directory.GetDirectories(path).Any();
            }
            catch (UnauthorizedAccessException)
            {
                hasChildren = false;
            }
            catch (IOException)
            {
                hasChildren = false;
            }

            var dir = new FolderViewModel(path);
            if (hasChildren)
            {
                dir.SubItems = new List<FolderViewModel> {new FolderViewModel("dummy")};
            }

            return dir;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}