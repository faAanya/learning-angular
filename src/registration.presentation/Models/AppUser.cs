using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

class AppUser : IdentityUser
{
    [PersonalData]
    [StringLength(255)]
    public string Fullname { get; set; }
    
    [PersonalData]
    [Column(TypeName = "VARCHAR(10)")]
    public string Gender { get; set; }
    
    [PersonalData]
    public DateOnly DateOfBirth { get; set; }
    
    [PersonalData]
    public int? LibraryId { get; set; }
}