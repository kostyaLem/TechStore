﻿using DevExpress.Mvvm;

namespace TechStore.BL.Models.Categories;

public sealed class Category : BindableBase
{
    public int Id { get; init; }
    public string Name { get; init; }
}
