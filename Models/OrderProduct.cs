﻿using WebApplication1.Models;

namespace EComDemo.Models;

public class OrderProduct
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }

    public int Qty { get; set; } = 0;
}