using System;
using System.Collections.Generic;
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



using InventoryApp.Models;
using System.ComponentModel;    // ListSortDirection

namespace InventoryApp
{
    // ===============================================================================================
    //
    //  SortAdorner class
    //
    //  This class draws a triangle, 
    //  ... either pointing up or down, 
    //  ... depending on the sort direction in the column header
    //
    //          http://www.wpf-tutorial.com/listview-control/listview-how-to-column-sorting/
    //
    //  Used in MainWindow method "lvItemsColumnHeader_Click()" 
    // 
    // ===============================================================================================

    /// <summary>
    /// Draws triangle in column header after user clicks on column header to sort
    /// </summary>
    public class SortAdorner : Adorner
    {
        private static Geometry ascGeometry =
                Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");

        private static Geometry descGeometry =
                Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");

        public ListSortDirection Direction { get; private set; }

        public SortAdorner(UIElement element, ListSortDirection dir)
                : base(element)
        {
            this.Direction = dir;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (AdornedElement.RenderSize.Width < 20)
                return;

            TranslateTransform transform = new TranslateTransform
                    (
                            AdornedElement.RenderSize.Width - 15,
                            (AdornedElement.RenderSize.Height - 5) / 2
                    );
            drawingContext.PushTransform(transform);

            Geometry geometry = ascGeometry;
            if (this.Direction == ListSortDirection.Descending)
                geometry = descGeometry;
            drawingContext.DrawGeometry(Brushes.Black, null, geometry);

            drawingContext.Pop();
        }
    }

    // ============================= end of SortAdorner class =========================================






    // ===============================================================================================
    //
    //  MainWindow class
    //
    //  Display ListView of Items here
    //
    // ===============================================================================================
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // ===============================================================================================
        //   private instance variables
        // ===============================================================================================
        #region private instance variables

        // for deleting or updating a specific selected Item in the ListView
        private ItemModel selectedItem;

        //  for sorting Columns
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        #endregion
        // ===============================================================================================


        // ===============================================================================================
        //   Constructors
        // ===============================================================================================
        #region Constructors
        public MainWindow()
        {
            InitializeComponent();

            LoadItems();
        }
        #endregion
        // ===============================================================================================



        // ===============================================================================================
        //   LoadItems method
        // ===============================================================================================
        #region LoadItems
        private void LoadItems()
        {
            var items = App.InventoryRepository.GetAll();

            uxItemList.ItemsSource = items
                .Select(t => ItemModel.ToModel(t))
                .ToList();

            toolBarButtonUpdate.IsEnabled = false;
            toolBarButtonDelete.IsEnabled = false;

            double totalValue = items.Sum(t => t.Quantity * t.Price);

            double totalCost = items.Sum(t => t.Quantity * t.Cost);

            uxTotalValueStatus.Text = String.Format("Total Value: {0:C}", totalValue);

            uxProgressBar.Value = (totalCost / totalValue ) * 100;

            uxPercentStatus.Text = String.Format("{0:C}% - (Total Cost / Total Value)", uxProgressBar.Value);

            // OR
            //var uiContactModelList = new List<ContactModel>();
            //foreach (var repositoryContactModel in contacts)
            //{
            //    This is the .Select(t => ... )
            //    var uiContactModel = ContactModel.ToModel(repositoryContactModel);
            //
            //    uiContactModelList.Add(uiContactModel);
            //}

            //uxContactList.ItemsSource = uiContactModelList;
        }
        #endregion
        // ===============================================================================================



        // ===============================================================================================
        //   uxItemNew_Click event handler method
        // ===============================================================================================
        #region uxItemNew_Click
        private void uxItemNew_Click(object sender, RoutedEventArgs e)
        {

            var window = new ItemWindow();

            if (window.ShowDialog() == true)
            {
                var uiItemModel = window.Item;

                var repositoryItemModel = uiItemModel.ToRepositoryModel();

                App.InventoryRepository.Add(repositoryItemModel);

                // OR
                //App.ContactRepository.Add(window.Contact.ToRepositoryModel());

                LoadItems();
            }
        }
        #endregion
        // ===============================================================================================



        // ===============================================================================================
        //   uxItemUpdate_Click event handler method
        // ===============================================================================================
        #region uxItemUpdate
        private void uxItemUpdate_Click(object sender, RoutedEventArgs e)
        {

            var window = new ItemWindow();
            window.Item = selectedItem.Clone();  

            if (window.ShowDialog() == true)
            {
                App.InventoryRepository.Update(window.Item.ToRepositoryModel());
                LoadItems();
            }

        }
        #endregion
        // ===============================================================================================



        // ===============================================================================================
        //   uxItemDelete_Click event handler method
        // ===============================================================================================
        #region uxItemDelete_Click
        private void uxItemDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Delete clicked");

            App.InventoryRepository.Remove(selectedItem.Id);
            selectedItem = null;
            LoadItems();

        }
        #endregion
        // ===============================================================================================



        // ===============================================================================================
        //   uxQuit_Click event handler method
        // ===============================================================================================
        #region uxQuit_Click
        private void uxQuit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion
        // ===============================================================================================



        // ===============================================================================================
        //   uxMenuItemList_Click event handler method
        // ===============================================================================================
        #region uxMenuItemList_Click
        private void uxMenuItemList_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("List clicked");
        }
        #endregion
        // ===============================================================================================




        // ===============================================================================================
        //   lvItemsColumnHeader_Click
        //
        //       Column Header Click event handler method
        //
        // ===============================================================================================
        #region lvItemsColumnHeader_Click
        private void lvItemsColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Items List column header clicked to Sort");

            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();

            // if User already clicked on a column to sort after the window opened
            // ... remove the old sort
            if (listViewSortCol != null)
            {
                // Remove the object that draws triangle in column header
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                uxItemList.Items.SortDescriptions.Clear();
            }

            // default the sort direction to Ascending
            ListSortDirection newDir = ListSortDirection.Ascending;
            // if the same column is being sorted as the last sort 
            // .. and if the direction is the same as the last sort
            // .. change the direction
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            // set private instance variable that holds column value from local column variable
            listViewSortCol = column;
            // create object that draws triangle in column header, pass in the column and direction
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            // add the SortAdorner to the AdornerLayer of the column header
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);

            // sort the uxItemList by passing in column to sort on and sort direction
            uxItemList.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));

        }
        #endregion
        // ===============================================================================================




        // ===============================================================================================
        //   xItemList_SelectionChanged
        //
        //       change instance variable when selection Item in ListView changes
        //
        // ===============================================================================================
        #region xItemList_SelectionChanged
        private void uxItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = (ItemModel)uxItemList.SelectedValue;
            toolBarButtonUpdate.IsEnabled = true;
            toolBarButtonDelete.IsEnabled = true;
        }
        #endregion
        // ===============================================================================================



        // ===============================================================================================
        //   uxItemDelete_Loaded
        //
        //       Enable or disable "Delete Item" menu item and Context menu item
        //
        // ===============================================================================================
        #region uxItemDelete_Loaded
        private void uxItemDelete_Loaded(object sender, RoutedEventArgs e)
        {
            uxItemDelete.IsEnabled = (selectedItem != null);
            uxContextItemDelete.IsEnabled = uxItemDelete.IsEnabled;

        }
        #endregion
        // ===============================================================================================



        // ===============================================================================================
        //   uxItemUpdate_Loaded
        //
        //       Enable or disable "Update Item" menu item  and Context menu item
        //
        // ===============================================================================================
        #region uxItemUpdate_Loaded
        private void uxItemUpdate_Loaded(object sender, RoutedEventArgs e)
        {
            uxItemUpdate.IsEnabled = (selectedItem != null);
            uxContextItemUpdate.IsEnabled = uxItemUpdate.IsEnabled;
        }
        #endregion
        // ===============================================================================================




        // ===============================================================================================
        //
        //   Event Handlers for Short Cut Key Commands
        //
        // ===============================================================================================
        #region Commands for Short Cut Keys

        // Click Ctrl-N to execute the shortcut.
        private void OnNew_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Set this to false if the New command is not available
            e.CanExecute = true;
        }

        // Click Ctrl-U to execute the shortcut.
        private void OnUpdate_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Set this to false if the Update command is not available
            e.CanExecute = (selectedItem != null);
        }

        // Click Ctrl-D to execute the shortcut.
        private void OnDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Set this to false if the Delete command is not available
            e.CanExecute = (selectedItem != null);
        }

        private void uxItemList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            uxItemUpdate_Click(sender, null);  
        }
        #endregion
        // ===============================================================================================

    }

    // StackOverflow on Short Cut Keys and Commands
    // http://stackoverflow.com/questions/4682915/defining-menuitem-shortcuts

    // ================================ end of MainWindow class ========================================================

}
