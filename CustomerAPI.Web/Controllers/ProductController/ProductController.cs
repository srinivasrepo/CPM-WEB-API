using CustomerAPI.Core.Common;
using CustomerAPI.Core.CommonMethods;
using CustomerAPI.Core.Entities.Product;
using CustomerAPI.Core.Interface.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CustomerAPI.Web.Controllers.ProductController
{
    public class ProductController : ApiController
    {
        IProduct pc;
        public ProductController(IProduct pc)
        {
            this.pc = pc;
        }

        [HttpPost]
        [Route("ManageProduct")]
        public string ManageProduct(ManageProduct obj)
        {
            return pc.ManageProduct(obj);
        }

        [HttpPost]
        [Route("SearchProduct")]
        public SearchResults<GetSearchProductDetails> SearchProduct(SearchProduct obj)
        {
            return pc.SearchProduct(obj);
        }

        [HttpGet]
        [Route("ViewProduct")]
        public ViewProductDetails ViewProduct(string encProductID)
        {
            return pc.ViewProduct(CommonStaticMethods.Decrypt<int>(encProductID));
        }

    }
}
