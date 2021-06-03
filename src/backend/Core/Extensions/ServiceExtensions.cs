using System;
using System.Reflection;
using Core.Features.Customers.Commands;
using Core.Services.Implementations;
using Core.Services.Interfaces;
using Data.Identity.Implementations;
using Data.Repositories;
using Data.Repositories.Implementations;
using Data.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Models.Implementations;
using Models.Interfaces;
using Data.Messages;

namespace Core.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));

            services.AddScoped<IUniqueUCustomerChecker, UniqueCustomerChecker>();
            services.AddScoped<ICurrencyConverter, CurrencyConverter>();

            services.AddMediatR(typeof(RegisterCustomerCommandHandler).GetTypeInfo().Assembly);

            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IStoredEventRepository, StoredEventRepository>();
            services.AddScoped<IEventSerializer, EventSerializer>();

            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IMessagePublisher, MessagePublisher>();
            services.AddScoped<IMessageProcessor, MessageProcessor>();
        }
    }
}