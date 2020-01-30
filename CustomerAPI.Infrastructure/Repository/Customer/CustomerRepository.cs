using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAPI.Infrastructure.Context;
using CustomerAPI.Core.Entities;
using CustomerAPI.Core.Interface.Customer;
using CustomerAPI.Core.Common;
using CustomerAPI.Core.Entities.Customer;
using System.Data.Entity.Infrastructure;

namespace CustomerAPI.Infrastructure.Repository.Customer
{
    public class CustomerRepository : ICustomer
    {
        TrainingContext context = new TrainingContext();
        DBHelper ctx = new DBHelper();

        public SearchResults<GetCustomerDetails> GetCustomerDetails(SearchCustomer obj)
        {
            var cmd = ctx.PrepareCommand(context);
            var list = new SearchResults<GetCustomerDetails>();
            ctx.PrepareProcedure(cmd, "customers.uspSearchCustomer");
            if (!string.IsNullOrEmpty(obj.CustomerName))
                ctx.AddInParameter<string>(cmd, "@CustomerName", obj.CustomerName);
            if (obj.StatusID > default(int))
                ctx.AddInParameter<int>(cmd, "@StatusID",obj.StatusID);
            if (!string.IsNullOrEmpty(obj.CustomerType))
                ctx.AddInParameter<string>(cmd, "@CustomerType", obj.CustomerType);
            ctx.AddInParameter<int>(cmd, "@PageIndex", obj.PageIndex);
            ctx.AddInParameter<int>(cmd, "@PageSize", obj.PageSize);
            using (var read = cmd.ExecuteReader())
            {
                var row = ((IObjectContextAdapter)context).ObjectContext.Translate<int>(read);
                foreach (var r in row)
                    list.TotalNumberOfRows = r;

                read.NextResult();

                List < GetCustomerDetails > lst = new List<GetCustomerDetails>();
                var customer = ((IObjectContextAdapter)context).ObjectContext.Translate<GetCustomerDetails>(read);
                foreach (var cus in customer)
                    lst.Add(cus);

                list.SearchList=lst;
                cmd.Connection.Close();
            }
            ctx.CloseConnection(context);
            return list;
        }

        public string ManageCustomer(ManageCustomer obj)
        {
            var retCode = string.Empty;

            var cmd = ctx.PrepareCommand(context);
            ctx.PrepareProcedure(cmd, "customers.uspManageCustomer");
            
            if (obj.CustomerID > default(int))
                ctx.AddInParameter<int>(cmd, "@CustomerID", obj.CustomerID);
            ctx.AddInParameter<string>(cmd, "@CustomerName", obj.CustomerName);
            ctx.AddInParameter<int>(cmd, "@CustomerType", obj.CustomerType);
            ctx.AddInParameter<string>(cmd, "@AddressMasterXML", obj.AddressMasterXML);
            ctx.AddOutParameter(cmd, "@RetCode", System.Data.DbType.String, 25);
            cmd.ExecuteNonQuery();
            retCode = ctx.GetOutputParameterValue(cmd,"@RetCode");

            cmd.Connection.Close();
            ctx.CloseConnection(context);

            return retCode;

        }

        public ViewCustomerDetails ViewCustomerDetails(int customerID)
        {
            var obj = new ViewCustomerDetails();

            var cmd = ctx.PrepareCommand(context);
            ctx.PrepareProcedure(cmd, "customers.uspViewCustomers");
            ctx.AddInParameter<int>(cmd, "@CustomerID", customerID);
            using (var read = cmd.ExecuteReader())
            {
                var row = ((IObjectContextAdapter)context).ObjectContext.Translate<ViewCustomerDetails>(read);
                foreach (var r in row)
                    obj = r;

                read.NextResult();

                var add = ((IObjectContextAdapter)context).ObjectContext.Translate<CustomerAddress>(read);

                obj.AddressList = new List<CustomerAddress>();

                foreach (var r in add)
                    obj.AddressList.Add(r);
    
                cmd.Connection.Close();
            }

            ctx.CloseConnection(context);
            return obj;
        }

        public GetAssignedProductsList GetAssignCustomerProducts(int customerID)
        {
            var list = new GetAssignedProductsList();

            var cmd = ctx.PrepareCommand(context);
            ctx.PrepareProcedure(cmd, "customers.uspGetAssignCustomerProducts");
            ctx.AddInParameter<int>(cmd, "@CustomerID", customerID);

            using (var read = cmd.ExecuteReader())
            {
                var row = ((IObjectContextAdapter)context).ObjectContext.Translate<GetAssignedProducts>(read);
                foreach (var r in row)
                    list.Add(r);

                cmd.Connection.Close();
            }

            ctx.CloseConnection(context);
            return list;
        }

      public string ManageAssignedProducts(ManageAssignedProducts obj)
        {
            var retCode = string.Empty;

            var cmd = ctx.PrepareCommand(context);
            ctx.PrepareProcedure(cmd, "customers.uspSaveProducts");
            ctx.AddInParameter<int>(cmd,"@CustomerID",obj.CustomerID);
            ctx.AddInParameter<string>(cmd, "@ProductMasterXML", obj.ProductMasterXML);
            ctx.AddOutParameter(cmd, "@RetCode", System.Data.DbType.String, 25);
            cmd.ExecuteNonQuery();
            retCode = ctx.GetOutputParameterValue(cmd,"@RetCode");
            ctx.CloseConnection(context);
            return retCode;
        }


        public GetCategoryItemList GetCategoryItem(string categoryCode)
        {
            var list = new GetCategoryItemList();

            var cmd = ctx.PrepareCommand(context);
            ctx.PrepareProcedure(cmd, "customers.uspGetAssignCategories");
            ctx.AddInParameter<string>(cmd, "@CategoryCode", categoryCode);

            using (var read = cmd.ExecuteReader())
            {
                var row = ((IObjectContextAdapter)context).ObjectContext.Translate<CategoryItem>(read);
                foreach (var r in row)
                    list.Add(r);
                cmd.Connection.Close();
            }

            ctx.CloseConnection(context);
            return list;

        }

    }
}
