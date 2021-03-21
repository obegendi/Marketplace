using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.Order;
using Marketplace.Domain.SharedKernel;
using MediatR;

namespace Marketplace.Application.OrderServices.CreateOrder
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
    {
        private readonly IMerchantAddressRepository _merchantLocationsRepository;
        private readonly IMerchantRepository _merchantRepository;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IMerchantRepository merchantRepository,
            IMerchantAddressRepository merchantLocationsRepository)
        {
            _orderRepository = orderRepository;
            _merchantRepository = merchantRepository;
            _merchantLocationsRepository = merchantLocationsRepository;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.OrderProducts is null || !request.OrderProducts.Any())
                throw new ProductNotFoundException();
            if (request.OrderProducts.Any(x => x.Quantity < 1))
                throw new ProductItemShouldHaveAtLeastOneQuantityException();

            var merchant = await _merchantRepository.GetAsync(request.MerchantCode);
            var merchantAddress = await _merchantLocationsRepository.GetAsync(request.MerchantCode, request.MerchantAddressCode);

            var address = new Address
            {
                Code = merchantAddress.Code,
                FullAddress = merchantAddress.Location,
                City = merchantAddress.City,
                Town = merchantAddress.Town,
                District = merchantAddress.District,
                Name = merchantAddress.Name,
            };

            var order = Order.Create(merchant.Name, merchant.Code, request.FirstName, request.LastName, request.Phone,
                request.OrderNote, address, request.ReceiverAddress, request.OrderProducts);
            await _orderRepository.CreateAsync(order);
            return Unit.Task.Result;
        }
    }
}
