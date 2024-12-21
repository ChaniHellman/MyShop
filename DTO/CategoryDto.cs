using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record getCategoryDto(string categoryName, List<ProductDto> products);

}
