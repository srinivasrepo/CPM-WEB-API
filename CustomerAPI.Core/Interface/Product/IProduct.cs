using CustomerAPI.Core.Common;
using CustomerAPI.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI.Core.Interface.Product
{
    public interface IProduct
    {
        string ManageProduct(ManageProduct obj);

        SearchResults<GetSearchProductDetails> SearchProduct(SearchProduct obj);

        ViewProductDetails ViewProduct(int productID);
    }
}
