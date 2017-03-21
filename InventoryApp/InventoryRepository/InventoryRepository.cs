using InventoryDB;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace InventoryRepository
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }
        public double Value { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }

    public class InventoryRepository
    {
        public ItemModel Add(ItemModel itemModel)
        {
            var inventoryDb = ToDbModel(itemModel);

            DatabaseManager.Instance.Items.Add(inventoryDb);

            try
            {
                DatabaseManager.Instance.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }

            itemModel = new ItemModel
            {
                Id = inventoryDb.ItemId,
                Desc = inventoryDb.ItemDesc,
                Price = inventoryDb.ItemPrice,
                Quantity = inventoryDb.ItemQuantity,
                Cost = inventoryDb.ItemCost,
                Value = inventoryDb.ItemValue,
                CreatedDate = inventoryDb.ItemCreatedDate
            };
            return itemModel;
        }

        public List<ItemModel> GetAll()
        {
            // Use .Select() to map the database contacts to ContactModel
            var items = DatabaseManager.Instance.Items
              .Select(t => new ItemModel
              {
                  Id = t.ItemId,
                  Desc = t.ItemDesc,
                  Price = t.ItemPrice,
                  Quantity = t.ItemQuantity,
                  Cost = t.ItemCost,
                  Value = t.ItemValue,
                  CreatedDate = t.ItemCreatedDate
              }).ToList();

            return items;
        }

        public bool Update(ItemModel itemModel)
        {
            var original = DatabaseManager.Instance.Items.Find(itemModel.Id);

            if (original != null)
            {
                DatabaseManager.Instance.Entry(original).CurrentValues.SetValues(ToDbModel(itemModel));

                // &&&
                try
                {
                    DatabaseManager.Instance.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    // Throw a new DbEntityValidationException with the improved exception message.
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                }
            }

            return false;
        }

        public bool Remove(int contactId)
        {
            var items = DatabaseManager.Instance.Items
                                .Where(t => t.ItemId == contactId);

            if (items.Count() == 0)
            {
                return false;
            }

            DatabaseManager.Instance.Items.Remove(items.First());
            DatabaseManager.Instance.SaveChanges();

            return true;
        }

        private Item ToDbModel(ItemModel itemModel)
        {
            var inventoryDb = new Item
            {
                ItemId = itemModel.Id,
                ItemDesc = itemModel.Desc,
                ItemPrice = itemModel.Price,
                ItemQuantity = itemModel.Quantity,
                ItemCost = itemModel.Cost,
                ItemValue = itemModel.Value,
                ItemCreatedDate = itemModel.CreatedDate
            };

            return inventoryDb;
        }
    }
}