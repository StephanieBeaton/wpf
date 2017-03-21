using System;
using System.Windows;
using System.Data.Entity.Validation;

namespace InventoryApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
        private static InventoryRepository.InventoryRepository inventoryRepository;

        public App()
        {
            inventoryRepository = new InventoryRepository.InventoryRepository();

            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
        }

        private void Current_DispatcherUnhandledException(object sender,
                               System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.Exception;

            MessageBox.Show(ex.Message, "Unhandled Exception");
            e.Handled = true;
        }

        public static InventoryRepository.InventoryRepository InventoryRepository
        {
            get
            {
                return inventoryRepository;
            }
        }
    }

}
