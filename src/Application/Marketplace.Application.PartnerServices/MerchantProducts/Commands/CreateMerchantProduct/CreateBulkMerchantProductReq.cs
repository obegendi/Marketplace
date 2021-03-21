using System.Collections.Generic;

namespace Marketplace.Application.MerchantServices.MerchantProducts.Commands.CreateMerchantProduct
{
    public class CreateBulkMerchantProductReq
    {
        public List<MerchantProductReqDto> ProductList { get; set; }
    }
}
