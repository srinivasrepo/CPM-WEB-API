using CustomerAPI.Core.Common;
using CustomerAPI.Core.CommonMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI.Core.Entities.Product
{
    public class ManageProduct
    {

       public int ProductID { get { return CommonStaticMethods.Decrypt<int>(EncProductID); } }

        public string EncProductID { get; set; }

        public string ProductName { get; set; }

       public decimal ProductCost { get; set; }

       public string Description { get; set; }


    }

    public class SearchProduct : Paging
    {

        public string Product { get; set; }

        public short StatusID { get; set; }
     
    }

    public class GetSearchProductDetails
    {

        public int ProductID { get; set; }

        public string EncProductID { get { return CommonStaticMethods.Encrypt(ProductID.ToString()); } }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public decimal ProductCost { get; set; }

        public string Status { get; set; }
              
    }

    public class ViewProductDetails
    {

        public string ProductName { get; set; }

        public decimal ProductCost { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public string ProductCode { get; set; }
    }

}
