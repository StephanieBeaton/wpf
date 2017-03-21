using InventoryDB;

namespace InventoryRepository
{
    public class DatabaseManager
    {
        private static readonly InventoryEntities entities;

        // Initialize and open the database connection
        static DatabaseManager()
        {
            entities = new InventoryEntities();
            entities.Database.Connection.Open();
        }

        // Provide an accessor to the database
        public static InventoryEntities Instance
        {
            get
            {
                return entities;
            }
        }
    }
}