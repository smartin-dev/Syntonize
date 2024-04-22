using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManagement.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}