using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pan.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pan.Infrastructure.EntityConfig
{
    public class postConfig : IEntityTypeConfiguration<post>
    {
        public void Configure(EntityTypeBuilder<post> builder)
        {
            builder.ToTable("post");
            builder.Property(x => x.del_url).HasMaxLength(256);
        }
    }
}
