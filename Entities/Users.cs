using System;
using System.ComponentModel.DataAnnotations;

public class Users
{
    [Key]
    public Guid User_id { get; set; } = Guid.NewGuid(); 
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
