using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI.Core.Entities.Login
{
    public class ValidLogin
    {

        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class ReturnLogin
    {

        public int UserID { get; set; }

        public string RetCode { get; set; }
    }
}
