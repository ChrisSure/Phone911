using System.Collections.Generic;

namespace Phone.Data.Entities.Catalog
{
    public class ProductOrder
    {
        /// <summary>
        /// Order Id
        /// </summary>
        public int OrderId { get; set; }
        public Order Order { get; set; }

        /// <summary>
        /// Product Id
        /// </summary>
        public int ProductId { get; set; }
        public Product Product { get; set; }

        /// <summary>
        /// Count product
        /// </summary>
        public short Count { get; set; }

    }
}
