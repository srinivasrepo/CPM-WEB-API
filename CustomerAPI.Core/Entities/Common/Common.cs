using CustomerAPI.Core.CommonMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI.Core.Common
{
    public class CommonStatus
    {

        public short StatusID { get; set; }

        public string Status { get; set; }

    }

    public class CommonStatusList : List<CommonStatus> { }


    public class SearchResults<T>
    {
        public int TotalNumberOfRows { get; set; }

        public IEnumerable<T> SearchList { get; set; }

    }

    public class Paging
    {
        public int PageIndex { get; set; }

        public int PageSize { get { return 10; } }
    }

    public class ChangeStatus
    {
        public string Code { get; set; }

        public string EncID { get; set; }

        public int ID { get { return CommonStaticMethods.Decrypt<int>(EncID); } }
    }


}
