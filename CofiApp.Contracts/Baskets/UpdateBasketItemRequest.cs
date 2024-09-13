﻿namespace CofiApp.Contracts.Baskets
{
    public class UpdateBasketItemRequest
    {
        public List<ProductOptionDto> ProductOptions { get; set; } = [];
        public int Quantity { get; set; }
    }
}
