using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record OrderItemDto(int OrderItemId, int quantity, int productId);

}
