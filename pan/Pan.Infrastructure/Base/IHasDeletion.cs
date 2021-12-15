using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pan.Infrastructure.Base
{
    public interface IHasDeletionTime
    {
        public DateTime DeletionTime { get; set; }
    }
    public interface IHasDelet : IHasDeletionTime
    {
        public DateTime Deletion { get; set; }
    }
}
