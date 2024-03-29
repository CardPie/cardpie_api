﻿using AppCore.Models;
using MainData.Entities;

namespace API.Dtos;

public class SavedDeckDto : BaseDto
{
    public Guid UserId { get; set; }
    public Guid DeckId { get; set; }
    public DeckDto Deck { get; set; } = new DeckDto();
    public string Note { get; set; } = string.Empty;
}

public class DetailSavedDeckDto : BaseDto
{
    public Guid UserId { get; set; }
    public Guid DeckId { get; set; }
    public DeckDto Deck { get; set; } = new DeckDto();
    public string Note { get; set; } = string.Empty;
}

public class CreateSavedDeckDto
{
    public Guid DeckId { get; set; }
    public string? Note { get; set; }
}

public class UpdateSavedDeckDto
{
    public string? Note { get; set; } = string.Empty;
}

public class SavedDeckQueryDto : BaseQueryDto{}