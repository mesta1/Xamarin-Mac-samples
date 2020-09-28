using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<FolderViewModel> SubItems { get; }

        public MainWindow()
        {
            InitializeComponent();
            SubItems = new ObservableCollection<FolderViewModel>();
            fileSystemTreeView.ItemsSource = Environment.GetLogicalDrives().Select(FolderViewModel.CreateDirectory);
            DataContext = this;
        }

        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            var dir = (IExpandable)((TreeViewItem)e.OriginalSource).DataContext;
            dir.LoadChildren();
        }
    }
}