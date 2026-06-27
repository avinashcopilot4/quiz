using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO.User
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Role { get; set; } = null!;

        public bool IsActive { get; set; }

    }
}
