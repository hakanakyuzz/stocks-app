using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Models;

[Table("User")]
public class User : IdentityUser
{
    public List<Portfolio> Portfolios { get; set; } = [];
}