using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.BLL.ViewModels.DTO
{
    public class Registration
    {
        [Required(ErrorMessage = "Please, enter name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, enter email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
