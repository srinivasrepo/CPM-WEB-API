using CustomerAPI.Core.Common;
using CustomerAPI.Core.CommonMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CustomerAPI.Core.Entities.Customer
{
    public class ManageCustomer
    {
        public int CustomerID { get { return CommonStaticMethods.Decrypt<int>(EncCustomerID); } }

        public string EncCustomerID { get; set; }

        public string CustomerName { get; set; }

        public int CustomerType { get; set; }

        public CustomerAddressList List { get; set; }

        public string AddressMasterXML { get { return CommonStaticMethods.Serialize<CustomerAddressList>(List); } }


    }

    [XmlType("ITEM")]
    public class CustomerAddress
    {
        public int AddressID { get; set; }

        public string Address { get; set; }

        public int Country { get; set; }

        public string CountryName { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }
    }

    [XmlType("RT")]
    public class CustomerAddressList : List<CustomerAddress> { }

    public class SearchCustomer : Paging
    {
        public string CustomerName { get; set; }

        public short StatusID { get; set; }

        public string CustomerType { get; set; }


    }

    public class GetCustomerDetails
    {
        public int CustomerID { get; set; }

        public string EncCustomerID { get { return CommonStaticMethods.Encrypt(CustomerID.ToString()); } }

        public string CustomerCode { get; set; }

        public string CustomerName { get; set; }

        public string Status { get; set; }

        public string CustomerType { get; set; }
    }

    public class ViewCustomerDetails
    {
        public string CustomerName { get; set; }

        public string CustomerCode { get; set; }

        public string CustomerType { get; set; }

        public string Status { get; set; }

        public string AssignedProducts { get; set; }

        public List<CustomerAddress> AddressList { get; set; }

       
    }

    public class GetAssignedProducts
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public bool IsSelect { get; set; }

    }

    public class GetAssignedProductsList : List<GetAssignedProducts> { }

    public class CategoryItem
    {

        public int CatItemID { get; set; }

        public string CatItem { get; set; }

    }

    public class GetCategoryItemList : List<CategoryItem> { }


    public class ManageAssignedProducts
    {

        public int CustomerID { get { return CommonStaticMethods.Decrypt<int>(encCustomerID); } }

        public string encCustomerID { get; set; }

        public ManageAssignedProductList List { get; set; }

        public string ProductMasterXML { get { return CommonStaticMethods.Serialize<ManageAssignedProductList>(List); } }
    }

    [XmlType("ITEM")]
    public class ManageAssignedProductsxml
    {
        public int ProductID { get; set; }

    }

    [XmlType("RT")]
    public class ManageAssignedProductList : List<ManageAssignedProductsxml> { }



   
}
