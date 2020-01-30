using CustomerAPI.Core.Common;
using CustomerAPI.Core.Interface.Common;
using CustomerAPI.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI.Infrastructure.Repository.Common
{
    public class CommonRepository : ICommon
    {
        TrainingContext context = new TrainingContext();
        DBHelper ctx = new DBHelper();

        public CommonStatusList GetStatusDetails(string code)
        {
            var list = new CommonStatusList();

            var cmd = ctx.PrepareCommand(context);
            ctx.PrepareProcedure(cmd, "customers.uspGetStatus");
            ctx.AddInParameter<string>(cmd,"@Code",code);

            using (var read = cmd.ExecuteReader())
            {
                var row = ((IObjectContextAdapter)context).ObjectContext.Translate<CommonStatus>(read);
                foreach (var r in row)
                    list.Add(r);
                cmd.Connection.Close();
            }   
            ctx.CloseConnection(context);
            return list;
        }

        public string ChangeStatus(ChangeStatus obj)
        {
            var retCode = string.Empty;

            var cmd = ctx.PrepareCommand(context);
            ctx.PrepareProcedure(cmd, "customers.uspChangeStatus");
            ctx.AddInParameter<string>(cmd,"@Code",obj.Code);
            ctx.AddInParameter<int>(cmd, "@ID", obj.ID);
            ctx.AddOutParameter(cmd,"@RetCode",System.Data.DbType.String,25);
            cmd.ExecuteNonQuery();
            retCode = ctx.GetOutputParameterValue(cmd, "@RetCode");
            cmd.Connection.Close();
            ctx.CloseConnection(context);
            return retCode;
        }
    }
}
