﻿using ETradeAPI.Domain.Entities.Common;

namespace ETradeAPI.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }

        public ICollection<Product> Products { get; set; }
        public Customer Customer { get; set; }
        public string Test { get; set; }
    }
}
