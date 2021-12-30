using Pan.Infrastructure.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Pan.Infrastructure.Entity
{
    public partial class Admin : BaseEntity
    {
        public string Name { get; set; }
        public string Account { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
        public DateTime LastLoginTime { get; set; }
        public int? Gender { get; set; }
    }
}
