using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public record UserByIdDto(string email, string firstName, string lastName, List<returnOrdersListDto> orders);
   public record addUserDto(string email, string firstName, string lastName, string password);
   public record returnPostUserDto(string email, string firstName, string lastName);
    public record returnLoginUserDto(int userId, string email, string firstName, string lastName);




}
