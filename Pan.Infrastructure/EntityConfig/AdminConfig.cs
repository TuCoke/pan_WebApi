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
    public class AdminConfig : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("Admin");
            builder.Property(x => x.Name).HasMaxLength(256);
            builder.Property(x => x.Phone).HasMaxLength(22);
        }
    }
}
