using System.Collections.Generic;

namespace BlazingPizza.Client
{
    public class CatalogPage
    {
        public int Count { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public IEnumerable<CatalogItem> Data { get; set; }
    }
}