using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record ProductDto(int productId, string productName, decimal price, string description, string imageUrl, string categoryCategoryName);
    
}
