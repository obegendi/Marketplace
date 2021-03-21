using System.Runtime.CompilerServices;
using AutoMapper;
using Marketplace.Application.MerchantServices;
using Marketplace.Application.MerchantServices.MerchantAddresses.Queries.GetMerchantAddress;
using Marketplace.Application.MerchantServices.MerchantCustomers;
using Marketplace.Application.MerchantServices.MerchantUsers;
using Marketplace.Application.OrderServices;
using Marketplace.Application.ProductServices;
using Marketplace.Domain.Merchant;
using Marketplace.Domain.Merchant.Customer;
using Marketplace.Domain.Order;
using Marketplace.Domain.Product;

namespace Marketplace.API
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<MerchantAddress, MerchantAddressDto>(); // means you want to map from User to UserDTO
            CreateMap<Product, ProductDto>();
            CreateMap<MerchantProduct, MerchantProductDto>();
            CreateMap<MerchantUser, MerchantUserDto>();
            CreateMap<MerchantCustomer, CustomerDto>();
            CreateMap<Order, OrderDto>()
                .ForMember(x => x.ProductItemList,
                    y =>
                        y.MapFrom(w => w.OrderProducts))
                .ForMember(x => x.StateList,
                    y =>
                        y.MapFrom(w => w.States))
                .ForMember(x => x.CanceledProductItemList,
                y => 
                    y.MapFrom(w => w.CanceledOrderProducts));
        }
    }
}
