using CustomerAPI.Core.Entities.Login;
using CustomerAPI.Core.Interface.ILogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CustomerAPI.Web.Controllers.LoginContoller
{
    public class LoginController : ApiController
    {
            ILogin db;

            public LoginController(ILogin db)
            {
                this.db = db;
            }
            
            [HttpPost]
            [Route("ValidLogin")]
            public ReturnLogin GetLoginID(ValidLogin obj)
            {
                return db.GetLoginID(obj);
            }

    }
}
