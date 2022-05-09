using Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Data.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
        builder.ToTable(nameof(User), "dbo");
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.CreateTime).HasDefaultValue(DateTime.Now);
        builder.Property(x => x.Status).HasDefaultValue(true);

        builder.HasMany(m => m.Post)
           .WithOne(f => f.User)
           //.HasForeignKey(x => x.AuthorId)
           .OnDelete(DeleteBehavior.Restrict);
        
        }
    }
}