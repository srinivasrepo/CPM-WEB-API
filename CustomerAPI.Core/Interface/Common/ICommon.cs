using CustomerAPI.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI.Core.Interface.Common
{
    public interface ICommon
    {

        CommonStatusList GetStatusDetails(string code);

        string ChangeStatus(ChangeStatus obj);
    }
}
