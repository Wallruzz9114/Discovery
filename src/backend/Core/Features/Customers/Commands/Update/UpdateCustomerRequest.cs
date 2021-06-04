using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Features.Customers.Commands.Update
{
    public class UpdateCustomerRequest
    {
        [Required(ErrorMessage = "The {0} field is mandatory")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 5)]
        public string Name { get; set; }
    }
}