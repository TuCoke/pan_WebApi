using Pan.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pan.Infrastructure.Base
{
    public interface IEntityStatus
    {
        /// <summary>
        /// 状态：-1-删除 0-禁用 1-正常
        /// </summary>
        public EntityStatusEnums Status { get; set; }
    }
}
