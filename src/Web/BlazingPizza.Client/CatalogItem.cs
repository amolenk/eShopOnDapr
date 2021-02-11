using System.Collections.Generic;

namespace BlazingPizza.Client
{
    public class CatalogItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUri { get; set; }

        public string GetFormattedBasePrice() => Price.ToString("0.00");
    }
}