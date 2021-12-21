using Pan.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pan.Infrastructure.Entity
{
    public class post : BaseEntity
    {
        public string Title { get; set; }
        public string HtmlContext { get; set; }
        public int Tags { get; set; }
        public string Request_id { get; set; }
        public string aliyun_url { get; set; }
        public string del_url { get; set; }
        public string next_url { get; set; }
        public string prev { get; set; }
        public string createTime { get; set; }
    }

}
