using System.ComponentModel.DataAnnotations;

namespace Catalog.DAL.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
