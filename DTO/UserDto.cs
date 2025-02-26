using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    namespace DTO
    {
        public record UserByIdDto(
            [Required, EmailAddress] string email,
            [Required, MinLength(1)] string firstName,
            [Required, MinLength(1)] string lastName,
            [Required] List<returnOrdersListDto> orders
        );

        public record addUserDto(
            [Required, EmailAddress] string email,
            [Required, MinLength(1)] string firstName,
            [Required, MinLength(1)] string lastName,
            [Required, MinLength(6)] string password 
        );

        public record returnPostUserDto(
            [Required, EmailAddress] string email,
            [Required, MinLength(1)] string firstName,
            [Required, MinLength(1)] string lastName
        );

        public record returnLoginUserDto(
            [Required] int userId,
            [Required, EmailAddress] string email,
            [Required, MinLength(1)] string firstName,
            [Required, MinLength(1)] string lastName
        );

        public record LoginDto(
            [Required, EmailAddress] string email,
            [Required, MinLength(6)] string password
        );
    }






}
