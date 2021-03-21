using Marketplace.Common.Application.Commands;
using System;

namespace Marketplace.Application.CustomerServices.Commands.DeleteCustomerAddress
{
    public class DeleteAddressCommand : CommandBase<bool>
    {
        public Guid Code { get; set; }
        public Guid AddressCode { get; set; }

        public DeleteAddressCommand(Guid code, Guid addressCode)
        {
            Code = code;
            AddressCode = addressCode;
        }
    }
}
