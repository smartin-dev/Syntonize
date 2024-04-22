using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManagement.Data.Models
{

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; } // Store hashed password
        public byte[] Salt { get; set; } // Store salt for password hashing
    }
}