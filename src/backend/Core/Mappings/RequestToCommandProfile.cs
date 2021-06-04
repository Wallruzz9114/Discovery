using AutoMapper;
using Core.Features.Customers.Commands.Register;
using Core.Features.Customers.Commands.Update;
using Core.ViewModels;

namespace Core.Mappings
{
    public class RequestToCommandProfile : Profile
    {
        public RequestToCommandProfile()
        {
            CreateMap<RegisterCustomerRequest, RegisterCustomerCommand>()
                .ConstructUsing(c => new RegisterCustomerCommand(c.Email, c.Name, c.Password, c.PasswordConfirm));

            CreateMap<UpdateCustomerRequest, UpdateCustomerCommand>()
                .ConstructUsing(c => new UpdateCustomerCommand(c.Id, c.Name));
        }
    }
}