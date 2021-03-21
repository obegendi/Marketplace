using LightInject;
using Marketplace.API.Infrastructure;
using Marketplace.Application.AuthenticationServices;
using Marketplace.Application.LocationServices;
using Marketplace.Application.MerchantServices.DomainServices;
using Marketplace.Application.MerchantServices.MerchantAddresses.Commands.AddMerchantAddress;
using Marketplace.Application.MerchantServices.MerchantUsers;
using Marketplace.Application.ProductServices;
using Marketplace.Data;
using Marketplace.Data.Repositories.Impls;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.Merchant.Rules;
using MediatR;
using MediatR.Pipeline;
using System.Reflection;
using Marketplace.Application.OrderServices;

namespace Marketplace.API
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            //TODO : add service referans

            serviceRegistry.Register<IMediator, Mediator>();

            serviceRegistry.RegisterAssembly(typeof(ProductDto).GetTypeInfo().Assembly, () => new PerScopeLifetime(), (serviceType, implemetingType) =>
                serviceType.IsConstructedGenericType && (
                    serviceType.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
                    serviceType.GetGenericTypeDefinition() == typeof(INotificationHandler<>)
                ));

            serviceRegistry.RegisterAssembly(typeof(AddMerchantAddressCommand).GetTypeInfo().Assembly, () => new PerScopeLifetime(),
                (serviceType, implemetingType) =>
                    serviceType.IsConstructedGenericType && (
                        serviceType.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
                        serviceType.GetGenericTypeDefinition() == typeof(INotificationHandler<>)
                    ));

            serviceRegistry.RegisterAssembly(typeof(MerchantUserDto).GetTypeInfo().Assembly, () => new PerScopeLifetime(), (serviceType, implemetingType) =>
                serviceType.IsConstructedGenericType && (
                    serviceType.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
                    serviceType.GetGenericTypeDefinition() == typeof(INotificationHandler<>)
                ));

            serviceRegistry.RegisterAssembly(typeof(CountryDto).GetTypeInfo().Assembly, () => new PerScopeLifetime(), (serviceType, implemetingType) =>
                serviceType.IsConstructedGenericType && (
                    serviceType.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
                    serviceType.GetGenericTypeDefinition() == typeof(INotificationHandler<>)
                ));

            serviceRegistry.RegisterAssembly(typeof(OrderDto).GetTypeInfo().Assembly, () => new PerScopeLifetime(), (serviceType, implemetingType) =>
                serviceType.IsConstructedGenericType && (
                    serviceType.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
                    serviceType.GetGenericTypeDefinition() == typeof(INotificationHandler<>)
                ));

            serviceRegistry.RegisterOrdered(typeof(IPipelineBehavior<,>),
                new[]
                {
                    typeof(RequestPreProcessorBehavior<,>),
                    typeof(RequestPostProcessorBehavior<,>),
                    typeof(GenericPipelineBehavior<,>)
                }, type => new PerContainerLifetime());

            serviceRegistry.RegisterOrdered(typeof(IRequestPostProcessor<,>),
                new[]
                {
                    typeof(GenericRequestPostProcessor<,>),
                    typeof(ConstrainedRequestPostProcessor<,>)

                }, type => new PerContainerLifetime()
            );

            serviceRegistry.Register(typeof(IRequestPreProcessor<>), typeof(GenericRequestPreProcessor<>));

            serviceRegistry.Register<ServiceFactory>(fac => fac.GetInstance);

            serviceRegistry.RegisterScoped<IMerchantUserUniquenessChecker, MerchantUserUniquenessChecker>();
            serviceRegistry.RegisterScoped<IMerchantRepository, MerchantRepository>();
            serviceRegistry.RegisterScoped<IMerchantAddressRepository, MerchantAddressRepository>();
            serviceRegistry.RegisterScoped<IMerchantUserRepository, MerchantUserRepository>();
            serviceRegistry.RegisterScoped<IAuthenticationService, AuthenticationService>();
            serviceRegistry.RegisterScoped<IProductRepository, ProductRepository>();
            serviceRegistry.RegisterScoped<IProductTagsRepository, ProductTagRepository>();
            serviceRegistry.RegisterScoped<IMerchantProductRepository, MerchantProductRepository>();
            serviceRegistry.RegisterScoped<ILocationRepository, LocationRepository>();
            serviceRegistry.RegisterScoped<IOrderRepository, OrderRepository>();
            serviceRegistry.Register<IMongoDbContext, MongoDbContext>(new PerContainerLifetime());
            serviceRegistry.Register<ILocationMongoDbContext, LocationMongoDbContext>(new PerContainerLifetime());
            serviceRegistry.RegisterScoped<IMerchantCustomerRepository, MerchantCustomerRepository>();
        }
    }
}
