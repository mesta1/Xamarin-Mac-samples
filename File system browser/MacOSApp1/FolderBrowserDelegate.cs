using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AppKit;
using Foundation;
using ObjCRuntime;
using WpfApp4;

namespace MacOSApp1
{
    public class FolderBrowserDelegate : NSBrowserDelegate
    {
        private readonly NSBrowser _browser;
        private readonly List<NSFolderViewModel> _items;

        public FolderBrowserDelegate(NSBrowser browser, List<NSFolderViewModel> items)
        {
            _browser = browser;
            _items = items;
            browser.SetCellClass(new Class(typeof(FolderViewCell)));
        }

        public override nint RowsInColumn(NSBrowser sender, nint column)
        {
            if (column == 0)
            {
                return _items.Count;
            }
            else
            {
                var row = (int)_browser.SelectedRow(column - 1);
                var cell = (FolderViewCell)ItemAtRow(row, (int)column - 1);
                cell.Dir.LoadChildren();
                return cell.Dir.SubItems.Count;
            }
        }

        public override void WillDisplayCell(NSBrowser sender, NSObject cell, nint row, nint column)
        {
            FolderViewCell customCell = (FolderViewCell)cell;
            if (column == 0)
            {
                customCell.Dir = _items[(int) row].Dir;
                customCell.Title = _items[(int) row].Dir.ShortPath;
                customCell.Leaf = !_items[(int) row].Dir.SubItems.Any();
            }
            else
            {
                var selectedRow = (int)_browser.SelectedRow(column - 1);
                var selectedCell = (FolderViewCell)ItemAtRow(selectedRow, (int)column - 1);
                Expand(selectedCell.Dir, (int)row, customCell);
            }
        }

        private void Expand(FolderViewModel dir, int row, FolderViewCell cell)
        {
            var subDir = dir.SubItems[row];
            cell.Dir = subDir;
            cell.Title = subDir.ShortPath;
            cell.Leaf = !subDir.SubItems.Any();
        }

        private NSObject ItemAtRow(int row, int col)
        {
            var matrix = _browser.MatrixInColumn(col);
            var cell = matrix.Cells[row];
            return cell;
        }
    }

    public class NSFolderViewModel : NSObject
    {
        public FolderViewModel Dir { get; }

        public NSFolderViewModel(FolderViewModel dir)
        {
            Dir = dir;
        }
    }

    public class FolderViewCell : NSBrowserCell
    {
        public FolderViewModel Dir { get; set; }

        public FolderViewCell(IntPtr intPtr) : base(intPtr)
        {
            
        }
    }
}