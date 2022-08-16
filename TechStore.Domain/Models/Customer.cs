﻿namespace TechStore.Domain.Models;

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly Birthday { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
