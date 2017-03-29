using InventoryApp.Models;
using System;
using System.Windows;

namespace InventoryApp
{
    /// <summary>
    /// Interaction logic for ContactWindow.xaml
    /// </summary>
    public partial class ItemWindow : Window
    {

        public ItemWindow()
        {
            InitializeComponent();

            // Don't show this window in the task bar
            ShowInTaskbar = false;
        }

        public ItemModel Item { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Item != null)
            {
                uxSubmit.Content = "Update";
            }
            else
            {
                Item = new ItemModel();
                Item.CreatedDate = DateTime.Now;
            }

           uxGrid.DataContext = Item;  
        }

        private void uxSubmit_Click(object sender, RoutedEventArgs e)
        {
            // Because of the data binding in ItemWindow.xaml
            //   e.g.  Text="{Binding Price}"
            // .. no need to copy data back to Item instance.
            // .. Its already done by the data binding

            
            //// Done in Window_Loaded
            //// Item = new ItemModel();

            ////  Id value is assigned by the database
            ////  Item.Id = uxId.Text;
            
            //Item.Desc = uxDesc.Text;

            //double priceDouble;
            //if (Double.TryParse(uxPrice.Text, out priceDouble))
            //    Item.Price = priceDouble;
            //else
            //    Console.WriteLine("{0} is outside the range of a Double.",
            //                      uxPrice.Text);

            //Item.Quantity = Convert.ToInt32(uxQuantitySlider.Value);
 
            //double costDouble;
            //if (Double.TryParse(uxCost.Text, out costDouble))
            //    Item.Cost = costDouble;
            //else
            //    Console.WriteLine("{0} is outside the range of a Double.",
            //                      uxCost.Text);

            ////  Value = Cost * Quantity  and is calculated by the database
            //// Item.Value = uxValue.Text;

            //// Done in Window_Loaded
            //// Item.CreatedDate = DateTime.Now;
            
            DialogResult = true;
            Close();
        }

        private void uxCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
 