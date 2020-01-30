using CustomerAPI.Core.Common;
using CustomerAPI.Core.CommonMethods;
using CustomerAPI.Core.Entities.Customer;
using CustomerAPI.Core.Interface.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CustomerAPI.Web.Controllers.Customer
{
    public class CustomerController : ApiController
    {
        ICustomer cs;

        public CustomerController(ICustomer cs)
        {
            this.cs = cs;
        }

        [HttpPost]
        [Route("ManageCustomer")]
        public string ManageCustomer(ManageCustomer obj)
        {
            return cs.ManageCustomer(obj);
        }

        [HttpPost]
        [Route("SearchCustomer")]
        public SearchResults<GetCustomerDetails> GetCustomerDetails(SearchCustomer obj)
        {
            return cs.GetCustomerDetails(obj);
        }

        [HttpGet]
        [Route("ViewCustomer")]
        public ViewCustomerDetails ViewCustomerDetails(string encCustomerID)
        {
            return cs.ViewCustomerDetails(CommonStaticMethods.Decrypt<int>(encCustomerID));
        }

        [HttpGet]
        [Route("GetAssignCustomerProducts")]
        public GetAssignedProductsList GetAssignCustomerProducts(string encCustomerID)
        {
            return cs.GetAssignCustomerProducts(CommonStaticMethods.Decrypt<int>(encCustomerID));
        }

        [HttpPost]
        [Route("ManageAssignedCustomerProducts")]
        public string ManageAssignedProducts(ManageAssignedProducts obj)
        {
            return cs.ManageAssignedProducts(obj);
        }

        [HttpGet]
        [Route("GetCategoryItems")]
        public GetCategoryItemList GetCategoryItem(string categoryCode)
        {
            return cs.GetCategoryItem(categoryCode);
        }
    }
}
