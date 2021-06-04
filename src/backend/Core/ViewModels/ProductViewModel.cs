using System;
using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels
{
    public class ProductViewModel
    {
        [Required(ErrorMessage = "The {0} field is mandatory")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Price value is invalid")]
        public int Quantity { get; set; }
    }
}