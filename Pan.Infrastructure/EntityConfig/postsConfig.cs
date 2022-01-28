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
    public class postsConfig : IEntityTypeConfiguration<posts>
    {
        public void Configure(EntityTypeBuilder<posts> builder)
        {
            builder.ToTable("posts");
            builder.Property(x => x.del_url).HasMaxLength(256);
        }
    }
}
