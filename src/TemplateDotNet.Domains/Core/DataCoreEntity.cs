﻿namespace TemplateDotNet.Domains;

public class DataCoreEntity
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}