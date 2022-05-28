
using System;
using Microsoft.AspNetCore.Identity;
namespace store.Models
{
    public class Employee : IdentityUser
    {
    public DateTime EmploymentDate { get; set; }
    public string Position { get; set; }
    }
}
