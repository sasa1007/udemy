using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace udemy.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    public string Name { get; set; }
    public string? StreetAddress { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }

    public int? CompanyId { get; set; }
    [ValidateNever]
    [ForeignKey("CompanyId")]
    public Company Company { get; set; }
}