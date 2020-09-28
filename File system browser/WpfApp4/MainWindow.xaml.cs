using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Dir> SubItems { get; }

        public MainWindow()
        {
            InitializeComponent();
            SubItems = new ObservableCollection<Dir>();
            fileSystemTreeView.ItemsSource = Environment.GetLogicalDrives().Select(Dir.CreateDirectory);
            DataContext = this;
        }

        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            var dir = (IExpandable) ((TreeViewItem) e.OriginalSource).DataContext;
            dir.LoadChildren();
        }
    }
}