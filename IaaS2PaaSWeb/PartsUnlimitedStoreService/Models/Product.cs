using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartsUnlimitedStoreService.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string SkuNumber { get; set; }

        public int CategoryId { get; set; }

        public int RecommendationId { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public decimal SalePrice { get; set; }

        public string ProductArtUrl { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public int Inventory { get; set; }

        public int LeadTime { get; set; }

        public Dictionary<string, string> ProductDetailList { get; set; }
    }
}
