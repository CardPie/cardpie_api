﻿using AppCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainData.Entities;

public class Folder : BaseEntity
{
    public string FolderName { get; set; } = string.Empty;
    public bool? IsPublic { get; set; }
    
    //Relationship
    //public IEnumerable<Deck> Decks { get; set; } = new List<Deck>();
}

public class FolderConfig : IEntityTypeConfiguration<Folder>
{
    public void Configure(EntityTypeBuilder<Folder> builder)
    {
        builder.Property(a => a.FolderName).IsRequired();
        builder.Property(a => a.IsPublic).IsRequired(false);
    }
}