﻿namespace Basket.API.Models;

public class ShoppingCartItem
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public string Color { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
