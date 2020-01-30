using CustomerAPI.Core.Common;
using CustomerAPI.Core.Entities.Product;
using CustomerAPI.Core.Interface.Product;
using CustomerAPI.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI.Infrastructure.Repository.Product
{
    public class ProductRespository : IProduct
    {
        TrainingContext context = new TrainingContext();
        DBHelper ctx = new DBHelper();

        public string ManageProduct(ManageProduct obj)
        {
            var retCode = string.Empty;

            var cmd = ctx.PrepareCommand(context);
            ctx.PrepareProcedure(cmd, "customers.uspManageProduct");
            if (obj.ProductID > default(int))
                ctx.AddInParameter<int>(cmd,"@ProductID",obj.ProductID);
            ctx.AddInParameter<string>(cmd, "@ProductName", obj.ProductName);
            ctx.AddInParameter<decimal>(cmd, "@ProductCost", obj.ProductCost);
            ctx.AddInParameter<string>(cmd, "@Description", obj.Description);
            ctx.AddOutParameter(cmd,"@RetCode",System.Data.DbType.String,25);
            cmd.ExecuteNonQuery();
            retCode = ctx.GetOutputParameterValue(cmd,"@RetCode");
            cmd.Connection.Close();
            ctx.CloseConnection(context);
            return retCode;
        }
        
        public SearchResults<GetSearchProductDetails> SearchProduct(SearchProduct obj)
        {
            var cmd = ctx.PrepareCommand(context);
            var list = new SearchResults<GetSearchProductDetails>();
            ctx.PrepareProcedure(cmd, "customers.uspSearchProducts");
            if(!string.IsNullOrEmpty(obj.Product))
            ctx.AddInParameter<string>(cmd, "@Product",obj.Product);
            if(obj.StatusID > default(int))
            ctx.AddInParameter<int>(cmd, "@StatusID", obj.StatusID);
            ctx.AddInParameter<int>(cmd, "@PageIndex", obj.PageIndex);
            ctx.AddInParameter<int>(cmd, "@PageSize", obj.PageSize);

            using (var read = cmd.ExecuteReader())
            {
                var row = ((IObjectContextAdapter)context).ObjectContext.Translate<int>(read);
                foreach (var r in row)
                    list.TotalNumberOfRows = r;
                read.NextResult();

                List<GetSearchProductDetails> lst = new List<GetSearchProductDetails>();   
                var Products = ((IObjectContextAdapter)context).ObjectContext.Translate<GetSearchProductDetails>(read);
                foreach (var prod in Products)
                    lst.Add(prod);

                list.SearchList = lst;
                cmd.Connection.Close();
            }
                
            ctx.CloseConnection(context);
            return list;
        }
        
        public ViewProductDetails ViewProduct(int productID)
        {
            var cmd = ctx.PrepareCommand(context);
            ViewProductDetails obj = new ViewProductDetails();
            ctx.PrepareProcedure(cmd, "customers.uspViewProducts");
            ctx.AddInParameter<int>(cmd,"@ProductID",productID);

            using (var read = cmd.ExecuteReader())
            {
                var details = ((IObjectContextAdapter)context).ObjectContext.Translate<ViewProductDetails>(read);
                foreach (var view in details)
                    obj = view;
                cmd.Connection.Close();
            }

            ctx.CloseConnection(context);
            return obj;
        } 
    }
}
