using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

class AppUser : IdentityUser
{
    [PersonalData]
    [StringLength(255)]
    public string Fullname { get; set; }
}