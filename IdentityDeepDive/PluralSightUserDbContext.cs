using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDeepDive
{
    public class PluralSightUserDbContext : IdentityDbContext<PluralSightUser>
    {
        public PluralSightUserDbContext(DbContextOptions<PluralSightUserDbContext> options): base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<PluralSightUser>(user => user.HasIndex(c => c.Locale).IsUnique(false));

            builder.Entity<Organization>(org =>
            {
                org.ToTable("organization");
                org.HasKey(x => x.Id);
                org.HasMany<PluralSightUser>().WithOne().HasForeignKey(x => x.OrgId).IsRequired(false);
            });
        }
    }
}
 