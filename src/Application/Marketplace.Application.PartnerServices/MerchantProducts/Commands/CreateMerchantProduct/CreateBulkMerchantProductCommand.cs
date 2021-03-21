using System;
using System.Collections.Generic;
using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.MerchantServices.MerchantProducts.Commands.CreateMerchantProduct
{
    public class CreateBulkMerchantProductCommand : CommandBase
    {
        public CreateBulkMerchantProductCommand(List<MerchantProductDto> createMerchantProductList, Guid merchantCode)
        {
            CreateMerchantProductList = createMerchantProductList;
            MerchantCode = merchantCode;
        }

        public List<MerchantProductDto> CreateMerchantProductList { get; set; }
        public Guid MerchantCode { get; set; }
    }
}
