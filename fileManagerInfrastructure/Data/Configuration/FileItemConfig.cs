using fileManagerCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace fileManagerInfrastructure.Data.Configuration
{
    class FileItemConfig : IEntityTypeConfiguration<FileItem>
    {
        public void Configure(EntityTypeBuilder<FileItem> builder)
        {
            builder.ToTable("FileItem");

            builder.HasKey(k => k.Id);
        }
    }
}
