﻿namespace TechStore.DAL.Repositories.Models.Employees;

public record RequestedEmployee
{
    public int Id { get; init; }
    public string Login { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
    public DateTime Birthday { get; init; }
    public DateTime UpdateOn { get; init; }
    public bool IsActive { get; set; }
}