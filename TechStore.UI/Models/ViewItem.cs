using System;

namespace TechStore.UI.Models;

public sealed class ViewItem
{
    public string Title { get; init; }
    public string Description { get; init; }
    public string ImageName { get; init; }
    public Type ViewType { get; init; }
    public bool IsProtected { get; init; }
}
