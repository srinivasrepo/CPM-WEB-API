using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAPI.Core.Entities;
using CustomerAPI.Core.Common;
using CustomerAPI.Core.Entities.Customer;

namespace CustomerAPI.Core.Interface.Customer
{
    public interface ICustomer
    {
        string ManageCustomer(ManageCustomer obj);

        SearchResults<GetCustomerDetails> GetCustomerDetails(SearchCustomer obj);

         ViewCustomerDetails ViewCustomerDetails(int customerID);

        GetCategoryItemList GetCategoryItem(string categoryCode);

        GetAssignedProductsList GetAssignCustomerProducts(int customerID);

        string ManageAssignedProducts(ManageAssignedProducts obj);
       
    }
}
