﻿namespace TechStore.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public UserType Type { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    public DateTime LastActivity { get; set; }
    public bool IsActive { get; set; }
}
