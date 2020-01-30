using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAPI.Core.Entities.Login;

namespace CustomerAPI.Core.Interface.ILogin
{
    public interface ILogin
    {
        ReturnLogin GetLoginID(ValidLogin obj );
    }
}
