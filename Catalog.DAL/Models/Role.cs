using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalog.DAL.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }

        public Role()
        {
            Users = new List<User>();
        }

    }
}
