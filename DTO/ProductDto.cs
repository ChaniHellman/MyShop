using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record ProductDto(string productName, decimal price, string description, string categoryCategoryName);
    
}
