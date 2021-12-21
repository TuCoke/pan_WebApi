using AutoMapper;
using Pan.Domain.Handler.Accounts;
using Pan.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pan.Domain.Mapper
{
    public class UtilMapper : Profile
    {
        public UtilMapper()
        {
          CreateMap<CreateAdminRequest, Admin>();
        }
    }
}
