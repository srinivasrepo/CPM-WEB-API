using CustomerAPI.Core.Common;
using CustomerAPI.Core.Interface.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CustomerAPI.Web.Controllers.Common
{
    public class CommonController : ApiController
    {
        ICommon cm;
        
        public CommonController(ICommon cm)
        {
            this.cm = cm;
        }

        [HttpGet]
        [Route("GetStatusDetails")]
        public CommonStatusList GetStatusDetails(string code)
        {
            return cm.GetStatusDetails(code);
        }

        [HttpPost]
        [Route("ChangeStatus")]
        public string ChangeStatus(ChangeStatus obj)
        {
            return cm.ChangeStatus(obj);
        }
    }
}
