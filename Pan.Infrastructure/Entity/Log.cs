using Pan.Infrastructure.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Pan.Infrastructure.Entity
{
    public partial class Log : BaseEntity
    {
        public string request_id { get; set; }
        public string request_http { get; set; }
        public string request_url { get; set; }
        public string request_date { get; set; }
    }
}
