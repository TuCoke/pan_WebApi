using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pan.Infrastructure.Base
{
    public interface ISoftDelete
    {
        public int? IsofDelete { get; set; }
    }
    public interface ISoftDeleteinfo : ISoftDelete
    {
        public int IsDeleted { get; set; }
    }
}
