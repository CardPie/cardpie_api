using AppCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainData.Entities;

public class Folder : BaseEntity
{
    public string FolderName { get; set; } = string.Empty;
    
    //Relationship
    public IEnumerable<Deck> Decks { get; set; } = new List<Deck>();
}

public class FolderConfig : IEntityTypeConfiguration<Folder>
{
    public void Configure(EntityTypeBuilder<Folder> builder)
    {
        builder.Property(a => a.FolderName).IsRequired();
        builder.HasMany(a => a.Decks).WithOne(a => a.Folder)
            .HasForeignKey(x => x.FolderId);
    }
}