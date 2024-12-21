using System;
using System.Collections.Generic;

namespace OurShop.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public int CategoryId { get; set; }

    public string? Description { get; set; }

    public virtual Category Category { get; set; } = null!;
}
