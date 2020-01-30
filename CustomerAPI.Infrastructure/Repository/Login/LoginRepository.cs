using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAPI.Infrastructure.Context;
using CustomerAPI.Core.Entities.Login;
using CustomerAPI.Core.Interface.ILogin;

namespace CustomerAPI.Infrastructure.Repository.Login
{
    public class LoginRepository : ILogin
    {
        TrainingContext context = new TrainingContext();
        DBHelper ctx = new DBHelper();

        public ReturnLogin GetLoginID(ValidLogin obj)
        {
            ReturnLogin rt = new ReturnLogin();
            var cmd = ctx.PrepareCommand(context);
            ctx.PrepareProcedure(cmd, "customers.uspCheckLogin");
            ctx.AddInParameter<string>(cmd, "@UserName", obj.UserName);
            ctx.AddInParameter<string>(cmd, "@Password", obj.Password);
            ctx.AddOutParameter(cmd, "@UserID", System.Data.DbType.Int32,10);
            ctx.AddOutParameter(cmd, "@RetCode", System.Data.DbType.String,25);
            cmd.ExecuteNonQuery();
            rt.UserID = !string.IsNullOrEmpty(ctx.GetOutputParameterValue(cmd,"@UserID")) ? Convert.ToInt16(ctx.GetOutputParameterValue(cmd,"@UserID")): default(int);
            rt.RetCode = ctx.GetOutputParameterValue(cmd,"@RetCode");
            cmd.Connection.Close();
            ctx.CloseConnection(context);
            return rt;
        }
    }
}
