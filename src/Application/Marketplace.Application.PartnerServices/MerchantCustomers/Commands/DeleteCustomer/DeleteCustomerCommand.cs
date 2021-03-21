using System;
using System.Collections.Generic;
using System.Text;
using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.MerchantServices.MerchantCustomers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : CommandBase<bool>
    {
        public Guid Code { get; set; }

        public DeleteCustomerCommand(Guid code)
        {
            Code = code;
        }
    }
}
