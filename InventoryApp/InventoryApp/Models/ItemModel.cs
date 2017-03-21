using System;

namespace InventoryApp.Models
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

        public InventoryRepository.ItemModel ToRepositoryModel()
        {
            var repositoryModel = new InventoryRepository.ItemModel
            {
                Id = Id,
                Desc = Desc,
                Price = Price,
                Quantity = Quantity,
                Cost = Cost,
                Value = Value,
                CreatedDate = CreatedDate
            };

            return repositoryModel;
        }

        public static ItemModel ToModel(InventoryRepository.ItemModel respositoryModel)
        {
            var itemModel = new ItemModel
            {
                Id = respositoryModel.Id,
                Desc = respositoryModel.Desc,
                Price = respositoryModel.Price,
                Quantity = respositoryModel.Quantity,
                Cost = respositoryModel.Cost,
                Value = respositoryModel.Value,
                CreatedDate = respositoryModel.CreatedDate
            };

            return itemModel;
        }
    }
}