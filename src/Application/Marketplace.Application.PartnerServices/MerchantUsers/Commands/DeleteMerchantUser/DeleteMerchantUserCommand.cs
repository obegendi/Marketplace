using System;
using Marketplace.Common.Application.Commands;
using MediatR;

namespace Marketplace.Application.MerchantServices.MerchantUsers.Commands.DeleteMerchantUser
{
    public class DeleteMerchantUserCommand : CommandBase<bool>
    {
        public DeleteMerchantUserCommand(Guid merchantCode, Guid code)
        {
            MerchantCode = merchantCode;
            Code = code;
        }

        public Guid MerchantCode { get; }
        public Guid Code { get; }
    }

}
