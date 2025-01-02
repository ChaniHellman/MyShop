using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record getCategoryDto(int categoryId,string categoryName, List<ProductDto> products);

}
