﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Catalog.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Password { get; set; }
        
        public string Email { get; set; }

        public int? Roleid { get; set; }

        public Role UserRole { get; set; }
    }
}
