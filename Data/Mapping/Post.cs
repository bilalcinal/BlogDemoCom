using Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Data.Mapping
{
    public class PostMapping : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
        builder.ToTable(nameof(Post), "dbo");
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.CreateTime).HasDefaultValue(DateTime.Now);
        builder.Property(x => x.Status).HasDefaultValue(true);

        builder.HasOne(m => m.User)
           .WithMany(f => f.Post)
           //.HasForeignKey(x => x.AuthorId)
           .OnDelete(DeleteBehavior.Restrict);
        }
    }
}