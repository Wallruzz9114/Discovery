using System.Collections.Generic;
using Core.ViewModels;

namespace Core.Features.Customers.Commands.PlaceOrder
{
    public class PlaceOrderRequest
    {
        public List<ProductViewModel> Products { get; set; }
        public string Currency { get; set; }
    }
}